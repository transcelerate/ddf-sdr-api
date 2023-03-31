using FluentValidation;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    /// <summary>
    /// This Class is the validator for GET User and Groups Endpoints
    /// </summary>
    public class UserGroupsQueryParametersValidator : AbstractValidator<UserGroupsQueryParameters>
    {
        public UserGroupsQueryParametersValidator()
        {
            RuleFor(x => x.SortOrder)
                 .Must(x => Enum.GetNames(typeof(SortOrder)).Contains(x.Trim()))
                 .When(x => !String.IsNullOrEmpty(x.SortOrder))
                 .WithMessage(Constants.ValidationErrorMessage.InvalidSortOrder);

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);


        }
    }
}
