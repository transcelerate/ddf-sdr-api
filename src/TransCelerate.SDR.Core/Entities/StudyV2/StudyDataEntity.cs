using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class StudyDataEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDataId)]
        public string Id { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDescription { get; set; }
        public string EcrfLink { get; set; }
    }
}
