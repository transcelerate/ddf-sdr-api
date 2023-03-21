namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ProcedureDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ProcedureId)]
        public string Id { get; set; }
        public CodeDto ProcedureCode { get; set; }
        public string ProcedureType { get; set; }
        public object ProcedureIsConditional { get; set; }
        public string ProcedureIsConditionalReason { get; set; }

    }
}
