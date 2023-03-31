using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Common
{
    public class SearchResponseEntity : ICheckAccess
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public object StudyType { get; set; }

        public object StudyPhase { get; set; }

        public List<object> StudyIdentifiers { get; set; }

        public IEnumerable<IEnumerable<string>> StudyIndications { get; set; }

        public IEnumerable<object> InterventionModel { get; set; }
        public IEnumerable<string> StudyDesignIds { get; set; }
        public IEnumerable<IEnumerable<string>> StudyDesignIdsMVP { get; set; }

        public IEnumerable<IEnumerable<string>> StudyIndicationsMVP { get; set; }

        public IEnumerable<IEnumerable<IEnumerable<IEnumerable<string>>>> InterventionModelMVP { get; set; }

        public DateTime EntryDateTime { get; set; }
        public int SDRUploadVersion { get; set; }
        public string UsdmVersion { get; set; }
        public bool HasAccess { get; set; }
    }
}
