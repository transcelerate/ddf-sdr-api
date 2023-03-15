using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
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

        /// <summary>
        /// Get filters for AuditTrail API
        /// </summary>
        /// <param name="studyId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static FilterDefinition<CommonStudyEntity> GetFiltersForGetAudTrail(string studyId, DateTime fromDate, DateTime toDate)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;
            filter &= builder.Where(s => s.ClinicalStudy.StudyId == studyId);

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);


            return filter;
        }

        /// <summary>
        /// Get filters for StudyHistory API
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studyTitle"></param>
        /// <returns></returns>
        public static FilterDefinition<CommonStudyEntity> GetFiltersForStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;
            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= fromDate
                                         && x.AuditTrail.EntryDateTime <= toDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(studyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(studyTitle.ToLower()));


            return filter;
        }
        /// <summary>
        /// Get filters for Search Study Title API
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static FilterDefinition<CommonStudyEntity> GetFiltersForSearchTitle(SearchTitleParametersEntity searchParameters, List<SDRGroupsEntity> groups, LoggedInUser user)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
            {
                filter &= builder.Or(
                                     builder.And(
                                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                                     }
                                                 )
                                             ),
                                     builder.And(
                                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierIdType, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID}$/i")}
                                                     }
                                                 )
                                             )
                                    );
            }

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
            {
                if (groups != null && groups.Any())
                {
                    Tuple<List<string>, List<string>> groupFilters = Core.Utilities.Helpers.GroupFilters.GetGroupFilters(groups);

                    if (!groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                    {
                        if (groupFilters.Item1.Any())
                        {
                            filter &= builder.Or(
                                        builder.Regex(Constants.DbFilter.StudyType, new BsonRegularExpression($"/{String.Join("$|", groupFilters.Item1)}$/i")),
                                        builder.Regex($"{Constants.DbFilter.StudyType}.{Constants.DbFilter.StudyPhaseDecode}", new BsonRegularExpression($"/{String.Join("$|", groupFilters.Item1)}$/i")),
                                        builder.In(x => x.ClinicalStudy.StudyId, groupFilters.Item2)
                                        );
                        }
                        else
                        {
                            filter &= builder.In(x => x.ClinicalStudy.StudyId, groupFilters.Item2);
                        }
                    }
                }
                else
                    filter &= builder.Where(x => x.ClinicalStudy == null); //if there are no groups assigned for the user
            }

            return filter;
        }

        /// <summary>
        /// Get filters for Search Study API
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static FilterDefinition<CommonStudyEntity> GetFiltersForSearchStudy(SearchParametersEntity searchParameters)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
            {
                filter &= builder.Or(
                     builder.And(
                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                     }
                                 )
                             ),
                     builder.And(
                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierIdType, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID}$/i")}
                                     }
                                 )
                             )
                    );
            }

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
            {
                filter &= builder.Or(
                     builder.Regex($"{Constants.DbFilter.IndicationMVP}", new BsonRegularExpression($"/{searchParameters.Indication}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.StudyIndicationsIndicationDesc}", new BsonRegularExpression($"/{searchParameters.Indication}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.StudyIndicationsIndicationDescription}", new BsonRegularExpression($"/{searchParameters.Indication}/i"))
                    );
            }

            ////Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
            {
                filter &= builder.Or(
                     builder.Regex($"{Constants.DbFilter.InterventionModelMVP}", new BsonRegularExpression($"/{searchParameters.InterventionModel}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.InterventionModel}", new BsonRegularExpression($"/{searchParameters.InterventionModel}/i"))
                    );
            }

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
            {
                filter &= builder.Or(
                     builder.Regex(Constants.DbFilter.StudyPhase, new BsonRegularExpression($"/{searchParameters.Phase}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyPhase}.{Constants.DbFilter.StudyPhaseDecode}", new BsonRegularExpression($"/{searchParameters.Phase}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyPhase}.{Constants.DbFilter.StudyPhaseStandardCodeDecode}", new BsonRegularExpression($"/{searchParameters.Phase}/i"))
                    );
            }



            return filter;
        }

        public static FilterDefinition<CommonStudyEntity> GetFiltersForSearchStudy(SearchParametersEntity searchParameters, List<SDRGroupsEntity> groups,LoggedInUser user)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
            {
                if (groups != null && groups.Any())
                {
                    Tuple<List<string>, List<string>> groupFilters = Core.Utilities.Helpers.GroupFilters.GetGroupFilters(groups);

                    if (!groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                    {                        
                        if (groupFilters.Item1.Any())
                        {
                            filter &= builder.Or(
                                        builder.Regex(Constants.DbFilter.StudyType, new BsonRegularExpression($"/{String.Join("$|", groupFilters.Item1)}$/i")),
                                        builder.Regex($"{Constants.DbFilter.StudyType}.{Constants.DbFilter.StudyPhaseDecode}", new BsonRegularExpression($"/{String.Join("$|", groupFilters.Item1)}$/i")),
                                        builder.In(x => x.ClinicalStudy.StudyId, groupFilters.Item2)
                                        );
                        }
                        else
                        {
                            filter &= builder.In(x => x.ClinicalStudy.StudyId, groupFilters.Item2);
                        }
                    }
                }
                else
                    filter &= builder.Where(x => x.ClinicalStudy == null); //if there are no groups assigned for the user
            }
                

            //Filter for StudyTitle
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyTitle))
                filter &= builder.Where(x => x.ClinicalStudy.StudyTitle.ToLower().Contains(searchParameters.StudyTitle.ToLower()));

            //Filter for OrgCode
            if (!String.IsNullOrWhiteSpace(searchParameters.StudyId))
            {
                filter &= builder.Or(
                     builder.And(
                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                     }
                                 )
                             ),
                     builder.And(
                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.StudyId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierIdType, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID}$/i")}
                                     }
                                 )
                             )
                    );
            }

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
            {
                filter &= builder.Or(
                     builder.Regex($"{Constants.DbFilter.IndicationMVP}", new BsonRegularExpression($"/{searchParameters.Indication}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.StudyIndicationsIndicationDesc}", new BsonRegularExpression($"/{searchParameters.Indication}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.StudyIndicationsIndicationDescription}", new BsonRegularExpression($"/{searchParameters.Indication}/i"))
                    );
            }

            ////Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
            {
                filter &= builder.Or(
                     builder.Regex($"{Constants.DbFilter.InterventionModelMVP}", new BsonRegularExpression($"/{searchParameters.InterventionModel}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyDesigns}.{Constants.DbFilter.InterventionModel}", new BsonRegularExpression($"/{searchParameters.InterventionModel}/i"))
                    );
            }

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
            {
                filter &= builder.Or(
                     builder.Regex(Constants.DbFilter.StudyPhase, new BsonRegularExpression($"/{searchParameters.Phase}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyPhase}.{Constants.DbFilter.StudyPhaseDecode}", new BsonRegularExpression($"/{searchParameters.Phase}/i")),
                     builder.Regex($"{Constants.DbFilter.StudyPhase}.{Constants.DbFilter.StudyPhaseStandardCodeDecode}", new BsonRegularExpression($"/{searchParameters.Phase}/i"))
                    );
            }



            return filter;
        }
        public static SortDefinition<SearchResponseEntity> GetSorterForSearchStudy(SearchParametersEntity searchParameters)
        {
            SortDefinitionBuilder<SearchResponseEntity> builder = Builders<SearchResponseEntity>.Sort;
            SortDefinition<SearchResponseEntity> sorter = builder.Descending(x => x.EntryDateTime);

            if (!String.IsNullOrWhiteSpace(searchParameters.Header))
            {
                switch (searchParameters.Header.ToLower())
                {
                    case "studytitle":
                        sorter = searchParameters.Asc ? builder.Ascending(x => x.StudyTitle) : builder.Descending(x => x.StudyTitle);
                        break;
                    case "sdrversion":
                        sorter = searchParameters.Asc ? builder.Ascending(x => x.SDRUploadVersion) : builder.Descending(x => x.SDRUploadVersion);
                        break;
                    case "lastmodifieddate":
                        sorter = searchParameters.Asc ? builder.Ascending(x => x.EntryDateTime) : builder.Descending(x => x.EntryDateTime);
                        break;
                    case "usdmversion":
                        sorter = searchParameters.Asc ? builder.Ascending(x => x.UsdmVersion) : builder.Descending(x => x.UsdmVersion);
                        break;
                }
            }

            return sorter;
        }

        public static SortDefinition<SearchTitleResponseEntity> GetSorterForSearchStudyTitle(SearchTitleParametersEntity searchParameters)
        {
            SortDefinitionBuilder<SearchTitleResponseEntity> builder = Builders<SearchTitleResponseEntity>.Sort;
            SortDefinition<SearchTitleResponseEntity> sorter = builder.Descending(x => x.EntryDateTime);

            if (!String.IsNullOrWhiteSpace(searchParameters.SortBy))
            {
                switch (searchParameters.SortBy.ToLower())
                {
                    case "studytitle":
                        sorter = searchParameters.SortOrder == SortOrder.asc.ToString() ? builder.Ascending(x => x.StudyTitle) : builder.Descending(x => x.StudyTitle);
                        break;
                    case "version":
                        sorter = searchParameters.SortOrder == SortOrder.asc.ToString() ? builder.Ascending(x => x.SDRUploadVersion) : builder.Descending(x => x.SDRUploadVersion);
                        break;
                    case "lastmodifieddate":
                        sorter = searchParameters.SortOrder == SortOrder.asc.ToString() ? builder.Ascending(x => x.EntryDateTime) : builder.Descending(x => x.EntryDateTime);
                        break;
                    case "usdmversion":
                        sorter = searchParameters.SortOrder == SortOrder.asc.ToString() ? builder.Ascending(x => x.UsdmVersion) : builder.Descending(x => x.UsdmVersion);
                        break;
                }
            }

            return sorter;
        }
    }
}
