using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Study
{
    

    public class SearchResponse
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public string studyType { get; set; }

        public string studyPhase { get; set; }
        public string studyStatus { get; set; }
        public string studyTag { get; set; }
        
        public List<StudyIdentifierEntity> studyIdentifiers { get; set; }

        public IEnumerable<List<StudyIndicationEntity>> studyIndications { get; set; }

        public IEnumerable<IEnumerable<IEnumerable<List<InvestigationalInterventionEntity>>>> investigationalInterventions { get; set; }

        public DateTime entryDateTime { get; set; }
        public string entrySystem { get; set; }
        public int studyVersion { get; set; }
    }
}
