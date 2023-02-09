using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class ProcedureEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.ProcedureId)]
        public string Id { get; set; }
        public CodeEntity ProcedureCode { get; set; }
        public string ProcedureType { get; set; }
        public bool ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }
    }
}
