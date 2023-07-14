using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyDesignPopulationDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.StudyDesignPopulationId)]
        public string Id { get; set; }
        public string PopulationDescription { get; set; }
        public object PlannedNumberOfParticipants { get; set; }
        public string PlannedMaximumAgeOfParticipants { get; set; }
        public string PlannedMinimumAgeOfParticipants { get; set; }
        public List<CodeDto> PlannedSexOfParticipants { get; set; }
    }
}
