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

    public enum IdentifierInstanceTypeV5
    {
        StudyIdentifier,
        ReferenceIdentifier,
        MedicalDeviceIdentifier,
        AdministrableProductIdentifier
    }
    public enum SyntaxTemplateInstanceTypeV5
    {
        Characteristic,
        EligibilityCriterionItem,
        Condition,
        IntercurrentEvent,
        Endpoint,
        Objective
    }
    public enum StudyDesignInstanceTypeV5
    {
        InterventionalStudyDesign,
        ObservationalStudyDesign
    }
    public enum ScheduledInstanceTypeV5
    {
        ScheduledActivityInstance,
        ScheduledDecisionInstance
    }
    public enum PopulationDefinitionInstanceTypeV5
    {
        StudyCohort,
        StudyDesignPopulation
    }
    public enum QuantityRangeInstanceTypeV5
    {
        Quantity,
        Range
    }
}
