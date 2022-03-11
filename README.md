- [Introduction](#introduction)
- [Pre-Requisites](#pre-requisites)
- [Base solution structure](#base-solution-structure)
- [List of Endpoints](#list-of-endpoints)
- [Authorization](#authorization)
- [Nuget packages](#nuget-packages)

# Introduction

Digital Data Flow Application is TransCelerate’s vision to catalyze industry-level transformation, enabling digital exchange of study definition information by collaborating with technology providers and standards bodies to create a sustainable open-source Study Definition Repository.

In this .NET Core Web API project, user can add and view different versions of study element and their details

# Pre-requisites

1. Install [Visual Studio](https://visualstudio.microsoft.com/) with default options.

### How to setup code

2. Clone the repo

```
git clone "repo_url"
cd SDR-API/SDR-WebAPI
```


### How To Run

3. In the Visual Studio IDE, on clicking the IIS Express Icon or on pressing F5, API will start running locally.

4. The browser will be automatically open the Swagger Hub for the API.

5. Relevant environment variables should be changed if we need to point localhost to a different environment. 


# Base solution structure

The solution has the following structure:

```  
  ├── TransCelerate.SDR.sln
      ├── TransCelerate.SDR.Core
      │   ├── AppSettings
      │   ├── ClassDiagrams
      │   ├── DTO
      │   ├── Entities
      │   ├── ErrorModels
      │   └── Utilities
      ├── TransCelerate.SDR.Repository
      │   ├── Interfaces
      │   └── Repositories         
      ├── TransCelerate.SDR.Services
      │   ├── Interfaces
      │   └── Services   
      ├── TransCelerate.SDR.UnitTesting
      │   ├── ControllerUnitTesting
      │   ├── Data
      │   └── ServicesUnitTesting 
      └── TransCelerate.SDR.WebAPI
          ├── Properties
          ├── Controllers
          ├── Mappers
          ├── appsettings.json
          ├── Program.cs
          └── Startup.cs

```

# List of Endpoints

**GET Endpoints**

The below endpoint can be used to fetch all the elements for a studyId or a specific section for a study also can be fetched.

```
/study​/{studyId}
```

The below endpoint can be used to fetch the sections of study design for a specific study ID and a specific study design ID.

```
​/{studyId}​/studydesign​/{studyDesignId}
```


The below endpoint can be used to fetch the audit trail for the study ID.

```
​/audittrail​/{studyId}
```

The below endpoint can be used to fetch all the study Id's in the SDR

```
/studyhistory
```


**POST Endpoints**

The below endpoint can be used to Create a new study document.

```
​/study
```
The below endpoint can be used to fetch all the study which matches the search criteria which was added in the request body.
```
/​search
```

# Authorization
All the API are automatically authorized with the azure client. So, all the request must have the bearer token to pass the authorization.

# Nuget Packages 

1.**Automapper.Extensions.Microsoft.DependencyInjection** - Used for mapping the classes

2.**Microsoft.ApplicationInsights.AspNetCore** - Used for Logging in Application Insights

3.**Newtonsoft.Json** - Used for Serialization/De-Serialization of JSON Data

4.**Microsoft.Identity.Web** - Used For Azure JWT Authorization

5.**Swashbuckle.AspNetCore** - Used For enabling swagger for the API's

6.**MongoDB.Driver** - Used For communicating with Azure cosmos DB API for Mongo DB

7.**NUnit** - Used for Unit Testing