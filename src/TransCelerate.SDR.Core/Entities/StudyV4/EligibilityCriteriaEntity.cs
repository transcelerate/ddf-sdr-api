using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV4
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    [MongoDB.Bson.Serialization.Attributes.BsonNoId]
    public class EligibilityCriteriaEntity : SyntaxTemplateEntity
    {
        public override string InstanceType { get; set; } = nameof(Utilities.SyntaxTemplateInstanceType.ELIGIBILITY_CRITERIA);
        public CodeEntity Category { get; set; }
        public string Identifier { get; set; }
        public string PreviousId { get; set; }
        public string NextId { get; set; }
        public string ContextId { get; set; }
    }
}
