namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class StudyDataCollectionDto : IUuid
    {
        public string Uuid { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDesc { get; set; }
        public string CrfLink { get; set; }
    }
}
