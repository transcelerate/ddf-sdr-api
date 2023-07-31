using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptPropertyDto : IId
    {        
        public string Id { get; set; }
        public string BcPropertyName { get; set; }
        public object BcPropertyRequired { get; set; }
        public object BcPropertyEnabled { get; set; }
        public string BcPropertyDataType { get; set; }
        public List<ResponseCodeDto> BcPropertyResponseCodes { get; set; }
        public AliasCodeDto BcPropertyConceptCode { get; set; }
    }
}
