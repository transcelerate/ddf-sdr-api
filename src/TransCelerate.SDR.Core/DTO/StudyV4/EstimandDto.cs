using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class EstimandDto : IId
    {        
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationDto AnalysisPopulation { get; set; }
        public string VariableOfInterestId { get; set; }
        public List<InterCurrentEventDto> IntercurrentEvents { get; set; }
    }
}
