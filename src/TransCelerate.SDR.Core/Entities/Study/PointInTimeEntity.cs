using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class PointInTimeEntity
    {
        [BsonElement("id")]
        public string pointInTimeId { get; set; }
        public string type { get; set; }
        public string subjectStatusGrouping { get; set; }

        [BsonDateTimeOptions(Kind =DateTimeKind.Utc)]
        public DateTime startDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime endDate { get; set; }
    }
}
