using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EstimandDto : IId
    {        
        public string Id { get; set; }
        public string InterventionId { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationDto AnalysisPopulation { get; set; }
        public string VariableOfInterestId { get; set; }
        public List<IntercurrentEventDto> IntercurrentEvents { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
