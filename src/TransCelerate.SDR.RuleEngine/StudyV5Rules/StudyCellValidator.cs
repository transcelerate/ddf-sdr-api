﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for StudyCells
    /// </summary>
    public class StudyCellValidator : AbstractValidator<StudyCellDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyCellValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellValidator), nameof(StudyCellDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
             .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
             .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
             .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellValidator), nameof(StudyCellDto.InstanceType)), ApplyConditionTo.AllValidators)
             .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.ElementIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellValidator), nameof(StudyCellDto.ElementIds)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.ArmId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellValidator), nameof(StudyCellDto.ArmId)), ApplyConditionTo.AllValidators);                

            RuleFor(x => x.EpochId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyCellValidator), nameof(StudyCellDto.EpochId)), ApplyConditionTo.AllValidators);                

        }
    }
}





