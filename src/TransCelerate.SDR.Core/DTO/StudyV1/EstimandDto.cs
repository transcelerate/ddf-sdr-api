using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class EstimandDto
    {
        public string Uuid { get; set; }
        public InvestigationalInterventionDto Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationDto AnalysisPopulation { get; set; }
        public EncounterDto VariableOfInterest { get; set; }
        public List<InterCurrentEventDto> InterCurrentEvents { get; set; }
    }
}
