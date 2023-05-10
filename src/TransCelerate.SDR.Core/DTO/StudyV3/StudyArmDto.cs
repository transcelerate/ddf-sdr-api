namespace TransCelerate.SDR.Core.DTO.StudyV3
{
    public class StudyArmDto : IId
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV3.StudyArmId)]
        public string Id { get; set; }
        public string StudyArmDataOriginDescription { get; set; }
        public CodeDto StudyArmDataOriginType { get; set; }
        public string StudyArmDescription { get; set; }
        public string StudyArmName { get; set; }
        public CodeDto StudyArmType { get; set; }
    }
}
