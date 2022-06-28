<a name='assembly'></a>
# TransCelerate.SDR.DataAccess

## Contents

- [ClinicalStudyRepository](#T-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository')
  - [ApplyOrderBy(filteredResult,property,asc)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-ApplyOrderBy-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-SearchResponse},System-String,System-Boolean- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.ApplyOrderBy(System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.SearchResponse},System.String,System.Boolean)')
  - [Filter(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-Filter-TransCelerate-SDR-Core-Entities-Study-SearchParameters- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.Filter(TransCelerate.SDR.Core.Entities.Study.SearchParameters)')
  - [Filter(searchParameters)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-Filter-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.Filter(TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters)')
  - [GetAllStudyId(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.GetAllStudyId(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetAuditTrail-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.GetAuditTrail(System.DateTime,System.DateTime,System.String)')
  - [GetStudyItemsAsync(studyId,version)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetStudyItemsAsync(studyId,version,tag)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32,System-String- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.GetStudyItemsAsync(System.String,System.Int32,System.String)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Study-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Study.SearchParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.DataAccess.Repositories.ClinicalStudyRepository.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
- [IClinicalStudyRepository](#T-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository')
  - [GetAllStudyId(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.GetAllStudyId(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetAuditTrail-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.GetAuditTrail(System.DateTime,System.DateTime,System.String)')
  - [GetGroupsOfUser(user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetGroupsOfUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.GetGroupsOfUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyItemsAsync(studyId,version)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.GetStudyItemsAsync(System.String,System.Int32)')
  - [GetStudyItemsAsync(studyId,version,tag)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32,System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.GetStudyItemsAsync(System.String,System.Int32,System.String)')
  - [PostStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.PostStudyItemsAsync(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
  - [SearchStudy(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Study-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.SearchStudy(TransCelerate.SDR.Core.Entities.Study.SearchParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.SearchTitle(TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [UpdateStudyItemsAsync(study)](#M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IClinicalStudyRepository.UpdateStudyItemsAsync(TransCelerate.SDR.Core.Entities.Study.StudyEntity)')
- [IUserGroupMappingRepository](#T-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository')
  - [AddAGroup(group)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.AddAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [GetAGroupById()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAGroupById-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetAGroupById(System.String)')
  - [GetAllUserGroups()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetAllUserGroups 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetAllUserGroups')
  - [GetGroupByName()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupByName-System-String- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroupByName(System.String)')
  - [GetGroupList()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroupList 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroupList')
  - [GetGroups()](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.GetGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [UpdateAGroup(group)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.UpdateAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [UpdateUsersToGroups(userGroupMappingEntity)](#M-TransCelerate-SDR-DataAccess-Interfaces-IUserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity- 'TransCelerate.SDR.DataAccess.Interfaces.IUserGroupMappingRepository.UpdateUsersToGroups(TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity)')
- [UserGroupMappingRepository](#T-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository')
  - [AddAGroup(group)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-AddAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.AddAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [GetAGroupById()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAGroupById-System-String- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetAGroupById(System.String)')
  - [GetAllUserGroups()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetAllUserGroups 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetAllUserGroups')
  - [GetGroupByName()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupByName-System-String- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroupByName(System.String)')
  - [GetGroupList()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroupList 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroupList')
  - [GetGroups()](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-GetGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.GetGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [UpdateAGroup(group)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateAGroup-TransCelerate-SDR-Core-Entities-UserGroups-SDRGroupsEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.UpdateAGroup(TransCelerate.SDR.Core.Entities.UserGroups.SDRGroupsEntity)')
  - [UpdateUsersToGroups(userGroupMappingEntity)](#M-TransCelerate-SDR-DataAccess-Repositories-UserGroupMappingRepository-UpdateUsersToGroups-TransCelerate-SDR-Core-Entities-UserGroups-UserGroupMappingEntity- 'TransCelerate.SDR.DataAccess.Repositories.UserGroupMappingRepository.UpdateUsersToGroups(TransCelerate.SDR.Core.Entities.UserGroups.UserGroupMappingEntity)')

<a name='T-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository'></a>
## ClinicalStudyRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Repositories

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-ApplyOrderBy-System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Study-SearchResponse},System-String,System-Boolean-'></a>
### ApplyOrderBy(filteredResult,property,asc) `method`

##### Summary

Sorting the result set

##### Returns

A Sorted [IEnumerable\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable`1 'System.Collections.Generic.IEnumerable`1')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filteredResult | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.SearchResponse}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Study.SearchResponse}') | Filtered result from database |
| property | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Property by which the sorting must be done |
| asc | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Ascending/Descending |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-Filter-TransCelerate-SDR-Core-Entities-Study-SearchParameters-'></a>
### Filter(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [FilterDefinition\`1](#T-MongoDB-Driver-FilterDefinition`1 'MongoDB.Driver.FilterDefinition`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchParameters 'TransCelerate.SDR.Core.Entities.Study.SearchParameters') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-Filter-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters-'></a>
### Filter(searchParameters) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [FilterDefinition\`1](#T-MongoDB-Driver-FilterDefinition`1 'MongoDB.Driver.FilterDefinition`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters') | Parameters to search in database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle,user) `method`

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

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetAuditTrail-System-DateTime,System-DateTime,System-String-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,version) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32,System-String-'></a>
### GetStudyItemsAsync(studyId,version,tag) `method`

##### Summary

GET a Study for a study ID with version and tag filter

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Study-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchParameters 'TransCelerate.SDR.Core.Entities.Study.SearchParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Repositories-ClinicalStudyRepository-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Update study in database |

<a name='T-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository'></a>
## IClinicalStudyRepository `type`

##### Namespace

TransCelerate.SDR.DataAccess.Interfaces

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetAllStudyId-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle,user) `method`

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

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetAuditTrail-System-DateTime,System-DateTime,System-String-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET List of study for a study ID

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetGroupsOfUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetGroupsOfUser(user) `method`

##### Summary

GET groups assigned to user

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,version) `method`

##### Summary

GET a Study for a study ID with version filter

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-GetStudyItemsAsync-System-String,System-Int32,System-String-'></a>
### GetStudyItemsAsync(studyId,version,tag) `method`

##### Summary

GET a Study for a study ID with version and tag filter

##### Returns

A [StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-PostStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### PostStudyItemsAsync(study) `method`

##### Summary

POST a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Study for Inserting into Database |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-SearchStudy-TransCelerate-SDR-Core-Entities-Study-SearchParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchParameters 'TransCelerate.SDR.Core.Entities.Study.SearchParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-SearchTitle-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParameters,user) `method`

##### Summary

Search the collection based on search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParameters | [TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters](#T-TransCelerate-SDR-Core-Entities-Study-SearchTitleParameters 'TransCelerate.SDR.Core.Entities.Study.SearchTitleParameters') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-DataAccess-Interfaces-IClinicalStudyRepository-UpdateStudyItemsAsync-TransCelerate-SDR-Core-Entities-Study-StudyEntity-'></a>
### UpdateStudyItemsAsync(study) `method`

##### Summary

Updates a Study

##### Returns

A studyId which was inserted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.Study.StudyEntity](#T-TransCelerate-SDR-Core-Entities-Study-StudyEntity 'TransCelerate.SDR.Core.Entities.Study.StudyEntity') | Update study in database |

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
