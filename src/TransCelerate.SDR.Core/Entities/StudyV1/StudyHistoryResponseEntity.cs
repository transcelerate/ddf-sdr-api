using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyHistoryResponseEntity
    {
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }
        public DateTime EntryDateTime { get; set; }
        public CodeEntity StudyType { get; set; }
        public IEnumerable<string> ProtocolVersions { get; set; }
        public string UsdmVersion { get; set; }
    }
}
