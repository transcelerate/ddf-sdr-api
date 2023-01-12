using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class SearchResponseDto
    {
        public SearchClinicalStudy ClinicalStudy { get; set; }
        public AuditTrailDto AuditTrail { get; set; }
        public LinksForUIDto Links { get; set; }
    }

    public class SearchClinicalStudy
    {
        [JsonProperty("uuid")]
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public CommonCodeDto StudyType { get; set; }
        public CommonCodeDto StudyPhase { get; set; }
        public List<CommonStudyIdentifiersDto> StudyIdentifiers { get; set; }
        public List<CommonStudyDesign> StudyDesigns { get; set; }
    }

    public class CommonStudyDesign
    {
        public List<CommonCodeDto> InterventionModel { get; set; }
        public List<CommonStudyIndication> StudyIndications { get; set; }
    }
    public class CommonStudyIndication
    {
        [JsonProperty("indicationDesc")]
        public string IndicationDescription { get; set; }
    }
}
