using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class MedicalDeviceDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string HardwareVersion { get; set; }
        public string SoftwareVersion { get; set; }
        public AdministrableProductDto EmbeddedProduct  { get; set; }
        public CodeDto Sourcing  { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public List<MedicalDeviceIdentifierDto> Identifiers { get; set; }
    }
}
