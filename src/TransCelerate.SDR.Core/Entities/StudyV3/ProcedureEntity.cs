namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ProcedureEntity : IId
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV3.ProcedureId)]
        public string Id { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public string ProcedureType { get; set; }
        public CodeEntity ProcedureCode { get; set; }
        public bool ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }
    }
}
