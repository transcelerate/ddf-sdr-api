using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TransCelerate.SDR.Core.Entities.Study
{

    [BsonIgnoreExtraElements]
    public class AmendmentEntity
    {        
        public string amendmentDate { get; set; }
        public string version { get; set; }
    }
}
