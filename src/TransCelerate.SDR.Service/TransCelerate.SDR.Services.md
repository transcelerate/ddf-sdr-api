<a name='assembly'></a>
# TransCelerate.SDR.Services

## Contents

- [CommonServices](#T-TransCelerate-SDR-Services-Services-CommonServices 'TransCelerate.SDR.Services.Services.CommonServices')
  - [CheckAccessForListOfStudies\`\`1(studies,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-CheckAccessForListOfStudies``1-System-Collections-Generic-List{``0},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.CheckAccessForListOfStudies``1(System.Collections.Generic.List{``0},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckAccessForStudyAudit(studyId,studies,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Common-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.CheckAccessForStudyAudit(System.String,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Common.AuditTrailResponseEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetLinks(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetLinks-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.GetLinks(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetRawJson(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetRawJson-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.GetRawJson(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.SearchStudy(TransCelerate.SDR.Core.DTO.Common.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Services-CommonServices-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.CommonServices.SearchTitle(TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IChangeAuditService](#T-TransCelerate-SDR-Services-Interfaces-IChangeAuditService 'TransCelerate.SDR.Services.Interfaces.IChangeAuditService')
  - [GetChangeAudit(studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IChangeAuditService-GetChangeAudit-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IChangeAuditService.GetChangeAudit(System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [ICommonService](#T-TransCelerate-SDR-Services-Interfaces-ICommonService 'TransCelerate.SDR.Services.Interfaces.ICommonService')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetLinks(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetLinks-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetLinks(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetRawJson(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetRawJson-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetRawJson(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.SearchStudy(TransCelerate.SDR.Core.DTO.Common.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.ICommonService.SearchTitle(TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IStudyServiceV1](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAccessForAStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.GetAccessForAStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.GetStudyDesigns(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParameters,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV1.SearchTitle(TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [IStudyServiceV2](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [DeleteStudy(studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.DeleteStudy(System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAccessForAStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetAccessForAStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetPartialStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetPartialStudyElements(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetSOAV2(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetSOAV2-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetSOAV2(System.String,System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GetStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GeteCPTV2(studyId,sdruploadversion,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GeteCPTV2-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.GeteCPTV2(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user,method)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV2.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String)')
- [IStudyServiceV3](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [DeleteStudy(studyId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.DeleteStudy(System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAccessForAStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetAccessForAStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetDifferences(System.String,System.Int32,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetPartialStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetPartialStudyElements(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetSOAV3(System.String,System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GeteCPTV3(studyId,sdruploadversion,studyDesignId,user)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GeteCPTV3(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user,method)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String)')
- [IUserGroupMappingService](#T-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService')
  - [CheckGroupName()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-CheckGroupName-System-String- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.CheckGroupName(System.String)')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostUserToGroups(userToGroupsDTO,user)](#M-TransCelerate-SDR-Services-Interfaces-IUserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Interfaces.IUserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [StudyServiceV1](#T-TransCelerate-SDR-Services-Services-StudyServiceV1 'TransCelerate.SDR.Services.Services.StudyServiceV1')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckAccessForStudyAudit(studyId,studies,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.CheckAccessForStudyAudit(System.String,System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.StudyV1.AuditTrailResponseEntity},TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetAuditTrail(fromDate,toDate,studyId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.GetAuditTrail(System.String,System.DateTime,System.DateTime,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.GetStudyDesigns(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyHistory(fromDate,toDate,studyTitle,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.GetStudyHistory(System.DateTime,System.DateTime,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchStudy(searchParametersDto,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.SearchStudy(TransCelerate.SDR.Core.DTO.StudyV1.SearchParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [SearchTitle(searchParametersDTO,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV1.SearchTitle(TransCelerate.SDR.Core.DTO.StudyV1.SearchTitleParametersDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
- [StudyServiceV2](#T-TransCelerate-SDR-Services-Services-StudyServiceV2 'TransCelerate.SDR.Services.Services.StudyServiceV2')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [DeleteStudy(studyId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.DeleteStudy(System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GetPartialStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GetPartialStudyElements(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetSOAV2(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetSOAV2-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GetSOAV2(System.String,System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GetStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GeteCPTV2(studyId,sdruploadversion,studyDesignId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-GeteCPTV2-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV2.GeteCPTV2(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user,method)](#M-TransCelerate-SDR-Services-Services-StudyServiceV2-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV2.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String)')
- [StudyServiceV3](#T-TransCelerate-SDR-Services-Services-StudyServiceV3 'TransCelerate.SDR.Services.Services.StudyServiceV3')
  - [CheckAccessForAStudy(study,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.CheckAccessForAStudy(TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [CheckPermissionForAUser(user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.CheckPermissionForAUser(TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [DeleteStudy(studyId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.DeleteStudy(System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetDifferences(System.String,System.Int32,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetPartialStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetPartialStudyElements(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetSOAV3(System.String,System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudy(studyId,sdruploadversion,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetStudy(System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetStudyDesigns(System.String,System.String,System.Int32,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String[])')
  - [GeteCPTV3(studyId,sdruploadversion,studyDesignId,user)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GeteCPTV3(System.String,System.Int32,System.String,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostAllElements(studyDTO,user,method)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV3.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,TransCelerate.SDR.Core.DTO.Token.LoggedInUser,System.String)')
- [UserGroupMappingService](#T-TransCelerate-SDR-Services-Services-UserGroupMappingService 'TransCelerate.SDR.Services.Services.UserGroupMappingService')
  - [CheckGroupName()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-CheckGroupName-System-String- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.CheckGroupName(System.String)')
  - [GetUserGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUserGroups-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUserGroups(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [GetUsersList()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-GetUsersList-TransCelerate-SDR-Core-DTO-UserGroups-UserGroupsQueryParameters- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.GetUsersList(TransCelerate.SDR.Core.DTO.UserGroups.UserGroupsQueryParameters)')
  - [ListGroups()](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-ListGroups 'TransCelerate.SDR.Services.Services.UserGroupMappingService.ListGroups')
  - [PostGroup(groupDTO,user)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostGroup-TransCelerate-SDR-Core-DTO-UserGroups-SDRGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostGroup(TransCelerate.SDR.Core.DTO.UserGroups.SDRGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')
  - [PostUserToGroups(userToGroupsDTO,loggedInUser)](#M-TransCelerate-SDR-Services-Services-UserGroupMappingService-PostUserToGroups-TransCelerate-SDR-Core-DTO-UserGroups-PostUserToGroupsDTO,TransCelerate-SDR-Core-DTO-Token-LoggedInUser- 'TransCelerate.SDR.Services.Services.UserGroupMappingService.PostUserToGroups(TransCelerate.SDR.Core.DTO.UserGroups.PostUserToGroupsDTO,TransCelerate.SDR.Core.DTO.Token.LoggedInUser)')

<a name='T-TransCelerate-SDR-Services-Services-CommonServices'></a>
## CommonServices `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-CheckAccessForListOfStudies``1-System-Collections-Generic-List{``0},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForListOfStudies\`\`1(studies,user) `method`

##### Summary

Check access for the Study Aduit

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studies | [System.Collections.Generic.List{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{``0}') | Study List for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-Common-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForStudyAudit(studyId,studies,user) `method`

##### Summary

Check access for the Study Aduit

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | StudyId of the study |
| studies | [System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Common.AuditTrailResponseEntity}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{TransCelerate.SDR.Core.Entities.Common.AuditTrailResponseEntity}') | Study List for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetLinks-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetLinks(studyId,sdruploadversion,user) `method`

##### Summary

GET Links

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetRawJson-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetRawJson(studyId,sdruploadversion,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDto,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.Common.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Interfaces-IChangeAuditService'></a>
## IChangeAuditService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IChangeAuditService-GetChangeAudit-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetChangeAudit(studyId,user) `method`

##### Summary

Get Change Audit for a StudyId

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='T-TransCelerate-SDR-Services-Interfaces-ICommonService'></a>
## ICommonService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetLinks-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetLinks(studyId,sdruploadversion,user) `method`

##### Summary

GET Links

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetRawJson-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetRawJson(studyId,sdruploadversion,user) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchStudy(searchParametersDto,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.Common.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### SearchTitle(searchParametersDTO,user) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto') | Parameters to search in database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1'></a>
## IStudyServiceV1 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAccessForAStudy(studyId,sdruploadversion,user) `method`

##### Summary

Check Access for a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2'></a>
## IStudyServiceV2 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### DeleteStudy(studyId,user) `method`

##### Summary

Delete all versions of Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | LoggedIn User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAccessForAStudy(studyId,sdruploadversion,user) `method`

##### Summary

Check Access for a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | List of study design elements |
| studyDesignId | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | study design Id |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements with comma separated values |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetSOAV2-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSOAV2(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user) `method`

##### Summary

GET SoA

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | workdflowId |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | study design Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | List of study design elements |
| studyDesignId | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | study design Id |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-GeteCPTV2-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GeteCPTV2(studyId,sdruploadversion,studyDesignId,user) `method`

##### Summary

GET eCPT Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV2-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String-'></a>
### PostAllElements(studyDTO,user,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3'></a>
## IStudyServiceV3 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### DeleteStudy(studyId,user) `method`

##### Summary

Delete all versions of Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | LoggedIn User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetAccessForAStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetAccessForAStudy(studyId,sdruploadversion,user) `method`

##### Summary

Check Access for a study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') |  |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,user) `method`

##### Summary

GET Differences between two versions of a study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdrUploadVersionOne | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | First Version of study |
| sdrUploadVersionTwo | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Second Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | List of study design elements |
| studyDesignId | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | study design Id |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements with comma separated values |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user) `method`

##### Summary

GET SoA

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | workdflowId |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | study design Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| listofelements | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | List of study design elements |
| studyDesignId | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | study design Id |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GeteCPTV3(studyId,sdruploadversion,studyDesignId,user) `method`

##### Summary

GET eCPT Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String-'></a>
### PostAllElements(studyDTO,user,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

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

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV1'></a>
## StudyServiceV1 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckAccessForStudyAudit-System-String,System-Collections-Generic-List{TransCelerate-SDR-Core-Entities-StudyV1-AuditTrailResponseEntity},TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetAuditTrail-System-String,System-DateTime,System-DateTime,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudyDesigns-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-GetStudyHistory-System-DateTime,System-DateTime,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### PostAllElements(studyDTO,user) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV1-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV1.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-SearchStudy-TransCelerate-SDR-Core-DTO-StudyV1-SearchParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV1-SearchTitle-TransCelerate-SDR-Core-DTO-StudyV1-SearchTitleParametersDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV2'></a>
## StudyServiceV2 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV2-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### DeleteStudy(studyId,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Delete Object
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements,user) `method`

##### Summary

GET Study Designs Elements of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | StudyDesign Id |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of study design elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetSOAV2-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSOAV2(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user) `method`

##### Summary

GET SoA for a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Schedule Timeline Id |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | study design Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-GeteCPTV2-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GeteCPTV2(studyId,sdruploadversion,studyDesignId,user) `method`

##### Summary

GET eCPT Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV2-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String-'></a>
### PostAllElements(studyDTO,user,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV2-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV3'></a>
## StudyServiceV3 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-CheckAccessForAStudy-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckAccessForAStudy(study,user) `method`

##### Summary

Check access for the study

##### Returns

A [StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') if the user have access `null` If user doesn't have access to the study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| study | [TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity](#T-TransCelerate-SDR-Core-Entities-StudyV3-StudyDefinitionsEntity 'TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity') | Study for which user access have to be checked |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-CheckPermissionForAUser-TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### CheckPermissionForAUser(user) `method`

##### Summary

Check READ_WRITE Permission for a user

##### Returns

`true` If the user have READ_WRITE access in any of the groups `false` If the user does not have READ_WRITE access in any of the groups

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-DeleteStudy-System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### DeleteStudy(studyId,user) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Delete Object
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo,user) `method`

##### Summary

GET Differences between two versions of a study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdrUploadVersionOne | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | First Version of study |
| sdrUploadVersionTwo | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Second Version of study |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements,user) `method`

##### Summary

GET Study Designs Elements of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | StudyDesign Id |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of study design elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyElements-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId,user) `method`

##### Summary

GET SoA for a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Version of study |
| scheduleTimelineId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Schedule Timeline Id |
| studyDesignId | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | study design Id |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudy-System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String[]-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements,user) `method`

##### Summary

GET Study Designs of a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Design ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | List of elements |
| user | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Logged In User |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String,TransCelerate-SDR-Core-DTO-Token-LoggedInUser-'></a>
### GeteCPTV3(studyId,sdruploadversion,studyDesignId,user) `method`

##### Summary

GET eCPT Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| studyDesignId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | studyDesignId |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged in user |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,TransCelerate-SDR-Core-DTO-Token-LoggedInUser,System-String-'></a>
### PostAllElements(studyDTO,user,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| user | [TransCelerate.SDR.Core.DTO.Token.LoggedInUser](#T-TransCelerate-SDR-Core-DTO-Token-LoggedInUser 'TransCelerate.SDR.Core.DTO.Token.LoggedInUser') | Logged In User |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

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
