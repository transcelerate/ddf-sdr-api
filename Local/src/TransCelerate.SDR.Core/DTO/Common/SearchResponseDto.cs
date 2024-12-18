using Newtonsoft.Json;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Common
{
    public class SearchResponseDto
    {
        public SearchStudy Study { get; set; }
        public AuditTrailDto AuditTrail { get; set; }
        public LinksForUIDto Links { get; set; }
    }

    public class SearchStudy
    {        
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
        public string IndicationDescription { get; set; }
    }
}
