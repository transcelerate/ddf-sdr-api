using System;

namespace TransCelerate.SDR.Core.Entities
{
    public class StudyHistoryEntity
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public int StudyVersion { get; set; }
        public DateTime EntryDateTime { get; set; }

        public string StudyType { get; set; }
        public string UsdmVersion { get; set; }
    }
}