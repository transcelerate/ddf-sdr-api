using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyAmendmentDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Number { get; set; }
        public string Summary { get; set; }
        public List<GovernanceDateDto> DateValues { get; set; }
        public List<GeographicScopeDto> GeographicScopes { get; set; }
        public List<StudyChangeDto> Changes { get; set; }
        public List<StudyAmendmentImpact> Impacts { get; set; }
        public List<SubjectEnrollmentDto> Enrollments { get; set; }
        public string PreviousId { get; set; }
        public StudyAmendmentReasonDto PrimaryReason { get; set; }
        public List<StudyAmendmentReasonDto> SecondaryReasons { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
    }
}
