using System;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class SearchParameters
    {
        public string StudyId { get; set; }
        public string StudyTitle { get; set; }
        public string Indication { get; set; }
        public string InterventionModel { get; set; }
        public string Phase { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Header { get; set; }
        public bool Asc { get; set; }
    }
}
