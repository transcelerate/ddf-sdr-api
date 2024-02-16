using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    public class StudyAmendmentEntity : IId
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Summary { get; set; }
        public bool SubstantialImpact { get; set; }
        public string PreviousId { get; set; }
        public List<SubjectEnrollmentEntity> Enrollments { get; set; }
        public StudyAmendmentReasonEntity PrimaryReason { get; set; }
        public List<StudyAmendmentReasonEntity> SecondaryReasons { get; set; }
        public string InstanceType { get; set; }
    }
}
