using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds all the constant strings used in the application
    /// </summary>
    public static class Constants
    {
        public const string UsdmVersion = "usdm-version";
        public struct KeyVault
        {
            public const string Key = "KeyVault:Vault";

            public const string ClientId = "KeyVault:ClientId";

            public const string ClientSecret = "KeyVault:ClientSecret";

            public const string DefaultKeyVault = "https://vault.azure.net/.default";
        }

        public struct Collections
        {
            public const string Study = "Study";
            public const string SDRGrouping = "Groups";
            public const string StudyV1 = "StudyDefinitionsV1";
            public const string ChangeAudit = "ChangeAudit";
            public const string StudyDefinitions = "StudyDefinitions";
        }       

        public struct IdType
        {
            public const string SPONSOR_ID = "SPONSOR_ID";
            public const string SPONSOR_ID_V1 = "Clinical Study Sponsor";
            public const string REGULATORY_AGENCY = "Regulatory Agency";
            public const string STUDY_PRIMARY_OBJECTIVE = "Study Primary Objective";
            public const string STUDY_SECONDARY_OBJECTIVE = "Study Secondary Objective";

        }

        public struct LogConstant
        {
            public const string Application = "SDRLogger";
        }

        public struct DateFormats
        {
            public const string DateFormatForAuditResponse = "yyyy-MMM-dd";
        }

        public struct RegularExpressions
        {
            public const string AlphaNumericsWithSpace = @"^[a-zA-Z0-9\s\.]*$";
        }
        public struct DefaultHeaders
        {
            public const string AppInsightsApiKey = "x-api-key";
        }
        public struct Roles
        {
            public const string Org_Admin = "org.admin";
            public const string App_User = "app.user";
            public const string Seperator = ",";
        }

        public struct StudyType
        {
            public const string ALL = "ALL";
        }
        public struct USDMVersions
        {
            public const string MVP = "mvp";
            public const string V1 = "1.0";
            public const string V2 = "2.0";
        }
        public struct ApiVersions
        {
            public const string MVP = "mvp";
            public const string V1 = "v1";
            public const string V2 = "v2";
        }
        public struct DbFilter
        {
            public const string StudyId = "clinicalStudy.studyId";
            public const string StudyIdentifiers = "clinicalStudy.studyIdentifiers";            
            public const string StudyIdentifierOrganisationIdentifier = "studyIdentifierScope.organisationIdentifier";
            public const string StudyIdentifierOrganisationTypeDecode = "studyIdentifierScope.organisationType.decode";
            public const string StudyIdentifierIdType = "idType";
            public const string StudyIdentifierOrgCode = "orgCode";
            public const string StudyPhase = "clinicalStudy.studyPhase";
            public const string StudyPhaseDecode = "decode";
            public const string StudyPhaseStandardCodeDecode = "standardCode.decode";
            public const string StudyDesigns = "clinicalStudy.studyDesigns";
            public const string InterventionModel = "interventionModel.decode";
            public const string StudyIndicationsIndicationDescription = "studyIndications.indicationDescription";
            public const string StudyIndicationsIndicationDesc = "studyIndications.indicationDesc";            
            public const string InterventionModelMVP = "clinicalStudy.currentSections.studyDesigns.currentSections.investigationalInterventions.interventionModel";
            public const string IndicationMVP = "clinicalStudy.currentSections.studyIndications.description";
        }
        public struct ValidationErrorMessage
        {
            public const string ConformanceError = "Field is missing or empty";

            public const string AlphaNumericErrorMessage = "Only Alphanumeric characters are allowed";

            public const string JsonParseError = "Cannot deserialize";

            public const string ValidDateError = "Enter A Valid Date";

            public const string AnyOneFieldError = "Any one of the field is required";

            public const string PropertyMissingError = "Field is missing";

            public const string ValidGroupFieldValue = "Group Field Value is null or empty";

            public const string PropertyEmptyError = "Field is empty";

            public const string UniquenessArrayError = "The {PropertyName} Ids are not unique";

            public const string NumberError = "Enter a valid number";

            public const string GroupFilterEmptyError = "Group Filter must not be empty";

            public const string GroupFilterValue = "Group Filter Value must not be empty";

            public const string SelectAtleastOneGroup = "Select Atleast One Group";

            public const string InvalidPermissionValue = "Permission value is not valid";

            public const string InvalidSortOrder = "Sort Order is not valid";

            public const string EnterValidNumber = "Enter a valid number";

            public const string RootElementMissing = "Root Element is missing";

            public const string InValidDays = "Invalid Date Range";

            public const string BooleanValidationFailed = "Type must be boolean (true/false)";
        }

        public struct ErrorMessages
        {
            public const string StudyNotFound = "The requested study document not found";

            public const string ChangeAuditNotFound = "The requested change audit is not found for the study with uuid :";

            public const string GenericError = "An Error Occured";

            public const string StudyDesignNotFound = "The requested study design not found";

            public const string DateError = "ToDate must be greater than or equal to FromDate";

            public const string StudyInputError = "Kindly provide a valid input";

            public const string SearchNotFound = "No study matches the search keywords";

            public const string SectionNotValid = "Kindly provide a valid section";

            public const string StudyElementNotValid = "Kindly provide a valid study element";

            public const string StudyDesignElementNotValid = "Kindly provide a valid study design element";

            public const string NotValidStudyId = "The provided studyId is not valid";

            public const string DowngradeError = "The usdm-version cannot be downgraded";

            public const string StudyIdNotFound = "The provided clinicalStudy.studyId is not found";

            public const string UsePutEndpoint = "Kindly use PUT Study Definitions endpoint to update study definitions";

            public const string UsePostEndpoint = "Kindly use POST Study Definitions endpoint to create new study definitions";

            public const string GroupsNotFound = "There are no user groups available";

            public const string UsersNotFound = "There are no users available";

            public const string GroupIdError = "The group Id is not valid";

            public const string PostGroupDataNotValid = "The data on the request body is not valid";

            public const string GroupingDetailsUpdated = "The grouping details are updated";

            public const string GroupNameExists = "The group name already exist";

            public const string UnAuthorized = "Access Denied";

            public const string Forbidden = "Access to the resource is restricted";

            public const string InvalidCredentials = "Invalid Credentials";

            public const string PostRestricted = "Operation restricted for the user";

            public const string UsageReportNotAvailable = "System Usage Report Not Available";

            public const string ErrorMessageForReferenceIntegrityInResponse = "Reference Integrity Error";

            public const string ErrorMessageForReferenceIntegrity = "Reference Integrity Failed";
                        
            public const string WorkFlowNotFound = "The requested StudyWorkflow not found";

            public const string EnterDesignIdError = "Kindly provide valid Study Design Id when providing Study WorkFlow Id";

            public const string SoANotFound = "The Schedule Of Activities Not Found";

            public const string UsdmVersionMissing = "The 'usdm-version' header is missing";

            public const string UsdmVersionAmbiguous = "The 'usdm-version' is ambiguous";

            public const string UsdmVersionMapError = "The usdm-version is not compatible with the api-version. Kindly refer versions endpoint for api-version -> usdm-version mapping ";

            public const string eCPTError = "CPT export data cannot be generated for this study";

            public const string CPTNotFound = "The requested CPT variables are not available";

            public const string StudyDesignIdNotFoundCPT = "The requested study design not found and eCPT data cannot be generated";

            public const string StudyDesignNotFoundCPT = "The study design is not available for the given study and eCPT data cannot be generated";

        }

        public struct TokenConstants
        {
            public const string ClientId = "client_id";
            public const string Grant_Type = "grant_type";
            public const string Grant_Type_Value = "password";
            public const string Username = "username";
            public const string Password = "password";
            public const string Scope = "scope";
            public const string Client_Secret = "client_secret";
        }

        public static readonly string[] ClinicalStudyElements = {            
            nameof(ClinicalStudyDto.StudyTitle),
            nameof(ClinicalStudyDto.StudyIdentifiers),
            nameof(ClinicalStudyDto.StudyProtocolVersions),
            nameof(ClinicalStudyDto.StudyVersion),
            nameof(ClinicalStudyDto.StudyPhase),
            nameof(ClinicalStudyDto.StudyType),            
            nameof(ClinicalStudyDto.BusinessTherapeuticAreas),            
            nameof(ClinicalStudyDto.StudyDesigns),
            nameof(ClinicalStudyDto.StudyAcronym),
            nameof(ClinicalStudyDto.StudyRationale),
        };

        public static readonly string[] StudyDesignElements = {            
            nameof(StudyDesignDto.StudyDesignName),
            nameof(StudyDesignDto.StudyDesignDescription),            
            nameof(StudyDesignDto.TherapeuticAreas),            
            nameof(StudyDesignDto.TrialType),
            nameof(StudyDesignDto.StudyInvestigationalInterventions),
            nameof(StudyDesignDto.TrialIntentType),
            nameof(StudyDesignDto.InterventionModel),
            nameof(StudyDesignDto.StudyIndications),
            nameof(StudyDesignDto.StudyPopulations),
            nameof(StudyDesignDto.StudyObjectives),
            nameof(StudyDesignDto.StudyCells),
            nameof(StudyDesignDto.StudyWorkflows),
            nameof(StudyDesignDto.StudyEstimands),
            nameof(StudyDesignDto.Activities),
            nameof(StudyDesignDto.Encounters),
            nameof(StudyDesignDto.StudyDesignRationale),
            nameof(StudyDesignDto.StudyDesignBlindingScheme),
            nameof(StudyDesignDto.BiomedicalConcepts),
            nameof(StudyDesignDto.BcCategories),
            nameof(StudyDesignDto.BcSurrogates)            
        };

        public struct FunctionAppConstants
        {
            public const string ChangeAuditFunction = "ChangeAuditFunction";
            public const string AzureServiceBusQueueName = "%AzureServiceBusQueueName%";
            public const string AzureServiceBusConnectionString = "AzureServiceBusConnectionString";
        }

        public static readonly string[] CharactersToBeRemovedForAudit =
        {
            nameof(CodeEntity.CodeSystemVersion),
            nameof(CodeEntity.CodeSystem),           
            nameof(CodeEntity.Decode),
            nameof(CodeEntity.Code),
            "T"
        };
        public static readonly string[] ParanthesisToBeRemovedForAudit =
        {
            "[",
            "]"
        };
        public struct ApiVersionErrorCodes 
        {
            public const string ApiVersionUnspecified = "ApiVersionUnspecified";
            public const string UnsupportedApiVersion = "UnsupportedApiVersion";
            public const string InvalidApiVersion = "InvalidApiVersion";
            public const string AmbiguousApiVersion = "AmbiguousApiVersion";
        }
        public static readonly string[] Male =
        { 
           "m",
           "male",
        };
        public static readonly string[] Female =
        {
           "f",
           "female",
        };
        public struct PlannedSexOfParticipants 
        {
            public const string Male = "Male";
            public const string Female = "Female";
            public const string MaleOrFemale ="Male Or Female";     
        }
        public struct SdrCptMasterDataEntities
        {
            public const string InterventionModel = "InterventionModel";
            public const string StudyPhase = "Study Phase";
            public const string TrialIntentType = "TrialIntentType";
            public const string ObjectiveLevel = "Objective Level";
        }
    }
}
