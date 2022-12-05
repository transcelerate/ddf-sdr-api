using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class AuditTrailResponseEntity
    {
        public CodeEntity StudyType { get; set; }
        public string UsdmVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public DateTime EntryDateTime { get; set; }
    }
}
