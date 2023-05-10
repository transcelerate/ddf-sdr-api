<a name='assembly'></a>
# TransCelerate.SDR.AzureFunctions

## Contents

- [ChangeAuditRepository](#T-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository')
  - [GetAuditTrailsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetAuditTrailsAsync-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.GetAuditTrailsAsync(System.String,System.Int32)')
  - [GetChangeAuditAsync(studyId)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetChangeAuditAsync-System-String- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.GetChangeAuditAsync(System.String)')
  - [GetStudyItemsAsyncV2(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetStudyItemsAsyncV2-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.GetStudyItemsAsyncV2(System.String,System.Int32)')
  - [GetStudyItemsAsyncV3(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.GetStudyItemsAsyncV3(System.String,System.Int32)')
  - [InsertChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-InsertChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.InsertChangeAudit(TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity)')
  - [UpdateChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditRepository.UpdateChangeAudit(TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity)')
- [IChangeAuditRepository](#T-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository')
  - [GetAuditTrailsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetAuditTrailsAsync-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.GetAuditTrailsAsync(System.String,System.Int32)')
  - [GetChangeAuditAsync(studyId)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetChangeAuditAsync-System-String- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.GetChangeAuditAsync(System.String)')
  - [GetStudyItemsAsyncV2(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetStudyItemsAsyncV2-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.GetStudyItemsAsyncV2(System.String,System.Int32)')
  - [GetStudyItemsAsyncV3(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.GetStudyItemsAsyncV3(System.String,System.Int32)')
  - [InsertChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-InsertChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.InsertChangeAudit(TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity)')
  - [UpdateChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditRepository.UpdateChangeAudit(TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity)')
- [IMessageProcessor](#T-TransCelerate-SDR-AzureFunctions-IMessageProcessor 'TransCelerate.SDR.AzureFunctions.IMessageProcessor')
  - [ProcessMessage(message)](#M-TransCelerate-SDR-AzureFunctions-IMessageProcessor-ProcessMessage-System-String- 'TransCelerate.SDR.AzureFunctions.IMessageProcessor.ProcessMessage(System.String)')
- [MessageProcessor](#T-TransCelerate-SDR-AzureFunctions-MessageProcessor 'TransCelerate.SDR.AzureFunctions.MessageProcessor')
  - [AddChangeAuditInDatabase(changeAuditStudyEntity,serviceBusMessageEntity,changedValues,currentVersionAudiTrail)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-AddChangeAuditInDatabase-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity,TransCelerate-SDR-Core-Entities-Common-ServiceBusMessageEntity,System-Collections-Generic-List{System-String},TransCelerate-SDR-Core-Entities-Common-AuditTrailEntity- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.AddChangeAuditInDatabase(TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity,TransCelerate.SDR.Core.Entities.Common.ServiceBusMessageEntity,System.Collections.Generic.List{System.String},TransCelerate.SDR.Core.Entities.Common.AuditTrailEntity)')
  - [FormatChangeAuditElements(elements)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-FormatChangeAuditElements-System-Collections-Generic-List{System-String}- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.FormatChangeAuditElements(System.Collections.Generic.List{System.String})')
  - [ProcessMessage(message)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-ProcessMessage-System-String- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.ProcessMessage(System.String)')

<a name='T-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository'></a>
## ChangeAuditRepository `type`

##### Namespace

TransCelerate.SDR.AzureFunctions.DataAccess

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetAuditTrailsAsync-System-String,System-Int32-'></a>
### GetAuditTrailsAsync(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetChangeAuditAsync-System-String-'></a>
### GetChangeAuditAsync(studyId) `method`

##### Summary

Get Audit Details for a Study Id from Change Audit Collections

##### Returns

A [ChangeAuditEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditEntity') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetStudyItemsAsyncV2-System-String,System-Int32-'></a>
### GetStudyItemsAsyncV2(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id for V2 API Version

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32-'></a>
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

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-InsertChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity-'></a>
### InsertChangeAudit(changeAudit) `method`

##### Summary

Insert a Change Audit for a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity') |  |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditRepository-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity-'></a>
### UpdateChangeAudit(changeAudit) `method`

##### Summary

Update existing change audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity') |  |

<a name='T-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository'></a>
## IChangeAuditRepository `type`

##### Namespace

TransCelerate.SDR.AzureFunctions.DataAccess

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetAuditTrailsAsync-System-String,System-Int32-'></a>
### GetAuditTrailsAsync(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetChangeAuditAsync-System-String-'></a>
### GetChangeAuditAsync(studyId) `method`

##### Summary

Get Audit Details for a Study Id from Change Audit Collections

##### Returns

A [ChangeAuditEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditEntity') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetStudyItemsAsyncV2-System-String,System-Int32-'></a>
### GetStudyItemsAsyncV2(studyId,sdruploadversion) `method`

##### Summary

Get Current and previous version of study for study Id for V2 API Version

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |
| sdruploadversion | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | current version |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-GetStudyItemsAsyncV3-System-String,System-Int32-'></a>
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

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-InsertChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity-'></a>
### InsertChangeAudit(changeAudit) `method`

##### Summary

Insert a Change Audit for a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity') |  |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditRepository-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity-'></a>
### UpdateChangeAudit(changeAudit) `method`

##### Summary

Update existing change audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity') |  |

<a name='T-TransCelerate-SDR-AzureFunctions-IMessageProcessor'></a>
## IMessageProcessor `type`

##### Namespace

TransCelerate.SDR.AzureFunctions

<a name='M-TransCelerate-SDR-AzureFunctions-IMessageProcessor-ProcessMessage-System-String-'></a>
### ProcessMessage(message) `method`

##### Summary

Process the Message for Change Audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message from service bus |

<a name='T-TransCelerate-SDR-AzureFunctions-MessageProcessor'></a>
## MessageProcessor `type`

##### Namespace

TransCelerate.SDR.AzureFunctions

<a name='M-TransCelerate-SDR-AzureFunctions-MessageProcessor-AddChangeAuditInDatabase-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity,TransCelerate-SDR-Core-Entities-Common-ServiceBusMessageEntity,System-Collections-Generic-List{System-String},TransCelerate-SDR-Core-Entities-Common-AuditTrailEntity-'></a>
### AddChangeAuditInDatabase(changeAuditStudyEntity,serviceBusMessageEntity,changedValues,currentVersionAudiTrail) `method`

##### Summary

Add or update the changes in change audit collection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAuditStudyEntity | [TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-Common-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.Common.ChangeAuditStudyEntity') | Change Audit Entity from database if exist |
| serviceBusMessageEntity | [TransCelerate.SDR.Core.Entities.Common.ServiceBusMessageEntity](#T-TransCelerate-SDR-Core-Entities-Common-ServiceBusMessageEntity 'TransCelerate.SDR.Core.Entities.Common.ServiceBusMessageEntity') | Service bus message after deserialization |
| changedValues | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | Changed values list |
| currentVersionAudiTrail | [TransCelerate.SDR.Core.Entities.Common.AuditTrailEntity](#T-TransCelerate-SDR-Core-Entities-Common-AuditTrailEntity 'TransCelerate.SDR.Core.Entities.Common.AuditTrailEntity') | Current auditTrail version |

<a name='M-TransCelerate-SDR-AzureFunctions-MessageProcessor-FormatChangeAuditElements-System-Collections-Generic-List{System-String}-'></a>
### FormatChangeAuditElements(elements) `method`

##### Summary

Format the changes to store in database

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| elements | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | List of changes |

<a name='M-TransCelerate-SDR-AzureFunctions-MessageProcessor-ProcessMessage-System-String-'></a>
### ProcessMessage(message) `method`

##### Summary

Process the Message for Change Audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message from service bus |
