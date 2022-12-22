﻿namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class OrganisationDto : Iid
    {
        [Newtonsoft.Json.JsonProperty(Utilities.Common.IdFieldPropertyName.StudyV2.OrganisationId)]
        public string Id { get; set; }
        public string OrganisationIdentifier { get; set; }
        public string OrganisationIdentifierScheme { get; set; }
        public string OrganisationName { get; set; }
        public CodeDto OrganisationType { get; set; }
        public AddressDto OrganizationLegalAddress { get; set; }
    }
}
