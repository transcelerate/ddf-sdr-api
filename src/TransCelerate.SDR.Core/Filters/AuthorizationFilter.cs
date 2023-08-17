using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                // Auth logic
                if (context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var apiKey))
                {
                    // check for valid api-key
                    ClaimsIdentity claims = new();
                    claims.AddClaim(new Claim("string", "app.user"));
                    context.HttpContext.User.AddIdentity(claims);                    
                    return;
                }

                else
                {
                    if (!context.HttpContext.User.Identity.IsAuthenticated)
                    {
                        context.Result = new UnauthorizedResult();
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        string response = string.Empty;
                        response = await HttpContextResponseHelper.Response(context.HttpContext, response);                        
                    }
                    return;
                }                
            }
        }
    }
}
