<a name='assembly'></a>
# TransCelerate.SDR.Services

## Contents

- [ClinicalStudyService](#T-TransCelerate-SDR-Services-Services-ClinicalStudyService 'TransCelerate.SDR.Services.Services.ClinicalStudyService')
  - [GetAllElements(studyId,version,tag)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllElements-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAllElements(System.String,System.Int32,System.String)')
  - [GetAllStudyId(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAllStudyId(System.DateTime,System.DateTime,System.String)')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetAuditTrail(System.DateTime,System.DateTime,System.String)')
  - [GetSections(studyId,version,tag,sections)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[]- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetSections(System.String,System.Int32,System.String,System.String[])')
  - [GetStudyDesignSections(studyId,version,tag,studyDesignId,sections)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[]- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.GetStudyDesignSections(System.String,System.String,System.Int32,System.String,System.String[])')
  - [PostAllElements(studyDTO,entrySystem)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.PostAllElements(TransCelerate.SDR.Core.DTO.PostStudyDTO,System.String)')
  - [SearchStudy(searchParametersDTO)](#M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO- 'TransCelerate.SDR.Services.Services.ClinicalStudyService.SearchStudy(TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO)')
- [IClinicalStudyService](#T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService')
  - [GetAllElements(studyId,version,tag)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllElements-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAllElements(System.String,System.Int32,System.String)')
  - [GetAllStudyId(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAllStudyId(System.DateTime,System.DateTime,System.String)')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetAuditTrail(System.DateTime,System.DateTime,System.String)')
  - [GetSections(studyId,version,tag,sections)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetSections(System.String,System.Int32,System.String,System.String[])')
  - [GetStudyDesignSections(studyId,version,tag,studyDesignId,sections)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.GetStudyDesignSections(System.String,System.String,System.Int32,System.String,System.String[])')
  - [PostAllElements(studyDTO,entrySystem)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.PostAllElements(TransCelerate.SDR.Core.DTO.PostStudyDTO,System.String)')
  - [SearchStudy(searchParametersDTO)](#M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO- 'TransCelerate.SDR.Services.Interfaces.IClinicalStudyService.SearchStudy(TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO)')
- [IUserGroupMappingService](#T-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO)')
  - [PostUserToGroups(userToGroupsDTO)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO)')
- [UserGroupMappingService](#T-TransCelerate-SDR-Services-Services-UserGroupMappingService 'TransCelerate.SDR.Services.Services.UserGroupMappingService')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Services.UserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO)')
  - [PostUserToGroups(userToGroupsDTO)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO)')

<a name='T-TransCelerate-SDR-Services-Services-ClinicalStudyService'></a>
## ClinicalStudyService `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllElements-System-String,System-Int32,System-String-'></a>
### GetAllElements(studyId,version,tag) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [GetStudyDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyDTO') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET Audit Trial

##### Returns

A [GetStudyAuditDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[]-'></a>
### GetSections(studyId,version,tag,sections) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[]-'></a>
### GetStudyDesignSections(studyId,version,tag,studyDesignId,sections) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String-'></a>
### PostAllElements(studyDTO,entrySystem) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for Inserting/Updating in Database |
| entrySystem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | System which made the request |

<a name='M-TransCelerate-SDR-Services-Services-ClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO-'></a>
### SearchStudy(searchParametersDTO) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO') | Parameters to search in database |

<a name='T-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService'></a>
## IClinicalStudyService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllElements-System-String,System-Int32,System-String-'></a>
### GetAllElements(studyId,version,tag) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [GetStudyDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyDTO') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| version | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Tag of a study |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAllStudyId-System-DateTime,System-DateTime,System-String-'></a>
### GetAllStudyId(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetAuditTrail-System-DateTime,System-DateTime,System-String-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

##### Summary

GET Audit Trial

##### Returns

A [GetStudyAuditDTO](#T-TransCelerate-SDR-Core-DTO-Study-GetStudyAuditDTO 'TransCelerate.SDR.Core.DTO.Study.GetStudyAuditDTO') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Start Date for Date Filter |
| toDate | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | End Date for Date Filter |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetSections-System-String,System-Int32,System-String,System-String[]-'></a>
### GetSections(studyId,version,tag,sections) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-GetStudyDesignSections-System-String,System-String,System-Int32,System-String,System-String[]-'></a>
### GetStudyDesignSections(studyId,version,tag,studyDesignId,sections) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-PostAllElements-TransCelerate-SDR-Core-DTO-PostStudyDTO,System-String-'></a>
### PostAllElements(studyDTO,entrySystem) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.PostStudyDTO](#T-TransCelerate-SDR-Core-DTO-PostStudyDTO 'TransCelerate.SDR.Core.DTO.PostStudyDTO') | Study for Inserting/Updating in Database |
| entrySystem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | System which made the request |

<a name='M-TransCelerate-SDR-Services-Interfaces-IClinicalStudyService-SearchStudy-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO-'></a>
### SearchStudy(searchParametersDTO) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO](#T-TransCelerate-SDR-Core-DTO-Study-SearchParametersDTO 'TransCelerate.SDR.Core.DTO.Study.SearchParametersDTO') | Parameters to search in database |

<a name='T-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService'></a>
## IUserGroupMappingService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

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

GET All Users for a group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with List of groupId and groupName `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO-'></a>
### PostGroup(groupDTO) `method`

##### Summary

Add/Modify A Group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Group that was added/modified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDTO | [TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO') | Group that needs to be added/modified |

<a name='M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO-'></a>
### PostUserToGroups(userToGroupsDTO) `method`

##### Summary

Add/Update User Group Mapping

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has user group mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userToGroupsDTO | [TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO') | User Group Mapping |

<a name='T-TransCelerate-SDR-Services-Services-UserGroupMappingService'></a>
## UserGroupMappingService `type`

##### Namespace

TransCelerate.SDR.Services.Services

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

GET All Users for a group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with List of groupId and groupName `null` if there are no groups

##### Parameters

This method has no parameters.

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO-'></a>
### PostGroup(groupDTO) `method`

##### Summary

Add/Modify A Group

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Group that was added/modified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupDTO | [TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO') | Group that needs to be added/modified |

<a name='M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO-'></a>
### PostUserToGroups(userToGroupsDTO) `method`

##### Summary

Add/Update User Group Mapping

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has user group mapping

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userToGroupsDTO | [TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO](#T-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO 'TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO') | User Group Mapping |
