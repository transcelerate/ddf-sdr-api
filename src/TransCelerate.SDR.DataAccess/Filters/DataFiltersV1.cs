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
        public static FilterDefinition<StudyEntity> GetFiltersForGetStudy(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.ClinicalStudy.Uuid == studyId);

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
            filter &= builder.Where(s => s.ClinicalStudy.Uuid == studyId);

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
        public static FilterDefinition<StudyEntity> GetFiltersForSearchStudy(SearchParameters searchParameters)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
                filter &= builder.Where(x => x.ClinicalStudy.StudyIdentifiers.Any(x=> (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.StudyId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x=>x.StudyIndications.Any(y=>y.IndicationDesc.ToLower().Contains(searchParameters.Indication.ToLower()))));

            //Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x=>x.InterventionModel.Any(y=>y.Decode.ToLower().Contains(searchParameters.InterventionModel.ToLower()))));

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
                filter &= builder.Where(x => x.ClinicalStudy.StudyPhase.Decode.ToLower().Contains(searchParameters.Phase.ToLower()));


            return filter;            
        }

        /// <summary>
        /// Get filters for Search Study Title API
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static FilterDefinition<StudyEntity> GetFiltersForSearchTitle(SearchTitleParameters searchParameters)
        {
            FilterDefinitionBuilder<StudyEntity> builder = Builders<StudyEntity>.Filter;
            FilterDefinition<StudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
                filter &= builder.Where(x => x.ClinicalStudy.StudyIdentifiers.Any(x => (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.StudyId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));            

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
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.Uuid);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);
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
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.Uuid);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);
            projector = projector.Include(x => x.ClinicalStudy.StudyDesigns);
            projector = projector.Exclude(x => x._id);

            return projector;
        }

        public static ProjectionDefinition<StudyEntity> GetProjectionForCheckAccessForAStudy()
        {
            ProjectionDefinitionBuilder<StudyEntity> projection = Builders<StudyEntity>.Projection;
            ProjectionDefinition<StudyEntity> projector = projection.Include(x => x.ClinicalStudy.Uuid);
            projector = projector.Include(x => x.ClinicalStudy.StudyType);            
            projector = projector.Exclude(x => x._id);

            return projector;
        }
    }
}
