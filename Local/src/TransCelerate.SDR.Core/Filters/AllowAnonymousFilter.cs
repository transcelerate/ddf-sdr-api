using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Filters
{
    /// <summary>
    /// This authorisation handler will bypass all requirements
    /// </summary>
    public class AllowAnonymousFilter : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
                context.Succeed(requirement); //Simply pass all requirements

            return Task.CompletedTask;
        }
    }
}
