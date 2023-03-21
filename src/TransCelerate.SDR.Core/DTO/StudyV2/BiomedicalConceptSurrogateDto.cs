namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class BiomedicalConceptSurrogateDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.BcSurrogateId)]
        public string Id { get; set; }
        public string BcSurrogateName { get; set; }
        public string BcSurrogateDescription { get; set; }
        public string BcSurrogateReference { get; set; }
    }
}
