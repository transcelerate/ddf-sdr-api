using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ValidateDatatype
    {
        public static bool ValidateBoolean(object field)
        {
            if (field.GetType() == typeof(bool))
                return true;
            return false;

        }
    }
}
