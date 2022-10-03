- [Introduction](#introduction)
  - [Overview](#overview)
  - [Scope of Document](#scope-of-document)
  - [Intended Audience](#intended-audience)
  - [Definitions and Acronyms](#definitions-and-acronyms)
- [Pre-Requisites](#pre-requisites)
    - [Self-signed Certificate Generation](#self-signed-certificate-generation)
    - [Certificate generation with Open SSL](#certificate-generation-with-open-ssl)
    - [Certificate generation with PowerShell](#certificate-generation-with-powershell)
 - [API End Points](#api-end-points)
    - [Token](#token)
    - [Study History](#study-history)
    - [Get Study](#get-study)
    - [Get Study Design](#get-study-design)
    - [Audit Trail](#audit-trail)
    - [Post Study](#post-study)
    - [POST - Add Study Definition](#post---add-study-definition)
    - [POST - Update Study Definition](#post---update-study-definition)
  - [Testing the API Endpoint](#testing-the-api-endpoint)
    - [Postman REST client Setup](#postman-rest-client-setup)
    - [Accessing the GET API](#accessing-the-get-api)

# Introduction

 ## Overview
   The API Layer of the SDR Reference Implementation complies with the Open API Specification (OAS) standards which allow systems to discover and understand the capabilities of the service without access to source code, documentation, or through network traffic inspection. When properly defined, a consumer can understand and interact with the remote service with a minimal amount of implementation logic.
          It follows the REST architectural style that uses HTTP requests to GET and POST data. REST standards are not linked with any technology or platform, it does not dictate exactly how to build an API. Instead, it introduces the best practices known as constraints. They describe how the server processes requests and responds to them. Operating within these constraints, the system gains desirable properties such as reliability, ease of use, improved scalability and security, low latency while enhancing the system performance and helping achieve technology independence in the process.

 ## Scope of Document

This document details the list of API endpoints available in Study Definition Repository and provides sample Request and Response for each endpoint. It also explains the possible reasons behind the various error codes returned by the API. The technical design and implementation of these APIs is out of scope.
## Intended Audience
The document is a guide for users/vendors who want to integrate and consume the SDR API to store and retrieve study definitions in USDM format.

## Definitions and Acronyms

| Term / Abbreviation      |   Definition     |
| ------------- |:-------------:| 
| API     | Application Programming Interface  |
| DDF     | Digital Data Flow  |
|JSON     | JavaScript Object Notation  |
|REST     | Representational State Transfer |
|SDR      | Study Definition Repository |
|URL      | Uniform Resource Locator  |
|USDM     | Unified Study Definitions Model |

# Pre-Requisites

To access APIs, a client certificate must be generated, and a postman rest client should be configured

  ## Self-signed Certificate Generation
  
  Certificate can be created by using any one of the below Tools.  
  • Open SSL
  
  • PowerShell
  ### Certificate generation with Open SSL
  - Install the Open SSL.
  - Run the OpenSSL Command Prompt and set the directory to the folder where certificate to be stored.
   
   <p align="center"> <img width="387" alt="OpenSSL Command Prompt" src="images/api-user-guide/openssl-command-prompt.png">
	
  - Run the below command → provide Enter PEM pass phrase and save the pass phrase to provide it on the command to generate .pfx certificate.
      ```
      openssl req -x509 -newkey rsa:4096 -sha256 -keyout my.key -out my.crt -subj "/CN=test.com" -days 600
      ```
 <p align="center"> <img width="389" alt="open-ssl-command" src="images/api-user-guide/open-ssl-command.png">
  
  - Run the below command to generate the .pfx certificate
    ```
     openssl pkcs12 -export -name “test.com” -out my.pfx -inkey my.key -in my.crt
     ```
     Provide the below details 
     Enter pass phrase for my.key:  Provide the pass phrase passed on step 3
     Enter Export Password: Provide the password for .pfx certificate and save it for later use.
     
     <p align="center"> <img width="382" alt="export-certificate" src="images/api-user-guide/export-certificate.png">
      
 - Exported .pfx certificate will be stored on the folder path provided on step 1
     Note: Share the .pfx certificate and the password to SDR Support team to get access to SDR API’s.
      ## Certificate generation with PowerShell
 - Open PowerShell and run the following command to generate the self-signed certificate
   ```
   New-SelfSignedCertificate -certstorelocation cert:\CurrentUser\My\ -dnsname example.com
   ```
   <p align="center"> <img width="382" alt="PowerShell Command"  src="images/api-user-guide/powershell-command.png">
   
 - To Export the certificate in .pfx format, go to run → type certmmgr.msc and click ok
   
  <p align="center"> <img width="382" alt="Run Window" src="images/api-user-guide/run-window.png">

 - In certificate manager go to personal → certificates → select the generated certificate and right click select All Tasks → Select Export.
 
<p align="center"> <img width="382" alt="Certificate Manager Console" src="images/api-user-guide/certificate-manager-console.png">

 - In Certificate Export Wizard click Next to continue.

<p align="center"> <img width="382" alt="cerificate-export-wizard" src="images/api-user-guide/certificate-export-wizard.png">

 - Select export the private key option and click next.

<p align="center"> <img width="382" alt="Export Private Key" src="images/api-user-guide/export-rivate-key.png">
  
 - Leave the defaults and click next.

<p align="center"> <img width="382" alt="Certificate Format .PFX" src="images/api-user-guide/certificate-format .pfx.png">

 - Provide the password for certificate and click next.

  <p align="center"> <img width="382" alt="Password for Certificate" src="images/api-user-guide/password-for-certificate.png">

 - Select the path to save the certificate.

 <p align="center"> <img width="382" alt="Certificate File Name" src="images/api-user-guide/certificate-file-name.png">

 - Click on Finish.

  <p align="center"> <img width="382" alt="Certificate Export Wizard-2" src="images/api-user-guide/certificate-export-wizard-2.png">

 - Exported .pfx certificate will be stored on the folder path provided on step 7.
```
Note: Share the .pfx certificate and the password to SDR Support team to get access to SDR API’s.
```
  ## API End Points
  
  SDR Reference Implementation exposes following REST Endpoints to deliver application functionality including storing and retrieving study definitions. All the API  should conform to the USDM format mentioned in [USDM Conformance for SDR RI](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/USDM_ConformanceRules_SDR_Reference_ImplementationV1.xlsx). For Reference Implementation, only required field validations mentioned in the USDM document have been included.
  ## Token
This endpoint allows user to get access token to access API’s by providing username and password in the request body. This access token must be passed as an authorization header for the other API endpoints.

| Method     |   URL     |
| ------------- |-------------| 
| POST     | /api/v1/auth/token  |
	  
#### REQUEST PARAMETERS
| Type     |   Mandatory     | Sample |
| ------------- |-------------|------------|
| Body     | Yes  |{<br>&nbsp;&nbsp;&nbsp;"userName": "xxxx@email.com",<br>&nbsp;&nbsp;&nbsp;"password": "xxxxx"<br>}|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| ------------- |-------------|------------|
|200|{<br>&nbsp;&nbsp;&nbsp;"token": "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjJaUXBKM1VwYmpBWVhZR2FYRU....."<br>}|Successful|
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}<br>OR<br>{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "Invalid Credentials"<br>}|Application Exception or entered username or password is invalid.|
|500|Internal Server Error|Web server/Web API down|
	  
## Study History
This endpoint allows user to retrieve the metadata information for Studies residing in SDR.

| Method     |   URL     |
| ------------- |-------------| 
| GET|/api/v1/studydefinitions/history|
	  
#### REQUEST PARAMETERS
| Type     |   Name     | Type |Mandatory|Sample|
| ------------- |-------------|------------|------------|------------|
|Query Parameter|FromDate|Date|No|2022-Mar-10|
|Query Parameter|ToDate|Date|No|2022-Mar-15|
|Query Parameter|studyTitle|String|No|“Study On Oncology”|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| ------------- |-------------|------------|
|403|Forbidden|Invalid Client Certificate|
|404|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "404",<br>&nbsp;&nbsp;&nbsp;"message": "The requested study document not found"<br>}|When Study Title is given in query param <br>and there is no matching study definition in SDR, <br>or the requested URI is not valid.|
|500|Internal Server Error|Web server/Web API down|
	  
## Get Study
This endpoint allows user to retrieve the different study elements for a particular study for a specific SDR upload version.
	  
| Method     |   URL     |
| ------------- |-------------| 
| GET|/api/v1/studydefinitions/{studyId}|
	  
#### REQUEST PARAMETERS
| Type     |   Name     | Type |Mandatory|Sample|
| ------------- |-------------|------------|------------|------------|
|Path|studyId|String|Yes|25f58eb5-bda8-4c85-8a27-685af9f6743d|
|Query Parameter|sdruploadversion|Integer|No|2|
|Header|Authorization|String|Yes|Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6I....|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| ------------- |-------------|------------|
|200|Refer [Response for GET StudyV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Response-GET-StudyV1.json) for the JSON Data  |
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}|Application Exception |
|401|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "401",<br>&nbsp;&nbsp;&nbsp;"message": "Access Denied"<br>}|When the API is invoked without access token <br>or an expired access token, <br>then the API will give an unauthorized response.|
|403|Forbidden|Invalid Client Certificate|
|404|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "404",<br>&nbsp;&nbsp;&nbsp;"message": "The requested study document not found"<br>}|Requested Study Id is not found, <br>or the request URL path <br>is not valid|
|500|Internal Server Error|Web server/Web API down|

## Get Study Design
Retrieves the different study design elements for a particular study for SDR upload version.
	  
| Method     |   URL     |
| ------------- |-------------| 
| GET|/api/v1/studydesigns|
	  
#### REQUEST PARAMETERS
| Type     |   Name     | Type |Mandatory|Sample|
| ------------- |-------------|------------|------------|------------|
|Query Parameter|study_uuid|String|Yes|25f58eb5-bda8-4c85-8a27-685af9f6743d|
|Query Parameter|sdruploadversion|Integer|No|3|
|Header|Authorization|String|Yes|Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6I....|
	  
#### RESPONSE
| Staus Code     |   Response     | Comments |
| ------------- |-------------|------------|
|200|Refer [Response for GET Study DesignV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Response-GET-StudyDesignV1.json)  for the JSON Data|Successful|
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}|Application Exception |
|401|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "401",<br>&nbsp;&nbsp;&nbsp;"message": "Access Denied"<br>}|When the API is invoked without access token <br>or an expired access token, <br>then the API will give an unauthorized response.|
|403|Forbidden|Invalid Client Certificate|
|404|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "404",<br>&nbsp;&nbsp;&nbsp;"message": "The requested study document not found"<br>}<br><br>OR<br><br>{<br>&nbsp;&nbsp;&nbsp;"statusCode": "404",<br>&nbsp;&nbsp;&nbsp;"message": "The requested study design not found"<br>}|This can occur when requested <br>Study Id is not found <br>or Study Design is not found.|
|500|Internal Server Error|Web server/Web API down|

## Audit Trail
This endpoint allows user to retrieve the audit trail information for a study residing in SDR Repository. Audit trail captures the history of all changes done on a study definition including entry date time and SDR upload version.

| Method     |   URL     |
| ------------- |-------------| 
| GET|/api/v1/audittrail/{studyId}|
	  
#### REQUEST PARAMETERS
| Type     |   Name     | Type |Mandatory|Sample|
| ------------- |-------------|------------|------------|------------|
|Path|studyId|String|Yes|25f58eb5-bda8-4c85-8a27-685af9f6743d|
|Query Parameter|FromDate|Date|No|2022-Mar-05|
|Query Parameter|ToDate|Date|No|2022-Mar-15|
|Header|Authorization|String|Yes|Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6I....|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| ------------- |-------------|------------|
|200|{<br>&nbsp;&nbsp;&nbsp;"uuid": "25f58eb5-bda8-4c85-8a27-685af9f6743d",<br>&nbsp;&nbsp;&nbsp;"auditTrail": [<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"entryDateTime": "2022-03-12T11:07:11.871Z",<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"SDRUploadVersion": 3<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;},<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"entryDateTime": "2022-03-10T12:04:12.421Z ",<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"SDRUploadVersion": 2<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>&nbsp;&nbsp;&nbsp;]<br>}|Successful|
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}|Application Exception| 
|401|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "401",<br>&nbsp;&nbsp;&nbsp;"message": "Access Denied"<br>}|When the API is invoked without access token <br>or an expired access token, <br>then the API will give an unauthorized response.|
|403|Forbidden|Invalid Client Certificate|
|404|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "404",<br>&nbsp;&nbsp;&nbsp;"message": "The requested study document not found"<br>}|Requested Study Id is not found, <br>or the request URL path <br>is not valid|
|500|Internal Server Error|Web server/Web API down|

## Post Study
This endpoint allows user to add and/or update a study definition in SDR that conforms to the USDM structure. The study GET endpoint allows the users to fetch data for individual section whereas the section specific adds/updates are not in scope of SDR Reference Implementation.
  #### POST - Add Study Definition
  For the first time, when a study is pushed into the SDR, the uuid fields in the JSON data must be empty. SDR will generate UUID for the uuid fields.
	  
| Method     |   URL     |
| ------------- |-------------| 
| POST|/api/v1/studydefinitions|

#### REQUEST PARAMETERS
| Type     |   Name     |Sample|	  
| ------------- |-------------|-------------| 
|Body||Refer [Request Body for POST – Add New Study DefinitionV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Request-POST-AddStudyDefinitionV1.json) for the JSON Data|
|Header|Authorization|Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6I....|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| --------------- |-------------|------------|
|201|Refer [Response Body for POST – Add Study DefinitionV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Response-POST-AddStudyDefinitionV1.json) for the JSON Data|Successful.<br>For a new study document added into SDR,<br> the unique IDs for Study and Study Design <br>are generated by system and returned <br>in response body along with SDR Upload Version defaulted to "1".<br>For updates to an existing study,<br>  the version is incremented by 1.|
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}|Application Exception| 
|401|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "401",<br>&nbsp;&nbsp;&nbsp;"message": "Access Denied"<br>}|When the API is invoked without access token <br>or an expired access token, <br>then the API will give an unauthorized response.|
|403|Forbidden|Invalid Client Certificate|
|404|Resource Not Found|When the request URL is not valid.|
|500|Internal Server Error|Web server/Web API down|
	  
  #### POST - Update Study Definition
  - For adding new elements to an existing study definition section
      - uuid for the study should be passed in the request body
      - uuid for the study and uuid for the studyDesign should be passed when updating study design data.
      - New elements should be passed without id field data along with existing elements including SDR generated id values in the request body.
      - SDR will generate UUID for only the new elements mentioned in the request and update the data for existing elements that had a valid UUID in the request.
   - For example, there is an existing study definition in SDR as mentioned in [Study Definition – Existing DataV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/StudyDefinition-ExistingDataV1.json). If a new element is to be added to study objectives, the existing study data with the new element must be sent in the request body as mentioned in [Request Body for POST – Update New Study DefinitionV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Request-POST-UpdateStudyDefinitionV1.json).

| Method     |   URL     |
| ------------- |-------------| 
| POST|/api/v1/studydefinitions|

#### REQUEST PARAMETERS
| Type     |   Name     |Sample|	  
| ------------- |-------------|-------------| 
|Body||Refer [Request Body for POST – Update New Study DefinitionV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Request-POST-UpdateStudyDefinitionV1.json) for the JSON Data|
|Header|Authorization|Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6I....|

#### RESPONSE
| Staus Code     |   Response     | Comments |
| --------------- |-------------|------------|
|201|Refer [Response Body for POST – Update Study DefinitionV1](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Response-POST-UpdateStudyDefinitionV1.json) for the JSON Data|Successful.<br>The new element in the section <br> will be added on along with <br> the existing elements.|
|400|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "400",<br>&nbsp;&nbsp;&nbsp;"message": "An error occurred"<br>}|Application Exception| 
|401|{<br>&nbsp;&nbsp;&nbsp;"statusCode": "401",<br>&nbsp;&nbsp;&nbsp;"message": "Access Denied"<br>}|When the API is invoked without access token <br>or an expired access token, <br>then the API will give an unauthorized response.|
|403|Forbidden|Invalid Client Certificate|
|404|Resource Not Found|When the request URL is not valid.|
|500|Internal Server Error|Web server/Web API down|

  # Testing the API Endpoint
  
   ## Postman REST client Setup
   #### Pre-Requisites
   - Self-Signed .pfx certificate configured for SDR API Access.
   - Install [Postman Tool](https://www.postman.com/downloads/)
   #### STEPS:
   - Open postman and go to “Settings” option

   <p align="center"> <img width="334" alt="Postman Upload Certificate" src="images/api-user-guide/postman-upload-certificate.png">

   - Go to “Certificates” tab and upload the client certificate and provide the Host Name and Passphrase (certificate password) for the client certificate

  <p align="center"> <img width="382" alt="Postman Add Client Certificate" src="images/api-user-guide/postman-add-client-certificate.png">

   ## Accessing the GET API
   #### STEPS:
   - Enter the URL for the GET Request URL (APIM endpoint) as mentioned below
 
```
https://apim-sdr-envname-eastus.azure-api.net/studydefinitionrepository/v1/study/
```

- Get the clinical study `StudyId` from [Response for GET Study](https://github.com/transcelerate/ddf-sdr-api/blob/main/DataModel/API_UserGuide_JSON_Files/Response-GET-StudyV1.json) and append the Study ID to the GET URL.

<p align="center"> <img width="382" alt="Postman set HTTP Request for GET" src="images/api-user-guide/postman-set-http-request-for-get.png">

- Click on the “Send” button to get the study definition data for given studyId.

 <p align="center"> <img width="382" alt="Postman Send Request" src="images/api-user-guide/postman-send-request.png">

- For a valid certificate and correct SDR API URL, the API Response of HTTP Status code 200 along with the data will be returned.

<p align="center"> <img width="382" alt="Postman HTTP Response" src="images/api-user-guide/postman-http-response.png">

- Similarly, below response codes returned by SDR APIM endpoints can also tested in postman. These scenarios can be tested in any of the REST API tools other than Postman.
  - 400 – The server cannot or will not process the request due to something that is perceived to be a client error
  - 403 - indicates that the server understands the request but refuses to authorize it
  - 404 - indicates that the server cannot find the requested resource
  - Incorrect Domain/hostname
  - Incorrect Protocol
  - Incorrect Method
