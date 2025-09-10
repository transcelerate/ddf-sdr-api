<a name='assembly'></a>
# TransCelerate.SDR.DataAccess

## Contents

- [ChangeAuditRepository](#T-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository 'TransCelerate.SDR.DataAccess.Repositories.ChangeAuditRepository')
  - [GetStudyItemsAsyncV3(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.ChangeAuditRepository.GetStudyItemsAsyncV3(System.String,System.Int32)')
  - [GetStudyItemsAsyncV4(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV4-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.ChangeAuditRepository.GetStudyItemsAsyncV4(System.String,System.Int32)')
  - [GetStudyItemsAsyncV5(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV5-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.ChangeAuditRepository.GetStudyItemsAsyncV5(System.String,System.Int32)')
- [CommonRepository](#T-TransCelerate-SDR-DataAccess-Repositories-CommonRepository 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetAuditTrailsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetAuditTrailsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetAuditTrailsAsync(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersion(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetUsdmVersion-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.GetUsdmVersion(System.String,System.Int32)')
  - [SearchStudy(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [SearchStudyV3(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV3(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [SearchStudyV4(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV4-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV4(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [SearchStudyV5(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV5-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchStudyV5(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [SearchTitle(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Repositories.CommonRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
- [DataFilterCommon](#T-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon')
  - [GetCommonFiltersForSearchTitle(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetCommonFiltersForSearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
  - [GetCommonFiltersForSearchTitleV4(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitleV4-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetCommonFiltersForSearchTitleV4(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
  - [GetCommonFiltersForSearchTitleV5(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitleV5-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetCommonFiltersForSearchTitleV5(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
  - [GetFiltersForChangeAudit(studyId)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForChangeAudit-System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForChangeAudit(System.String)')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForSearchTitleV5(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchTitleV5-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchTitleV5(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
  - [GetFiltersForSearchV3(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV3(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [GetFiltersForSearchV4(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV4-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV4(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [GetFiltersForSearchV5(searchParameters)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV5-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForSearchV5(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetFiltersForStudyHistoryV4(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistoryV4-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForStudyHistoryV4(System.DateTime,System.DateTime,System.String)')
  - [GetFiltersForStudyHistoryV5(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistoryV5-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFilterCommon.GetFiltersForStudyHistoryV5(System.DateTime,System.DateTime,System.String)')
- [DataFiltersV3](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3')
  - [GetFiltersForGetAudTrail(studyId,fromDate,toDate)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAudTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetAudTrail(System.String,System.DateTime,System.DateTime)')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetFiltersForStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetFiltersForStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV3-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV3.GetProjectionForPartialStudyElements(System.String[])')
- [DataFiltersV4](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV4')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV4.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV4.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV4.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV4.GetProjectionForPartialStudyElements(System.String[])')
- [DataFiltersV5](#T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV5')
  - [GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV5.GetFiltersForGetAuditTrailOfAStudy(System.String,System.Int32)')
  - [GetFiltersForGetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetFiltersForGetStudy-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV5.GetFiltersForGetStudy(System.String,System.Int32)')
  - [GetProjectionForPartialStudyDesignElementsFullStudy()](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetProjectionForPartialStudyDesignElementsFullStudy 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV5.GetProjectionForPartialStudyDesignElementsFullStudy')
  - [GetProjectionForPartialStudyElements(listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetProjectionForPartialStudyElements-System-String[]- 'TransCelerate.SDR.DataAccess.Filters.DataFiltersV5.GetProjectionForPartialStudyElements(System.String[])')
- [ICommonRepository](#T-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetAuditTrailsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetAuditTrailsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetAuditTrailsAsync(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [GetUsdmVersion(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetUsdmVersion-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.GetUsdmVersion(System.String,System.Int32)')
  - [SearchStudy(searchParameters)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity)')
  - [SearchTitle(searchParameters)](#M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity- 'TransCelerate.SDR.DataAccess.Interfaces.ICommonRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity)')
- [IStudyRepositoryV3](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV3.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [IStudyRepositoryV4](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV4.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
- [IStudyRepositoryV5](#T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IStudyRepositoryV5.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
- [StudyRepositoryV3](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV3-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV3.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity)')
- [StudyRepositoryV4](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV4.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity)')
- [StudyRepositoryV5](#T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5')
  - [CountAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-CountAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.CountAsync(System.String)')
  - [DeleteStudyAsync(study_uuid)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-DeleteStudyAsync-System-String- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.DeleteStudyAsync(System.String)')
  - [GetPartialStudyDesignItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetPartialStudyDesignItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.GetPartialStudyDesignItemsAsync(System.String,System.Int32)')
  - [GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.GetPartialStudyItemsAsync(System.String,System.Int32,System.String[])')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetUsdmVersionAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetUsdmVersionAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.GetUsdmVersionAsync(System.String,System.Int32)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity- 'TransCelerate.SDR.DataAccess.Repositories.StudyRepositoryV5.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity)')

<a name='T-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository'></a>
## ChangeAuditRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32-'></a>
### GetStudyItemsAsyncV3(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id for V3 API Version

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV4-System-String,System-Int32-'></a>
### GetStudyItemsAsyncV4(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id for V4 API Version

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ChangeAuditRepository-GetStudyItemsAsyncV5-System-String,System-Int32-'></a>
### GetStudyItemsAsyncV5(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id for V5 API Version

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

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

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetAuditTrailsAsync-System-String,System-Int32-'></a>
### GetAuditTrailsAsync(studyId,sdruploadversion) `method`

##### Summary

Get a list of study for a given study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of Study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### SearchStudy(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### SearchStudyV3(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV4-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### SearchStudyV4(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchStudyV5-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### SearchStudyV5(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-CommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### SearchTitle(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') | Parameters to search in database |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon'></a>
## DataFilterCommon `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### GetCommonFiltersForSearchTitle(searchParameters) `method`

##### Summary

Get filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitleV4-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### GetCommonFiltersForSearchTitleV4(searchParameters) `method`

##### Summary

Get filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetCommonFiltersForSearchTitleV5-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### GetCommonFiltersForSearchTitleV5(searchParameters) `method`

##### Summary

Get filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForChangeAudit-System-String-'></a>
### GetFiltersForChangeAudit(studyId) `method`

##### Summary

Get filters for ChangeAudit

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

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

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchTitleV5-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### GetFiltersForSearchTitleV5(searchParameters) `method`

##### Summary

Get V5 filters for Search Study Title API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV3-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### GetFiltersForSearchV3(searchParameters) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV4-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### GetFiltersForSearchV4(searchParameters) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForSearchV5-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### GetFiltersForSearchV5(searchParameters) `method`

##### Summary

Search Filters

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
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

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistoryV4-System-DateTime,System-DateTime,System-String-'></a>
### GetFiltersForStudyHistoryV4(fromDate,toDate,studyTitle) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFilterCommon-GetFiltersForStudyHistoryV5-System-DateTime,System-DateTime,System-String-'></a>
### GetFiltersForStudyHistoryV5(fromDate,toDate,studyTitle) `method`

##### Summary

Get filters for StudyHistory API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

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

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4'></a>
## DataFiltersV4 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

##### Summary

DataFilters for getting data from data base

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32-'></a>
### GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for AuditTrail

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetProjectionForPartialStudyDesignElementsFullStudy'></a>
### GetProjectionForPartialStudyDesignElementsFullStudy() `method`

##### Summary

Get Study Design Projection Definition

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV4-GetProjectionForPartialStudyElements-System-String[]-'></a>
### GetProjectionForPartialStudyElements(listofelementsArray) `method`

##### Summary

Get projectio definition for partial study elements

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | list of study elements |

<a name='T-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5'></a>
## DataFiltersV5 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Filters

##### Summary

DataFilters for getting data from data base

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetFiltersForGetAuditTrailOfAStudy-System-String,System-Int32-'></a>
### GetFiltersForGetAuditTrailOfAStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for AuditTrail

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetFiltersForGetStudy-System-String,System-Int32-'></a>
### GetFiltersForGetStudy(studyId,sdruploadversion) `method`

##### Summary

Get filters for GET StudyDefinitons API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetProjectionForPartialStudyDesignElementsFullStudy'></a>
### GetProjectionForPartialStudyDesignElementsFullStudy() `method`

##### Summary

Get Study Design Projection Definition

##### Returns



##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-DataAccess-Filters-DataFiltersV5-GetProjectionForPartialStudyElements-System-String[]-'></a>
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

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetAuditTrailsAsync-System-String,System-Int32-'></a>
### GetAuditTrailsAsync(studyId,sdruploadversion) `method`

##### Summary

Get a list of study for a given study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of Study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity-'></a>
### SearchStudy(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchParametersEntity') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-ICommonRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity-'></a>
### SearchTitle(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity](#T-TransCelerate-SDR-Core-Entities-Common-SearchTitleParametersEntity 'TransCelerate.SDR.Core.Entities.Common.SearchTitleParametersEntity') | Parameters to search in database |

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

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4'></a>
## IStudyRepositoryV4 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all version of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV4.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV4-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5'></a>
## IStudyRepositoryV5 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all version of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV5.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IStudyRepositoryV5-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Update study in database |

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

<a name='T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4'></a>
## StudyRepositoryV4 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all versions of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV4.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV4-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV4-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5'></a>
## StudyRepositoryV5 `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-CountAsync-System-String-'></a>
### CountAsync(study_uuid) `method`

##### Summary

Count Documents

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-DeleteStudyAsync-System-String-'></a>
### DeleteStudyAsync(study_uuid) `method`

##### Summary

Delete all versions of a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetPartialStudyDesignItemsAsync-System-String,System-Int32-'></a>
### GetPartialStudyDesignItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET Study Designs for a Study Id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetPartialStudyItemsAsync-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyItemsAsync(studyId,sdruploadversion,listofelementsArray) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelementsArray | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Array of study elements |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-GetUsdmVersionAsync-System-String,System-Int32-'></a>
### GetUsdmVersionAsync(studyId,sdruploadversion) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.StudyV5.AuditTrailEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-StudyRepositoryV5-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV5-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity') | Update study in database |
