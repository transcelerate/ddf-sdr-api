namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class ProcedureDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.ProcedureId)]
        public string Id { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public string ProcedureType { get; set; }
        public CodeDto ProcedureCode { get; set; }
        public object ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }

    }
}
