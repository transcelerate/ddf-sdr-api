using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class WorkflowEntity
    {
        public string WorkflowId { get; set; }
        public string WorkflowDesc { get; set; }
        public List<WorkFlowItemEntity> WorkflowItems { get; set; }
    }
}
