using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class BiomedicalConceptDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public List<string> Synonyms { get; set; }
        public string Reference { get; set; }
        public List<BiomedicalConceptPropertyDto> Properties { get; set; }
        public AliasCodeDto Code { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
