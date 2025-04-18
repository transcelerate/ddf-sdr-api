﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class NarrativeContentDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SectionNumber { get; set; }
        public string SectionTitle { get; set; }
        public List<string> ChildIds { get; set; }
        public string PreviousId { get; set; }
        public string NextId { get; set; }
        public string InstanceType { get; set; }
        public bool DisplaySectionNumber { get; set; }
        public bool DisplaySectionTitle { get; set; }
        public string ContentItemId { get; set; }
	}
}
