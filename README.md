- [Introduction](#introduction)
  - [Requirements to Contribute and Propose Changes](#requirements-to-contribute-and-propose-changes)
- [Intended Audience](#intended-audience)
- [Overview](#overview)
  - [Development Setup and Code Access](#development-setup-and-code-access)
  - [Running the API using Docker Image](#running-the-api-using-docker-image)
- [Other Information](#other-information)
  - [Base solution structure](#base-solution-structure)
  - [Sample Data](#sample-data)
  - [SDR API](#sdr-api)
    - [List Of Endpoints](#list-of-endpoints)
      - [V3 Endpoints (USDM Version 2.0)](#v3-endpoints-usdm-version-20)
      - [V4 Endpoints (USDM Version 3.0)](#v4-endpoints-usdm-version-30)
      - [V5 Endpoints (USDM Version 4.0)](#v5-endpoints-usdm-version-40)
      - [Version Neutral Endpoints](#version-neutral-endpoints)
      - [API Spec](#api-spec)
    - [API Versioning](#api-versioning)
  - [Nuget Packages](#nuget-packages)
- [Changes for Release V5.0 (September 2025)](#changes-for-release-v50-september-2025)
- [Support](#support)

# Introduction

Digital Data Flow is TransCelerate’s vision to catalyze industry-level transformation, enabling digital exchange of study definition information by collaborating with technology providers and standards bodies to create a sustainable open-source Study Definition Repository.

The Study Definitions Repository is a potential solution that may help support use of CDISC’s USDM standard as well as end-to-end data flow.

This is a .NET 8 Web API project that is designed to expose APIs which upstream/downstream systems can utilize to store and retrieve study definitions from SDR. The latest Release of SDR (Release V4.0) supports study definitions conformant with USDM V2.0, USDM V3.0 and USDM V4.0.

Please refer to the [DDF SDR API User Guide](documents/sdr-release-v5.0/ddf-sdr-ri-api-user-guide-v8.0.pdf) to get started.

**NOTES:** 
- These materials and information are provided by TransCelerate Biopharma Inc. AS IS. Any party using or relying on this information and these materials do so entirely at their own risk. Neither TransCelerate nor its members will bear any responsibility or liability for any harm, including indirect or consequential harm, that a user may incur from use or misuse of this information or materials.
- An SDR is not mandatory to achieve end-to-end data flow but rather represents one potential solution that may support end-to-end data flow.  Nothing in this document should be construed as a recommendation that companies use an SDR or this SDR.  Companies are solely responsible for determining how to manage their own end-to-end data flow systems and processes.
- This SDR is not a commercial product, rather it is TransCelerate’s attempt to illustrate what might be possible in implementing the USDM developed by CDISC. TransCelerate does not endorse any particular software, system, or service.  Users can use the USDM for any purpose they choose and can build their own implementations of the SDR using the resources available on the initiative’s [GitHub page](https://github.com/transcelerate).
- As of September 2025, the DDF initiative is still the process of setting up operations, and any pull requests submitted will not be triaged at this point in time.

## Requirements to Contribute and Propose Changes
Before participating, you must acknowledge the Contribution License Agreement (CLA).

To acknowledge the CLA, follow these instructions:

- Click [here](https://github.com/transcelerate/ddf-home/blob/main/documents/DDF_CLA_2022MAR28_FINAL.pdf) to download and carefully read the CLA.
- Print the document.
- Complete and sign the document.
- Scan and email a PDF version of the completed and signed document to [DDF@transceleratebiopharmainc.com](mailto:DDF@transceleratebiopharmainc.com?subject=Signed%20CLA).

NOTE: Keep a copy for your records.

# Intended Audience
The contents in this repository enable users to set up SDR Reference Implementation API with a Docker container through their own GitHub repositories and workflows. The deployment scripts (YAML Scripts) can be configured and executed from GitHub Actions, leveraging GitHub Secrets to configure target environment specific values.

It assumes a good understanding of Docker concepts and containerization for running the API application in a container. The audience for this document should:
- have clear understanding of C# and .NET Web APIs
- have basic understanding of MongoDB and MongoDB C# driver
- be familiar with Docker and container management
- have basic understanding of GitHub Actions, Secrets & Yaml Scripts

# Overview
The SDR Reference Implementation implements the CDISC DDF Reference Architecture which includes the USDM model and API Specifications defined using the OpenAPI Specification (OAS). The API Layer of the SDR Reference Implementation complies with the OpenAPI Specification and follows the REST architectural style that uses HTTP requests to GET, POST and PUT data.

## Development Setup and Code Access

Refer to the [DDF SDR RI System Maintenance Guide Document](documents/sdr-release-v5.0/ddf-sdr-ri-system-maintenance-guide-v2.0.pdf) for setting up a development instance of SDR. 

## Running the API using Docker Image

Our published Docker image is the recommended way to run an instance of the SDR API:

```bash
docker run \
    -e ConnectionStrings__DefaultConnection=’<CONNECTION_STRING>’ \
    -e ConnectionStrings__DatabaseName=’SDR’ \
    ghcr.io/transcelerate/ddf-sdr-api:latest
```

Where the `ConnectionString parameters` point to a mongo-compatible database in which study information can be stored and retrieved from. Refer to the SDR Platform project for a ready-to-use containerized database solution.


> **Note**  
> **API to USDM Version mapping** - SDR supports 3 major USDM versions at a given point in time along with all their minor versions. API endpoints are up-versioned for breaking changes in USDM (API V3 -> USDM 2.0, API V4 -> USDM 3.0, API V5 -> USDM 4.0).

# Other Information
## Base solution structure

The solution has the following structure:

```  
  ├── TransCelerate.SDR.sln
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
      │   ├── StudyV3Rules
      │   ├── StudyV4Rules
      │   ├── StudyV5Rules
      │   ├── Utilities
      │   ├── ValidationDependenciesCommon.cs
      │   ├── ValidationDependenciesV3.cs
      │   ├── ValidationDependenciesV4.cs
      │   ├── ValidationDependenciesV5.cs 
      │   └── TransCelerate.SDR.RuleEngine.md
      ├── TransCelerate.SDR.Service
      │   ├── Interfaces
      │   ├── Services
      │   └── TransCelerate.SDR.Service.md  
      ├── TransCelerate.SDR.UnitTesting
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
**[TransCelerate.SDR.Core](src/TransCelerate.SDR.Core/TransCelerate.SDR.Core.md)** - contains entities, DTOs and helper classes.

**[TransCelerate.SDR.DataAccess](src/TransCelerate.SDR.DataAccess/TransCelerate.SDR.DataAccess.md)** - contains code for communicating with MongoDB database.

**[TransCelerate.SDR.RuleEngine](src/TransCelerate.SDR.RuleEngine/TransCelerate.SDR.RuleEngine.md)** - contains code for model validations based on data conformance rules as well as integration with the CDISC Rules Engine.

**[TransCelerate.SDR.Service](src/TransCelerate.SDR.Service/TransCelerate.SDR.Services.md)** - contains code for service layer which is a bridge between API controller and repository.

**[TransCelerate.SDR.UnitTesting](src/TransCelerate.SDR.UnitTesting/TransCelerate.SDR.UnitTesting.md)** - contains code for unit testing (NUnit Framework).

**[TransCelerate.SDR.WebApi](src/TransCelerate.SDR.WebApi/TransCelerate.SDR.WebApi.md)** - contains API controllers, mappers and the startup for the application.

## Sample Data
For those looking to evaluate the USDM with a sample data set, please see the following files in the Data Model folder:
- [USDM V2.0 conformant Sample JSON](data-model/sdr-release-v2.0.2/ddf-sdr-api-study-sample-json-v2.0.json)
- [USDM V3.0 conformant Sample JSON](data-model/sdr-release-v3.0/ddf-sdr-api-study-sample-json-v3.0.json)
- [USDM V4.0 conformant Sample JSON](data-model/sdr-release-v5.0/sample-studies/ddf-sdr-api-study-cdisc-pilot-sample-json-v5.0.json)

## SDR API
### List Of Endpoints

The below GET endpoint can be used to GET API Version -> USDM Version mapping.
```
/versions
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
#### V5 Endpoints (USDM Version 4.0)

For V5 endpoints, the "usdmVersion" header parameter is mandatory and the header value must be "4.0"

**POST Endpoint**
The below endpoint can be used to create new study definitions.
```
/v5/studydefinitions
```
The below endpoint can be used to validate the USDM conformance rules for a study definition using the CDISC Rules Engine
```
/v5/studydefinitions/validate-usdm-conformance
```
**PUT Endpoint**
The below endpoint can be used to update existing study definitions (create new version for a study definition).
```
/v5/studydefinitions/{studyId}
```
**GET Endpoints**

The below endpoint can be used to fetch all the elements for a given StudyId.

```
/v5/studydefinitions/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a given StudyId.

```
/v5/studydesigns?studyId={studyId}
```
The below endpoint can be used to get the changes between two SDR Upload Versions of a specific study definition
```
/v5/studydefinitions/{studyId}/version-comparison?sdruploadversionone={sdruploadversionone}&sdruploadversiontwo={sdruploadversiontwo}
```
The below endpoint can be used to export study details mapped to a limited set of CPT Variables grouped by sections within the Common Protocol Template
```
/v5/studydefinitions/{studyId}/studydesigns/ecpt
```
The below endpoint can be used to fetch data from study definitions that help build the Schedule of Activities matrix for a given Schedule Timeline in a Study Design
```
/v5/studydefinitions/{studyId}/studydesigns/soa
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
**Note**: Refer **[DDF SDR API User Guide](documents/sdr-release-v5.0/ddf-sdr-ri-api-user-guide-v8.0.pdf)** for detailed information on all the endpoints.

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
 **Note**: SDR V5 integrates with the CDISC Rules Engine for USDM conformance validation. This means the SDR no longer implements its own validation rules and utilizes the Core Engine for conformance validation. As a result, a new version for the RuleEngine no longer has to be created
- For version neutral endpoint search endpoint, data filters need to be added in below components to support new API version
```
 TransCelerate.SDR.DataAccess
 TransCelerate.SDR.Services
```

## Nuget Packages 

1. **Automapper.Extensions.Microsoft.DependencyInjection** - Used for mapping two different classes.

2. **Newtonsoft.Json** - Used for Serialization/De-Serialization of JSON Data.
 
3. **MongoDB.Driver** - Used for communicating with Mongo DB.
 
4. **NUnit** - Used for Unit Testing.

5. **Moq** - Used for mocking in unit testing.

6. **Autofac.Extras.Moq** - Used for mocking in unit testing.
 
7.  **FluentValidation.AspNetCore** - Used for implementing the RuleEngine.

8.  **Swashbuckle.AspNetCore** - Used for enabling Swagger for the API's.

9.  **Swashbuckle.AspNetCore.Annotations** - Used for adding comments, sample request and response in Swagger.

10. **Microsoft.Extensions.Logging** - Used for logging.

11. **Microsoft.Extensions.Configuration.Abstractions** - Used for Key-Value abstractions.

12. **Microsoft.AspNetCore.Mvc.Core** - Contains common action result types, attribute routing, application model conventions, API explorer, application parts, filters, formatters, model binding, and more.

13. **Microsoft.AspNetCore.Authorization** - Used for API Authorization

14. **Vsxmd** - Used for Converting xml comments into markdown file

15. **ObjectsComparer** - Used for comparing two objects of same type and return the differences

16. **Microsoft.AspNetCore.Mvc.Versioning** - Used for API Versioning

17. **JsonSubTypes** - Used to Serialize/Deserialize the inherited classes.

# Changes for Release V5.0 (September 2025)

SDR Release V5.0 marks a fundamental shift from previous versions by eliminating Azure dependencies from its architecture, more easily enabling platform-agnostic deployment capabilities across various environments.

# Support

- For any technical queries on SDR API repository, please create an issue [DDF SDR Support](https://github.com/transcelerate/ddf-sdr-support/issues/new?assignees=sdr-support&labels=techSupport&template=TechSupport.yml&title=%5BTechSupport%5D%3A).
