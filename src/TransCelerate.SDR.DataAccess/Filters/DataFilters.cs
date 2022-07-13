using MongoDB.Driver;
using System.Linq;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.DataAccess.Filters
{
    public static class DataFilters
    {
        public static FilterDefinition<StudyEntity> GetFiltersForGetStudy(string studyId, int version)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.ClinicalStudy.Uuid == studyId);

            if (version != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == version);           

            return filter;
        }
    }
}
