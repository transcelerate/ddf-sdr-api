using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class InterventionalStudyDesignDto : StudyDesignDto
    {
        public List<CodeDto> SubTypes { get; set; }
        public List<CodeDto> IntentTypes { get; set; }
        public CodeDto Model { get; set; }
        public CodeDto BlindingSchema { get; set; }
	}
}
