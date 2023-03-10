using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.DTO.Study
{
    public class SearchTitleDTO
    {
        public SearchTitleClinicalStudy clinicalStudy { get; set; }
        public SearchTitleAuditTrail auditTrail { get; set; }
    }    

    public class SearchTitleClinicalStudy
    {
        public string studyId { get; set; }
        public string studyTitle { get; set; }
        public string studyTag { get; set; }
    }

    public class SearchTitleAuditTrail
    {
        public string entryDateTime { get; set; }
        public int studyVersion { get; set; }
        public string UsdmVersion { get; set; }
    }
}
