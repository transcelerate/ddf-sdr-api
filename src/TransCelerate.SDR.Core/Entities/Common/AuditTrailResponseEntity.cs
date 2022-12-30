
using System;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class AuditTrailResponseEntity
    {
        public object StudyType { get; set; }
        public string UsdmVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public DateTime EntryDateTime { get; set; }
        public bool HasAccess { get; set; }
    }
}
