using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class SdrCptMapping
    {
        [JsonProperty(nameof(SdrCptMasterDataMapping))]
        public static List<MasterDataMapping> SdrCptMasterDataMapping { get; set; }
    }

    public class SdrCptMapping_NonStatic
    {
        [JsonProperty(nameof(SdrCptMasterDataMapping))]
        public List<MasterDataMapping> SdrCptMasterDataMapping { get; set; }
    }

    public class MasterDataMapping
    {
        public string Entity { get; set; }
        public List<Mapping> Mapping { get; set; }

    }

    public class Mapping
    {
        public string Code { get; set; }
        public string CDISC { get; set; }
        public string CPT { get; set; }
    }
}
