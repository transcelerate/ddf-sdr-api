using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class DefinedProcedureEntity : IUuid
    {
        public string Uuid { get; set; }
        public List<CodeEntity> ProcedureCode { get; set; }
        public string ProcedureType { get; set; }
    }
}
