using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyIdentifierDTO
    {
       
        public string id { get; set; }
      
        public string orgCode { get; set; }
      
        public string name { get; set; }
      
        public string idType { get; set; }
        
    }
}
