using FluentValidation;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    /// <summary>
    /// This Class is the validator for POST User Endpoint
    /// </summary>
    public class PostUserToGroupValidator : AbstractValidator<PostUserToGroupsDTO>
    {
        public PostUserToGroupValidator()
        {
            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.Groups)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .Must(x => x.Count > 0).WithMessage(Constants.ValidationErrorMessage.SelectAtleastOneGroup)
               .ForEach(child =>
               {
                   child.ChildRules(x => x.RuleFor(x => x.GroupId)
                                            .Cascade(CascadeMode.Stop)
                                            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                                            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError));
                   child.ChildRules(x => x.RuleFor(x => x.GroupName)
                                            .Cascade(CascadeMode.Stop)
                                            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                                            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError));
               });
        }
    }
}
