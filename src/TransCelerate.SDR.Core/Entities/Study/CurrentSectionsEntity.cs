using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class CurrentSectionsEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string CurrentSectionsId { get; set; }
        public string SectionType { get; set; }

        [BsonIgnoreIfNull]
        public List<PlannedWorkFlowEntity> PlannedWorkflows { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyPopulationEntity> StudyPopulations { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyCellEntity> StudyCells { get; set; }

        [BsonIgnoreIfNull]
        public List<InvestigationalInterventionEntity> InvestigationalInterventions { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyDesignEntity> StudyDesigns { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyObjectiveEntity> Objectives { get; set; }

        [BsonIgnoreIfNull]
        public List<StudyIndicationEntity> StudyIndications { get; set; }
        //Removed Study Protocol
        //[BsonIgnoreIfNull]
        //public StudyProtocolEntity studyProtocol { get; set; }
    }
}
