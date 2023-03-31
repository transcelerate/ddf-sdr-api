using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class IndicationEntity : IUuid
    {
        public string Uuid { get; set; }
        public string IndicationDesc { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
