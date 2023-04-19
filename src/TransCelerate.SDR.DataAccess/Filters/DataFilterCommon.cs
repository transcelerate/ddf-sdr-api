using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static FilterDefinition<BsonDocument> GetFiltersForGetStudyBsonDocument(string studyId, int sdruploadversion)
        {
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Empty;
            filter &= builder.Eq(Constants.DbFilter.StudyId, studyId);

            if (sdruploadversion != 0)
                filter &= builder.Eq("auditTrail.SDRUploadVersion", sdruploadversion);

            return filter;
        }
        public static SortDefinition<BsonDocument> GetSorterForBsonDocument()
        {
            SortDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Sort;
            SortDefinition<BsonDocument> sorter = builder.Descending("auditTrail.entryDateTime");

            return sorter;
        }
        public static ProjectionDefinition<BsonDocument> GetProjectorForBsonDocument()
        {
            ProjectionDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Projection;
            ProjectionDefinition<BsonDocument> projector = builder.Exclude("_id");

            return projector;
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
            {
                filter &= builder.Or(
                                     builder.And(
                                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                                     }
                                                 )
                                             ),
                                     builder.And(
                                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierIdType, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID}$/i")}
                                                     }
                                                 )
                                             )
                                    );
            }

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
            {
                filter &= builder.Or(
                     builder.And(
                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                     }
                                 )
                             ),
                     builder.And(
                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
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

        public static FilterDefinition<CommonStudyEntity> GetFiltersForSearchStudy(SearchParametersEntity searchParameters, List<SDRGroupsEntity> groups, LoggedInUser user)
        {
            FilterDefinitionBuilder<CommonStudyEntity> builder = Builders<CommonStudyEntity>.Filter;
            FilterDefinition<CommonStudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
            {
                filter &= builder.Or(
                     builder.And(
                             builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrganisationIdentifier, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
                                                         { Constants.DbFilter.StudyIdentifierOrganisationTypeDecode, new BsonRegularExpression($"/{Constants.IdType.SPONSOR_ID_V1}$/i")}
                                     }
                                 )
                             ),
                     builder.And(
                            builder.ElemMatch<BsonDocument>(Constants.DbFilter.StudyIdentifiers, new BsonDocument()
                                     {
                                                         { Constants.DbFilter.StudyIdentifierOrgCode, new BsonRegularExpression($"/{searchParameters.SponsorId}/i") } ,
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

        public static FilterDefinition<Core.Entities.Study.StudyEntity> GetFiltersForSearchMVP(SearchParametersEntity searchParameters, List<SDRGroupsEntity> groups, LoggedInUser user)
        {
            FilterDefinitionBuilder<Core.Entities.Study.StudyEntity> builder = Builders<Core.Entities.Study.StudyEntity>.Filter;
            FilterDefinition<Core.Entities.Study.StudyEntity> filter = builder.Empty;

            //Filter for usdmVersion
            filter &= builder.Where(x => x.AuditTrail.UsdmVersion.ToLower() == searchParameters.UsdmVersion.ToLower());

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);
            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
                filter &= builder.Where(x => x.ClinicalStudy.StudyIdentifiers.Any(x => x.IdType.ToLower() == Constants.IdType.SPONSOR_ID.ToLower() && x.OrgCode.ToLower().Contains(searchParameters.SponsorId)));

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
                filter &= builder.Where(x => x.ClinicalStudy.CurrentSections.Any(y=>y.StudyIndications.Any(z=>z.Description.ToLower().Contains(searchParameters.Indication.ToLower()))));

            //Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
                filter &= builder.Where(x => x.ClinicalStudy.CurrentSections.Any(y=>y.StudyDesigns.Any(z=>z.CurrentSections.Any(a=>a.InvestigationalInterventions.Any(b=>b.InterventionModel.ToLower().Contains(searchParameters.InterventionModel.ToLower()))))));

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
                filter &= builder.Where(x => x.ClinicalStudy.StudyPhase.ToLower().Contains(searchParameters.Phase.ToLower()));

            return filter;
        }

        /// <summary>
        /// Search Filters
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static FilterDefinition<Core.Entities.StudyV1.StudyEntity> GetFiltersForSearchV1(SearchParametersEntity searchParameters, List<SDRGroupsEntity> groups, LoggedInUser user)
        {
            FilterDefinitionBuilder<Core.Entities.StudyV1.StudyEntity> builder = Builders<Core.Entities.StudyV1.StudyEntity>.Filter;
            FilterDefinition<Core.Entities.StudyV1.StudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for usdmVersion
            filter &= builder.Where(x => x.AuditTrail.UsdmVersion.ToLower() == searchParameters.UsdmVersion.ToLower());

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
            {
                if (groups != null && groups.Any())
                {
                    Tuple<List<string>, List<string>> groupFilters = Core.Utilities.Helpers.GroupFilters.GetGroupFilters(groups);

                    if (!groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                    {
                        if (groupFilters.Item1.Any())
                        {
                            filter &= builder.Or(                                        
                                        builder.Regex($"{Constants.DbFilter.StudyType}.{Constants.DbFilter.StudyPhaseDecode}", new BsonRegularExpression($"/{String.Join("$|", groupFilters.Item1)}$/i")),
                                        builder.In(x => x.ClinicalStudy.Uuid, groupFilters.Item2)
                                        );
                        }
                        else
                        {
                            filter &= builder.In(x => x.ClinicalStudy.Uuid, groupFilters.Item2);
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
                filter &= builder.Where(x => x.ClinicalStudy.StudyIdentifiers.Any(x => (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.SponsorId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x => x.StudyIndications.Any(y => y.IndicationDesc.ToLower().Contains(searchParameters.Indication.ToLower()))));

            //Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x => x.InterventionModel.Any(y => y.Decode.ToLower().Contains(searchParameters.InterventionModel.ToLower()))));

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
                filter &= builder.Where(x => x.ClinicalStudy.StudyPhase.Decode.ToLower().Contains(searchParameters.Phase.ToLower()));


            return filter;
        }

        /// <summary>
        /// Search Filters
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static FilterDefinition<Core.Entities.StudyV2.StudyEntity> GetFiltersForSearchV2(SearchParametersEntity searchParameters, List<SDRGroupsEntity> groups, LoggedInUser user)
        {
            FilterDefinitionBuilder<Core.Entities.StudyV2.StudyEntity> builder = Builders<Core.Entities.StudyV2.StudyEntity>.Filter;
            FilterDefinition<Core.Entities.StudyV2.StudyEntity> filter = builder.Empty;

            //Filter for Date Range
            filter &= builder.Where(x => x.AuditTrail.EntryDateTime >= searchParameters.FromDate
                                         && x.AuditTrail.EntryDateTime <= searchParameters.ToDate);

            //Filter for usdmVersion
            filter &= builder.Where(x => x.AuditTrail.UsdmVersion.ToLower() == searchParameters.UsdmVersion.ToLower());

            //For Data Segmentation
            if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
            {
                if (groups != null && groups.Any())
                {
                    Tuple<List<string>, List<string>> groupFilters = Core.Utilities.Helpers.GroupFilters.GetGroupFilters(groups);

                    if (!groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                    {
                        if (groupFilters.Item1.Any())
                        {
                            filter &= builder.Or(
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
            if (!String.IsNullOrWhiteSpace(searchParameters.SponsorId))
                filter &= builder.Where(x => x.ClinicalStudy.StudyIdentifiers.Any(x => (x.StudyIdentifierScope.OrganisationIdentifier.ToLower().Contains(searchParameters.SponsorId.ToLower())) && (x.StudyIdentifierScope.OrganisationType.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower())));

            //Filter for Indication
            if (!String.IsNullOrWhiteSpace(searchParameters.Indication))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x => x.StudyIndications.Any(y => y.IndicationDescription.ToLower().Contains(searchParameters.Indication.ToLower()))));

            //Filter for Intervention Model
            if (!String.IsNullOrWhiteSpace(searchParameters.InterventionModel))
                filter &= builder.Where(x => x.ClinicalStudy.StudyDesigns.Any(x => x.InterventionModel.Decode.ToLower().Contains(searchParameters.InterventionModel.ToLower())));

            //Filter for Study Phase
            if (!String.IsNullOrWhiteSpace(searchParameters.Phase))
                filter &= builder.Where(x => x.ClinicalStudy.StudyPhase.StandardCode.Decode.ToLower().Contains(searchParameters.Phase.ToLower()));


            return filter;
        }


        public static IEnumerable<Core.Entities.StudyV2.SearchResponseEntity> SortSearchResultsV2(List<Core.Entities.StudyV2.SearchResponseEntity> searchResponses, string property, bool asc)
        {
            if (!String.IsNullOrWhiteSpace(property))
            {
                return property.ToLower() switch
                {
                    //Sort by studyTitle
                    "studytitle" => asc ? searchResponses.OrderBy(s => s.StudyTitle) : searchResponses.OrderByDescending(s => s.StudyTitle),

                    //Sort by studyIdentifier: orgCode
                    "sponsorid" => asc ? searchResponses.OrderBy(s => s.StudyIdentifiers != null ? s.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower()).Any() ? s.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : "")
                                                                                    : searchResponses.OrderByDescending(s => s.StudyIdentifiers != null ? s.StudyIdentifiers.FindAll(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower()).Any() ? s.StudyIdentifiers.Find(x => x.StudyIdentifierScope?.OrganisationType?.Decode.ToLower() == Constants.IdType.SPONSOR_ID_V1.ToLower()).StudyIdentifierScope.OrganisationIdentifier ?? "" : "" : ""),

                    //Sort by studyIndication: description
                    "indication" => asc ? searchResponses.OrderBy(s => (s.StudyIndications != null && s.StudyIndications.Any()) ? (s.StudyIndications.First() != null && s.StudyIndications.First().Any()) ? s.StudyIndications.First().First() != null ? s.StudyIndications.First().First().IndicationDescription ?? "" : "" : "" : "")
                                                                                        : searchResponses.OrderByDescending(s => (s.StudyIndications != null && s.StudyIndications.Any()) ? (s.StudyIndications.First() != null && s.StudyIndications.First().Any()) ? s.StudyIndications.First().First() != null ? s.StudyIndications.First().First().IndicationDescription ?? "" : "" : "" : ""),

                    //Sort by studyDesign: Intervention Model
                    "interventionmodel" => asc ? searchResponses.OrderBy(s => (s.InterventionModel != null && s.InterventionModel.Any()) ? s.InterventionModel.First() != null ? s.InterventionModel.First().Decode :"" :"")
                                                           : searchResponses.OrderByDescending(s => (s.InterventionModel != null && s.InterventionModel.Any()) ? s.InterventionModel.First() != null ? s.InterventionModel.First().Decode : "" : ""),

                    //Sort by studyPhase
                    "phase" => asc ? searchResponses.OrderBy(s => s.StudyPhase != null ? s.StudyPhase.StandardCode?.Decode ?? "" : "") : searchResponses.OrderByDescending(s => s.StudyPhase != null ? s.StudyPhase.StandardCode?.Decode ?? "" : ""),

                    //Sort by SDR version
                    "sdrversion" => asc ? searchResponses.OrderBy(s => s.SDRUploadVersion) : searchResponses.OrderByDescending(s => s.SDRUploadVersion),

                    //Sort by entryDateTime
                    "lastmodifieddate" => asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime),

                    //Sort by entrySystem Descending by default
                    _ => asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime),
                };
            }
            else
            {
                return asc ? searchResponses.OrderBy(s => s.EntryDateTime) : searchResponses.OrderByDescending(s => s.EntryDateTime);
            }
        }
    }
}
