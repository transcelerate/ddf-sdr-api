using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class PostUserToGroupValidator : AbstractValidator<PostUserToGroupsDTO>
    {
        public PostUserToGroupValidator()
        {
            RuleFor(x => x.email)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.groups)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .Must(x => x.Count > 0).WithMessage(Constants.ValidationErrorMessage.SelectAtleastOneGroup)
               .ForEach(child =>
               {
                   child.ChildRules(x => x.RuleFor(x => x.groupId)
                                            .Cascade(CascadeMode.Stop)
                                            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                                            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError));
                   child.ChildRules(x => x.RuleFor(x => x.groupName)
                                            .Cascade(CascadeMode.Stop)
                                            .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                                            .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError));
               });
        }
    }
}
