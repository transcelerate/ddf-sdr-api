using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class StudyHistoryResponseEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public List<object> StudyIdentifiers { get; set; }
        public DateTime EntryDateTime { get; set; }
        public object StudyType { get; set; }
        public IEnumerable<string> ProtocolVersions { get; set; }
        public string UsdmVersion { get; set; }
        public IEnumerable<IEnumerable<string>> StudyDesignIdsMVP { get; set; }
        public IEnumerable<string> StudyDesignIds { get; set; }        
    }
}
