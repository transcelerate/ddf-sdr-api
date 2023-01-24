using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.Common;

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class StudyDesignDto
    {
        public string StudyDesignId { get; set; }
        public string StudyDesignName { get; set; }       
        public string StudyDesignLink { get; set; }
        public eCPTDataDto ECPTData { get; set; }
    }
}
