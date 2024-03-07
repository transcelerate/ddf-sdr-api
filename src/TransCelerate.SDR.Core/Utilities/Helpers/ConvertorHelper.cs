using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ConvertorHelper
    {
        public static object BsonToObjectConvertor(this BsonDocument bsonDocument)
        {
            return BsonSerializer.Deserialize<object>(bsonDocument);
        }

        public static object V4StudyTitleToObjectConvertor(this object studyTitles, string usdmVersion)
        {
            if (usdmVersion == Constants.USDMVersions.V3)
            {
                var studyTitleV4 = studyTitles is not null ? JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(studyTitles)) : null;
                return studyTitleV4.GetStudyTitle(Constants.StudyTitle.OfficialStudyTitle);
            }
            return studyTitles;
        }
    }
}
