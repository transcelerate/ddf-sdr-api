using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class HeaderValidationHelper
    {
        public static string ValidateUsdmVersionHeaderMvp(HttpContext context, string response)
        {
            if (context.Request.Path.Value.Equals(Route.PostElements))
            {
                if (String.IsNullOrWhiteSpace(context.Request.Headers["usdmVersion"]))
                {
                    response = Constants.ErrorMessages.UsdmVersionMissing;                                       
                }
                else
                {
                    var usdmVersion = context.Request.Headers["usdmVersion"];
                    if (!ApiUsdmVersionMapping.SDRVersions.Where(x => x.ApiVersion == "mvp").Any(x => x.UsdmVersions.Contains(usdmVersion)))
                    {
                        response = Constants.ErrorMessages.UsdmVersionMapError;                       
                    }
                }
            }
            return response;
        }
    }
}
