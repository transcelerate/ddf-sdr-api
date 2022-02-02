using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    /// <summary>
    /// This class holds all the constant strings used in the application
    /// </summary>
    public static class Constants
    {
        public struct Collections
        {
            public const string Study = "Study";
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
            public const string AlphaNumericsWithSpace = @"^[a-zA-Z0-9\s]*$";
        }
        public struct ValidationErrorMessage
        {
            public const string ConformanceError = "Field is missing or empty";

            public const string AlphaNumericErrorMessage = "Only Alphanumeric characters are allowed";

            public const string JsonParseError = "Cannot deserialize";

            public const string ValidDateError = "Enter A Valid Date";
        }
    }
}
