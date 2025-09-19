using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AdministrableProductDto : IId
    {
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public List<CommentAnnotationDto> Notes { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public List<AdministrableProductPropertyDto> Properties { get; set; }
        public List<AdministrableProductIdentifierDto> Identifiers { get; set; }
        public AliasCodeDto AdministrableDoseForm { get; set; }
        public CodeDto PharmacologicClass { get; set; }
        public CodeDto ProductDesignation { get; set; }
        public CodeDto Sourcing { get; set; }
    }
}
