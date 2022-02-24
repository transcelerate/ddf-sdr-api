using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class PostStudyResponseDTO
    {
        public string studyId { get; set; }
        public int studyVersion { get; set; }
        public List<string> studyDesignId { get; set; }
    }
}
