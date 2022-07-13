using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Entities.StudyV1
{
    public class StudyDataCollectionEntity
    {
        public string Uuid { get; set; }
        public string StudyDataName { get; set; }
        public string StudyDataDesc { get; set; }
        public string CrfLink { get; set; }
    }
}
