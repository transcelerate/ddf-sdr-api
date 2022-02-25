using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class CodingDTO
    {

        public string code { get; set; }

        public string codeSystem { get; set; }

        public string codeSystemVersion { get; set; }

        public string decode { get; set; }
    }
}
