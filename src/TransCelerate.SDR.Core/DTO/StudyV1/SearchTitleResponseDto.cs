using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
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
    }
}
