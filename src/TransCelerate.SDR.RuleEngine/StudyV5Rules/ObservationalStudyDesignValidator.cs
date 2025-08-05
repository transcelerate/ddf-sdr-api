using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine.Common;

namespace TransCelerate.SDR.RuleEngineV5
{
    /// <summary>
    /// This Class is the validator for ObservationalStudyDesign
    /// </summary>
    public class ObservationalStudyDesignValidator : AbstractValidator<ObservationalStudyDesignDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ObservationalStudyDesignValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InstanceType)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.InstanceType)), ApplyConditionTo.AllValidators)
               .Must(x => this.GetType().Name.RemoveValidator() == x).WithMessage(Constants.ValidationErrorMessage.InstanceTypeError);

            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Rationale)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Rationale)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Activities)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Activities)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Activities)
                .SetValidator(new ActivityValidator(_httpContextAccessor));

            RuleFor(x => x.StudyPhase)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.StudyPhase)), ApplyConditionTo.AllValidators)
                .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.BiospecimenRetentions)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.BiospecimenRetentions)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.BiospecimenRetentions)
                .SetValidator(new BiospecimenRetentionValidator(_httpContextAccessor));

            RuleFor(x => x.StudyType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.StudyType)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.TherapeuticAreas)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.TherapeuticAreas)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.TherapeuticAreas)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.Characteristics)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Characteristics)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Characteristics)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.Notes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Notes)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Notes)
                .SetValidator(new CommentAnnotationValidator(_httpContextAccessor));

            RuleFor(x => x.Encounters)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Encounters)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Encounters)
                .SetValidator(new EncounterValidator(_httpContextAccessor));

            RuleFor(x => x.Estimands)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Estimands)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Estimands)
                .SetValidator(new EstimandValidator(_httpContextAccessor));

            RuleFor(x => x.Indications)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Indications)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Indications)
                .SetValidator(new IndicationValidator(_httpContextAccessor));

            RuleFor(x => x.Objectives)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Objectives)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Objectives)
                .SetValidator(new ObjectiveValidator(_httpContextAccessor));

            RuleFor(x => x.ScheduleTimelines)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.ScheduleTimelines)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.ScheduleTimelines)
                .SetValidator(new ScheduleTimelineValidator(_httpContextAccessor));

            RuleFor(x => x.Arms)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Arms)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Arms)
                .SetValidator(new StudyArmValidator(_httpContextAccessor));

            RuleFor(x => x.StudyCells)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.StudyCells)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.StudyCells)
                .SetValidator(new StudyCellValidator(_httpContextAccessor));

            RuleFor(x => x.DocumentVersions)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.DocumentVersions)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.DocumentVersions)
                .SetValidator(new StudyDefinitionDocumentVersionValidator(_httpContextAccessor));

            RuleFor(x => x.Elements)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Elements)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Elements)
                .SetValidator(new StudyElementValidator(_httpContextAccessor));

            RuleFor(x => x.StudyInterventionIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.StudyInterventionIds)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateStringList(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleFor(x => x.Epochs)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Epochs)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.Epochs)
                .SetValidator(new StudyEpochValidator(_httpContextAccessor));

            RuleFor(x => x.Population)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Population)), ApplyConditionTo.AllValidators)
                .SetValidator(new StudyDesignPopulationValidator(_httpContextAccessor));

            RuleFor(x => x.SubTypes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.SubTypes)), ApplyConditionTo.AllValidators);

            RuleForEach(x => x.SubTypes)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.TimePerspective)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.TimePerspective)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.Model)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.Model)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.SamplingMethod)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(ObservationalStudyDesignValidator), nameof(ObservationalStudyDesignDto.SamplingMethod)), ApplyConditionTo.AllValidators)
                .SetValidator(new CodeValidator(_httpContextAccessor));
        }
    }
}





