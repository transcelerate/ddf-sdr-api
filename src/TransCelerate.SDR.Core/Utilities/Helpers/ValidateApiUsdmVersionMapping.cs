using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ValidateApiUsdmVersionMapping
    {
        public static bool IsValidMapping(string apiVersion, string usdmVersion)
        {
            return ApiUsdmVersionMapping.SDRVersions.Where(x => x.ApiVersion == apiVersion).Any(x => x.UsdmVersions.Contains(usdmVersion));
        }
    }
}
