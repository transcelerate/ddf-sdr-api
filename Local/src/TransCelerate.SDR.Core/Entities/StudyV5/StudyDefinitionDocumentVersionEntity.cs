using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV5
{
    public class StudyDefinitionDocumentVersionEntity : IId
	{
        public string Id { get; set; }
        public string version { get; set; }
        public CodeEntity status { get; set; }
        public List<GovernanceDateEntity> DateValues { get; set; }
        public List<NarrativeContentEntity> Contents { get; set; }
        public List<string> ChildIds { get; set; }
        public List<CommentAnnotationEntity> Notes { get; set; }
        public string InstanceType { get; set; }
    }
}
