using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class WorkflowDto : IUuid
    {
        public string Uuid { get; set; }
        public string WorkflowDescription { get; set; }
        public List<WorkflowItemDto> WorkflowItems { get; set; }
    }
}
