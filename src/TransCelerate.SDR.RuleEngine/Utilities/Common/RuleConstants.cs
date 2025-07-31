namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public static class RuleConstants
    {
        public struct RuleValidationWarningMessages
        {
            public const string DDF00171 = "The expanded text for all abbreviations defined for a study version are expected to be unique.";

            public const string DDF00075 = "An activity is expected to refer to at least one procedure, biomedical concept, biomedical concept category or biomedical concept surrogate.";
        }

        public struct RuleValidationErrorMessages
        {
            public const string DDF00170 = "All abbreviations defined for a study version must be unique.";
        }
    }
}
