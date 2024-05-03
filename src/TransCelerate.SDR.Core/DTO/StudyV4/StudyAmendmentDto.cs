using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyAmendmentDto : IId
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Summary { get; set; }
        public bool SubstantialImpact { get; set; }
        public string PreviousId { get; set; }
        public List<SubjectEnrollmentDto> Enrollments { get; set; }
        public StudyAmendmentReasonDto PrimaryReason { get; set; }
        public List<StudyAmendmentReasonDto> SecondaryReasons { get; set; }
        public string InstanceType { get; set; }
    }
}
