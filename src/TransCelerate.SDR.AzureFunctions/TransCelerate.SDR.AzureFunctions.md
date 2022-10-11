<a name='assembly'></a>
# TransCelerate.SDR.AzureFunctions

## Contents

- [ChangeAuditFunction](#T-TransCelerate-SDR-AzureFunctions-ChangeAuditFunction 'TransCelerate.SDR.AzureFunctions.ChangeAuditFunction')
  - [Run(myQueueItem)](#M-TransCelerate-SDR-AzureFunctions-ChangeAuditFunction-Run-System-String- 'TransCelerate.SDR.AzureFunctions.ChangeAuditFunction.Run(System.String)')
- [ChangeAuditReposotory](#T-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditReposotory')
  - [GetChangeAuditAsync(studyId)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-GetChangeAuditAsync-System-String- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditReposotory.GetChangeAuditAsync(System.String)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditReposotory.GetStudyItemsAsync(System.String,System.Int32)')
  - [InsertChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-InsertChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditReposotory.InsertChangeAudit(TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity)')
  - [UpdateChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.ChangeAuditReposotory.UpdateChangeAudit(TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity)')
- [IChangeAuditReposotory](#T-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditReposotory')
  - [GetChangeAuditAsync(studyId)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-GetChangeAuditAsync-System-String- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditReposotory.GetChangeAuditAsync(System.String)')
  - [GetStudyItemsAsync(studyId,sdruploadversion)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-GetStudyItemsAsync-System-String,System-Int32- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditReposotory.GetStudyItemsAsync(System.String,System.Int32)')
  - [InsertChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-InsertChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditReposotory.InsertChangeAudit(TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity)')
  - [UpdateChangeAudit(changeAudit)](#M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity- 'TransCelerate.SDR.AzureFunctions.DataAccess.IChangeAuditReposotory.UpdateChangeAudit(TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity)')
- [IMessageProcessor](#T-TransCelerate-SDR-AzureFunctions-IMessageProcessor 'TransCelerate.SDR.AzureFunctions.IMessageProcessor')
  - [ProcessMessage(message)](#M-TransCelerate-SDR-AzureFunctions-IMessageProcessor-ProcessMessage-System-String- 'TransCelerate.SDR.AzureFunctions.IMessageProcessor.ProcessMessage(System.String)')
- [MessageProcessor](#T-TransCelerate-SDR-AzureFunctions-MessageProcessor 'TransCelerate.SDR.AzureFunctions.MessageProcessor')
  - [AddChangeAuditInDatabase(changeAuditStudyEntity,serviceBusMessageEntity,changedValues,currentStudyVersion)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-AddChangeAuditInDatabase-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-ServiceBusMessageEntity,System-Collections-Generic-List{System-String},TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.AddChangeAuditInDatabase(TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity,TransCelerate.SDR.Core.Entities.StudyV1.ServiceBusMessageEntity,System.Collections.Generic.List{System.String},TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity)')
  - [FormatChangeAuditElements(elements)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-FormatChangeAuditElements-System-Collections-Generic-List{System-String}- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.FormatChangeAuditElements(System.Collections.Generic.List{System.String})')
  - [ProcessMessage(message)](#M-TransCelerate-SDR-AzureFunctions-MessageProcessor-ProcessMessage-System-String- 'TransCelerate.SDR.AzureFunctions.MessageProcessor.ProcessMessage(System.String)')
- [Startup](#T-TransCelerate-SDR-AzureFunctions-Startup 'TransCelerate.SDR.AzureFunctions.Startup')
  - [Configure(builder)](#M-TransCelerate-SDR-AzureFunctions-Startup-Configure-Microsoft-Azure-Functions-Extensions-DependencyInjection-IFunctionsHostBuilder- 'TransCelerate.SDR.AzureFunctions.Startup.Configure(Microsoft.Azure.Functions.Extensions.DependencyInjection.IFunctionsHostBuilder)')

<a name='T-TransCelerate-SDR-AzureFunctions-ChangeAuditFunction'></a>
## ChangeAuditFunction `type`

##### Namespace

TransCelerate.SDR.AzureFunctions

<a name='M-TransCelerate-SDR-AzureFunctions-ChangeAuditFunction-Run-System-String-'></a>
### Run(myQueueItem) `method`

##### Summary

Azure Service Bus Trigger for Change Audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| myQueueItem | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Queue Message |

<a name='T-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory'></a>
## ChangeAuditReposotory `type`

##### Namespace

TransCelerate.SDR.AzureFunctions.DataAccess

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-GetChangeAuditAsync-System-String-'></a>
### GetChangeAuditAsync(studyId) `method`

##### Summary

Get Audit Details for a Study Id from Change Audit Collections

##### Returns

A [ChangeAuditEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditEntity') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

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

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-InsertChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity-'></a>
### InsertChangeAudit(changeAudit) `method`

##### Summary

Insert a Change Audit for a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity') |  |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-ChangeAuditReposotory-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity-'></a>
### UpdateChangeAudit(changeAudit) `method`

##### Summary

Update existing change audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity') |  |

<a name='T-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory'></a>
## IChangeAuditReposotory `type`

##### Namespace

TransCelerate.SDR.AzureFunctions.DataAccess

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-GetChangeAuditAsync-System-String-'></a>
### GetChangeAuditAsync(studyId) `method`

##### Summary

Get Audit Details for a Study Id from Change Audit Collections

##### Returns

A [ChangeAuditEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditEntity') with matching studyId
`null` If no study is matching with studyId

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| studyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Study UUID |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-GetStudyItemsAsync-System-String,System-Int32-'></a>
### GetStudyItemsAsync(studyId,sdruploadversion) `method`

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

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-InsertChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity-'></a>
### InsertChangeAudit(changeAudit) `method`

##### Summary

Insert a Change Audit for a study

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity') |  |

<a name='M-TransCelerate-SDR-AzureFunctions-DataAccess-IChangeAuditReposotory-UpdateChangeAudit-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity-'></a>
### UpdateChangeAudit(changeAudit) `method`

##### Summary

Update existing change audit

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAudit | [TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity') |  |

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

<a name='M-TransCelerate-SDR-AzureFunctions-MessageProcessor-AddChangeAuditInDatabase-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity,TransCelerate-SDR-Core-Entities-StudyV1-ServiceBusMessageEntity,System-Collections-Generic-List{System-String},TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity-'></a>
### AddChangeAuditInDatabase(changeAuditStudyEntity,serviceBusMessageEntity,changedValues,currentStudyVersion) `method`

##### Summary

Add or update the changes in change audit collection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| changeAuditStudyEntity | [TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ChangeAuditStudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ChangeAuditStudyEntity') | Change Audit Entity from database if exist |
| serviceBusMessageEntity | [TransCelerate.SDR.Core.Entities.StudyV1.ServiceBusMessageEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-ServiceBusMessageEntity 'TransCelerate.SDR.Core.Entities.StudyV1.ServiceBusMessageEntity') | Service bus message after deserialization |
| changedValues | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') | Changed values list |
| currentStudyVersion | [TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity](#T-TransCelerate-SDR-Core-Entities-StudyV1-StudyEntity 'TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity') | Current study version |

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

<a name='T-TransCelerate-SDR-AzureFunctions-Startup'></a>
## Startup `type`

##### Namespace

TransCelerate.SDR.AzureFunctions

##### Summary

Startup for Azure Function App

<a name='M-TransCelerate-SDR-AzureFunctions-Startup-Configure-Microsoft-Azure-Functions-Extensions-DependencyInjection-IFunctionsHostBuilder-'></a>
### Configure(builder) `method`

##### Summary

Add depenedncies for Azure Function App

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| builder | [Microsoft.Azure.Functions.Extensions.DependencyInjection.IFunctionsHostBuilder](#T-Microsoft-Azure-Functions-Extensions-DependencyInjection-IFunctionsHostBuilder 'Microsoft.Azure.Functions.Extensions.DependencyInjection.IFunctionsHostBuilder') |  |