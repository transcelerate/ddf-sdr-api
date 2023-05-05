using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyDesign
    /// </summary>
    public class StudyDesignValidator : AbstractValidator<StudyDesignDto>
    {
        public StudyDesignValidator()
        {
            RuleFor(x => x.InterventionModel)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.TrialIntentType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.TrialType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .ForEach(x => x.SetValidator(new CodeValidator()));

            RuleFor(x => x.StudyIndications)
                .ForEach(x => x.SetValidator(new IndicationValidator()));

            RuleFor(x => x.StudyInvestigationalInterventions)
                .ForEach(x => x.SetValidator(new InvestigationalInterventionValidator()));

            RuleFor(x => x.StudyObjectives)
                .ForEach(x => x.SetValidator(new StudyObjectiveValidator()));

            RuleFor(x => x.StudyPopulations)
                .ForEach(x => x.SetValidator(new StudyDesignPopulationValidator()));

            RuleFor(x => x.StudyCells)
                .ForEach(x => x.SetValidator(new StudyCellsValidator()));

            RuleFor(x => x.StudyWorkflows)
                .ForEach(x => x.SetValidator(new WorkflowValidator()));

            RuleFor(x => x.StudyEstimands)
                .ForEach(x => x.SetValidator(new StudyEstimandsValidator()));
        }
    }
}





