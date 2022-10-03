using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class ChangeAuditEntity
    {
        public string Study_uuid { get; set; }

        public List<ChangesEntity> Changes { get; set; }
    }
}
