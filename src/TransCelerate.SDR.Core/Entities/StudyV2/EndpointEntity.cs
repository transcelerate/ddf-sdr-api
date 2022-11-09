using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class EndpointEntity : IUuid
    {
        public string Uuid { get; set; }
        public string EndpointDesc { get; set; }
        public string EndpointPurposeDesc { get; set; }
        public List<CodeEntity> EndpointLevel { get; set; }
    }
}
