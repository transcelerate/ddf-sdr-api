namespace TransCelerate.SDR.Core.DTO.Reports
{
    public class SystemUsageReportDTO
    {
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Operation { get; set; }
        public string Api { get; set; }
        public string RequestDate { get; set; }
        public string CallerIpAddress { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseCodeDescription { get; set; }
    }
}
