using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV3
{
    public class SearchResponseEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public CodeEntity StudyType { get; set; }

        public AliasCodeEntity StudyPhase { get; set; }

        public List<StudyIdentifierEntity> StudyIdentifiers { get; set; }

        public IEnumerable<List<IndicationEntity>> StudyIndications { get; set; }

        public IEnumerable<CodeEntity> InterventionModel { get; set; }
        public IEnumerable<string> StudyDesignIds { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int SDRUploadVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
