using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
