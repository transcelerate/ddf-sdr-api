﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for Analysis Population
    /// </summary>
    public class AnalysisPopulationValidator : AbstractValidator<AnalysisPopulationDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnalysisPopulationValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(AnalysisPopulationValidator), nameof(AnalysisPopulationDto.Id)), ApplyConditionTo.AllValidators);


            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(AnalysisPopulationValidator), nameof(AnalysisPopulationDto.Description)), ApplyConditionTo.AllValidators);
        }
    }
}





