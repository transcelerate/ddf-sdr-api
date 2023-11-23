using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class ActivityDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<ProcedureDto> DefinedProcedures { get; set; }
        public string NextId { get; set; }
        public string PreviousId { get; set; }
        public object IsConditional { get; set; }
        public string IsConditionalReason { get; set; }
        public List<string> BcCategoryIds { get; set; }
        public List<string> BcSurrogateIds { get; set; }
        public List<string> BiomedicalConceptIds { get; set; }
        public string TimelineId { get; set; }
    }
}
