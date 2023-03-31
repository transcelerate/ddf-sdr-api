using System;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class DateValidationHelper
    {
        /// <summary>
        /// Validator for Date
        /// </summary>
        /// <param name="value">Value which needed to be checked for valid date</param>
        /// <returns>        
        /// <see langword="true"/> If passed value is a valid date
        /// </returns>
        public static bool IsValid(object value)
        {
            var dateString = value as string;
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return true;
            }

            var success = DateTime.TryParse(dateString, out _);
            return success;
        }
    }
}
