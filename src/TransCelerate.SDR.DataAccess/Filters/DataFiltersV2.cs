using MongoDB.Driver;
using System;
using System.Linq;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.DataAccess.Filters
{
    /// <summary>
    /// DataFilters for getting data from data base
    /// </summary>
    public static class DataFiltersV2
    {
        /// <summary>
        /// Get filters for GET StudyDefinitons API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForGetStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V1_9);
            filter &= builder.Where(s => s.Study.StudyId == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }

        /// <summary>
        /// Get filters for AuditTrail 
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="sdruploadversion"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForGetAuditTrailOfAStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.Study.StudyId == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }
        public static FilterDefinition<Core.Entities.Common.ChangeAuditStudyEntity> GetFiltersForChangeAudit(string studyId)
        {
            FilterDefinitionBuilder<Core.Entities.Common.ChangeAuditStudyEntity> builder = Builders<Core.Entities.Common.ChangeAuditStudyEntity>.Filter;
            FilterDefinition<Core.Entities.Common.ChangeAuditStudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.ChangeAudit.StudyId == studyId);

            return filter;
        }
        /// <summary>
        /// Get filters for StudyHistory API
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyTitle"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(studyTitle))
                filter &= builder.Where(x => x.Study.StudyTitle.ToLower().Contains(studyTitle.ToLower()));


            return filter;
        }

        /// <summary>
        /// Get filters for AuditTrail API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForGetAudTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V1_9);
            filter &= builder.Where(s => s.Study.StudyId == studyId);

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);


            return filter;
        }

        /// <summary>
        /// Get projectio definition for partial study elements
        /// </summary>
        /// <param name="listofelementsArray">list of study elements</param>
        /// <returns></returns>
        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForPartialStudyElements(string[] listofelementsArray)
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.StudyId);
            projector = projector.Include(x => x.Study.StudyType);
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x.Id);

            listofelementsArray.ToList().ForEach(elements =>
            {
                if (elements is not null)
                {
                    if (elements.ToLower().Equals(nameof(StudyEntity.StudyTitle).ToLower()))
                        projector = projector.Include(x => x.Study.StudyTitle);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyPhase).ToLower()))
                        projector = projector.Include(x => x.Study.StudyPhase);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyVersion).ToLower()))
                        projector = projector.Include(x => x.Study.StudyVersion);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyProtocolVersions).ToLower()))
                        projector = projector.Include(x => x.Study.StudyProtocolVersions);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyIdentifiers).ToLower()))
                        projector = projector.Include(x => x.Study.StudyIdentifiers);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyDesigns).ToLower()))
                        projector = projector.Include(x => x.Study.StudyDesigns);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.BusinessTherapeuticAreas).ToLower()))
                        projector = projector.Include(x => x.Study.BusinessTherapeuticAreas);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyRationale).ToLower()))
                        projector = projector.Include(x => x.Study.StudyRationale);
                    else if (elements.ToLower().Equals(nameof(StudyEntity.StudyAcronym).ToLower()))
                        projector = projector.Include(x => x.Study.StudyAcronym);
                }
            });
            return projector;
        }

        /// <summary>
        /// Get Study Design Projection Definition
        /// </summary>
        /// <returns></returns>
        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForPartialStudyDesignElementsFullStudy()
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.StudyId);
            projector = projector.Include(x => x.Study.StudyType);
            projector = projector.Include(x => x.Study.StudyDesigns);
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x.Id);

            return projector;
        }

        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForCheckAccessForAStudy()
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.StudyId);
            projector = projector.Include(x => x.Study.StudyType);
            projector = projector.Exclude(x => x.Id);

            return projector;
        }
    }
}
