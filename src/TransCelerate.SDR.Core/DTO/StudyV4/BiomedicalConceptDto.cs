﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class BiomedicalConceptDto : IId
    {        
        public string Id { get; set; }
        public string BcName { get; set; }
        public List<string> BcSynonyms { get; set; }
        public string BcReference { get; set; }
        public List<BiomedicalConceptPropertyDto> BcProperties { get; set; }
        public AliasCodeDto BcConceptCode { get; set; }
    }
}
