using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class AuditTrailResponseEntity
    {
        public CodeEntity StudyType { get; set; }
        public int SDRUploadVersion { get; set; }
        public DateTime EntryDateTime { get; set; }
    }
}
