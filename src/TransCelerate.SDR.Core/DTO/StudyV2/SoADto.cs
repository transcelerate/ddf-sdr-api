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

        public List<StudyWorkflows> StudyWorkflows { get; set; }
    }

    public class StudyWorkflows
    {
        public string WorkFlowId { get; set; }
        public string WorkflowDescription { get; set; }

        public WorkFlowSoA WorkFlowSoA { get; set; }
    }

    public class WorkFlowSoA
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
