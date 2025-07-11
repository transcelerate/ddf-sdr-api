namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class BiospecimenRetentionDto : IId
    {        
        public string Id { get; set; }
        public string InstanceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public bool IsRetained { get; set; }
        public bool IncludesDNA { get; set; }
       
	}
}
