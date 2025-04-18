﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class PopulationDefinitionEntity : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IncludesHealthySubjects { get; set; }
        public RangeEntity PlannedAge { get; set; }        
        public RangeEntity PlannedCompletionNumber { get; set; }
        public RangeEntity PlannedEnrollmentNumber { get; set; }
        public List<CodeEntity> PlannedSex { get; set; }		
		public List<string> Criterionids { get; set; }
		public string InstanceType { get; set; }
		public List<CommentAnnotationEntity> Notes { get; set; }
	}
}
