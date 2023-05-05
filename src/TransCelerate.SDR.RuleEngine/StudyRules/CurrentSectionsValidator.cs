using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class CurrenSectionsValidator : AbstractValidator<CurrentSectionsDTO>
    {
        /// <summary>
        /// Validator for CurrenSections
        /// </summary>
        public CurrenSectionsValidator()
        {
            RuleFor(x => x.PlannedWorkflows)
                .ForEach(y => y.SetValidator(new PlannedWorkFlowValidator()));

            RuleFor(x => x.StudyPopulations)
                .ForEach(y => y.SetValidator(new StudyPopulationValidator()));

            RuleFor(x => x.StudyCells)
                .ForEach(y => y.SetValidator(new StudyCellsValidator()));

            RuleFor(x => x.InvestigationalInterventions)
                .ForEach(y => y.SetValidator(new InvestigationalInterventionValidatior()));

            RuleFor(x => x.StudyDesigns)
                .ForEach(y => y.SetValidator(new StudyDesignValidator()));

            RuleFor(x => x.Objectives)
                .ForEach(y => y.SetValidator(new StudyObjectivesValidator()));

            RuleFor(x => x.StudyIndications)
                .ForEach(y => y.SetValidator(new StudyIndicationValidator()));
        }
    }
}
