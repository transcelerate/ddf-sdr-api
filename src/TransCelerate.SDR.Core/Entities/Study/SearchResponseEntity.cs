using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{


    public class SearchResponse
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyType { get; set; }

        public string StudyPhase { get; set; }
        public string StudyStatus { get; set; }
        public string StudyTag { get; set; }

        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }

        public IEnumerable<List<StudyIndicationEntity>> StudyIndications { get; set; }

        public IEnumerable<IEnumerable<IEnumerable<List<InvestigationalInterventionEntity>>>> InvestigationalInterventions { get; set; }

        public DateTime EntryDateTime { get; set; }
        public string EntrySystem { get; set; }
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
