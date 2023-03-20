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
