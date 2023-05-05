using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Procedure
    /// </summary>
    public class ProcedureValidator : AbstractValidator<ProcedureDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProcedureValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.ProcedureId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.ProcedureId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ProcedureValidator), nameof(ProcedureDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ProcedureType)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ProcedureValidator), nameof(ProcedureDto.ProcedureType)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.ProcedureCode)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ProcedureValidator), nameof(ProcedureDto.ProcedureCode)), ApplyConditionTo.AllValidators)
               .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.ProcedureIsConditional)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ProcedureValidator), nameof(ProcedureDto.ProcedureIsConditional)), ApplyConditionTo.AllValidators)
                .Must(ValidateDatatype.ValidateBoolean).WithMessage(Constants.ValidationErrorMessage.BooleanValidationFailed);

            RuleFor(x => x.ProcedureIsConditionalReason)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ProcedureValidator), nameof(ProcedureDto.ProcedureIsConditionalReason)), ApplyConditionTo.AllValidators);
        }
    }
}





