namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class DocumentContentReferenceEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string SectionNumber { get; set; }
        public string SectionTitle { get; set; }
        public string AppliesToId { get; set; }
    }
}
