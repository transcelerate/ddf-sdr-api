using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyInterventionDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<CodeDto> Codes { get; set; }
        public CodeDto Role { get; set; }
        public CodeDto Type { get; set; }
        public QuantityDto MinimumResponseDuration { get; set; }
        public AgentAdministrationDto Administrations { get; set; }
        public CodeDto ProductDesignation { get; set; }
        public CodeDto PharmacologicClass { get; set; }
        public string InstanceType { get; set; }
    }
}
