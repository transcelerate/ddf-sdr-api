namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDataDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.StudyDataId)]
        public string Id { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDescription { get; set; }
        public string EcrfLink { get; set; }
    }
}
