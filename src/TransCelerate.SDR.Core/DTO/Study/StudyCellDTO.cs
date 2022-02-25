using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyCellDTO
    {
   
        public string id { get; set; }
              
        public List<StudyElementDTO> studyElements { get; set; }
      
        public StudyArmDTO studyArm { get; set; }
      
        public StudyEpochDTO studyEpoch { get; set; }
    }
}
