namespace TransCelerate.SDR.Core.Utilities
{
    /// <summary>
    /// Enum for Scheduled Instance Types
    /// </summary>

    // When updating the below enum, Constants.ScheduleInstanceType also must be updated.
    public enum ScheduledInstanceType
    {
        ACTIVITY,
        DECISION
    }

    public enum ScheduledInstanceTypeV4
    {
        ScheduledActivityInstance,
        ScheduledDecisionInstance
    }

    public enum SyntaxTemplateInstanceType
    {
        ENDPOINT,
        OBJECTIVE,
        ELIGIBILITY_CRITERIA,
        CHARACTERISTIC
    }
    public enum GeographicScopeInstanceType
    {
        GEOGRAPHIC_SCOPE,
        SUBJECT_ENROLLMENT
    }
}
