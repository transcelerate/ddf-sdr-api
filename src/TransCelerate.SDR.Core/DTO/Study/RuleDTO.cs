using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class RuleDTO
    {
       
        public string id { get; set; }

        public string description { get; set; }

        public List<CodingDTO> coding { get; set; }
    }
}
