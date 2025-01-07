using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Common
{
    /// <summary>
    /// This class is a DTO for response of Search StudyTitle Endpoint
    /// </summary>
    public class SearchTitleResponseDto
    {
        public SearchTitleStudy Study { get; set; }
        public SearchTitleAuditTrail AuditTrail { get; set; }
        public LinksForUIDto Links { get; set; }
    }

    public class SearchTitleStudy
    {        
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public List<CommonStudyIdentifiersDto> StudyIdentifiers { get; set; }
    }

    public class SearchTitleAuditTrail
    {
        public DateTime EntryDateTime { get; set; }
        [JsonProperty(nameof(SDRUploadVersion))]
        public int SDRUploadVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
