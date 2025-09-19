namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public static class RuleConstants
    {
        public struct RuleValidationWarningMessages
        {
            public const string DDF00171 = "The expanded text for all abbreviations defined for a study version are expected to be unique.";
        }

        public struct RuleValidationErrorMessages
        {
            public const string DDF00170 = "All abbreviations defined for a study version must be unique.";
        }
    }
}
