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
        public SearchTitleStudy Study { get; set; }
        public SearchTitleAuditTrail AuditTrail { get; set; }
    }

    public class SearchTitleStudy
    {
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
    }

    public class SearchTitleAuditTrail
    {
        public DateTime EntryDateTime { get; set; }
        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
