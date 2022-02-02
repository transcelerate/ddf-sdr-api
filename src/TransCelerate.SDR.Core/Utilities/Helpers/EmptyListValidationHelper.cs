using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public class EmptyListValidationHelper : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list == null)
            {
                return true;
            }
            else if (list != null)
            {
                return list.Count > 0;
            }
            return true;
        }
    }   
}
