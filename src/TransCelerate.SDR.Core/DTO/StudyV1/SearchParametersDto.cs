namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class SearchParametersDto
    {
        public string StudyId { get; set; }

        public string StudyTitle { get; set; }

        public string Indication { get; set; }
        public string InterventionModel { get; set; }
        public string Phase { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
        public string Header { get; set; }
        public bool Asc { get; set; }

        public SearchParametersDto()
        {
            PageNumber = 1;
            PageSize = 20;
        }
    }

}
