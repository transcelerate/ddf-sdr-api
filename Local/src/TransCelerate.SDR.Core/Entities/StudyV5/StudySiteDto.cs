using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudySiteEntity : IId
    {
        public string Id { get; set; }
        public string Name {  get; set; }
        public string Label {  get; set; }
        public string Description {  get; set; }
        public SubjectEnrollmentEntity CurrentEnrollment { get; set; }
        public string InstanceType { get; set; }
    }
}
