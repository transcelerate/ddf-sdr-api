using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ProductOrganizationRoleDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<string> AppliesToIds { get; set; }
        public CodeDto Code { get; set; }
        public string OrganizationId { get; set; }
   
    }
}