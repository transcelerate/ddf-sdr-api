using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class EstimandDto : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string PopulationSummary { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<StudyInterventionDto> Interventions { get; set; }
        public AnalysisPopulationDto AnalysisPopulation { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public string VariableOfInterestId { get; set; }
        public List<IntercurrentEventDto> IntercurrentEvents { get; set; }
    }
}
