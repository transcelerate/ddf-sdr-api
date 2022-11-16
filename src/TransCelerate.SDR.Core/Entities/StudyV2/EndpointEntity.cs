using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class EndpointEntity : IUuid
    {
        public string Uuid { get; set; }
        public string EndpointDescription { get; set; }
        public string EndpointPurposeDescription { get; set; }
        public List<CodeEntity> EndpointLevel { get; set; }
    }
}
