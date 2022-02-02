using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public class DateValidationHelper : ValidationAttribute
    {
        public override bool IsValid(object value)
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
