using MongoDB.Driver;
using System;
using System.Linq;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.DataAccess.Filters
{
    /// <summary>
    /// DataFilters for getting data from data base
    /// </summary>
    public static class DataFiltersV1
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
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V1);
            filter &= builder.Where(s => s.Study.Uuid == studyId);

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
            filter &= builder.Where(s => s.Study.Uuid == studyId);

            if (sdruploadversion != 0)
                filter &= builder.Where(x => x.AuditTrail.SDRUploadVersion == sdruploadversion);

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
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V1);
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
            filter &= builder.Where(s => s.AuditTrail.UsdmVersion == Constants.USDMVersions.V1);
            filter &= builder.Where(s => s.Study.Uuid == studyId);

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);


            return filter;
        }

        /// <summary>
        /// Get filters for Search Study API
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForSearchStudy(SearchParameters searchParameters)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.Study.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
                filter &= builder.Where(x => x.Study.StudyIdentifiers.Any(x => (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.StudyId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
                filter &= builder.Where(x => x.Study.StudyDesigns.Any(x => x.StudyIndications.Any(y => y.IndicationDesc.ToLower().Contains(searchParameters.Indication.ToLower()))));

            //Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
                filter &= builder.Where(x => x.Study.StudyDesigns.Any(x => x.InterventionModel.Any(y => y.Decode.ToLower().Contains(searchParameters.InterventionModel.ToLower()))));

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
                filter &= builder.Where(x => x.Study.StudyPhase.Decode.ToLower().Contains(searchParameters.Phase.ToLower()));


            return filter;
        }

        /// <summary>
        /// Get filters for Search Study Title API
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyDefinitionsEntity> GetFiltersForSearchTitle(SearchTitleParameters searchParameters)
        {
            FilterDefinitionBuilder<StudyDefinitionsEntity> builder = Builders<StudyDefinitionsEntity>.Filter;
            FilterDefinition<StudyDefinitionsEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.Study.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
                filter &= builder.Where(x => x.Study.StudyIdentifiers.Any(x => (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.StudyId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));

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
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Uuid);
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
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Uuid);
            projector = projector.Include(x => x.Study.StudyType);
            projector = projector.Include(x => x.Study.StudyDesigns);
            projector = projector.Include(x => x.AuditTrail);
            projector = projector.Exclude(x => x.Id);

            return projector;
        }

        public static ProjectionDefinition<StudyDefinitionsEntity> GetProjectionForCheckAccessForAStudy()
        {
            ProjectionDefinitionBuilder<StudyDefinitionsEntity> projection = Builders<StudyDefinitionsEntity>.Projection;
            ProjectionDefinition<StudyDefinitionsEntity> projector = projection.Include(x => x.Study.Uuid);
            projector = projector.Include(x => x.Study.StudyType);
            projector = projector.Exclude(x => x.Id);

            return projector;
        }
    }
}
