using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class SubstanceDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Label { get; set; }
        public List<StrengthDto> Strengths { get; set; }
        public SubstanceDto ReferenceSubstance { get; set; }
        public List<CodeDto> Codes { get; set; }
    }
}
