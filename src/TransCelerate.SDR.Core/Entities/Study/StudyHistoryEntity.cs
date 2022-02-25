using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities
{
    public class StudyHistoryEntity
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public int studyVersion { get; set; }
        public DateTime entryDateTime { get; set; }
    }
}