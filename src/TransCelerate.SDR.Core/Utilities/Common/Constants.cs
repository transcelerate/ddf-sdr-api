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
        }
   
        public struct IdType
        {
            public const string SPONSOR_ID = "SPONSOR_ID";
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
        
        public struct ValidationErrorMessage
        {
            public const string ConformanceError = "Field is missing or empty";

            public const string AlphaNumericErrorMessage = "Only Alphanumeric characters are allowed";

            public const string JsonParseError = "Cannot deserialize";

            public const string ValidDateError = "Enter A Valid Date";

            public const string AnyOneFieldError = "Any one of the field is required";

            public const string PropertyMissingError = "Field is missing";

            public const string PropertyEmptyError = "Field is empty";

            public const string NumberError = "Enter a valid number";

            public const string GroupFilterEmptyError = "Group Filter must not be empty";

            public const string SelectAtleastOneGroup = "Select Atleast One Group";

            public const string InvalidPermissionValue = "Permission value is not valid";

            public const string InvalidSortOrder = "Sort Order is not valid";
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

            public const string NotValidStudyId = "The provided studyId is not valid";

            public const string GroupsNotFound = "There are no user groups available";

            public const string UsersNotFound = "There are no users available";

            public const string GroupIdError = "The group Id is not valid";

            public const string PostGroupDataNotValid = "The data on the request body is not valid";

            public const string GroupingDetailsUpdated = "The grouping details are updated";

            public const string GroupNameExists = "The group name already exist";
        }
    }
}
