﻿using System.Collections.Generic;

namespace TransCelerate.SDR.Core.DTO.StudyV2
{
    public class ActivityDto : IUuid
    {
        public string Uuid { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityName { get; set; }
        public List<DefinedProcedureDto> DefinedProcedures { get; set; }
        public string NextActivityId { get; set; }
        public string PreviousActivityId { get; set; }
        public List<StudyDataCollectionDto> StudyDataCollection { get; set; }
    }
}
