using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyIndicationDTO
    {

        public string Id { get; set; }

        public string Description { get; set; }

        public List<CodingDTO> Coding { get; set; }
    }
}
