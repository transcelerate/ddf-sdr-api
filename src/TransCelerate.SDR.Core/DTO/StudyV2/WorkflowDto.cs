using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class WorkflowDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.WorkflowId)]
        public string Id { get; set; }
        public string WorkflowDescription { get; set; }
        public List<WorkflowItemDto> WorkflowItems { get; set; }
    }
}
