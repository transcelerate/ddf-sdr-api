﻿namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class AddressEntity
    {
        public string Text { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public CodeEntity Country { get; set; }
    }
}
