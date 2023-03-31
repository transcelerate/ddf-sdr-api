using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class DefinedProcedureDto : IUuid
    {
        public string Uuid { get; set; }
        public List<CodeDto> ProcedureCode { get; set; }
        public string ProcedureType { get; set; }
    }
}
