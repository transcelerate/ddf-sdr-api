namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class WorkFlowItemEntity : IUuid
    {
        public string Uuid { get; set; }
        public string WorkflowItemDesc { get; set; }
        public ActivityEntity WorkflowItemActivity { get; set; }
        public EncounterEntity WorkflowItemEncounter { get; set; }
        public string NextWorkflowItemId { get; set; }
        public string PreviousWorkflowItemId { get; set; }
    }
}
