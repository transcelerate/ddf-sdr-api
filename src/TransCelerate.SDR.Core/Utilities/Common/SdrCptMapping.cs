using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class SdrCptMapping
    {
        [JsonProperty("SdrCptMasterDataMapping")]
        public static List<MasterDataMapping> SdrCptMasterDataMapping { get; set; }
    }

    public class SdrCptMapping_NonStatic
    {
        [JsonProperty("SdrCptMasterDataMapping")]
        public  List<MasterDataMapping> SdrCptMasterDataMapping { get; set; }
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
