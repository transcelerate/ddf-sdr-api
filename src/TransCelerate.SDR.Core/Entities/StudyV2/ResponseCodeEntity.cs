using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class ResponseCodeEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ResponseCodeId)]
        public string Id { get; set; }
        public bool ResponseCodeEnabled { get; set; }
        public CodeEntity Code { get; set; }
    }
}
