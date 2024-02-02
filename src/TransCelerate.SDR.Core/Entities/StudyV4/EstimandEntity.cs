﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EstimandEntity : IId
    {        
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string SummaryMeasure { get; set; }
        public AnalysisPopulationEntity AnalysisPopulation { get; set; }
        public string VariableOfInterestId { get; set; }
        public List<InterCurrentEventEntity> IntercurrentEvents { get; set; }
    }
}