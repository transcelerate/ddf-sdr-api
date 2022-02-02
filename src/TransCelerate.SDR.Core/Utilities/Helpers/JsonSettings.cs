using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    ///   This class is for adding Json settings to the objects
    /// </summary>
    public static class JsonSettings
    {
        /// <summary>
        ///   This method is for removing empty arrays and null values in the JSON response
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings JsonSerializerSettings()
        {
            JsonSerializerSettings JsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = ShouldSerializeContractResolver.Instance,
            };
            return JsonSettings;
        }
    }
}
