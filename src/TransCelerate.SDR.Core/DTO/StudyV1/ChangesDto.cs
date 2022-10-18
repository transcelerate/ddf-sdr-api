using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV1
{
    public class ChangesDto
    {
        public DateTime EntryDateTime { get; set; }

        [JsonProperty("SDRUploadVersion")]
        public int SDRUploadVersion { get; set; }

        public List<string> Elements { get; set; }
    }
}
