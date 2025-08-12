using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;

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
            public const string StudyDefinitions = "StudyDefinitions";
        }

        public struct IdType
        {
            public const string SPONSOR_ID = "SPONSOR_ID";
            public const string SPONSOR_ID_V1 = "Clinical Study Sponsor";
            public static readonly string[] RegulatoryAgencyIdentifierNumberConstants =
            {
                "Regulatory Agency","Clinical Study Registry"
            };
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
            public const string ApiKeyAuthenticationHeader = "x-api-key";
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
            public const string V1_9 = "1.9";
            public const string V2 = "2.0";
            public const string V3 = "3.0";
            public const string V4 = "4.0";
        }
        public struct ApiVersions
        {
            public const string MVP = "mvp";
            public const string V1 = "v1";
            public const string V2 = "v2";
            public const string V3 = "v3";
            public const string V4 = "v4";
            public const string V5 = "v5";
        }
        public struct DbFilter
        {
            public const string StudyId = "study.studyId";
            public const string StudyIdV4 = "study.id";
            public const string StudyTitlesV4 = "study.versions.titles";
            public const string StudyTitlesTextV4 = "text";
            public const string StudyTitle = "study.studyTitle";
            public const string Study = "study";
            public const string AuditTrail = "auditTrail";
            public const string StudyType = "study.studyType";
            public const string StudyTypeV4 = "study.versions.type";
            public const string StudyIdentifiers = "study.studyIdentifiers";
            public const string StudyIdentifiersV4 = "study.versions.studyIdentifiers";
            public const string StudyIdentifierOrganisationIdentifier = "studyIdentifierScope.organisationIdentifier";
            public const string StudyIdentifierOrganisationTypeDecode = "studyIdentifierScope.organisationType.decode";
            public const string StudyIdentifierIdType = "idType";
            public const string StudyIdentifierOrgCode = "orgCode";
            public const string StudyPhase = "study.studyPhase";
            public const string StudyPhaseDecode = "decode";
            public const string StudyPhaseStandardCodeDecode = "standardCode.decode";
            public const string StudyDesigns = "study.studyDesigns";
            public const string InterventionModel = "interventionModel.decode";
            public const string StudyIndicationsIndicationDescription = "studyIndications.indicationDescription";
            public const string StudyIndicationsIndicationDesc = "studyIndications.indicationDesc";
        }
        public struct StudyTitle
        {
            public const string OfficialStudyTitle = "Official Study Title";
            public const string BriefStudyTitle = "Brief Study Title";
            public const string PublicStudyTitle = "Public Study Title";
            public const string ScientificStudyTitle = "Scientific Study Title";
            public const string StudyAcronym = "Study Acronym";
        }
        public struct DateValue
        {
            public const string ProtocolEffectiveDate = "Protocol Effective Date";
        }
        public struct DefaultValues
        {
            public const int IntegerMinimumValue = 0;
        }

        public struct ValidationErrorMessage
        {
            public const string ValidDateError = "Enter A Valid Date";

            public const string AnyOneFieldError = "Any one of the field is required";

            public const string PropertyMissingError = "Field is missing";

            public const string PropertyEmptyError = "Field is empty";

            public const string IntegerMinimumValueError = "Value must be greater than or equal to zero";

            public const string ScheduledInstanceTypesError = $"The value must be {nameof(ScheduledInstanceType.ACTIVITY)}/{nameof(ScheduledInstanceType.DECISION)}";
            public const string ScheduledInstanceTypesV4Error = $"The value must be {nameof(ScheduledInstanceTypeV4.ScheduledActivityInstance)}/{nameof(ScheduledInstanceTypeV4.ScheduledDecisionInstance)}";

            public const string UniquenessArrayError = "The {PropertyName} Ids are not unique";

            public const string V4StudyVersionCountError = "Only one study version per study is allowed";

            public const string InstanceTypeError = "The Instance Type is incorrect";

            public const string GroupFilterEmptyError = "Group Filter must not be empty";

            public const string SelectAtleastOneGroup = "Select Atleast One Group";

            public const string InvalidPermissionValue = "Permission value is not valid";

            public const string InvalidSortOrder = "sortOrder value must be asc/desc";

            public const string EnterValidNumber = "PageNumber must be >=1";

            public const string RootElementMissing = "Root Element {PropertyName} is missing";

            public const string InValidDays = "Invalid Date Range";

            public const string BooleanValidationFailed = "Type must be boolean (true/false)";

            public const string IntegerValidationFailed = "Type must be integer";

            public const string UsdmVersionMismatch = "usdmVersion value must be the same value mentioned in request header";

            public const string OfficialTitleError = "Atleast one Offical Title is required";
        }

        public struct SuccessMessages
        {
            public const string ValidationSuccess = "The study definition is conformant with USDM Version : ";
        }

        public struct ErrorMessages
        {
            public const string StudyNotFound = "The requested study document not found";

            public const string OneVersionNotFound = "The requested one or more SDRUploadVersions not found";

            public const string ChangeAuditNotFound = "The requested change audit is not found for the study with id :";

            public const string GenericError = "An Error Occured";

            public const string StudyDesignNotFound = "The requested study design not found";

            public const string DateError = "ToDate must be greater than or equal to FromDate";

            public const string DateErrorForReports = "ToDate must be greater than FromDate";

            public const string StudyInputError = "Kindly provide a valid input";

            public const string DateMissingError = "Kindly provide a date range";

            public const string SearchNotFound = "No study matches the search keywords";

            public const string StudyElementNotValid = "A valid study element is required in the listofelements query parameter";

            public const string StudyDesignElementNotValid = "A valid study design element is required in the listofelements query parameter";

            public const string NotValidStudyId = "The provided studyId is not found";

            public const string GroupsNotFound = "There are no user groups available";

            public const string UsersNotFound = "There are no users available";

            public const string GroupIdError = "The group Id is not valid";

            public const string PostGroupDataNotValid = "The data on the request body is not valid";

            public const string GroupNameExists = "The group name already exist";

            public const string UnAuthorized = "Access Denied";

            public const string Forbidden = "Access to the resource is restricted";

            public const string ForbiddenForAStudy = "Access to one or more SDRUploadVersions of the study is restricted";

            public const string ProvideDifferentVersion = "SDRUploadVersions for the Compare API must be different";

            public static readonly string[] ProvideValidVersion = { "A valid SDRUploadVersion for ", " must be provided" };

            public const string InvalidCredentials = "Invalid Credentials";

            public const string PostRestricted = "User does not have permission to create or update this study";

            public const string UsageReportNotAvailable = "System Usage Report Not Available";

            public const string ErrorMessageForReferenceIntegrityInResponse = "Reference Integrity Error";

            public const string ErrorMessageForReferenceIntegrity = "Reference Integrity Failed";

            public const string ScheduleTimelineNotFound = "The requested StudyScheduleTimeline not found";

            public const string EnterDesignIdError = "A valid studyDesignId is required when specifying a studyScheduleTimelineId";

            public const string SoANotFound = "The Schedule Of Activities Not Found";

            public const string UsdmVersionMissing = "The 'usdmVersion' header is missing";

            public const string UsdmVersionAmbiguous = "Only one usdmVersion header is allowed";

            public const string UsdmVersionMapError = "The usdmVersion is not compatible with the apiVersion. Kindly refer versions endpoint for apiVersion -> usdmVersion mapping ";

            public const string eCPTError = "CPT export data cannot be generated for this study";

            public const string CPTNotFound = "The requested CPT variables are not available";

            public const string StudyDesignIdNotFoundCPT = "The requested study design not found and eCPT data cannot be generated";

            public const string StudyDesignNotFoundCPT = "The study design is not available for the given study and eCPT data cannot be generated";

            public const string InvalidUsdmVersion = "The provided USDM Version is invalid";

            public const string ConformanceErrorMessage = "The study definition is not conformant with USDM Version : ";
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

        public static readonly string[] StudyElementsV2 = {
            nameof(StudyDto.StudyTitle),
            nameof(StudyDto.StudyIdentifiers),
            nameof(StudyDto.StudyProtocolVersions),
            nameof(StudyDto.StudyVersion),
            nameof(StudyDto.StudyPhase),
            nameof(StudyDto.StudyType),
            nameof(StudyDto.BusinessTherapeuticAreas),
            nameof(StudyDto.StudyDesigns),
            nameof(StudyDto.StudyAcronym),
            nameof(StudyDto.StudyRationale),
        };

        public static readonly string[] StudyDesignElementsV2 = {
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
            nameof(StudyDesignDto.StudyScheduleTimelines),
            nameof(StudyDesignDto.StudyEstimands),
            nameof(StudyDesignDto.Activities),
            nameof(StudyDesignDto.Encounters),
            nameof(StudyDesignDto.StudyDesignRationale),
            nameof(StudyDesignDto.StudyDesignBlindingScheme),
            nameof(StudyDesignDto.BiomedicalConcepts),
            nameof(StudyDesignDto.BcCategories),
            nameof(StudyDesignDto.BcSurrogates)
        };
        public static readonly string[] StudyElementsV3 = {
            nameof(DTO.StudyV3.StudyDto.StudyTitle),
            nameof(DTO.StudyV3.StudyDto.StudyIdentifiers),
            nameof(DTO.StudyV3.StudyDto.StudyProtocolVersions),
            nameof(DTO.StudyV3.StudyDto.StudyVersion),
            nameof(DTO.StudyV3.StudyDto.StudyPhase),
            nameof(DTO.StudyV3.StudyDto.StudyType),
            nameof(DTO.StudyV3.StudyDto.BusinessTherapeuticAreas),
            nameof(DTO.StudyV3.StudyDto.StudyDesigns),
            nameof(DTO.StudyV3.StudyDto.StudyAcronym),
            nameof(DTO.StudyV3.StudyDto.StudyRationale),
        };

        public static readonly string[] StudyDesignElementsV3 = {
            nameof(DTO.StudyV3.StudyDesignDto.StudyDesignName),
            nameof(DTO.StudyV3.StudyDesignDto.StudyDesignDescription),
            nameof(DTO.StudyV3.StudyDesignDto.TherapeuticAreas),
            nameof(DTO.StudyV3.StudyDesignDto.TrialType),
            nameof(DTO.StudyV3.StudyDesignDto.StudyInvestigationalInterventions),
            nameof(DTO.StudyV3.StudyDesignDto.TrialIntentTypes),
            nameof(DTO.StudyV3.StudyDesignDto.InterventionModel),
            nameof(DTO.StudyV3.StudyDesignDto.StudyIndications),
            nameof(DTO.StudyV3.StudyDesignDto.StudyPopulations),
            nameof(DTO.StudyV3.StudyDesignDto.StudyObjectives),
            nameof(DTO.StudyV3.StudyDesignDto.StudyCells),
            nameof(DTO.StudyV3.StudyDesignDto.StudyArms),
            nameof(DTO.StudyV3.StudyDesignDto.StudyEpochs),
            nameof(DTO.StudyV3.StudyDesignDto.StudyElements),
            nameof(DTO.StudyV3.StudyDesignDto.StudyScheduleTimelines),
            nameof(DTO.StudyV3.StudyDesignDto.StudyEstimands),
            nameof(DTO.StudyV3.StudyDesignDto.Activities),
            nameof(DTO.StudyV3.StudyDesignDto.Encounters),
            nameof(DTO.StudyV3.StudyDesignDto.StudyDesignRationale),
            nameof(DTO.StudyV3.StudyDesignDto.StudyDesignBlindingScheme),
            nameof(DTO.StudyV3.StudyDesignDto.BiomedicalConcepts),
            nameof(DTO.StudyV3.StudyDesignDto.BcCategories),
            nameof(DTO.StudyV3.StudyDesignDto.BcSurrogates)
        };
        public static readonly string[] StudyElementsV4 = {
            nameof(DTO.StudyV4.StudyDto.Name),
            nameof(DTO.StudyV4.StudyDto.Description),
            nameof(DTO.StudyV4.StudyDto.Label),
            nameof(DTO.StudyV4.StudyVersionDto.Titles),
            nameof(DTO.StudyV4.StudyVersionDto.StudyIdentifiers),
            nameof(DTO.StudyV4.StudyVersionDto.DocumentVersionId),
            nameof(DTO.StudyV4.StudyVersionDto.VersionIdentifier),
            nameof(DTO.StudyV4.StudyVersionDto.StudyPhase),
            nameof(DTO.StudyV4.StudyVersionDto.StudyType),
            nameof(DTO.StudyV4.StudyVersionDto.BusinessTherapeuticAreas),
            nameof(DTO.StudyV4.StudyVersionDto.StudyDesigns),
            nameof(DTO.StudyV4.StudyVersionDto.Rationale),
            nameof(DTO.StudyV4.StudyVersionDto.Amendments),
            nameof(DTO.StudyV4.StudyVersionDto.DateValues),
            nameof(DTO.StudyV4.StudyVersionDto.InstanceType)
        };
		public static readonly string[] StudyElementsV5 = {
            nameof(Core.DTO.StudyV5.StudyDto.Name),
            nameof(Core.DTO.StudyV5.StudyDto.Description),
            nameof(Core.DTO.StudyV5.StudyDto.Label),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Titles),
            nameof(Core.DTO.StudyV5.StudyVersionDto.StudyIdentifiers),
            nameof(Core.DTO.StudyV5.StudyVersionDto.ReferenceIdentifiers),
            nameof(Core.DTO.StudyV5.StudyVersionDto.DocumentVersionIds),
            nameof(Core.DTO.StudyV5.StudyVersionDto.VersionIdentifier),
            nameof(Core.DTO.StudyV5.StudyVersionDto.BusinessTherapeuticAreas),
            nameof(Core.DTO.StudyV5.StudyVersionDto.StudyDesigns),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Rationale),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Amendments),
            nameof(Core.DTO.StudyV5.StudyVersionDto.DateValues),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Abbreviations),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Notes),
            nameof(Core.DTO.StudyV5.StudyVersionDto.InstanceType),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Conditions),
            nameof(Core.DTO.StudyV5.StudyVersionDto.BcSurrogates),
            nameof(Core.DTO.StudyV5.StudyVersionDto.BcCategories),
            nameof(Core.DTO.StudyV5.StudyVersionDto.Dictionaries)
		};

        public static readonly string[] StudyDesignElementsV4 = {
            nameof(DTO.StudyV4.StudyDesignDto.Name),
            nameof(DTO.StudyV4.StudyDesignDto.Description),
            nameof(DTO.StudyV4.StudyDesignDto.Label),
            nameof(DTO.StudyV4.StudyDesignDto.TherapeuticAreas),
            nameof(DTO.StudyV4.StudyDesignDto.TrialTypes),
            nameof(DTO.StudyV4.StudyDesignDto.Indications),
            nameof(DTO.StudyV4.StudyDesignDto.TrialIntentTypes),
            nameof(DTO.StudyV4.StudyDesignDto.InterventionModel),
            nameof(DTO.StudyV4.StudyDesignDto.StudyInterventions),
            nameof(DTO.StudyV4.StudyDesignDto.Population),
            nameof(DTO.StudyV4.StudyDesignDto.Objectives),
            nameof(DTO.StudyV4.StudyDesignDto.StudyCells),
            nameof(DTO.StudyV4.StudyDesignDto.Arms),
            nameof(DTO.StudyV4.StudyDesignDto.Epochs),
            nameof(DTO.StudyV4.StudyDesignDto.Elements),
            nameof(DTO.StudyV4.StudyDesignDto.ScheduleTimelines),
            nameof(DTO.StudyV4.StudyDesignDto.Estimands),
            nameof(DTO.StudyV4.StudyDesignDto.Activities),
            nameof(DTO.StudyV4.StudyDesignDto.Encounters),
            nameof(DTO.StudyV4.StudyDesignDto.Rationale),
            nameof(DTO.StudyV4.StudyDesignDto.BlindingSchema),
            nameof(DTO.StudyV4.StudyDesignDto.BiomedicalConcepts),
            nameof(DTO.StudyV4.StudyDesignDto.BcCategories),
            nameof(DTO.StudyV4.StudyDesignDto.BcSurrogates),
            nameof(DTO.StudyV4.StudyDesignDto.DocumentVersionId),
            nameof(DTO.StudyV4.StudyDesignDto.Dictionaries),
            nameof(DTO.StudyV4.StudyDesignDto.Characteristics),
            nameof(DTO.StudyV4.StudyDesignDto.Conditions),
            nameof(DTO.StudyV4.StudyDesignDto.InstanceType),
            nameof(DTO.StudyV4.StudyDesignDto.MaskingRoles),
            nameof(DTO.StudyV4.StudyDesignDto.Organizations)
        };

		public static readonly string[] StudyDesignElementsV5 = {
            nameof(Core.DTO.StudyV5.StudyDesignDto.Name),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Description),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Label),
            nameof(Core.DTO.StudyV5.StudyDesignDto.TherapeuticAreas),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Indications),
            nameof(Core.DTO.StudyV5.StudyDesignDto.StudyInterventions),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Population),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Objectives),
            nameof(Core.DTO.StudyV5.StudyDesignDto.StudyCells),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Arms),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Epochs),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Elements),
            nameof(Core.DTO.StudyV5.StudyDesignDto.ScheduleTimelines),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Estimands),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Activities),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Encounters),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Rationale),
            nameof(Core.DTO.StudyV5.StudyDesignDto.BiomedicalConcepts),
            nameof(Core.DTO.StudyV5.StudyDesignDto.Characteristics),
            nameof(Core.DTO.StudyV5.StudyDesignDto.InstanceType),
            nameof(Core.DTO.StudyV5.StudyDesignDto.StudyPhase),
			nameof(Core.DTO.StudyV5.StudyDesignDto.StudyType)
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
        public struct StringToBeRemovedForChangeAudit
        {
            public const string ConditionAssignmentsValue = ".Value";
        }
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
            public const string MaleOrFemale = "Male or Female";
        }
        public struct SdrCptMasterDataEntities
        {
            public const string InterventionModel = "InterventionModel";
            public const string StudyPhase = "Study Phase";
            public const string TrialIntentType = "TrialIntentType";
            public const string ObjectiveLevel = "Objective Level";
            public const string SexofParticipants = "SexofParticipants";
        }

        public static readonly string[] CharactersToBeRemovedForVersionCompare =
        {
            nameof(Entities.StudyV3.CodeEntity.Id),
            nameof(Entities.StudyV3.CodeEntity.CodeSystemVersion),
            nameof(Entities.StudyV3.CodeEntity.CodeSystem),
            nameof(Entities.StudyV3.CodeEntity.Decode),
            nameof(Entities.StudyV3.CodeEntity.Code)
        };

        public struct VersionCompareConstants
        {
            public const string ArrayBrackets = "[]";
        }

        public readonly struct CodeFieldArrayElements
        {
            public static readonly string[] V3 =
            {
                nameof(Entities.StudyV3.AliasCodeEntity.StandardCodeAliases),
                nameof(Entities.StudyV3.StudyEntity.BusinessTherapeuticAreas),
                nameof(Entities.StudyV3.StudyDesignEntity.TrialIntentTypes),
                nameof(Entities.StudyV3.StudyDesignEntity.TrialType),
                nameof(Entities.StudyV3.StudyDesignPopulationEntity.PlannedSexOfParticipants),
                nameof(Entities.StudyV3.StudyDesignEntity.TherapeuticAreas),
                nameof(Entities.StudyV3.EncounterEntity.EncounterContactModes),
                nameof(Entities.StudyV3.IndicationEntity.Codes),
                nameof(Entities.StudyV3.InvestigationalInterventionEntity.Codes)
            };
        }
    }
}
