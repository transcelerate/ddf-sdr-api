using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyAmendmentReasonEntity : IId
    {
        public string Id { get; set; }
        public CodeEntity Code { get; set; }
        public string OtherReason { get; set; }
    }
}
