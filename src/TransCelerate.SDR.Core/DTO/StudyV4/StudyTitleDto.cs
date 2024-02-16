﻿namespace TransCelerate.SDR.Core.DTO.StudyV4
{
    public class StudyTitleDto : IId
    {        
        public string Id { get; set; }
        public string Text { get; set; }
        public CodeDto Type { get; set; }
        public string InstanceType { get; set; }
    }
}
