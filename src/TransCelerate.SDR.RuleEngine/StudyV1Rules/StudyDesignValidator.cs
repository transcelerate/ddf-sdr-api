using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV1
{
    /// <summary>
    /// This Class is the validator for StudyDesign
    /// </summary>
    public class StudyDesignValidator : AbstractValidator<StudyDesignDto>
    {
        public StudyDesignValidator()
        {
            RuleFor(x => x.Uuid)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.InterventionModel)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.TrialIntentType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.TrialType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyInvestigationalInterventions)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyIndications)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyObjectives)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyPopulations)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyCells)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyWorkflows)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.StudyEstimands)
                .Must(x => UniquenessArrayValidator.ValidateArrayV1(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}





