

namespace TransCelerate.SDR.Core.DTO.eCPT
{
    public class ECPTDataDto
    {
        public TitlePageDto TitlePage { get; set; }

        public ProtocolSummaryDto ProtocolSummary { get; set; }

        public PageHeaderDto PageHeader { get; set; }

        public StudyPopulationDto StudyPopulation { get; set; }
        public IntroductionDto Introduction { get; set; }
        public StudyDesignCptDto StudyDesign { get; set; }
        public StudyInterventionsAndConcomitantTherapyDto StudyInterventionsAndConcomitantTherapy { get; set; }
        public StatisticalConsiderationsDto StatisticalConsiderations { get; set; }
        public ObjectivesEndpointsAndEstimandsDto ObjectivesEndpointsAndEstimands { get; set; }
    }
}
