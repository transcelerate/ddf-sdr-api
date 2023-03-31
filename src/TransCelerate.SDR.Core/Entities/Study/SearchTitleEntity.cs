using System;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class SearchTitleEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string StudyTag { get; set; }
        public string StudyType { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int StudyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
