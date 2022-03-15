using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    /// <summary>
    /// This class is a DTO for the response of POST Method for a study
    /// </summary>
    public class PostStudyResponseDTO
    {
        public string studyId { get; set; }
        public int studyVersion { get; set; }
        public List<string> studyDesignId { get; set; }
    }
}
