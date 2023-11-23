using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptCategoryDto : IId
    {        
        public string Id { get; set; }
        public List<string> ChildrenIds  { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<string> MemberIds { get; set; }
        public AliasCodeDto Code { get; set; }
    }
}
