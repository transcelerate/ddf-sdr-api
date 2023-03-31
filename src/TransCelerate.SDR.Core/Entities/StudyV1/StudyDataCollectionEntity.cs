namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyDataCollectionEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDesc { get; set; }
        public string CrfLink { get; set; }
    }
}
