<a name='assembly'></a>
# TransCelerate.SDR.DataAccess

## Contents

- [CommonRepository](#T-TransCelerate-SDR-DataAccess-Repositories-CommonRepository 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersion(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetUsdmVersion-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetUsdmVersion(System.String,System.Int32)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudyV1(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV1-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV1(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudyV2(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV2-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV2(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudyV3(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV3(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [DataFilterCommon](#T-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForSearchTitle(searchParameters,groups,user)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetFiltersForSearchV1(searchParameters,groups,user)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV1-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV1(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetFiltersForSearchV2(searchParameters,groups,user)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV2-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV2(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetFiltersForSearchV3(searchParameters,groups,user)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV3(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle,groups,user)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [DataFiltersV1](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForSearchStudy(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForSearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForSearchStudy(TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters)')
  - [GetFiltersForSearchTitle(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForSearchTitle(TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV1.GetProjectionForPartialStudyElements(System.String[])')
- [DataFiltersV2](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV2.GetProjectionForPartialStudyElements(System.String[])')
- [DataFiltersV3](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetProjectionForPartialStudyElements(System.String[])')
- [ICommonRepository](#T-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetUsdmVersion(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetUsdmVersion-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetUsdmVersion(System.String,System.Int32)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IStudyRepositoryV1](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-SearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.SearchStudy(TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-SearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.SearchTitle(TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV1.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity)')
- [IStudyRepositoryV2](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV2.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
- [IStudyRepositoryV3](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [IUserGroupMappingRepository](#T-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository')
  - [AddAGroup(group)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.AddAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [GetAGroupById()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAGroupById-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetAGroupById(System.String)')
  - [GetAllUserGroups()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAllUserGroups 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetAllUserGroups')
  - [GetGroupByName()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupByName-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroupByName(System.String)')
  - [GetGroupList()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupList 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroupList')
  - [GetGroups()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [UpdateAGroup(group)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.UpdateAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [UpdateUsersToGroups(userGroupMappingEntity)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.UpdateUsersToGroups(TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity)')
- [StudyRepositoryV1](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-SearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.SearchStudy(TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-SearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.SearchTitle(TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV1.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity)')
- [StudyRepositoryV2](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV2.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity)')
- [StudyRepositoryV3](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [UserGroupMappingRepository](#T-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository')
  - [AddAGroup(group)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.AddAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [GetAGroupById()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAGroupById-System-String- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetAGroupById(System.String)')
  - [GetAllUserGroups()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAllUserGroups 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetAllUserGroups')
  - [GetGroupByName()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupByName-System-String- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroupByName(System.String)')
  - [GetGroupList()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupList 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroupList')
  - [GetGroups()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [UpdateAGroup(group)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.UpdateAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [UpdateUsersToGroups(userGroupMappingEntity)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.UpdateUsersToGroups(TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity)')

<a name='T-TransCelerate-SDR-DataAccess-Repositories-CommonRepository'></a>
## CommonRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get List of all studyId

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetUsdmVersion-System-String,System-Int32-'></a>
### GetUsdmVersion(studyId,sdruploadversion) `method`

##### Summary

GET UsdmVersion

##### Returns

`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Loggedin User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV1-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudyV1(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Loggedin User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV2-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudyV2(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Loggedin User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudyV3(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Loggedin User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | LoggedIn User |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon'></a>
## DataFilterCommon `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetFiltersForGetAudTrail(studyId,fromDate,toDate) `method`

##### Summary

Get filters for AuditTrail API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetFiltersForSearchTitle(searchParameters,groups,user) `method`

##### Summary

Get filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') |  |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV1-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetFiltersForSearchV1(searchParameters,groups,user) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV2-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetFiltersForSearchV2(searchParameters,groups,user) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetFiltersForSearchV3(searchParameters,groups,user) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetFiltersForStudyHistory(fromDate,toDate,studyTitle,groups,user) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| groups | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity}') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1'></a>
## DataFiltersV1 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

##### Summary

DataFilters for getting data from data base

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetFiltersForGetAudTrail(studyId,fromDate,toDate) `method`

##### Summary

Get filters for AuditTrail API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32-'></a>
### GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for AuditTrail

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForSearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters-'></a>
### GetFiltersForSearchStudy(searchParameters) `method`

##### Summary

Get filters for Search Study API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters-'></a>
### GetFiltersForSearchTitle(searchParameters) `method`

##### Summary

Get filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetFiltersForStudyHistory(fromDate,toDate,studyTitle) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetProjectionForPartialStudyDesignElementsFullStudy'></a>
### GetProjectionForPartialStudyDesignElementsFullStudy() `method`

##### Summary

Get Study Design Projection Definition

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV1-GetProjectionForPartialStudyElements-System-String[]-'></a>
### GetProjectionForPartialStudyElements(listofelementsArray) `method`

##### Summary

Get projectio definition for partial study elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | list of study elements |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2'></a>
## DataFiltersV2 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

##### Summary

DataFilters for getting data from data base

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetFiltersForGetAudTrail(studyId,fromDate,toDate) `method`

##### Summary

Get filters for AuditTrail API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32-'></a>
### GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for AuditTrail

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetFiltersForStudyHistory(fromDate,toDate,studyTitle) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetProjectionForPartialStudyDesignElementsFullStudy'></a>
### GetProjectionForPartialStudyDesignElementsFullStudy() `method`

##### Summary

Get Study Design Projection Definition

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV2-GetProjectionForPartialStudyElements-System-String[]-'></a>
### GetProjectionForPartialStudyElements(listofelementsArray) `method`

##### Summary

Get projectio definition for partial study elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | list of study elements |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3'></a>
## DataFiltersV3 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

##### Summary

DataFilters for getting data from data base

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetFiltersForGetAudTrail(studyId,fromDate,toDate) `method`

##### Summary

Get filters for AuditTrail API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32-'></a>
### GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for AuditTrail

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetFiltersForStudyHistory(fromDate,toDate,studyTitle) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyDesignElementsFullStudy'></a>
### GetProjectionForPartialStudyDesignElementsFullStudy() `method`

##### Summary

Get Study Design Projection Definition

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyElements-System-String[]-'></a>
### GetProjectionForPartialStudyElements(listofelementsArray) `method`

##### Summary

Get projectio definition for partial study elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | list of study elements |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository'></a>
## ICommonRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get List of all studyId

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetUsdmVersion-System-String,System-Int32-'></a>
### GetUsdmVersion(studyId,sdruploadversion) `method`

##### Summary

GET UsdmVersion

##### Returns

`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Loggedin User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | LoggedIn User |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1'></a>
## IStudyRepositoryV1 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get List of all studyId

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-SearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-SearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV1-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2'></a>
## IStudyRepositoryV2 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all version of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV2.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV2-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3'></a>
## IStudyRepositoryV3 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all version of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV3.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository'></a>
## IUserGroupMappingRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity-'></a>
### AddAGroup(group) `method`

##### Summary

Add a Group

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| group | [TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') | Update User Group Mapping |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAGroupById-System-String-'></a>
### GetAGroupById() `method`

##### Summary

GET a group with groupId

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAllUserGroups'></a>
### GetAllUserGroups() `method`

##### Summary

GET All Users for a group

##### Returns

A [UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupByName-System-String-'></a>
### GetGroupByName() `method`

##### Summary

GET a group by groupName

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupList'></a>
### GetGroupList() `method`

##### Summary

GET group list

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetGroups() `method`

##### Summary

GET All groups

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity-'></a>
### UpdateAGroup(group) `method`

##### Summary

Updates A Group

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| group | [TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') | Add User Group Mapping |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity-'></a>
### UpdateUsersToGroups(userGroupMappingEntity) `method`

##### Summary

Updates User Group Mapping

##### Returns

A [UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userGroupMappingEntity | [TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity') | Add User Group Mapping |

<a name='T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1'></a>
## StudyRepositoryV1 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get List of all studyId

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-SearchStudy-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-SearchTitle-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-StudyV1-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.StudyV1.SearchTitleParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV1-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2'></a>
## StudyRepositoryV2 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all versions of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV2.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV2-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3'></a>
## StudyRepositoryV3 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all versions of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV3.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository'></a>
## UserGroupMappingRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity-'></a>
### AddAGroup(group) `method`

##### Summary

Add a Group

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| group | [TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') | Update User Group Mapping |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAGroupById-System-String-'></a>
### GetAGroupById() `method`

##### Summary

GET a group with groupId

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAllUserGroups'></a>
### GetAllUserGroups() `method`

##### Summary

GET All Users for a group

##### Returns

A [UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupByName-System-String-'></a>
### GetGroupByName() `method`

##### Summary

GET group by groupName

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupList'></a>
### GetGroupList() `method`

##### Summary

GET group list

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of Group Name and Group Id `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetGroups() `method`

##### Summary

GET all groups

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity-'></a>
### UpdateAGroup(group) `method`

##### Summary

Updates A Group

##### Returns

A [SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| group | [TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity 'TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity') | Add User Group Mapping |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity-'></a>
### UpdateUsersToGroups(userGroupMappingEntity) `method`

##### Summary

Updates User Group Mapping

##### Returns

A [UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userGroupMappingEntity | [TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity](#T-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity 'TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity') | Add User Group Mapping |
