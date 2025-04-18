﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class ObjectiveDto : SyntaxTemplateDto
    {
        public CodeDto Level { get; set; }
        public List<EndpointDto> Endpoints { get; set; }
    }
}
