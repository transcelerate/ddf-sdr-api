using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class WorkFlowItemMatrixEntity
    {
        [BsonElement("id")]
        public string workFlowItemMatrixId { get; set; }
        public List<MatrixEntity> matrix { get; set; }
    }
}
