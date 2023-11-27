using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptPropertyDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public object Required { get; set; }
        public object Enabled { get; set; }
        public string Datatype { get; set; }
        public List<ResponseCodeDto> ResponseCodes { get; set; }
        public AliasCodeDto Code { get; set; }
    }
}
