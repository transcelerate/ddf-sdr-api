using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class SoADto
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }

        public List<StudyDesigns> StudyDesigns { get; set; }
    }

    public class StudyDesigns
    {
        public string StudyDesignId { get; set; }
        public string StudyDesignName { get; set; }
        public string StudyDesignDescription { get; set; }

        public List<ScheduleTimelines> StudyScheduleTimelines { get; set; }
    }

    public class ScheduleTimelines
    {
        public string ScheduleTimelineId { get; set; }
        public string ScheduleTimelineName { get; set; }
        public string ScheduleTimelineDescription { get; set; }
        public string EntryCondition { get; set; }

        public ScheduleTimelineSoA ScheduleTimelineSoA { get; set; }
    }

    public class ScheduleTimelineSoA
    {
        public List<OrderOfActivities> OrderOfActivities { get; set; }
        [Newtonsoft.Json.JsonProperty(nameof(SoA))]
        public List<SoA> SoA { get; set; }
    }

    public class SoA
    {
        public string EncounterId { get; set; }
        public string EncounterName { get; set; }
        public string EncounterScheduledAtTimingValue { get; set; }

        public List<TimingSoA> Timings { get; set; }
    }
    public class OrderOfActivities
    {
        public string ActivityId { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<ProcedureSoA> DefinedProcedures { get; set; }
        public object ActivityIsConditional { get; set; }
        public string ActivityIsConditionalReason { get; set; }
        public List<string> BiomedicalConcepts { get; set; }
        public string ActivityTimelineId { get; set; }
        public string ActivityTimelineName { get; set; }
        public string FootnoteId { get; set; }
        public string FootnoteDescription { get; set; }
    }

    public class ProcedureSoA
    {
        public string ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public bool ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }
        public string FootnoteId { get; set; }
        public string FootnoteDescription { get; set; }
    }
    public class TimingSoA
    {
        public string TimingValue { get; set; }
        public string TimingWindow { get; set; }
        public string TimingType { get; set; }
        public List<string> Activities { get; set; }
    }

}
