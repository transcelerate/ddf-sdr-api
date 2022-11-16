namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class WorkflowItemDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.WorkflowItemId)]
        public string Id { get; set; }
        public string WorkflowItemDescription { get; set; }
        public ActivityDto WorkflowItemActivity { get; set; }
        public EncounterDto WorkflowItemEncounter { get; set; }       
        public string NextWorkflowItemId { get; set; }
        public string PreviousWorkflowItemId { get; set; }
        
    }
}
