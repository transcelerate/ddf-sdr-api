using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
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
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
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
