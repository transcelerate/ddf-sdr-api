namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AssignedPersonDto : IId
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public PersonNameDto PersonName { get; set; }
        public string JobTitle { get; set; }
        public string InstanceType { get; set; }
        public string OrganizationId { get; set; }
	}
}
