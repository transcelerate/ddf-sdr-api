using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{
   
    [BsonIgnoreExtraElements]
    public class StudyProtocolEntity
    {        
        public string protocolId { get; set; }
        public string studyProtocolVersion { get; set; }       
    }
}
