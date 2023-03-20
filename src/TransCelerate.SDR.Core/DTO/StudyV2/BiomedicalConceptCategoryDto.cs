using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class BiomedicalConceptCategoryDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.BiomedicalConceptCategoryId)]
        public string Id { get; set; }
        public List<string> BcCategoryParentIds { get; set; }
        public List<string> BcCategoryChildrenIds { get; set; }
        public string BcCategoryName { get; set; }
        public string BcCategoryDescription { get; set; }
        public List<string> BcCategoryMemberIds { get; set; }
    }
}
