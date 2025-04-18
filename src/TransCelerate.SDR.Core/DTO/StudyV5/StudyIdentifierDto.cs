﻿namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class StudyIdentifierDto : IId
    {        
        public string Id { get; set; }
        public string StudyIdentifier { get; set; }
        public OrganizationDto StudyIdentifierScope { get; set; }
        public string InstanceType { get; set; }
    }
}
