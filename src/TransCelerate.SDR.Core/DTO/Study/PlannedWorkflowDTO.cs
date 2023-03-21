namespace TransCelerate.SDR.Core.DTO.Study
{
    public class PlannedWorkflowDTO
    {

        public string Id { get; set; }

        public string Description { get; set; }

        public PointInTimeDTO StartPoint { get; set; }

        public PointInTimeDTO EndPoint { get; set; }

        //public List<TransitionDTO> transitions { get; set; }

        public WorkflowItemMatrixDTO WorkflowItemMatrix { get; set; }
    }
}
