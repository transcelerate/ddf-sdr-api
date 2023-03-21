using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class PointInTimeEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string PointInTimeId { get; set; }
        public string Type { get; set; }
        public string SubjectStatusGrouping { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EndDate { get; set; }
    }
}
