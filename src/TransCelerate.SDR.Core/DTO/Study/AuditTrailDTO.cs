using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class AuditTrailDTO
    {
        public string studyTag { get; set; }
        public string studyStatus { get; set; }
        public string entryDateTime { get; set; }     
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
    }
}
