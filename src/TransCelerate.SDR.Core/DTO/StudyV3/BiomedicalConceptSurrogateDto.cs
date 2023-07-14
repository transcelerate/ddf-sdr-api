namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class BiomedicalConceptSurrogateDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.BcSurrogateId)]
        public string Id { get; set; }
        public string BcSurrogateName { get; set; }
        public string BcSurrogateDescription { get; set; }
        public string BcSurrogateReference { get; set; }
    }
}
