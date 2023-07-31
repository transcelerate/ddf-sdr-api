using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptCategoryDto : IId
    {        
        public string Id { get; set; }
        public List<string> BcCategoryChildIds { get; set; }
        public string BcCategoryName { get; set; }
        public string BcCategoryDescription { get; set; }
        public List<string> BcCategoryMemberIds { get; set; }
        public AliasCodeDto BcCategoryCode { get; set; }
    }
}
