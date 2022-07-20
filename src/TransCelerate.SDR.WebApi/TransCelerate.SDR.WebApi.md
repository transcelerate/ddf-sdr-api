<a name='assembly'></a>
# TransCelerate.SDR.WebApi

## Contents

- [AutoMapperProfies](#T-TransCelerate-SDR-WebApi-Mappers-AutoMapperProfies 'TransCelerate.SDR.WebApi.Mappers.AutoMapperProfies')
- [ClinicalStudyController](#T-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController')
  - [GetAllStudyId(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetAllStudyId-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.GetAllStudyId(System.DateTime,System.DateTime,System.String)')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetStudy(studyId,version,tag,sections)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetStudy-System-String,System-Int32,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.GetStudy(System.String,System.Int32,System.String,System.String)')
  - [GetStudyDesignSections(studyId,studyDesignId,version,tag,sections)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.GetStudyDesignSections(System.String,System.String,System.Int32,System.String,System.String)')
  - [PostAllElements(studyDTO,entrySystem)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.PostAllElements(TransCelerate.SDR.Core.DTO.PostStudyDTO,System.String)')
  - [SearchStudy(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.SearchStudy(TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO)')
  - [SearchTitle(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyController.SearchTitle(TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO)')
- [ClinicalStudyV1Controller](#T-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyV1Controller')
  - [GetStudy(studyId,version)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyV1Controller.GetStudy(System.String,System.Int32)')
  - [PostAllElements(studyDTO)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyV1Controller.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDto)')
  - [SearchStudy(searchparameters)](#M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto- 'TransCelerate.SDR.WebApi.Controllers.ClinicalStudyV1Controller.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto)')
- [ReportsController](#T-TransCelerate-SDR-WebApi-Controllers-ReportsController 'TransCelerate.SDR.WebApi.Controllers.ReportsController')
  - [GetUsageReport()](#M-TransCelerate-SDR-WebApi-Controllers-ReportsController-GetUsageReport-TransCelerate-SDR-Core-DTO-Reports-ReportBodyParameters- 'TransCelerate.SDR.WebApi.Controllers.ReportsController.GetUsageReport(TransCelerate.SDR.Core.DTO.Reports.ReportBodyParameters)')
- [Startup](#T-TransCelerate-SDR-WebApi-Startup 'TransCelerate.SDR.WebApi.Startup')
  - [Configure(app,env,logger)](#M-TransCelerate-SDR-WebApi-Startup-Configure-Microsoft-AspNetCore-Builder-IApplicationBuilder,Microsoft-AspNetCore-Hosting-IWebHostEnvironment,Microsoft-Extensions-Logging-ILogger{TransCelerate-SDR-WebApi-Startup}- 'TransCelerate.SDR.WebApi.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment,Microsoft.Extensions.Logging.ILogger{TransCelerate.SDR.WebApi.Startup})')
  - [ConfigureServices(services)](#M-TransCelerate-SDR-WebApi-Startup-ConfigureServices-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'TransCelerate.SDR.WebApi.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
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

<a name='T-TransCelerate-SDR-WebApi-Mappers-AutoMapperProfies'></a>
## AutoMapperProfies `type`

##### Namespace

TransCelerate.SDR.WebApi.Mappers

##### Summary

This class is for creating the mappers between DTOs and Entities

<a name='T-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController'></a>
## ClinicalStudyController `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetAllStudyId-System-DateTime,System-DateTime,System-String-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle) `method`

##### Summary

Get All StudyId's in the database

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET Audit Trail of a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetStudy-System-String,System-Int32,System-String,System-String-'></a>
### GetStudy(studyId,version,tag,sections) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| sections | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study sections which have to be fetched |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String-'></a>
### GetStudyDesignSections(studyId,studyDesignId,version,tag,sections) `method`

##### Summary

GET For a StudyDesign sections for a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design Id |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| sections | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design sections which have to be fetched |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String-'></a>
### PostAllElements(studyDTO,entrySystem) `method`

##### Summary

POST All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for Inserting/Updating in Database |
| entrySystem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | System which made the request |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO-'></a>
### SearchStudy(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO') | Parameters to search in database |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyController-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO-'></a>
### SearchTitle(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO') | Parameters to search in database |

<a name='T-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller'></a>
## ClinicalStudyV1Controller `type`

##### Namespace

TransCelerate.SDR.WebApi.Controllers

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,version) `method`

##### Summary

GET All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto-'></a>
### PostAllElements(studyDTO) `method`

##### Summary

POST All Elements For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDto') | Study for Inserting/Updating in Database |

<a name='M-TransCelerate-SDR-WebApi-Controllers-ClinicalStudyV1Controller-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto-'></a>
### SearchStudy(searchparameters) `method`

##### Summary

Search For a Study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchparameters | [TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto') | Parameters to search in database |

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
