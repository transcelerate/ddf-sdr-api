using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.Core.DTO.Study
{
    #region Depricated response classes
    //public class InterventionModelResponse
    //{
    //    public string interventionModel { get; set; }
    //}

    //public class StudyIdentifierResponse
    //{
    //    public List<StudyIdentifierDTO> studyIdentifier { get; set; }

    //}

    //public class StudyPhaseResponse
    //{
    //    public string studyPhase { get; set; }

    //}

    //public class StudyProtocolResponse
    //{
    //    public StudyProtocolDTO studyProtocol { get; set; }

    //}
    //public class InvestigationalInterventionResponse
    //{
    //    public List<InvestigationalInterventionDTO> investigationalIntervention { get; set; }

    //}

    //public class StudyTargetPopulationResponse
    //{
    //    public List<StudyTargetPopulationDTO> studyTargetPopulation { get; set; }

    //}
    //public class StudyObjectivesResponse
    //{
    //    public List<StudyObjectiveDTO> studyObjective { get; set; }

    //}

    //public class StudyTitleResponse
    //{
    //    public string studyTitle { get; set; }

    //}

    //public class StudyTypeResponse
    //{
    //    public string studyType { get; set; }

    //}

    //public class StudyIndicationResponse
    //{
    //    public StudyTargetIndicationDTO studyTargetIndication { get; set; }

    //} 
    #endregion
    public class GetStudyAuditDTO
    {    
        public string studyId { get; set; }     
        public List<AuditTrailDTO> auditTrail { get; set; }
    }
}
