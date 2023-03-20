namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchTitleParametersDTO
    {
        public string StudyTitle { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool GroupByStudyId { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }

        public SearchTitleParametersDTO()
        {
            PageNumber = 1;
            PageSize = 20;
        }
    }
}
