using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class UUIDConformanceValidationHelper
    {
        public static bool CheckForUUIDConformance(string uuid,string method,string path)
        {
            if(method == HttpMethod.Put.Method && path == Route.PostElementsV2)
            {
                if (string.IsNullOrWhiteSpace(uuid))
                    return false;
            }
            return true;
        }

        public static string GetMessageForUUIDConformance(string uuid)
        {
            string errorMessage = uuid == null ? Constants.ValidationErrorMessage.PropertyMissingError : Constants.ValidationErrorMessage.PropertyEmptyError;           
            return errorMessage;
        }
    }
}
