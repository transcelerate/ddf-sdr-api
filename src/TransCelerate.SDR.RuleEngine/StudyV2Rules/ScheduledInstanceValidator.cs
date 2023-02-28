using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using TransCelerate.SDR.Core.Utilities;
using System.Linq;

namespace TransCelerate.SDR.RuleEngineV2
{
    public class ScheduledInstanceValidator : AbstractValidator<ScheduledInstanceDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduledInstanceValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.ScheduledInstanceType)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .Must(x => Enum.GetNames(typeof(ScheduledInstanceType)).Contains(x))
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[Constants.UsdmVersion], nameof(ScheduledInstanceValidator), nameof(ScheduledInstanceDto.ScheduledInstanceType)), ApplyConditionTo.AllValidators);
        }
    }
}
