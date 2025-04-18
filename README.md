- [Introduction](#introduction)
  - [Requirements to Contribute and Propose Changes](#requirements-to-contribute-and-propose-changes)
- [Intended-Audience](#intended-audience)
- [Overview](#overview)
- [Setup and Code Access](#setup-and-code-access)
  - [Pre-requisites](#pre-requisites)
  - [How To Setup Code](#how-to-setup-code)
  - [How To Run](#how-to-run)
- [Other Information](#other-information)
  - [Base solution structure](#base-solution-structure)
  - [Sample Data](#sample-data)
  - [SDR-API](#sdr-api)
    - [List of Endpoints](#list-of-endpoints)
    - [API Versioning](#api-versioning)
  - [Nuget packages](#nuget-packages)
- [Support](#support)

# Introduction

Study Definition Repository (SDR) Reference Implementation is TransCelerate’s vision to catalyze industry-level transformation, enabling digital exchange of study definition information by collaborating with technology providers and standards bodies to create a sustainable open-source Study Definition Repository.

This is a .NET 6 Web API project that is designed to expose APIs which upstream/downstream systems can utilize to store and retrieve study definitions from SDR. The latest Release of SDR (Release V3.0) supports study definitions conformant with USDM V1.9, USDM 2.0 and USDM V3.0.

This [Process Flow Document](https://github.com/transcelerate/ddf-sdr-platform/blob/main/documents/sdr-release-v3.0/ddf-sdr-ri-process-flows-v4.0.pdf) provides information regarding user interface functions and system interactions with the SDR at a high level. Please also refer to the [DDF SDR API User Guide](documents/sdr-release-v3.0/ddf-sdr-ri-api-user-guide-v7.0.pdf) to get started, and the [DDF SDR RI API Demo video](https://www.youtube.com/playlist?list=PLMXS-Xt7Ou1KNUF-HQKQRRzqfPQEXWb1u). 

**NOTES:** 
- These materials and information are provided by TransCelerate Biopharma Inc. AS IS.  Any party using or relying on this information and these materials do so entirely at their own risk.  Neither TransCelerate nor its members will bear any responsibility or liability for any harm, including indirect or consequential harm, that a user may incur from use or misuse of this information or materials.
- Please be advised that if you implement the code as written, the functionality is designed to collect and store certain personal data (user credentials, email address, IP address) for authentication and audit log purposes.  None of this information will be shared with TransCelerate or Accenture for any purpose.  Neither TransCelerate nor Accenture bears any responsibility for any collection, use or misuse of personal data that occurs from any implementation of this source code.  If you or your organization employ any of the features that collect personal data, you are responsible for compliance with any relevant privacy laws or regulations in any applicable jurisdiction.  
- Please be aware that any information you put into the provided tools (including the UI or API) will be visible to all users, so we recommend not using commercially sensitive or confidential information.  You and/or your employer bear all responsibility for anything you share with this project.  TransCelerate, its member companies and any vendors affiliated with the DDF project are not responsible for any harm or loss you occur as a result of uploading any information or code: commercially sensitive, confidential or otherwise.
- To the extent that the SDR Reference Implementation incorporates or relies on any specific branded products or services, such as Azure, this resulted out of the practical necessities associated with making a reference implementation available to demonstrate the SDR’s capabilities.  Users are free to download the source code for the SDR from GitHub and design their own implementations.  Those implementations can be in an environment of the user’s choice, and do not have to be on Azure. 
- As of May 2024, the DDF initiative is still the process of setting up operations, and any pull requests submitted will not be triaged at this point in time.

## Requirements to Contribute and Propose Changes
Before participating, you must acknowledge the Contribution License Agreement (CLA).

To acknowledge the CLA, follow these instructions:

- Click [here](https://github.com/transcelerate/ddf-home/blob/main/documents/DDF_CLA_2022MAR28_FINAL.pdf) to download and carefully read the CLA.
- Print the document.
- Complete and sign the document.
- Scan and email a PDF version of the completed and signed document to [DDF@transceleratebiopharmainc.com](mailto:DDF@transceleratebiopharmainc.com?subject=Signed%20CLA).

NOTE: Keep a copy for your records.

# Intended Audience
The contents in this repository allows users to develop SDR Reference Implementation API onto their Azure Cloud Subscription via their own GitHub Repos and Workflows. The deployment scripts (YAML Scripts) can be configured and executed from GitHub Actions, leveraging GitHub Secrets to configure target environment specific values.

It assumes a good understanding of Azure concepts and services. The audience for this document should:
- have clear understanding of C# and .NET Web APIs
- have basic understanding of MongoDB and MongoDB C# driver
- be aware of how to use Azure portal and basic understanding of Azure Cloud Platform
- have basic understanding of GitHub Actions, Secrets & Yaml Scripts

# Overview
The SDR Reference Implementation  implements the CDISC DDF Reference Architecture which include USDM model and API Specifications defined using the OpenAPI Specification (OAS). The API Layer of the SDR Reference Implementation complies with the OpenAPI Specification which allow systems to discover and understand the capabilities of the service without access to source code, documentation, or through network traffic inspection. When properly defined, a consumer can understand and interact with the remote service with a minimal amount of implementation logic.

It follows the REST architectural style that uses HTTP requests to GET, POST and PUT data. RESTful architecture is not linked with any technology or platform, it does not dictate exactly how to build an API. Instead, it introduces the best practices known as constraints. They describe how the server processes requests and responds to them. Operating within these constraints, the system gains desirable properties such as reliability, ease of use, improved scalability and security, low latency while enhancing the system performance and helping achieve technology independence in the process.

# Setup and Code Access
## Pre-requisites

1. Install [Visual Studio 2022](https://visualstudio.microsoft.com/) with default options to run the solution.

2. Create a Mongo DB(or equivalent) and collections with names mentioned below.
```
StudyDefinitions
Groups
ChangeAudit
```
3. A Service Bus Queue must be created for Change Audit functionality. This is optional if user does not intend to capture changes between versions of a study. No action is needed in code setup to disable change audit. You can also skip creation of ChangeAudit collection in previous step.

## How to setup code

1. Clone the repo into a local directory using below git command.

```shell
git clone "repo_url"
```
2. Once repo is cloned, open the solution in Visual Studio 2022 IDE.

## How To Run

**API** 
1. For running the API code locally, take a copy of appsettings.json file and rename the copied file to appsettings.Development.json file in the root folder of TransCelerate.SDR.WebApi project.

2. Edit the appsettings.Development.json and add the values for below mentioned settings.

```
"ConnectionStrings": {
    //Database connection string here
    "ServerName": "mongodb+sre://SDRADMIN:KasdeafsfhttDxaqj@study.cph52.mongodb.net/db", 
    "DatabaseName": "Database Name here"
 },
"StudyHistory": {
	// This parameter will be used to restrict the historical data (last 30/60/90 days) in study history endpoint response, if no date filters are passed in request.
	// Keep this value as "-1" to disable this restriction.
    "DateRange": "30"
 },
 "isGroupFilterEnabled": true  // change value to false to disable user based data filtering,
 "isAuthEnabled": true  // change value to false to disable authorization
 "ApiVersionUsdmVersionMapping":"" // {"SDRVersions":[{"apiVersion":"v2","usdmVersions":["1.9"]},{"apiVersion":"v3","usdmVersions":["2.0"]},{"apiVersion":"v4","usdmVersions":["3.0"]}]}
```
> **Note**  
> **API to USDM Version mapping** - SDR supports 3 major USDM versions at a given point in time along with all their minor versions. API endpoints are up-versioned for breaking changes in USDM (API V2 -> USDM V1.9, API V3 -> USDM 2.0, API V4 -> USDM 3.0).

3. Then, In the Visual Studio IDE, on clicking the IIS Express Icon or on pressing F5, WebApi solution will start running locally.

4. The browser will automatically open the Swagger UI having the SDR API specifications.

**Azure Function App**
1. An Azure Function App is developed to capture Change Audit. An Azure ServiceBus queue message triggers this function app. To run the Azure Function App locally, creation of Azure ServiceBus queue is mandatory. 
2. A local.settings.json file must be created and must be copied to the root folder of TransCelerate.SDR.AzureFunctions project.
3. Edit the local.settings.json and add the values for below mentioned settings.

```

  "Values": {    
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    
    "AzureServiceBusConnectionString": "Azure Service Bus Connection String here",
    
    "AzureServiceBusQueueName": "Queue Name here",
    
    //Database connection string here
    "ConnectionStrings:ServerName": "mongodb+sre://SDRADMIN:KasdeafsfhttDxaqj@study.cph52.mongodb.net/db",
    
    "ConnectionStrings:DatabaseName": "Database Name here",
    
    "ApiVersionUsdmVersionMapping": "", //{"SDRVersions":[{"apiVersion":"v1","usdmVersions":["1.0"]},{"apiVersion":"v2","usdmVersions":["1.9"]},{"apiVersion":"v3","usdmVersions":["2.0"]}]}
```

3. Then, In the Visual Studio IDE, select TransCelerate.SDR.AzureFunctions project on the startup project and click Start.

4. The browser will automatically open a console which will start listen on the Azure Service Bus Queue.

# Other Information
## Base solution structure

The solution has the following structure:

```  
  ├── TransCelerate.SDR.sln
      ├── TransCelerate.SDR.AzureFunctions
      │   ├── Properties
      │   ├── DataAccess
      │   ├── MessageProcessor
      │   ├── ChangeAuditFunction.cs
      │   ├── host.json
      │   ├── Startup.cs
      │   └── TransCelerate.SDR.Core.md
      ├── TransCelerate.SDR.Core
      │   ├── AppSettings
      │   ├── DTO
      │   ├── Entities
      │   ├── ErrorModels
      │   ├── Filters
      │   ├── Utilities
      │   └── TransCelerate.SDR.Core.md
      ├── TransCelerate.SDR.DataAccess
      │   ├── Filters
      │   ├── Interfaces
      │   ├── Repositories
      │   └── TransCelerate.SDR.DataAccess.md
      ├── TransCelerate.SDR.RuleEngine
      │   ├── Common
      │   ├── StudyV2Rules
      │   ├── StudyV3Rules
      │   ├── StudyV4Rules
      │   ├── Token
      │   ├── UserGroupMappingRules
      │   ├── ValidationDependencies.cs
      │   ├── ValidationDependenciesCommon.cs
      │   ├── ValidationDependenciesV2.cs 
      │   ├── ValidationDependenciesV3.cs
      │   ├── ValidationDependenciesV4.cs
      │   └── TransCelerate.SDR.RuleEngine.md
      ├── TransCelerate.SDR.Service
      │   ├── Interfaces
      │   ├── Services
      │   └── TransCelerate.SDR.Service.md  
      ├── TransCelerate.SDR.UnitTesting
      │   ├── AzureFunctionsUnitTesting
      │   ├── CommonClassesUnitTesting
      │   ├── ControllerUnitTesting
      │   ├── Data
      │   ├── ServicesUnitTesting
      │   └── TransCelerate.SDR.UnitTesting.md
      └── TransCelerate.SDR.WebApi
          ├── Properties
	  ├── Data
          ├── DependencyInjection
          ├── Controllers
          ├── Mappers
          ├── appsettings.json
          ├── Program.cs
          ├── Startup.cs
          └── TransCelerate.SDR.WebApi.md

```
**[TransCelerate.SDR.AzureFunctions](src/TransCelerate.SDR.AzureFunctions/TransCelerate.SDR.AzureFunctions.md)** - contains Azure function app for change audit.

**[TransCelerate.SDR.Core](src/TransCelerate.SDR.Core/TransCelerate.SDR.Core.md)** - contains entities, DTOs and helper classes.

**[TransCelerate.SDR.DataAccess](src/TransCelerate.SDR.DataAccess/TransCelerate.SDR.DataAccess.md)** - contains code for communicating with MongoDB database.

**[TransCelerate.SDR.RuleEngine](src/TransCelerate.SDR.RuleEngine/TransCelerate.SDR.RuleEngine.md)** - contains code for model validations based on data conformance rules.

**[TransCelerate.SDR.Service](src/TransCelerate.SDR.Service/TransCelerate.SDR.Services.md)** - contains code for service layer which is a bridge between API controller and repository.

**[TransCelerate.SDR.UnitTesting](src/TransCelerate.SDR.UnitTesting/TransCelerate.SDR.UnitTesting.md)** - contains code for unit testing (NUnit Framework).

**[TransCelerate.SDR.WebApi](src/TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.md)** - contains API controllers, mappers and the startup for the application.

## Sample Data
For those looking to evaluate the USDM with a sample data set, please see the following files in the Data Model folder:
- [USDM V1.9 conformant Sample JSON](data-model/sdr-release-v2.0/ddf-sdr-api-study-sample-json-v1.9.json)
- [USDM V2.0 conformant Sample JSON](data-model/sdr-release-v2.0.2/ddf-sdr-api-study-sample-json-v2.0.json)
- [USDM V3.0 conformant Sample JSON](data-model/sdr-release-v3.0/ddf-sdr-api-study-sample-json-v3.0.json)

## SDR API
### List Of Endpoints

The below GET endpoint can be used to GET API Version -> USDM Version mapping.
```
/versions
```

#### V2 Endpoints (USDM Version 1.9)

For V2 endpoints, the "usdmVersion" header parameter is mandatory and the header value must be "1.9"

**POST Endpoint**
The below endpoint can be used to create new study definitions.
```
/v2/studydefinitions
```
**PUT Endpoint**
The below endpoint can be used to update existing study definitions (create new version for a study definition).
```
/v2/studydefinitions/{studyId}
```
**GET Endpoints**

The below endpoint can be used to fetch all the elements for a given StudyId.

```
/v2/studydefinitions/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a given StudyId.

```
/v2/studydesigns?studyId={studyId}
```
The below endpoint can be used to export study details mapped to a limited set of CPT Variables grouped by sections within the Common Protocol Template
```
/v2/studydefinitions/{studyId}/studydesigns/ecpt
```
The below endpoint can be used to fetch data from study definitions that help build the Schedule of Activities matrix for a given Schedule Timeline in a Study Design
```
/v2/studydefinitions/{studyId}/studydesigns/soa
```
#### V3 Endpoints (USDM Version 2.0)

For V3 endpoints, the "usdmVersion" header parameter is mandatory and the header value must be "2.0"

**POST Endpoint**
The below endpoint can be used to create new study definitions.
```
/v3/studydefinitions
```
The below endpoint can be used to validate the USDM conformance rules for a study definition
```
/v3/studydefinitions/validate-usdm-conformance
```
**PUT Endpoint**
The below endpoint can be used to update existing study definitions (create new version for a study definition).
```
/v3/studydefinitions/{studyId}
```
**GET Endpoints**

The below endpoint can be used to fetch all the elements for a given StudyId.

```
/v3/studydefinitions/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a given StudyId.

```
/v3/studydesigns?studyId={studyId}
```
The below endpoint can be used to get the changes between two SDR Upload Versions of a specific study definition
```
/v3/studydefinitions/{studyId}/version-comparison?sdruploadversionone={sdruploadversionone}&sdruploadversiontwo={sdruploadversiontwo}
```
The below endpoint can be used to export study details mapped to a limited set of CPT Variables grouped by sections within the Common Protocol Template
```
/v3/studydefinitions/{studyId}/studydesigns/ecpt
```
The below endpoint can be used to fetch data from study definitions that help build the Schedule of Activities matrix for a given Schedule Timeline in a Study Design
```
/v3/studydefinitions/{studyId}/studydesigns/soa
```
#### V4 Endpoints (USDM Version 3.0)

For V4 endpoints, the "usdmVersion" header parameter is mandatory and the header value must be "3.0"

**POST Endpoint**
The below endpoint can be used to create new study definitions.
```
/v4/studydefinitions
```
The below endpoint can be used to validate the USDM conformance rules for a study definition
```
/v4/studydefinitions/validate-usdm-conformance
```
**PUT Endpoint**
The below endpoint can be used to update existing study definitions (create new version for a study definition).
```
/v4/studydefinitions/{studyId}
```
**GET Endpoints**

The below endpoint can be used to fetch all the elements for a given StudyId.

```
/v4/studydefinitions/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a given StudyId.

```
/v4/studydesigns?studyId={studyId}
```
The below endpoint can be used to get the changes between two SDR Upload Versions of a specific study definition
```
/v4/studydefinitions/{studyId}/version-comparison?sdruploadversionone={sdruploadversionone}&sdruploadversiontwo={sdruploadversiontwo}
```
The below endpoint can be used to export study details mapped to a limited set of CPT Variables grouped by sections within the Common Protocol Template
```
/v4/studydefinitions/{studyId}/studydesigns/ecpt
```
The below endpoint can be used to fetch data from study definitions that help build the Schedule of Activities matrix for a given Schedule Timeline in a Study Design
```
/v4/studydefinitions/{studyId}/studydesigns/soa
```
#### Version Neutral Endpoints

The below endpoints can be used to fetch the revision history for a given StudyId.

```
/studydefinitions/{studyId}/revisionhistory
```

The below endpoint can be used to fetch basic details of all study definitions in SDR.

```
/studydefinitions/studyhistory
```
The below endpoint can be used to fetch study definitons in raw JSON string format

```
/studydefinitions/{studyId}/rawdata
```
The below endpoint can be used to fetch the change audit details of a study definiton
```
/studydefinitions/{studyId}/changeaudit
```

#### API Spec
To view the API specifications and to run the endpoints locally, the below swagger url can be used.

```
https://localhost:44358/swagger/index.html
```
**Note**: Refer **[DDF SDR API User Guide](documents/sdr-release-v3.0/ddf-sdr-ri-api-user-guide-v7.0.pdf)** for detailed information on all the endpoints.

### API Versioning
SDR APIs are defined in such a way that an API version can handle more than one USDM Version. If there are no breaking changes between the USDM Versions, with same API version, more than one USDM Versions can be handled. But, when there is a breaking change in a new USDM Version, a new API version must be created to support the new USDM Version. Below are the list of changes that are required when creating a new API version.
- Configuration for **ApiVersionUsdmVersionMapping** and **ConformanceRules** must be updated to support new API version.
- Create new version for the below listed components
 ```
 TransCelerate.SDR.Core.DTO
 TransCelerate.SDR.Core.Entities
 TransCelerate.SDR.Core.Utilities.Helpers
 TransCelerate.SDR.RuleEngine
 TransCelerate.SDR.DataAccess
 TransCelerate.SDR.Services
 TransCelerate.SDR.WebApi.Controllers
 TransCelerate.SDR.WebApi.Mappers
 ```
- For version neutral endpoint search endpoint, data filters need to be added in below components to support new API version
```
 TransCelerate.SDR.DataAccess
 TransCelerate.SDR.Services
```

## Nuget Packages 

1. **Automapper.Extensions.Microsoft.DependencyInjection** - Used for mapping two different classes.

2. **Microsoft.ApplicationInsights.AspNetCore** - Used for Logging in Azure Application Insights.

3. **Newtonsoft.Json** - Used for Serialization/De-Serialization of JSON Data.
 
4. **Azure.Security.KeyVaults.Secrets** - Used for accessing Azure KeyVault.
 
5. **Azure.Identity** - Used for accessing Azure KeyVault.
 
6. **MongoDB.Driver** - Used for communicating with Mongo DB.
 
7. **NUnit** - Used for Unit Testing.

8. **Moq** - Used for mocking in unit testing.

9. **Autofac.Extras.Moq** - Used for mocking in unit testing.
 
10. **FluentValidation.AspNetCore** - Used for implementing the RuleEngine.

11. **Swashbuckle.AspNetCore** - Used for enabling Swagger for the API's.

12. **Swashbuckle.AspNetCore.Annotations** - Used for adding comments, sample request and response in Swagger.

13. **Microsoft.Extensions.Logging** - Used for logging.

14. **Microsoft.Extensions.Configuration.Abstractions** - Used for Key-Value abstractions.

15. **Microsoft.AspNetCore.Mvc.Core** - Contains common action result types, attribute routing, application model conventions, API explorer, application parts, filters, formatters, model binding, and more.

16. **Microsoft.AspNetCore.Authorization** - Used for API Authorization

17. **Vsxmd** - Used for Converting xml comments into markdown file

18. **ObjectsComparer** - Used for comparing two objects of same type and return the differences

19. **Azure.Messaging.ServiceBus** - Used for sending messages in the service bus queue

20. **Microsoft.NET.Sdk.Functions** - SDK for Azure Funtions

21. **Microsoft.AspNetCore.Mvc.Versioning** - Used for API Versioning

Support

22. **Azure.Extensions.AspNetCore.Configuration.Secrets** - Used to get values from Key vault

23. **JsonSubTypes** - Used to Serialize/Deserialize the inherited classes.

24. **Microsoft.Identity.Web.MicrosoftGraph** - Used to connect with Azure AD and list the users available

# Support

- For any technical queries on SDR API repository, please create an issue [DDF SDR Support](https://github.com/transcelerate/ddf-sdr-support/issues/new?assignees=sdr-support&labels=techSupport&template=TechSupport.yml&title=%5BTechSupport%5D%3A).
