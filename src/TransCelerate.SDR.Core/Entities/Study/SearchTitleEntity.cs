using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.Study
{
    public class SearchTitleEntity
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public string studyTag { get; set; }
        public string studyType { get; set; }
        public DateTime entryDateTime { get; set; }
        public int studyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
