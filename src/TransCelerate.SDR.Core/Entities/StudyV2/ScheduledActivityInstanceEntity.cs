using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ScheduledActivityInstanceEntity : ScheduledInstanceEntity
    {
        public override string ScheduleInstanceType { get; set; } = TransCelerate.SDR.Core.Utilities.ScheduledInstanceType.ACTIVITY.ToString();
        public List<string> ActivityIds { get; set; }
    }
}
