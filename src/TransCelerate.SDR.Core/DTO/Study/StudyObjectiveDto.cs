using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.Study
{

    public class StudyObjectiveDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string Level { get; set; }

        public List<EndpointsDTO> Endpoints { get; set; }
    }


    public class EndpointsDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string Purpose { get; set; }

        public string OutcomeLevel { get; set; }
    }
}
