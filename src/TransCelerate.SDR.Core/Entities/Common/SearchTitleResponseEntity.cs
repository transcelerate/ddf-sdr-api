using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class SearchTitleResponseEntity : ICheckAccess
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }

        public object StudyIdentifiers { get; set; }
        public object StudyType { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int SDRUploadVersion { get; set; }
        [BsonElement("usdm-version")]
        public string UsdmVersion { get; set; }

        public bool HasAccess { get; set; }
    }
}
