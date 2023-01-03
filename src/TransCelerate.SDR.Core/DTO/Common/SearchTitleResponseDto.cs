using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    /// <summary>
    /// This class is a DTO for response of Search StudyTitle Endpoint
    /// </summary>
    public class SearchTitleResponseDto
    {
        public SearchTitleClinicalStudy ClinicalStudy { get; set; }
        public SearchTitleAuditTrail AuditTrail { get; set; }
    }

    public class SearchTitleClinicalStudy
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public object StudyIdentifiers { get; set; }
    }

    public class SearchTitleAuditTrail
    {
        public DateTime EntryDateTime { get; set; }
        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }
        [JsonProperty("usdm-version")]
        public string UsdmVersion { get; set; }
    }
}
