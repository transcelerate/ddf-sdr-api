namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptSurrogateDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
    }
}
