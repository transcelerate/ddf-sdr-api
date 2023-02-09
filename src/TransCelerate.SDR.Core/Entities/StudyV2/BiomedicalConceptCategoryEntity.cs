using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class BiomedicalConceptCategoryEntity : Iid
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement(Utilities.Common.IdFieldPropertyName.StudyV2.BiomedicalConceptCategoryId)]
        public string Id { get; set; }
        public List<string> BcCategoryParentIds { get; set; }
        public List<string> BcCategoryChildrenIds { get; set; }
        public string BcCategoryName { get; set; }
        public string BcCategoryDescription { get; set; }
        public List<string> BcCategoryMemberIds { get; set; }
    }
}
