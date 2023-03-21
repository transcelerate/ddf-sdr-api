using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class InvestigationalInterventionDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string InterventionModel { get; set; }
        public string Status { get; set; }
        public List<CodingDTO> Coding { get; set; }
    }

}
