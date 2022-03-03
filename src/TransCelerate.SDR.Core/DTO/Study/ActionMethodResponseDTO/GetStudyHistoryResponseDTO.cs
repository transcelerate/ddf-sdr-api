using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudyHistoryResponseDTO
    {        
        public List<StudyHistoryDTO> study { get; set; }
    }  
    
    public class StudyHistoryDTO
    {
        public string studyTitle { get; set; }
        public string studyId { get; set; }
        public int[] studyVersion { get; set; }      
    }
}
