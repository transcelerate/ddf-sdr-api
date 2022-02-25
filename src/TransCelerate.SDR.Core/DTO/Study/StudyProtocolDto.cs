using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class StudyProtocolDTO
    {
        public List<AmendmentDTO> amendments { get; set; }
        public string protocolId { get; set; }
        public string briefTitle { get; set; }
        public string officialTitle { get; set; }
        public string publicTitle { get; set; }
        public string version { get; set; }

        public List<string> sections { get; set; }
    }
}
