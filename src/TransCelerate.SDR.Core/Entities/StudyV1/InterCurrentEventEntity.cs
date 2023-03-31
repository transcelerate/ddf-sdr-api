namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class InterCurrentEventEntity : IUuid
    {
        public string Uuid { get; set; }
        public string IntercurrentEventDesc { get; set; }
        public string IntercurrentEventName { get; set; }
        public string IntercurrentEventStrategy { get; set; }
    }
}
