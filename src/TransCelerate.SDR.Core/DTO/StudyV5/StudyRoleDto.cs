using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyRoleDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<string> OrganizationIds { get; set; }
        public CodeDto Code { get; set; }
        public List<string> AppliesToIds { get; set; }
        public MaskingDto Masking { get; set; }
        public List<AssignedPersonDto> AssignedPersons { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
