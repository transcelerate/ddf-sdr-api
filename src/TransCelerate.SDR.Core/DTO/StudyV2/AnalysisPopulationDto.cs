namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class AnalysisPopulationDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.AnalysisPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
    }
}
