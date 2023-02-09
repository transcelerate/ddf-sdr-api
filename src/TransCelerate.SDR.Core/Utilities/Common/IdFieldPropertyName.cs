using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public class IdFieldPropertyName
    {
        public struct StudyV2
        {
            public const string StudyIdentifierId = "studyIdentifierId";
            public const string OrganisationId = "organisationId";
            public const string StudyProtocolVersionId = "studyProtocolVersionId";
            public const string StudyDesignId = "studyDesignId";
            public const string StudyCellId = "studyCellId";
            public const string StudyArmId = "studyArmId";
            public const string StudyEpochId = "studyEpochId";
            public const string StudyElementId = "studyElementId";
            public const string IndicationId = "indicationId";
            public const string InvestigationalInterventionId = "investigationalInterventionId";
            public const string ObjectiveId = "objectiveId";
            public const string EndpointId = "endpointId";
            public const string StudyDesignPopulationId = "studyDesignPopulationId";
            public const string WorkflowId = "workflowId";
            public const string WorkflowItemId = "workflowItemId";
            public const string ActivityId = "activityId";
            public const string ProcedureId = "procedureId";
            public const string StudyDataId = "studyDataId";
            public const string EstimandId = "estimandId";
            public const string AnalysisPopulationId = "analysisPopulationId";
            public const string IntercurrentEventId = "intercurrentEventId";
            public const string CodeId = "codeId";
            public const string AliasCodeId = "aliasCodeId";
            public const string TransitionRuleId = "transitionRuleId";
            public const string EncounterId = "encounterId";
            public const string BiomedicalConceptCategoryId = "biomedicalConceptCategoryId";
            public const string BiomedicalConceptId = "biomedicalConceptId";
            public const string BcPropertyId = "bcPropertyId";
            public const string BcSurrogateId = "bcSurrogateId";
            public const string ResponseCodeId = "responseCodeId";
        }
        public struct StudyV1
        {
            public const string StudyId = "studyId";
            public const string Uuid = "uuid";
        }

        public struct Common
        {
            public const string UsdmVersion = "usdm-version";
            public const string UsdmVersions = "usdm-versions";
            public const string ApiVersion = "api-version";
            public const string SDRVersions = "SDRVersions";
        }
    }
}
