using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                // Auth logic
                if (context.HttpContext.Request.Headers.TryGetValue(Constants.DefaultHeaders.ApiKeyAuthenticationHeader, out var apiKey) && !apiKey.Any(x => x == string.Empty )
                    && Roles != Constants.Roles.Org_Admin)
                {                    
                    // check for valid api-key
                    context.HttpContext.User = new ClaimsPrincipal();                    
                    ClaimsIdentity claims = new("ApiKeyAuthentication");
                    claims.AddClaim(new Claim(ClaimTypes.Role, Constants.Roles.App_User));
                    claims.AddClaim(new Claim(ClaimTypes.Email, "ApiKeyUser"));                    
                    context.HttpContext.User.AddIdentity(claims);                                                                                                                      
                }

                else
                {
                    var jsonSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    };
                    //If Api Key is available in the request but the api which is requested needs admin access
                    if (apiKey.Any() && !apiKey.Any(x => x == string.Empty) && Roles == Constants.Roles.Org_Admin)
                    {
                        if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["AuthFilter"]))
                            context.HttpContext.Response.Headers.Add("AuthFilter", "True");
                        if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["Content-Type"]))
                            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");

                        context.Result = new UnauthorizedObjectResult(new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                        (context.Result as UnauthorizedObjectResult).StatusCode = (int)HttpStatusCode.Forbidden;                        
                    }
                    //If Api Key is not available in the request
                    else
                    {
                        //If the JWT is not valid or expired
                        if (!context.HttpContext.User.Identity.IsAuthenticated)
                        {
                            if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["AuthFilter"]))
                                context.HttpContext.Response.Headers.Add("AuthFilter", "True");
                            if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["Content-Type"]))
                                context.HttpContext.Response.Headers.Add("Content-Type", "application/json");

                            context.Result = new UnauthorizedObjectResult(new JsonResult(ErrorResponseHelper.UnAuthorizedAccess()).Value);

                        }
                        // If the JWT is valid but the user does not have admin access but requested for admin Apis
                        if (context.HttpContext.User.Identity.IsAuthenticated && Roles == Constants.Roles.Org_Admin && context.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value == Constants.Roles.App_User)
                        {
                            if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["AuthFilter"]))
                                context.HttpContext.Response.Headers.Add("AuthFilter", "True");
                            if (String.IsNullOrWhiteSpace(context.HttpContext.Response.Headers["Content-Type"]))
                                context.HttpContext.Response.Headers.Add("Content-Type", "application/json");

                            context.Result = new UnauthorizedObjectResult(new JsonResult(ErrorResponseHelper.Forbidden()).Value);
                            (context.Result as UnauthorizedObjectResult).StatusCode = (int)HttpStatusCode.Forbidden;                            
                        }
                    }                    
                }                
            }
        }
    }
}
