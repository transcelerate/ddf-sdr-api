namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ConditionAssignmentDto : IId
    {        
        public string Id { get; set; }
        public string Condition { get; set; }
        public string ConditionTargetId { get; set; }
        public string InstanceType { get; set; }
    }
}
