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
        public string ScheduleTimelineDescription { get; set; }

        public ScheduleTimelineSoA ScheduleTimelineSoA { get; set; }
    }

    public class ScheduleTimelineSoA
    {
        public List<string> OrderOfActivities { get; set; }
        [Newtonsoft.Json.JsonProperty(nameof(SoA))]
        public List<SoA> SoA { get; set; }
    }

    public class SoA
    {
        public string EncounterName { get; set; }

        public List<string> Activities { get; set; }
    }
}
