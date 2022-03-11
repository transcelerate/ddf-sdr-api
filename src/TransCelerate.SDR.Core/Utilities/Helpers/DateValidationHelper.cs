using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class DateValidationHelper 
    {
        /// <summary>
        /// Validator for Date
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Returns true if the input value is a valid date</returns>
        public static bool IsValid(object value)
        {
            var dateString = value as string;
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return true; 
            }
            var success = DateTime.TryParse(dateString, out DateTime result);
            return success;
        }
    }
}
