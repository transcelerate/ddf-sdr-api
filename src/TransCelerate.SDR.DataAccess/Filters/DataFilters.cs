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
    public static class DataFilters
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
    }
}
