- [Introduction](#introduction)
  - [Requirements to Contribute and Propose Changes](#requirements-to-contribute-and-propose-changes)
- [Sample Data](#sample-data)
- [Pre-requisites](#pre-requisites)
- [Code setup and debugging](#code-setup-and-debugging)
- [Base solution structure](#base-solution-structure)
- [Commit changes to repository](#commit-changes-to-repository)
- [List of Endpoints](#list-of-endpoints)
- [Nuget packages](#nuget-packages)


# Introduction

Study Definition Repository (SDR) Reference Implementation is TransCelerate’s vision to catalyze industry-level transformation, enabling digital exchange of study definition information by collaborating with technology providers and standards bodies to create a sustainable open-source Study Definition Repository.

This is a .NET 6 Web API project that is designed to expose APIs which upstream/downstream systems can utilize to store and retrieve study definitions from SDR.

This [Process Flow Document](https://github.com/transcelerate/ddf-sdr-platform/blob/main/documents/MVP%20Process%20Flows%20(final).pdf) provides information regarding user interface functions and system interactions with the SDR at a high level. Please also refer to the [DDF SDR API User Guide](documents/ddf-sdr-user-guide-api-v3.0.pdf) to get started, and the [DDF SDR RI API Demo video](https://www.youtube.com/watch?v=s9Qnzxy7HME&list=PLMXS-Xt7Ou1KNUF-HQKQRRzqfPQEXWb1u&index=7). 

**NOTES:** 
- These materials and information are provided by TransCelerate Biopharma Inc. AS IS.  Any party using or relying on this information and these materials do so entirely at their own risk.  Neither TransCelerate nor its members will bear any responsibility or liability for any harm, including indirect or consequential harm, that a user may incur from use or misuse of this information or materials.
- Please be aware that any information you put into the provided tools (including the UI or API) will be visible to all users, so we recommend not using commercially sensitive or confidential information.  You and/or your employer bear all responsibility for anything you share with this project.  TransCelerate, its member companies and any vendors affiliated with the DDF project are not responsible for any harm or loss you occur as a result of uploading any information or code: commercially sensitive, confidential or otherwise.  
- As of May 2022, the DDF initiative is still the process of setting up operations, and any pull requests submitted will not be triaged at this point in time.

## Requirements to Contribute and Propose Changes
Before participating, you must acknowledge the Contribution License Agreement (CLA).

To acknowledge the CLA, follow these instructions:

- Click [here](https://github.com/transcelerate/ddf-home/blob/main/documents/DDF_CLA_2022MAR28_FINAL.pdf) to download and carefully read the CLA.
- Print the document.
- Complete and sign the document.
- Scan and email a PDF version of the completed and signed document to [DDF@transceleratebiopharmainc.com](mailto:DDF@transceleratebiopharmainc.com?subject=Signed%20CLA).

NOTE: Keep a copy for your records.

# Sample Data
For those looking to evaluate the USDM with a sample data set, please see the following to files in the Data Model folder:
- [Sample Data via JSON](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Request-POST-AddStudyDefinitionV1.json)


# Code setup and debugging
## Pre-requisites

1. Install [Visual Studio 2022](https://visualstudio.microsoft.com/) with default options to run the solution.

2. Create a Mongo DB(or equivalent) with two collections with names mentioned below.
```
StudyDefinitions
StudyDefinitionsV1
Groups
ChangeAudit
```

## How to setup code

1. Clone the repo into a local directory using below git command.

```shell
git clone "repo_url"
```
2. Once repo is cloned, open the solution in Visual Studio 2022 IDE.

## How To Run

1. For running the code locally, take a copy of appsettings.json file and rename the copied file to appsettings.Development.json file in the root folder of TransCelerate.SDR.WebApi project.

2. Edit the appsettings.Development.json and add the values for below mentioned settings.

```
"ConnectionStrings": {
    "ServerName": "mongodb+sre://SDRADMIN:KasdeafsfhttDxaqj@study.cph52.mongodb.net/db",
    "DatabaseName": "Database Name here"
 },
"StudyHistory": {
	// This parameter will be used to restrict the historical data (last 30/60/90 days) in study history endpoint response, if no date filters are passed in request.
	// Keep this value as "-1" to disable this restriction.
    "DateRange": "No. of days"
 },
 "isGroupFilterEnabled": true  // change value to false to disable user based data filtering,
 "isAuthEnabled": true  // change value to false to disable authorization
 "ApiVersionUsdmVersionMapping":"" //Api Version -> USDM Version Mapping JSON must be converted to JSON String and should be added
```

3. Then, In the Visual Studio IDE, on clicking the IIS Express Icon or on pressing F5, WebApi solution will start running locally.

4. The browser will automatically open the Swagger UI having the SDR API specifications.


# Base solution structure

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
      │   ├── StudyRules
      │   ├── StudyV1Rules
      │   ├── StudyV2Rules
      │   ├── Token
      │   ├── UserGroupMappingRules
      │   ├── ValidationDependencies.cs
      │   ├── ValidationDependenciesCommon.cs
      │   ├── ValidationDependenciesV1.cs 
      │   ├── ValidationDependenciesV2.cs
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
**[TransCelerate.SDR.Core](src/TransCelerate.SDR.AzureFunctions/TransCelerate.SDR.AzureFunctions.md)** - contains Azure function app for change audit.

**[TransCelerate.SDR.Core](src/TransCelerate.SDR.Core/TransCelerate.SDR.Core.md)** - contains entities, DTO's and helper classes.

**[TransCelerate.SDR.DataAccess](src/TransCelerate.SDR.DataAccess/TransCelerate.SDR.DataAccess.md)** - contains code for communicating with database (Mongo DB).

**[TransCelerate.SDR.RuleEngine](src/TransCelerate.SDR.RuleEngine/TransCelerate.SDR.RuleEngine.md)** - contains code for model validations based on data conformance rules.

**[TransCelerate.SDR.Service](src/TransCelerate.SDR.Service/TransCelerate.SDR.Services.md)** - contains code for service layer which is a bridge between API controller and repository.

**[TransCelerate.SDR.UnitTesting](src/TransCelerate.SDR.UnitTesting/TransCelerate.SDR.UnitTesting.md)** - contains code for unit testing (NUnit).

**[TransCelerate.SDR.WebApi](src/TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.md)** - contains controllers, mappers and the startup for the application.

# Commit changes to repository
1. After doing the necessary changes, build the solution.

2. Once the build is successful, run all the unit test cases from Test Explorer.

3. Verify all the unit test cases are passed and the changes are reflecting in Swagger UI.

4. Use below command from the git to push the code changes back to the Repository.

```
git commit -m 'message for commit'
git push
```

# List of Endpoints

The below POST endpoint can be used to generate authentication token to access other API endpoints.
```
/auth/token
```
The below GET endpoint can be used to GET API Version -> USDM Version mapping.
```
/versions
```

### V1 Endpoints (USDM Version 1.0)

For V1 endpoints, the "usdmVersion" header paramter is mandatory and the header value must be "1.0"

**POST Endpoint**

The below endpoint can be used to create new (or) update existing study definitions.
```
/v1/studydefinitions
```

**GET Endpoints**

The below endpoint can be used to fetch all the elements for a given StudyId.

```
/v1/studydefinitions/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a given StudyId.

```
​/v1​/studydesign​s?study_uuid={studyId}
```

### V2 Endpoints (USDM Version 1.9)

For V2 endpoints, the "usdmVersion" header paramter is mandatory and the header value must be "1.9"

**POST Endpoint**
The below endpoint can be used to create new (or) update existing study definitions.
```
/v2/studydefinitions
```
**PUT Endpoint**
The below endpoint can be used to create new (or) update existing study definitions.
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
​/v2​/studydesign​s?study_uuid={studyId}
```

### Version Neutral Endpoints

The below endpoints can be used to fetch the audit trail for a given StudyId.

```
studydefinitions/{studyId}/audittrail
```

The below endpoint can be used to fetch basic details of all study definitions in SDR.

```
/studydefinitions/studyhistory
```

To view the API specifications and to run the endpoints locally, the below swagger url can be used.

```
https://localhost:44358/swagger/index.html
```
**Note**: Refer **[DDF SDR API User Guide](documents/ddf-sdr-user-guide-api-v3.0.pdf)** for detailed information on all the endpoints.

# Nuget Packages 

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

22. **Azure.Extensions.AspNetCore.Configuration.Secrets** - Used to get values from Key vault
