using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class WorkflowDto
    {
        public string Uuid { get; set; }
        public string WorkflowDesc { get; set; }
        public List<WorkflowItemDto> WorkflowItems { get; set; }
    }
}
