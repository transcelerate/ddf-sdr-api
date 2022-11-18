using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ProcedureDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.ProcedureId)]
        public string Id { get; set; }
        public List<CodeDto> ProcedureCode { get; set; }
        public string ProcedureType { get; set; }
    }
}
