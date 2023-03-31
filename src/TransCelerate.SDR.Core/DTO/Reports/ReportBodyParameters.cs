using System;

namespace TransCelerate.SDR.Core.DTO.Reports
{
    public class ReportBodyParameters
    {
        public int Days { get; set; }
        public string Operation { get; set; }
        public int ResponseCode { get; set; }
        public int RecordNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public bool FilterByTime { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
    }
}
