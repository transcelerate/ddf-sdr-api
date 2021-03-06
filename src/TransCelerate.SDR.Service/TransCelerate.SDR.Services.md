<a name='assembly'></a>
# TransCelerate.SDR.Services

## Contents

- [ClinicalStudyService](#T-TransCelerate-SDR-Services-Services-ClinicalStudyService 'TransCelerate.SDR.Services.Services.ClinicalStudyService')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.Study.StudyEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckAccessForAuditTrail(studyList,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckAccessForAuditTrail-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.CheckAccessForAuditTrail(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAllElements(studyId,version,tag,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllElements-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAllElements(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAllStudyId(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAllStudyId(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAuditTrail(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetSections(studyId,version,tag,sections,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetSections(System.String,System.Int32,System.String,System.String[],TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesignSections(studyId,version,tag,studyDesignId,sections,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetStudyDesignSections(System.String,System.String,System.Int32,System.String,System.String[],TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,entrySystem,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.PostAllElements(TransCelerate.SDR.Core.DTO.PostStudyDTO,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.SearchStudy(TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.SearchTitle(TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [ClinicalStudyServiceV1](#T-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckAccessForStudyAudit(studyId,studies,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.CheckAccessForStudyAudit(System.String,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailResponseEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.GetStudyDesigns(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.ClinicalStudyServiceV1.SearchTitle(TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IClinicalStudyService](#T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.Study.StudyEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAllElements(studyId,version,tag,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllElements-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAllElements(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAllStudyId(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAllStudyId(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAuditTrail(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetSections(studyId,version,tag,sections,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetSections(System.String,System.Int32,System.String,System.String[],TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesignSections(studyId,version,tag,studyDesignId,sections,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetStudyDesignSections(System.String,System.String,System.Int32,System.String,System.String[],TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,entrySystem,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.PostAllElements(TransCelerate.SDR.Core.DTO.PostStudyDTO,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.SearchStudy(TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.SearchTitle(TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IClinicalStudyServiceV1](#T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.GetStudyDesigns(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyServiceV1.SearchTitle(TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IUserGroupMappingService](#T-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService')
  - [CheckGroupName()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-CheckGroupName-System-String- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.CheckGroupName(System.String)')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostUserToGroups(userToGroupsDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [UserGroupMappingService](#T-TransCelerate-SDR-Services-Services-UserGroupMappingService 'TransCelerate.SDR.Services.Services.UserGroupMappingService')
  - [CheckGroupName()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-CheckGroupName-System-String- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.CheckGroupName(System.String)')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Services.UserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO,user)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostUserToGroups(userToGroupsDTO,loggedInUser)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')

<a name='T-TransCelerate-SDR-Services-Services-ClinicalStudyService'></a>
## ClinicalStudyService `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckAccessForAuditTrail-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-StudyEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAuditTrail(studyList,user) `method`

##### Summary

Check access for the Study Aduit

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyList | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.StudyEntity}') | Study List for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllElements-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllElements(studyId,version,tag,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get AllStudy Id's

##### Returns

A [GetStudyHistoryResponseDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyHistoryResponseDTO') which has list of study ID's `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAuditTrail(fromDate,toDate,studyId,user) `method`

##### Summary

GET Audit Trial

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSections(studyId,version,tag,sections,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') of study sections with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study sections which have to be fetched |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyDesignSections(studyId,version,tag,studyDesignId,sections,user) `method`

##### Summary

GET For a StudyDesign sections for a study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') of studyDesign sections with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| tag | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Tag of a study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design Id |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study Design sections which have to be fetched |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,entrySystem,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for Inserting/Updating in Database |
| entrySystem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | System which made the request |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1'></a>
## ClinicalStudyServiceV1 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForStudyAudit(studyId,studies,user) `method`

##### Summary

Check access for the Study Aduit

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | StudyId of the study |
| studies | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailResponseEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailResponseEntity}') | Study List for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAuditTrail(fromDate,toDate,studyId,user) `method`

##### Summary

GET Audit Trial

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudy(studyId,sdruploadversion,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyDesigns(studyId,sdruploadversion,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get AllStudy Id's

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which has list of study ID's `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDto,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService'></a>
## IClinicalStudyService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-Study-StudyEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllElements-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllElements(studyId,version,tag,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get AllStudy Id's

##### Returns

A [GetStudyHistoryResponseDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyHistoryResponseDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyHistoryResponseDTO') which has list of study ID's `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAuditTrail(fromDate,toDate,studyId,user) `method`

##### Summary

GET Audit Trial

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSections(studyId,version,tag,sections,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') of study sections with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study sections which have to be fetched |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[],TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyDesignSections(studyId,version,tag,studyDesignId,sections,user) `method`

##### Summary

GET For a StudyDesign sections for a study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') of studyDesign sections with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| tag | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Tag of a study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design Id |
| sections | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Study Design sections which have to be fetched |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,entrySystem,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for Inserting/Updating in Database |
| entrySystem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | System which made the request |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchTitle-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchTitleParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchTitleParametersDTO') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1'></a>
## IClinicalStudyServiceV1 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAuditTrail(fromDate,toDate,studyId,user) `method`

##### Summary

GET Audit Trial

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudy(studyId,sdruploadversion,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyDesigns(studyId,sdruploadversion,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle,user) `method`

##### Summary

Get AllStudy Id's

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which has list of study ID's `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyTitle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Title Filter |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDto,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService'></a>
## IUserGroupMappingService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-CheckGroupName-System-String-'></a>
### CheckGroupName() `method`

##### Summary

Check GroupName

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with groupId and isExists `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUserGroups() `method`

##### Summary

GET All Groups

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUsersList() `method`

##### Summary

GET All Users for a group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with List of users with groups assined `null` if there are no users in the group

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-ListGroups'></a>
### ListGroups() `method`

##### Summary

GET All groups

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of groupId and groupName `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostGroup(groupDTO,user) `method`

##### Summary

Add/Modify A Group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Group that was added/modified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDTO | [TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO') | Group that needs to be added/modified |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostUserToGroups(userToGroupsDTO,user) `method`

##### Summary

Add/Update User Group Mapping

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has user group mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userToGroupsDTO | [TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO') | User Group Mapping |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Services-UserGroupMappingService'></a>
## UserGroupMappingService `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-CheckGroupName-System-String-'></a>
### CheckGroupName() `method`

##### Summary

GET Group by groupName

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with List of groupId and groupName `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUserGroups() `method`

##### Summary

GET All Groups

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of Groups `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters-'></a>
### GetUsersList() `method`

##### Summary

GET All Users for a group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with List of users with groups assined `null` if there are no users in the group

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-ListGroups'></a>
### ListGroups() `method`

##### Summary

GET All groups

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with List of groupId and groupName `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostGroup(groupDTO,user) `method`

##### Summary

Add/Modify A Group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Group that was added/modified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDTO | [TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO') | Group that needs to be added/modified |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostUserToGroups(userToGroupsDTO,loggedInUser) `method`

##### Summary

Add/Update User Group Mapping

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has user group mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userToGroupsDTO | [TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO') | User Group Mapping |
| loggedInUser | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |
