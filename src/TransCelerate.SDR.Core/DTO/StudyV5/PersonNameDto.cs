using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class PersonNameDto : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Text { get; set; }
        public string FamilyName { get; set; }
        public List<string> GivenNames { get; set; }
        public List<string> Prefixes { get; set; }
        public List<string> Suffixes { get; set; }
    }
}
