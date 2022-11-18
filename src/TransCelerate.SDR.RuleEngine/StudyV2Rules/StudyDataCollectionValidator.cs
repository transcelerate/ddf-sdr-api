using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for StudyDataCollection
    /// </summary>
    public class StudyDataCollectionValidator : AbstractValidator<StudyDataDto>
    {
        public StudyDataCollectionValidator()
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyDataId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.StudyDataId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.EcrfLink)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyDataName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.StudyDataDescription)
             .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
             .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
        }
    }
}





