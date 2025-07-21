using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyAmendmentEntity : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string Summary { get; set; }
        public List<GovernanceDateEntity> DateValues { get; set; }
        public List<GeographicScopeEntity> GeographicScopes { get; set; }
        public List<StudyChangeEntity> Changes { get; set; }
        public List<StudyAmendmentImpactEntity> Impacts { get; set; }
        public List<SubjectEnrollmentEntity> Enrollments { get; set; }
        public string PreviousId { get; set; }
        public StudyAmendmentReasonEntity PrimaryReason { get; set; }
        public List<StudyAmendmentReasonEntity> SecondaryReasons { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
    }
}
