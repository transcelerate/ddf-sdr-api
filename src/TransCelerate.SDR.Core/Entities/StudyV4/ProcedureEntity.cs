namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ProcedureEntity : IId
    {        
        public string Id { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public string ProcedureType { get; set; }
        public CodeEntity ProcedureCode { get; set; }
        public bool ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }
    }
}
