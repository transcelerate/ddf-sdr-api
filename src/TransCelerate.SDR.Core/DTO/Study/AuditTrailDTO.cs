using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class AuditTrailDTO
    {
        public string tag { get; set; }
        public string status { get; set; }
        public string entryDateTime { get; set; }
        public string entrySystemId { get; set; }
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
    }
}
