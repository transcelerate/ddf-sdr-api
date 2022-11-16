using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV2
{
    public class StudyDataCollectionEntity : IUuid
    {
        public string Uuid { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDescription { get; set; }
        public string EcrfLink { get; set; }
    }
}
