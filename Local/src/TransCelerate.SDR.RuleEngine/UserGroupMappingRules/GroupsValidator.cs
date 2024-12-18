using FluentValidation;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngine
{
    /// <summary>
    /// This Class is the validator for POST Group Endpoint
    /// </summary>
    public class GroupsValidator : AbstractValidator<SDRGroupsDTO>
    {
        public GroupsValidator()
        {
            RuleFor(x => x.GroupName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);
            RuleFor(x => x.GroupFilter)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .Must(x => x.Count > 0).WithMessage(Constants.ValidationErrorMessage.GroupFilterEmptyError)
                .ForEach(x => x.SetValidator(new GroupFilterValidator()));
            RuleFor(x => x.Permission)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .Must(x => Enum.GetNames(typeof(Permissions)).Contains(x.Trim())).WithMessage(Constants.ValidationErrorMessage.InvalidPermissionValue);
            RuleFor(x => x.GroupCreatedOn)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
            RuleFor(x => x.GroupModifiedOn)
                .Must(x => DateValidationHelper.IsValid(x))
                .WithMessage(Constants.ValidationErrorMessage.ValidDateError);
        }
    }
}
