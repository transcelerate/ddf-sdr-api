namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDataCollectionDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDescription { get; set; }
        public string EcrfLink { get; set; }
    }
}
