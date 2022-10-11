using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds all the constant strings used in the application
    /// </summary>
    public static class Constants
    {
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
        }       

        public struct IdType
        {
            public const string SPONSOR_ID = "SPONSOR_ID";
            public const string SPONSOR_ID_V1 = "Clinical Study Sponsor";
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

            public const string NumberError = "Enter a valid number";

            public const string GroupFilterEmptyError = "Group Filter must not be empty";

            public const string GroupFilterValue = "Group Filter Value must not be empty";

            public const string SelectAtleastOneGroup = "Select Atleast One Group";

            public const string InvalidPermissionValue = "Permission value is not valid";

            public const string InvalidSortOrder = "Sort Order is not valid";

            public const string EnterValidNumber = "Enter a valid number";

            public const string RootElementMissing = "Root Element is missing";

            public const string InValidDays = "Invalid Date Range";
        }

        public struct ErrorMessages
        {
            public const string StudyNotFound = "The requested study document not found";

            public const string GenericError = "An Error Occured";

            public const string StudyDesignNotFound = "The requested study design not found";

            public const string DateError = "ToDate must be greater than or equal to FromDate";

            public const string StudyInputError = "Kindly provide a valid input";

            public const string SearchNotFound = "No study matches the search keywords";

            public const string SectionNotValid = "Kindly provide a valid section";

            public const string StudyElementNotValid = "Kindly provide a valid study element";

            public const string StudyDesignElementNotValid = "Kindly provide a valid study design element";

            public const string NotValidStudyId = "The provided studyId is not valid";

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
            nameof(ClinicalStudyDto.StudyDesigns),
        };

        public static readonly string[] StudyDesignElements = {
            nameof(StudyDesignDto.Uuid),
            nameof(ClinicalStudyDto.StudyTitle),
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
    }
}
