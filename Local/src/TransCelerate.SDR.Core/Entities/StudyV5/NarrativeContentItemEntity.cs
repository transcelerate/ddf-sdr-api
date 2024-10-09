using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    internal class NarrativeContentItemEntity :IId
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string InstanceType { get; set; }
    }
}
