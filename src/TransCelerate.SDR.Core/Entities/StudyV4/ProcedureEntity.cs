namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ProcedureEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public CodeEntity Code { get; set; }
        public bool IsConditional { get; set; }
        public string IsConditionalReason { get; set; }
        public string StudyInterventionId { get; set; }
    }
}
