using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyInterventionDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeDto> Codes { get; set; }
        public CodeDto Role { get; set; }
        public CodeDto Type { get; set; }
        public QuantityDto MinimumResponseDuration { get; set; }
        public string InstanceType { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
