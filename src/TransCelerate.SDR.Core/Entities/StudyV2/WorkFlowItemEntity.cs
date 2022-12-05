namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class WorkFlowItemEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.WorkflowItemId)]
        public string Id { get; set; }
        public string WorkflowItemDescription { get; set; }
        public string WorkflowItemActivityId { get; set; }
        public string WorkflowItemEncounterId { get; set; }        
        public string NextWorkflowItemId { get; set; }
        public string PreviousWorkflowItemId { get; set; }
    }
}
