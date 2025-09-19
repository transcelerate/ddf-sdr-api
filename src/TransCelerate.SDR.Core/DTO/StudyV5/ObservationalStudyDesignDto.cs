using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ObservationalStudyDesignDto : StudyDesignDto
    {
        public CodeDto TimePerspective { get; set; }
        public List<CodeDto> SubTypes { get; set; }
        public CodeDto Model { get; set; }
        public CodeDto SamplingMethod { get; set; }
	}
}
