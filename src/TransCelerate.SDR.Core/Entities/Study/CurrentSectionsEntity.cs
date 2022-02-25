using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class CurrentSectionsEntity
    {
        [BsonElement("id")]
        public string currentSectionsId { get; set; }
        public string sectionType { get; set; }

        [BsonIgnoreIfNull]
        public List<PlannedWorkFlowEntity> plannedWorkflows { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyPopulationEntity> studyPopulations { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyCellEntity> studyCells { get; set; }

        [BsonIgnoreIfNull]
        public List<InvestigationalInterventionEntity> investigationalInterventions { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyDesignEntity> studyDesigns { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyObjectiveEntity> objectives { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyIndicationEntity> studyIndications { get; set; }
        //Removed Study Protocol
        //[BsonIgnoreIfNull]
        //public StudyProtocolEntity studyProtocol { get; set; }
    }
}
