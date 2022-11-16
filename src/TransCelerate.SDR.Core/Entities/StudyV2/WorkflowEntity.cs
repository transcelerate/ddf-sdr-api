using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class WorkflowEntity : IUuid
    {
        public string Uuid { get; set; }
        public string WorkflowDescription{ get; set; }
        public List<WorkFlowItemEntity> WorkflowItems { get; set; }
    }
}
