using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AdministrationDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public AliasCodeDto Frequency { get; set; }
        public AliasCodeDto Route { get; set; }
        public DurationDto Duration { get; set; }
        public QuantityDto Dose { get; set; }
        public AdministrableProductDto AdministrableProduct { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public MedicalDeviceDto MedicalDevice  { get; set; }
    }
}
