using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyArmDTO
    {

        public string description { get; set; }
     
        public string id { get; set; }

        public string studyArmType { get; set; }

        public string studyOriginType { get; set; }

        public string studyArmOrigin { get; set; }

        public string name { get; set; }
    }
}
