using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class WorkFlowItemMatrixEntity
    {
        [BsonElement(Utilities.Common.IdFieldPropertyName.MVP.Id)]
        public string WorkFlowItemMatrixId { get; set; }
        public List<MatrixEntity> Matrix { get; set; }
    }
}
