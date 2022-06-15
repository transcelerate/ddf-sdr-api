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
    public class UserGroupsQueryParametersValidator : AbstractValidator<UserGroupsQueryParameters>
    {
        public UserGroupsQueryParametersValidator()
        {
            RuleFor(x => x.sortOrder)
                 .Must(x => Enum.GetNames(typeof(SortOrder)).Contains(x.Trim()))
                 .When(x=> !String.IsNullOrEmpty(x.sortOrder))
                 .WithMessage(Constants.ValidationErrorMessage.InvalidSortOrder);

            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);

            RuleFor(x => x.pageSize)
                .GreaterThanOrEqualTo(0)
                .WithMessage(Constants.ValidationErrorMessage.EnterValidNumber);


        }
    }
}
