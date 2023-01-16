using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class StudyHistoryResponseEntity : ICheckAccess
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public int SDRUploadVersion { get; set; }
        public List<object> StudyIdentifiers { get; set; }
        public DateTime EntryDateTime { get; set; }
        public object StudyType { get; set; }
        public IEnumerable<string> ProtocolVersions { get; set; }
        [BsonElement("usdm-version")]
        public string UsdmVersion { get; set; }
        public IEnumerable<IEnumerable<string>> StudyDesignIdsMVP { get; set; }
        public IEnumerable<string> StudyDesignIds { get; set; }
        public bool HasAccess { get; set; }
    }
}
