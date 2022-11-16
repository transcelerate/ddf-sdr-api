using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class IndicationEntity : IUuid
    {
        public string Uuid { get; set; }
        public string IndicationDescription { get; set; }
        public List<CodeEntity> Codes { get; set; }
    }
}
