using System.Security.Claims;
using TransCelerate.SDR.Core.DTO.Token;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class LoggedInUserHelper
    {
        public static LoggedInUser GetLoggedInUser(ClaimsPrincipal user)
        {
            return new LoggedInUser
            {
                UserName = user?.FindFirst(ClaimTypes.Email)?.Value,
                UserRole = user?.FindFirst(ClaimTypes.Role)?.Value
            };
        }
    }
}
