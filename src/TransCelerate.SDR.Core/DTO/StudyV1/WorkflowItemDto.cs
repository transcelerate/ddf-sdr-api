namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class WorkflowItemDto : IUuid
    {
        public string Uuid { get; set; }
        public string WorkflowItemDesc { get; set; }
        public ActivityDto WorkflowItemActivity { get; set; }
        public EncounterDto WorkflowItemEncounter { get; set; }
        public string NextWorkflowItemId { get; set; }
        public string PreviousWorkflowItemId { get; set; }

    }
}
