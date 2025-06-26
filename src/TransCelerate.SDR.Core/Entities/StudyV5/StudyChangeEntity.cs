using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyChangeEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Rationale { get; set; }
        public string Summary { get; set; }
        public List<DocumentContentReferenceEntity> ChangedSections { get; set; }
   
    }
}
