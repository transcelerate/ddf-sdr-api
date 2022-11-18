﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    /// <summary>
    /// This class is a DTO for response of GET Study History Endpoint
    /// </summary>
    public class StudyHistoryResponseDto
    {
        public string StudyId { get; set; }
        [JsonProperty("SDRUploadVersion")]
        public List<UploadVersionDto> SDRUploadVersion { get; set; }
    }

    public class UploadVersionDto
    {
        public int UploadVersion { get; set; }
        public string StudyTitle { get; set; }
        public string StudyVersion { get; set; }
        public List<StudyIdentifierDto> StudyIdentifiers { get; set; }
        public List<string> ProtocolVersions { get; set; }
    }
}