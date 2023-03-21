using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ActivityDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ActivityId)]
        public string Id { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<ProcedureDto> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public object ActivityIsConditional { get; set; }
        public string ActivityIsConditionalReason { get; set; }
        public List<string> BcCategoryIds { get; set; }
        public List<string> BcSurrogateIds { get; set; }
        public List<string> BiomedicalConceptIds { get; set; }
        public string ActivityTimelineId { get; set; }
    }
}
