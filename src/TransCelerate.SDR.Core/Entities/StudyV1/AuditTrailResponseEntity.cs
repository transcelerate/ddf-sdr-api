using System;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class AuditTrailResponseEntity
    {
        public CodeEntity StudyType { get; set; }
        public int SDRUploadVersion { get; set; }
        public DateTime EntryDateTime { get; set; }
        public string UsdmVersion { get; set; }
    }
}
