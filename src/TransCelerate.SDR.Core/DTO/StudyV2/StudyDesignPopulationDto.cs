namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDesignPopulationDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDesignPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
    }
}
