namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class WorkflowItemDto
    {
        public string Uuid { get; set; }
        public ActivityDto WorkflowItemActivity { get; set; }
        public EncounterDto WorkflowItemEncounter { get; set; }
        public string NextWorkflowItemId { get; set; }
        public string PreviousWorkflowItemId { get; set; }
        
    }
}
