using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class EstimandDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.EstimandId)]
        public string Id { get; set; }
        public string Treatment { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationDto AnalysisPopulation { get; set; }
        public string VariableOfInterest { get; set; }
        public List<InterCurrentEventDto> IntercurrentEvents { get; set; }
    }
}
