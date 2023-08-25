using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Azure;
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
    public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public string Roles { get; set; }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
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
                    if (!apiKey.Any(x => x == string.Empty) && Roles == Constants.Roles.Org_Admin)
                    {
                        context.Result = new ForbidResult();
                        //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        string response = string.Empty;
                        await HttpContextResponseHelper.Response(context.HttpContext, response);                        
                    }
                    else
                    {
                        if (!context.HttpContext.User.Identity.IsAuthenticated)
                        {
                            context.Result = new UnauthorizedResult();
                            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            string response = string.Empty;
                            await HttpContextResponseHelper.Response(context.HttpContext, response);
                        }
                        if (context.HttpContext.User.Identity.IsAuthenticated && Roles == Constants.Roles.Org_Admin && context.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value == Constants.Roles.App_User)
                        {
                            context.Result = new ForbidResult();
                            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            string response = string.Empty;
                            await HttpContextResponseHelper.Response(context.HttpContext, response);
                        }
                    }                    
                }                
            }
        }
    }
}
