using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.DataAccess.Filters
{
    public static class DataFilterCommon
    {
        /// <summary>
        /// Get filters for GET StudyDefinitons API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <returns></returns>
        public static FilterDefinition<GetRawJsonEntity> GetFiltersForGetStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<GetRawJsonEntity> builder = Builders<GetRawJsonEntity>.Filter;
            FilterDefinition<GetRawJsonEntity> filter = builder.Empty;
            filter &= builder.Eq(Constants.DbFilter.StudyId, studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }
    }
}
