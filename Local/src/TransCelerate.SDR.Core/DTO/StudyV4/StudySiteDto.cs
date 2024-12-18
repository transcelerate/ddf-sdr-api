using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudySiteDto : IId
    {
        public string Id { get; set; }
        public string Name {  get; set; }
        public string Label {  get; set; }
        public string Description {  get; set; }
        public SubjectEnrollmentDto CurrentEnrollment { get; set; }
        public string InstanceType { get; set; }
    }
}
