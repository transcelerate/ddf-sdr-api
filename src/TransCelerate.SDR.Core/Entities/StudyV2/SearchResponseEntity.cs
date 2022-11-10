using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class SearchResponseEntity
    {
        public string Uuid { get; set; }
        public string StudyTitle { get; set; }
        public CodeEntity StudyType { get; set; }

        public CodeEntity StudyPhase { get; set; }        

        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }

        public IEnumerable<List<IndicationEntity>> StudyIndications { get; set; }

        public IEnumerable<List<CodeEntity>> InterventionModel { get; set; }

        public DateTime EntryDateTime { get; set; }
        public int SDRUploadVersion { get; set; }
    }
}
