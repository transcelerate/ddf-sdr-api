using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine
{
    public  class GroupsValidator : AbstractValidator<SDRGroupsDTO>
    {
        public GroupsValidator()
        {
            RuleFor(x=>x.groupName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.groupFilter)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .Must(x=>x.Count>0).WithMessage(Constants.ValidationErrorMessage.GroupFilterEmptyError);
            RuleFor(x => x.permission)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x=> Enum.GetNames(typeof(Permissions)).Contains(x.Trim())).WithMessage(Constants.ValidationErrorMessage.InvalidPermissionValue);
            RuleFor(x=>x.groupCreatedOn)    
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x=>x.groupModifiedOn)              
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
        }
    }
}
