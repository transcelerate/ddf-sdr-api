using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class PostStudyElementsCheck
    {
        public static bool StudyComparison(StudyEntity incoming, StudyEntity existing)
        {
            existing.auditTrail.entryDateTime = incoming.auditTrail.entryDateTime;
            existing.auditTrail.entryDateTime = incoming.auditTrail.entryDateTime;          
            existing.auditTrail.entrySystem = incoming.auditTrail.entrySystem;         
            existing.auditTrail.entrySystemId = incoming.auditTrail.entrySystemId;
            incoming._id = existing._id;
            incoming.auditTrail.studyVersion = existing.auditTrail.studyVersion;
            var incomingJObject = JObject.Parse(JsonConvert.SerializeObject(incoming));
            var existingJObject = JObject.Parse(JsonConvert.SerializeObject(existing));

            var areEqual = JToken.DeepEquals(incomingJObject, existingJObject);

            return areEqual;
        }
    }
}
