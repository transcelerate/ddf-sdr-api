using System;

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

        public static bool ValidateInt(object field)
        {            
            if (field != null)
            {
                if (int.TryParse(Convert.ToString(field), out var _))
                    return true;
                return false;
            }
            return true;
        }
    }
}
