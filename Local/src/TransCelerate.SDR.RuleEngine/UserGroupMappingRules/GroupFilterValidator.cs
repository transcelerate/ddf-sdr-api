using FluentValidation;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    /// <summary>
    /// This Class is the validator for Group Filter
    /// </summary>
    public class GroupFilterValidator : AbstractValidator<GroupFilterDTO>
    {
        public GroupFilterValidator()
        {
            RuleFor(x => x.GroupFieldName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.GroupFilterValues)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .Must(x => x.Count > 0).WithMessage(Constants.ValidationErrorMessage.GroupFilterEmptyError)
                .ForEach(x => x.SetValidator(new GroupFilterValuesValidator()));
        }
    }
}
