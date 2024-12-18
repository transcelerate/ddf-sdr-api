using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyAmendmentReasonDto : IId
    {
        public string Id { get; set; }
        public CodeDto Code { get; set; }
        public string OtherReason { get; set; }
        public string InstanceType { get; set; }
    }
}
