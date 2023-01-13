using System.Collections.Generic;


namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class StudyeCPTDto
    {
        public string  StudyId { get; set; }
        public string  StudyTitle { get; set; }
        public string StudyDesignId { get; set; }
        public string StudyDesignName { get; set; }
        public eCPTDataDto ECPTData { get; set; }
    }
}
