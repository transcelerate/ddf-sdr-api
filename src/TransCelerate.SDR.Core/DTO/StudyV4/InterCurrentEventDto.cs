﻿namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class IntercurrentEventDto : IId
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Strategy { get; set; }
        public string InstanceType { get; set; }
    }
}
