﻿using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDesignDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyDesignName { get; set; }
        public string StudyDesignDescription { get; set; }
        public List<CodeDto> InterventionModel { get; set; }
        public List<CodeDto> TrialIntentType { get; set; }
        public List<CodeDto> TherapeuticAreas { get; set; }
        public List<CodeDto> TrialType { get; set; }
        public List<IndicationDto> StudyIndications { get; set; }
        public List<InvestigationalInterventionDto> StudyInvestigationalInterventions { get; set; }
        public List<ObjectiveDto> StudyObjectives { get; set; }
        public List<StudyDesignPopulationDto> StudyPopulations { get; set; }
        public List<StudyCellDto> StudyCells { get; set; }
        public List<WorkflowDto> StudyWorkflows { get; set; }
        public List<EstimandDto> StudyEstimands { get; set; }
    }
}
