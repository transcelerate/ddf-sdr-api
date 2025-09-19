using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AbbreviationDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string ExpandedText { get; set; }
        public string AbbreviatedText { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }

    }
}
