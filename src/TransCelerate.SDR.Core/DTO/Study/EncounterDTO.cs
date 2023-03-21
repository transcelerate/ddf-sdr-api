namespace TransCelerate.SDR.Core.DTO.Study
{
    public class EncounterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactMode { get; set; }
        public string EnvironmentalSetting { get; set; }
        public RuleDTO StartRule { get; set; }
        public RuleDTO EndRule { get; set; }
        public EpochDTO Epoch { get; set; }
    }
}
