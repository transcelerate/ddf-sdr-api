<a name='assembly'></a>
# TransCelerate.SDR.Services

## Contents

- [CommonServices](#T-TransCelerate-SDR-Services-Services-CommonServices 'TransCelerate.SDR.Services.Services.CommonServices')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.Services.Services.CommonServices.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetLinks(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetLinks-System-String,System-Int32- 'TransCelerate.SDR.Services.Services.CommonServices.GetLinks(System.String,System.Int32)')
  - [GetRawJson(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetRawJson-System-String,System-Int32- 'TransCelerate.SDR.Services.Services.CommonServices.GetRawJson(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-Services-Services-CommonServices-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Services.CommonServices.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [SearchStudy(searchParametersDto)](#M-TransCelerate-SDR-Services-Services-CommonServices-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto- 'TransCelerate.SDR.Services.Services.CommonServices.SearchStudy(TransCelerate.SDR.Core.DTO.Common.SearchParametersDto)')
  - [SearchTitle(searchParametersDTO)](#M-TransCelerate-SDR-Services-Services-CommonServices-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto- 'TransCelerate.SDR.Services.Services.CommonServices.SearchTitle(TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto)')
- [IChangeAuditService](#T-TransCelerate-SDR-Services-Interfaces-IChangeAuditService 'TransCelerate.SDR.Services.Interfaces.IChangeAuditService')
  - [GetChangeAudit(studyId)](#M-TransCelerate-SDR-Services-Interfaces-IChangeAuditService-GetChangeAudit-System-String- 'TransCelerate.SDR.Services.Interfaces.IChangeAuditService.GetChangeAudit(System.String)')
- [ICommonService](#T-TransCelerate-SDR-Services-Interfaces-ICommonService 'TransCelerate.SDR.Services.Interfaces.ICommonService')
  - [GetAuditTrail(fromDate,toDate,studyId)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetAuditTrail-System-String,System-DateTime,System-DateTime- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetAuditTrail(System.String,System.DateTime,System.DateTime)')
  - [GetLinks(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetLinks-System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetLinks(System.String,System.Int32)')
  - [GetRawJson(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetRawJson-System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetRawJson(System.String,System.Int32)')
  - [GetStudyHistory(fromDate,toDate,studyTitle)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetStudyHistory-System-DateTime,System-DateTime,System-String- 'TransCelerate.SDR.Services.Interfaces.ICommonService.GetStudyHistory(System.DateTime,System.DateTime,System.String)')
  - [SearchStudy(searchParametersDto)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto- 'TransCelerate.SDR.Services.Interfaces.ICommonService.SearchStudy(TransCelerate.SDR.Core.DTO.Common.SearchParametersDto)')
  - [SearchTitle(searchParametersDTO)](#M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto- 'TransCelerate.SDR.Services.Interfaces.ICommonService.SearchTitle(TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto)')
- [IStudyServiceV3](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetSOAV3(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV3(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.GeteCPTV3(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV3.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,System.String)')
- [IStudyServiceV4](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV4(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetSOAV4-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetSOAV4(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV4(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GeteCPTV4-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.GeteCPTV4(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV4.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto,System.String)')
- [IStudyServiceV5](#T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV5(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetSOAV5-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetSOAV5(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV5(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GeteCPTV5-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.GeteCPTV5(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Interfaces.IStudyServiceV5.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto,System.String)')
- [StudyServiceV3](#T-TransCelerate-SDR-Services-Services-StudyServiceV3 'TransCelerate.SDR.Services.Services.StudyServiceV3')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV3.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetSOAV3(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV3(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV3.GeteCPTV3(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Services-StudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV3.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto,System.String)')
- [StudyServiceV4](#T-TransCelerate-SDR-Services-Services-StudyServiceV4 'TransCelerate.SDR.Services.Services.StudyServiceV4')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV4.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV4(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetSOAV4-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetSOAV4(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV4(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-GeteCPTV4-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV4.GeteCPTV4(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Services-StudyServiceV4-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV4.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto,System.String)')
- [StudyServiceV5](#T-TransCelerate-SDR-Services-Services-StudyServiceV5 'TransCelerate.SDR.Services.Services.StudyServiceV5')
  - [DeleteStudy(studyId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-DeleteStudy-System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV5.DeleteStudy(System.String)')
  - [GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetDifferences-System-String,System-Int32,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetDifferences(System.String,System.Int32,System.Int32)')
  - [GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetPartialStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GetPartialStudyElements(studyId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetPartialStudyElements-System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetPartialStudyElements(System.String,System.Int32,System.String[])')
  - [GetSOAV5(studyId,sdruploadversion,scheduleTimelineId,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetSOAV5-System-String,System-String,System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetSOAV5(System.String,System.String,System.String,System.Int32)')
  - [GetStudy(studyId,sdruploadversion)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetStudy-System-String,System-Int32- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetStudy(System.String,System.Int32)')
  - [GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GetStudyDesigns(System.String,System.String,System.Int32,System.String[])')
  - [GeteCPTV5(studyId,sdruploadversion,studyDesignId)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-GeteCPTV5-System-String,System-Int32,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV5.GeteCPTV5(System.String,System.Int32,System.String)')
  - [PostAllElements(studyDTO,method)](#M-TransCelerate-SDR-Services-Services-StudyServiceV5-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto,System-String- 'TransCelerate.SDR.Services.Services.StudyServiceV5.PostAllElements(TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto,System.String)')

<a name='T-TransCelerate-SDR-Services-Services-CommonServices'></a>
## CommonServices `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetLinks-System-String,System-Int32-'></a>
### GetLinks(studyId,sdruploadversion) `method`

##### Summary

GET Links

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetRawJson-System-String,System-Int32-'></a>
### GetRawJson(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto-'></a>
### SearchStudy(searchParametersDto) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.Common.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchParametersDto') | Parameters to search in database |

<a name='M-TransCelerate-SDR-Services-Services-CommonServices-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto-'></a>
### SearchTitle(searchParametersDTO) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto') | Parameters to search in database |

<a name='T-TransCelerate-SDR-Services-Interfaces-IChangeAuditService'></a>
## IChangeAuditService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IChangeAuditService-GetChangeAudit-System-String-'></a>
### GetChangeAudit(studyId) `method`

##### Summary

Get Change Audit for a StudyId

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-TransCelerate-SDR-Services-Interfaces-ICommonService'></a>
## ICommonService `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetAuditTrail-System-String,System-DateTime,System-DateTime-'></a>
### GetAuditTrail(fromDate,toDate,studyId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetLinks-System-String,System-Int32-'></a>
### GetLinks(studyId,sdruploadversion) `method`

##### Summary

GET Links

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetRawJson-System-String,System-Int32-'></a>
### GetRawJson(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-GetStudyHistory-System-DateTime,System-DateTime,System-String-'></a>
### GetStudyHistory(fromDate,toDate,studyTitle) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchStudy-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto-'></a>
### SearchStudy(searchParametersDto) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDto | [TransCelerate.SDR.Core.DTO.Common.SearchParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchParametersDto') | Parameters to search in database |

<a name='M-TransCelerate-SDR-Services-Interfaces-ICommonService-SearchTitle-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto-'></a>
### SearchTitle(searchParametersDTO) `method`

##### Summary

Search Study Elements with search criteria

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') which matches serach criteria `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| searchParametersDTO | [TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto](#T-TransCelerate-SDR-Core-DTO-Common-SearchTitleParametersDto 'TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto') | Parameters to search in database |

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3'></a>
## IStudyServiceV3 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

Delete all versions of Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements with comma separated values |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String-'></a>
### GeteCPTV3(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4'></a>
## IStudyServiceV4 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

Delete all versions of Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements with comma separated values |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetSOAV4-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV4(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-GeteCPTV4-System-String,System-Int32,System-String-'></a>
### GeteCPTV4(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV4-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5'></a>
## IStudyServiceV5 `type`

##### Namespace

TransCelerate.SDR.Services.Interfaces

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

Delete all versions of Study

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements with comma separated values |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetSOAV5-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV5(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,sdruploadversion,listofelements,studyDesignId) `method`

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
| studyDesignId | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | study design Id |

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-GeteCPTV5-System-String,System-Int32,System-String-'></a>
### GeteCPTV5(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Interfaces-IStudyServiceV5-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV3'></a>
## StudyServiceV3 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Delete Object
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of study design elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetSOAV3-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV3(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-GeteCPTV3-System-String,System-Int32,System-String-'></a>
### GeteCPTV3(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV3-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV3-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV3.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV4'></a>
## StudyServiceV4 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Delete Object
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of study design elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetSOAV4-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV4(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-GeteCPTV4-System-String,System-Int32,System-String-'></a>
### GeteCPTV4(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV4-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV4-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |

<a name='T-TransCelerate-SDR-Services-Services-StudyServiceV5'></a>
## StudyServiceV5 `type`

##### Namespace

TransCelerate.SDR.Services.Services

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-DeleteStudy-System-String-'></a>
### DeleteStudy(studyId) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') Delete Object
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetDifferences-System-String,System-Int32,System-Int32-'></a>
### GetDifferences(studyId,sdrUploadVersionOne,sdrUploadVersionTwo) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetPartialStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyDesigns(studyId,sdruploadversion,studyDesignId,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of study design elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetPartialStudyElements-System-String,System-Int32,System-String[]-'></a>
### GetPartialStudyElements(studyId,sdruploadversion,listofelements) `method`

##### Summary

GET Partial Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetSOAV5-System-String,System-String,System-String,System-Int32-'></a>
### GetSOAV5(studyId,sdruploadversion,scheduleTimelineId,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetStudy-System-String,System-Int32-'></a>
### GetStudy(studyId,sdruploadversion) `method`

##### Summary

GET All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') with matching studyId `null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study ID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Version of study |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GetStudyDesigns-System-String,System-String,System-Int32,System-String[]-'></a>
### GetStudyDesigns(studyId,studyDesignId,sdruploadversion,listofelements) `method`

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
| listofelements | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | List of elements |

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-GeteCPTV5-System-String,System-Int32,System-String-'></a>
### GeteCPTV5(studyId,sdruploadversion,studyDesignId) `method`

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

<a name='M-TransCelerate-SDR-Services-Services-StudyServiceV5-PostAllElements-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto,System-String-'></a>
### PostAllElements(studyDTO,method) `method`

##### Summary

POST All Elements For a Study

##### Returns

A [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') which has study ID and study design ID's `null` If the insert is not done

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyDTO | [TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto](#T-TransCelerate-SDR-Core-DTO-StudyV5-StudyDefinitionsDto 'TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto') | Study for Inserting/Updating in Database |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | POST/PUT |
