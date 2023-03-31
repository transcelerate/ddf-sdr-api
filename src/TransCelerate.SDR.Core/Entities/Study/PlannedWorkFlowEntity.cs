using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class PlannedWorkFlowEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string PlannedWorkFlowId { get; set; }
        public string Description { get; set; }
        public PointInTimeEntity StartPoint { get; set; }
        public PointInTimeEntity EndPoint { get; set; }
        public WorkFlowItemMatrixEntity WorkflowItemMatrix { get; set; }
    }
}
