﻿namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class TitlePageDto
    {
        public string Acronym { get; set; }
        public string AmendmentNumber { get; set; }
        public string ApprovalDate { get; set; }
        public string ConditionDisease { get; set; }
        public string RegulatoryAgencyId { get; set; }
        public string RegulatoryAgencyNumber { get; set; }
        public string SponsorLegalAddress { get; set; }
        public string SponsorName { get; set; }
        public string StudyPhase { get; set; }
        public ProtocolDto Protocol { get; set; }
    }
}
