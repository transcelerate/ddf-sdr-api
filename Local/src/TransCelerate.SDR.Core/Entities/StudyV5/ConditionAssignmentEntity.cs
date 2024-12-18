namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ConditionAssignmentEntity : IId
    {        
        public string Id { get; set; }
        public string Condition { get; set; }
        public string ConditionTargetId { get; set; }
        public string InstanceType { get; set; }
    }
}
