
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class AuditTrailResponseEntity
    {
        public string StudyId { get; set; }
        public object StudyType { get; set; }
        public string UsdmVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public DateTime EntryDateTime { get; set; }
        public bool HasAccess { get; set; }
        public IEnumerable<string> StudyDesignIds { get; set; }
        public IEnumerable<IEnumerable<string>> StudyDesignIdsMVP { get; set; }
    }
}
