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
        public static FilterDefinition<StudyEntity> GetFiltersForGetStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V2);
            filter &= builder.Where(s => s.ClinicalStudy.StudyId == studyId);

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
        public static FilterDefinition<StudyEntity> GetFiltersForGetAuditTrailOfAStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;            
            filter &= builder.Where(s => s.ClinicalStudy.StudyId == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

            return filter;
        }
        public static FilterDefinition<ChangeAuditStudyEntity> GetFiltersForChangeAudit(string studyId)
        {
            FilterDefinitionBuilder<ChangeAuditStudyEntity> builder = Builders<ChangeAuditStudyEntity>.Filter;
            FilterDefinition<ChangeAuditStudyEntity> filter = builder.Empty;
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
        public static FilterDefinition<StudyEntity> GetFiltersForStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;
            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(studyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(studyTitle.ToLower()));


            return filter;
        }

        /// <summary>
        /// Get filters for AuditTrail API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyEntity> GetFiltersForGetAudTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V2);
            filter &= builder.Where(s => s.ClinicalStudy.StudyId == studyId);

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
        public static ProjectionDefinition<StudyEntity> GetProjectionForPartialStudyElements(string[] listofelementsArray)
        {
            ProjectionDefinitionBuilder<StudyEntity> projection = Builders<StudyEntity>.Projection;
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.StudyId);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x._id);

            listofelementsArray.ToList().ForEach(elements =>
            {
                if (elements is not null)
                {
                    if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyTitle).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyTitle);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyPhase).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyPhase);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyVersion).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyVersion);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyProtocolVersions).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyProtocolVersions);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyIdentifiers).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyIdentifiers);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyDesigns).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyDesigns);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.BusinessTherapeuticAreas).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.BusinessTherapeuticAreas);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyRationale).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyRationale);
                    else if (elements.ToLower().Equals(nameof(ClinicalStudyEntity.StudyAcronym).ToLower()))
                        projector = projector.Include(x => x.ClinicalStudy.StudyAcronym);
                }
            });
            return projector;
        }

        /// <summary>
        /// Get Study Design Projection Definition
        /// </summary>
        /// <returns></returns>
        public static ProjectionDefinition<StudyEntity> GetProjectionForPartialStudyDesignElementsFullStudy()
        {
            ProjectionDefinitionBuilder<StudyEntity> projection = Builders<StudyEntity>.Projection;
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.StudyId);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);
            projector = projector.Include(x => x.ClinicalStudy.StudyDesigns);
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x._id);

            return projector;
        }

        public static ProjectionDefinition<StudyEntity> GetProjectionForCheckAccessForAStudy()
        {
            ProjectionDefinitionBuilder<StudyEntity> projection = Builders<StudyEntity>.Projection;
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.StudyId);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);
            projector = projector.Exclude(x => x._id);

            return projector;
        }
    }
}
