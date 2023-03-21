namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyElementDTO
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public RuleDTO StartRule { get; set; }

        public RuleDTO EndRule { get; set; }
    }
}
