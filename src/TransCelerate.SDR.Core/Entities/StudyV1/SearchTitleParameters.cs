using System;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class SearchTitleParameters
    {
        public string StudyTitle { get; set; }
        public string StudyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool GroupByStudyId { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
    }
}
