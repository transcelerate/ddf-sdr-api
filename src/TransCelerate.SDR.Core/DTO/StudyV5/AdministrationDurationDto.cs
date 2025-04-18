﻿namespace TransCelerate.SDR.Core.DTO.StudyV5
{
    public class AdministrationDurationDto : IId
    {
        public string Id { get; set; }
        public QuantityDto Quantity { get; set; }
        public string Description { get; set; }
        public object DurationWillVary { get; set; }
        public string ReasonDurationWillVary { get; set; }
        public string InstanceType { get; set; }
    }
}
