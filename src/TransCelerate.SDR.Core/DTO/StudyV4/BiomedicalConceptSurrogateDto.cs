namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptSurrogateDto : IId
    {        
        public string Id { get; set; }
        public string BcSurrogateName { get; set; }
        public string BcSurrogateDescription { get; set; }
        public string BcSurrogateReference { get; set; }
    }
}
