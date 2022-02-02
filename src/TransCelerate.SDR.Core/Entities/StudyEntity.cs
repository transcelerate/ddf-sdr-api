using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace TransCelerate.SDR.Core.Entities.Study
{
    [BsonIgnoreExtraElements]
    public class StudyEntity
    {
        public Object _id { get; set; }
        public ClinicalStudyEntity clinicalStudy { get; set; }       
        public AuditTrailEntity auditTrail { get; set; }
    }
    

   
}
