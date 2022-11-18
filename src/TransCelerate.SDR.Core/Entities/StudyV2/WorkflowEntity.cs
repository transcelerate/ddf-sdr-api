using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class WorkflowEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.WorkflowId)]
        public string Id { get; set; }
        public string WorkflowDescription { get; set; }
        public List<WorkFlowItemEntity> WorkflowItems { get; set; }
    }
}
