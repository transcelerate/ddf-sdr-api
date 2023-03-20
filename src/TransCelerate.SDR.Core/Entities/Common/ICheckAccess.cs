namespace TransCelerate.SDR.Core.Entities.Common
{
    public interface ICheckAccess
    {
        public string StudyId { get; set; }
        public object StudyType { get; set; }
        public string UsdmVersion { get; set; }
        public bool HasAccess { get; set; }
    }
}
