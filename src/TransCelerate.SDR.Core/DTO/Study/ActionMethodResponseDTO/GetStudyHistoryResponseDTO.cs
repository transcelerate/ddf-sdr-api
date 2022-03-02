using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class GetStudyHistoryResponseDTO
    {        
        public List<StudyHistory> study { get; set; }
    }  
    
    public class StudyHistory
    {
        public string studyTitle { get; set; }
        public string studyId { get; set; }
        public int[] studyVersion { get; set; }      
    }
}
