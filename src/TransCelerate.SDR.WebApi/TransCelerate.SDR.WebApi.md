<a name='assembly'></a>
# TransCelerate.SDR.WebApi

## Contents

- [ChangeAuditController](#T-TransCelerate-SDR-WebApi-Controllers-ChangeAuditController 'TransCelerate.SDR.WebApi.Controllers.ChangeAuditController')
  - [GetChangeAudit(studyId)](#M-TransCelerate-SDR-WebApi-Controllers-ChangeAuditController-GetChangeAudit-System-String- 'TransCelerate.SDR.WebApi.Controllers.ChangeAuditController.GetChangeAudit(System.String)')
- [CommonController](#T-TransCelerate-SDR-WebApi-Controllers-CommonController 'TransCelerate.SDR.WebApi.Controllers.CommonController')
  - [GetApiUsdmMapping()](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetApiUsdmMapping 'TransCelerate.SDR.WebApi.Controllers.CommonController.GetApiUsdmMapping')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.WebApi.Controllers.CommonController.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetLinks(studyId,sdruploadversion)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetLinks-System-String,System-Int32- 'TransCelerate.SDR.WebApi.Controllers.CommonController.GetLinks(System.String,System.Int32)')
  - [GetRawJson(studyId,sdruploadversion)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetRawJson-System-String,System-Int32- 'TransCelerate.SDR.WebApi.Controllers.CommonController.GetRawJson(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.WebApi.Controllers.CommonController.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [SearchStudy(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto- 'TransCelerate.SDR.WebApi.Controllers.CommonController.SearchStudy(TransCelerate.SDR.Core.DTO.Common.SearchParametersDto)')
  - [SearchTitle(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-CommonController-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto- 'TransCelerate.SDR.WebApi.Controllers.CommonController.SearchTitle(TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto)')
- [ReportsController](#T-TransCelerate-SDR-WebApi-Controllers-ReportsController 'TransCelerate.SDR.WebApi.Controllers.ReportsController')
  - [GetUsageReport()](#M-TransCelerate-SDR-WebApi-Controllers-ReportsController-GetUsageReport-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters- 'TransCelerate.SDR.WebApi.Controllers.ReportsController.GetUsageReport(TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters)')
- [Startup](#T-TransCelerate-SDR-WebApi-Startup 'TransCelerate.SDR.WebApi.Startup')
  - [Configure(app,env,logger)](#M-TransCelerate-SDR-WebApi-Startup-Configure-Microsoft-AspNetCore-Builder-IApplicationBuilder,Microsoft-AspNetCore-Hosting-IWebHostEnvironment,Microsoft-Extensions-Logging-ILogger{TransCelerate-SDR-WebApi-Startup}- 'TransCelerate.SDR.WebApi.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment,Microsoft.Extensions.Logging.ILogger{TransCelerate.SDR.WebApi.Startup})')
  - [ConfigureServices(services)](#M-TransCelerate-SDR-WebApi-Startup-ConfigureServices-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.WebApi.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [StudyV1Controller](#T-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetStudy(studyId,sdruploadversion,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudy-System-String,System-Int32,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.GetStudy(System.String,System.Int32,System.String)')
  - [GetStudyDesigns(study_uuid,sdruploadversion,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudyDesigns-System-String,System-Int32,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.GetStudyDesigns(System.String,System.Int32,System.String)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [PostAllElements(studyDTO,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto,System.String)')
  - [SearchStudy(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto)')
  - [SearchTitle(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto- 'TransCelerate.SDR.WebApi.Controllers.StudyV1Controller.SearchTitle(TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto)')
- [StudyV2Controller](#T-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-DeleteStudy-System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.DeleteStudy(System.String)')
  - [GetSOAV2(studyId,studyDesignId,sdruploadversion,scheduleTimelineId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetSOAV2-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.GetSOAV2(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion,listofelements,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetStudy-System-String,System-Int32,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.GetStudy(System.String,System.Int32,System.String,System.String)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetStudyDesigns-System-String,System-Int32,System-String,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.GetStudyDesigns(System.String,System.Int32,System.String,System.String,System.String)')
  - [GeteCPTV2(studyId,sdruploadversion,studydesignId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GeteCPTV2-System-String,System-Int32,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.GeteCPTV2(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto,System.String)')
  - [PutStudy(studyDTO,usdmVersion,studyId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-PutStudy-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV2Controller.PutStudy(TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto,System.String,System.String)')
- [StudyV3Controller](#T-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-DeleteStudy-System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetDifferences-System-String,System-Int32,System-Int32,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.GetDifferences(System.String,System.Int32,System.Int32,System.String)')
  - [GetSOAV3(studyId,studyDesignId,sdruploadversion,scheduleTimelineId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetSOAV3-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.GetSOAV3(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion,listofelements,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetStudy-System-String,System-Int32,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.GetStudy(System.String,System.Int32,System.String,System.String)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetStudyDesigns-System-String,System-Int32,System-String,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.GetStudyDesigns(System.String,System.Int32,System.String,System.String,System.String)')
  - [GeteCPTV3(studyId,sdruploadversion,studydesignId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GeteCPTV3-System-String,System-Int32,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.GeteCPTV3(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,System.String)')
  - [PutStudy(studyDTO,usdmVersion,studyId)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-PutStudy-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.PutStudy(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,System.String,System.String)')
  - [ValidateUsdmConformance(studyDTO,usdmVersion)](#M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-ValidateUsdmConformance-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.WebApi.Controllers.StudyV3Controller.ValidateUsdmConformance(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,System.String)')
- [TokenController](#T-TransCelerate-SDR-WebApi-Controllers-TokenController 'TransCelerate.SDR.WebApi.Controllers.TokenController')
  - [GetToken(user)](#M-TransCelerate-SDR-WebApi-Controllers-TokenController-GetToken-TransCelerate-SDR-Core-DTO-Token-UserLogin- 'TransCelerate.SDR.WebApi.Controllers.TokenController.GetToken(TransCelerate.SDR.Core.DTO.Token.UserLogin)')
- [UserGroupsController](#T-TransCelerate-SDR-WebApi-Controllers-UserGroupsController 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController')
  - [CheckGroupName()](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-CheckGroupName-System-String- 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.CheckGroupName(System.String)')
  - [GetGroupList()](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetGroupList 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.GetGroupList')
  - [GetUserGroups()](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUserList()](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUserList 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.GetUserList')
  - [GetUsersList()](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [PostGroup(groupDTO)](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO- 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO)')
  - [PostUserToGroups(userToGroupsDTO)](#M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO- 'TransCelerate.SDR.WebApi.Controllers.UserGroupsController.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO)')

<a name='T-TransCelerate-SDR-WebApi-Controllers-ChangeAuditController'></a>
## ChangeAuditController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-ChangeAuditController-GetChangeAudit-System-String-'></a>
### GetChangeAudit(studyId) `method`

##### Summary

GET Change Audit For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='T-TransCelerate-SDR-WebApi-Controllers-CommonController'></a>
## CommonController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetApiUsdmMapping'></a>
### GetApiUsdmMapping() `method`

##### Summary

GET API -> USDM Version Mapping

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET Revision History of a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetLinks-System-String,System-Int32-'></a>
### GetLinks(studyId,sdruploadversion) `method`

##### Summary

GET Links

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetRawJson-System-String,System-Int32-'></a>
### GetRawJson(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

##### Summary

Get All StudyId's in the database

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto-'></a>
### SearchStudy(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.Common.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchParametersDto') | Parameters to search in database |

<a name='M-TransCelerate-SDR-WebApi-Controllers-CommonController-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto-'></a>
### SearchTitle(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto') | Parameters to search in database |

<a name='T-TransCelerate-SDR-WebApi-Controllers-ReportsController'></a>
## ReportsController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-ReportsController-GetUsageReport-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters-'></a>
### GetUsageReport() `method`

##### Summary

GET System Usage Report

##### Parameters

This method has no parameters.

<a name='T-TransCelerate-SDR-WebApi-Startup'></a>
## Startup `type`

##### Namespace

TransCelerate.SDR.WebApi

<a name='M-TransCelerate-SDR-WebApi-Startup-Configure-Microsoft-AspNetCore-Builder-IApplicationBuilder,Microsoft-AspNetCore-Hosting-IWebHostEnvironment,Microsoft-Extensions-Logging-ILogger{TransCelerate-SDR-WebApi-Startup}-'></a>
### Configure(app,env,logger) `method`

##### Summary

This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| app | [Microsoft.AspNetCore.Builder.IApplicationBuilder](#T-Microsoft-AspNetCore-Builder-IApplicationBuilder 'Microsoft.AspNetCore.Builder.IApplicationBuilder') |  |
| env | [Microsoft.AspNetCore.Hosting.IWebHostEnvironment](#T-Microsoft-AspNetCore-Hosting-IWebHostEnvironment 'Microsoft.AspNetCore.Hosting.IWebHostEnvironment') |  |
| logger | [Microsoft.Extensions.Logging.ILogger{TransCelerate.SDR.WebApi.Startup}](#T-Microsoft-Extensions-Logging-ILogger{TransCelerate-SDR-WebApi-Startup} 'Microsoft.Extensions.Logging.ILogger{TransCelerate.SDR.WebApi.Startup}') |  |

<a name='M-TransCelerate-SDR-WebApi-Startup-ConfigureServices-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### ConfigureServices(services) `method`

##### Summary

This method gets called by the runtime. Use this method to add services to the container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |

<a name='T-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller'></a>
## StudyV1Controller `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET Audit Trail of a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudy-System-String,System-Int32,System-String-'></a>
### GetStudy(studyId,sdruploadversion,usdmVersion) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | usdmVersion |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudyDesigns-System-String,System-Int32,System-String-'></a>
### GetStudyDesigns(study_uuid,sdruploadversion,usdmVersion) `method`

##### Summary

GET Study Designs of a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study_uuid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

##### Summary

Get All StudyId's in the database

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,usdmVersion) `method`

##### Summary

POST All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto-'></a>
### SearchStudy(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto') | Parameters to search in database |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV1Controller-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto-'></a>
### SearchTitle(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto') | Parameters to search in database |

<a name='T-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller'></a>
## StudyV2Controller `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

Delete a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetSOAV2-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV2(studyId,studyDesignId,sdruploadversion,scheduleTimelineId) `method`

##### Summary

GET SoA For a Study USDM Version 1.9

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Schedule Timeline Id |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetStudy-System-String,System-Int32,System-String,System-String-'></a>
### GetStudy(studyId,sdruploadversion,listofelements,usdmVersion) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | List of elements with comma separated values |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | usdm-vreison header |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GetStudyDesigns-System-String,System-Int32,System-String,System-String,System-String-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,usdmVersion) `method`

##### Summary

GET Study Designs of a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Study Design ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | List of study design elements with comma separated values |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-GeteCPTV2-System-String,System-Int32,System-String-'></a>
### GeteCPTV2(studyId,sdruploadversion,studydesignId) `method`

##### Summary

GET eCPT Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studydesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,usdmVersion) `method`

##### Summary

POST/PUT All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV2Controller-PutStudy-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,System-String,System-String-'></a>
### PutStudy(studyDTO,usdmVersion,studyId) `method`

##### Summary

POST/PUT All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='T-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller'></a>
## StudyV3Controller `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

Delete a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetDifferences-System-String,System-Int32,System-Int32,System-String-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,usdmVersion) `method`

##### Summary

GET Differences between two versions of a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdrUploadVersionOne | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | First Version of study |
| sdrUploadVersionTwo | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Second Version of study |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | usdm-vreison header |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetSOAV3-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV3(studyId,studyDesignId,sdruploadversion,scheduleTimelineId) `method`

##### Summary

GET SoA For a Study USDM Version 2.0

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Schedule Timeline Id |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetStudy-System-String,System-Int32,System-String,System-String-'></a>
### GetStudy(studyId,sdruploadversion,listofelements,usdmVersion) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | List of elements with comma separated values |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | usdm-vreison header |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GetStudyDesigns-System-String,System-Int32,System-String,System-String,System-String-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,usdmVersion) `method`

##### Summary

GET Study Designs of a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Study Design ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | List of study design elements with comma separated values |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-GeteCPTV3-System-String,System-Int32,System-String-'></a>
### GeteCPTV3(studyId,sdruploadversion,studydesignId) `method`

##### Summary

GET eCPT Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studydesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,usdmVersion) `method`

##### Summary

POST All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-PutStudy-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String,System-String-'></a>
### PutStudy(studyDTO,usdmVersion,studyId) `method`

##### Summary

PUT All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='M-TransCelerate-SDR-WebApi-Controllers-StudyV3Controller-ValidateUsdmConformance-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String-'></a>
### ValidateUsdmConformance(studyDTO,usdmVersion) `method`

##### Summary

Validate USDM Conformance rules for a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Validation |
| usdmVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | USDM Version |

<a name='T-TransCelerate-SDR-WebApi-Controllers-TokenController'></a>
## TokenController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-TokenController-GetToken-TransCelerate-SDR-Core-DTO-Token-UserLogin-'></a>
### GetToken(user) `method`

##### Summary

GET Token for accessing API's

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.UserLogin](#T-TransCelerate-SDR-Core-DTO-Token-UserLogin 'TransCelerate.SDR.Core.DTO.Token.UserLogin') | logging user details |

<a name='T-TransCelerate-SDR-WebApi-Controllers-UserGroupsController'></a>
## UserGroupsController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-CheckGroupName-System-String-'></a>
### CheckGroupName() `method`

##### Summary

Check Group name

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetGroupList'></a>
### GetGroupList() `method`

##### Summary

GET group list

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUserGroups() `method`

##### Summary

GET All Groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUserList'></a>
### GetUserList() `method`

##### Summary

GET user list from AD

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUsersList() `method`

##### Summary

GET All Users

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO-'></a>
### PostGroup(groupDTO) `method`

##### Summary

POST a group

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDTO | [TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO') | Group which needs to be added/modified |

<a name='M-TransCelerate-SDR-WebApi-Controllers-UserGroupsController-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO-'></a>
### PostUserToGroups(userToGroupsDTO) `method`

##### Summary

POST a user to groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userToGroupsDTO | [TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO') | User which needs to be added/modified to groups |
