using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class EpochDTO
    {
        public string id { get; set; }
        public string epochType { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int sequenceInStudy { get; set; }
    }
}
