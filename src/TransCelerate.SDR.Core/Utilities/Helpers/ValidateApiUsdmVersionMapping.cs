using System.Linq;
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
