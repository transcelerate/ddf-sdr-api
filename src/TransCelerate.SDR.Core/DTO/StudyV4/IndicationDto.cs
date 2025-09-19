using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class IndicationDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeDto> Codes { get; set; }
        public string InstanceType { get; set; }
        public bool isRareDisease { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }

    }
}
