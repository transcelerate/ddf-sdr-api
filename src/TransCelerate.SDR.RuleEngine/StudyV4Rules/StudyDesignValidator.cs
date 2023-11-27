using FluentValidation;
using Microsoft.AspNetCore.Http;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV4
{
    /// <summary>
    /// This Class is the validator for StudyDesign
    /// </summary>
    public class StudyDesignValidator : AbstractValidator<StudyDesignDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudyDesignValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Id)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Label)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
              .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
              .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Label)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.InterventionModel)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.InterventionModel)), ApplyConditionTo.AllValidators)
               .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.BlindingSchema)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.BlindingSchema)), ApplyConditionTo.AllValidators)
               .SetValidator(new AliasCodeValidator(_httpContextAccessor));

            RuleFor(x => x.TrialIntentTypes)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.TrialIntentTypes)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.TrialIntentTypes)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.TrialType)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.TrialType)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.TrialType)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.StudyInterventions)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.StudyInterventions)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyInterventions)
                .SetValidator(new StudyInterventionValidator(_httpContextAccessor));

            RuleFor(x => x.Indications)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Indications)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Indications)
                .SetValidator(new IndicationValidator(_httpContextAccessor));

            RuleFor(x => x.Objectives)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Objectives)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Objectives)
                .SetValidator(new ObjectiveValidator(_httpContextAccessor));

            RuleFor(x => x.Populations)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Populations)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Populations)
                .SetValidator(new StudyDesignPopulationValidator(_httpContextAccessor));

            RuleFor(x => x.StudyCells)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.StudyCells)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.StudyCells)
                .SetValidator(new StudyCellsValidator(_httpContextAccessor));

            RuleFor(x => x.ScheduleTimelines)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.ScheduleTimelines)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.ScheduleTimelines)
                .SetValidator(new ScheduleTimelinesValidator(_httpContextAccessor));

            RuleFor(x => x.Estimands)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Estimands)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Estimands)
                .SetValidator(new StudyEstimandsValidator(_httpContextAccessor));

            RuleFor(x => x.TherapeuticAreas)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.TherapeuticAreas)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.TherapeuticAreas)
                .SetValidator(new CodeValidator(_httpContextAccessor));

            RuleFor(x => x.Activities)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Activities)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Activities)
                .SetValidator(new ActivityValidator(_httpContextAccessor));

            RuleFor(x => x.Encounters)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Encounters)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Encounters)
                .SetValidator(new EncounterValidator(_httpContextAccessor));

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Name)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Description)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.Rationale)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Rationale)), ApplyConditionTo.AllValidators);

            RuleFor(x => x.BiomedicalConcepts)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.BiomedicalConcepts)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.BiomedicalConcepts)
                .SetValidator(new BiomedicalConceptValidator(_httpContextAccessor));

            RuleFor(x => x.BcCategories)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.BcCategories)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.BcCategories)
                .SetValidator(new BiomedicalConceptCategoryValidator(_httpContextAccessor));

            RuleFor(x => x.BcSurrogates)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.BcSurrogates)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.BcSurrogates)
                .SetValidator(new BiomedicalConceptSurrogateValidator(_httpContextAccessor));

            RuleFor(x => x.Arms)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Arms)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Arms)
                .SetValidator(new StudyArmValidator(_httpContextAccessor));

            RuleFor(x => x.Epochs)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Epochs)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Epochs)
                .SetValidator(new StudyEpochValidator(_httpContextAccessor));

            RuleFor(x => x.Elements)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
                .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Elements)), ApplyConditionTo.AllValidators)
                .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Elements)
                .SetValidator(new StudyElementsValidator(_httpContextAccessor));

            RuleFor(x => x.EligibilityCriteria)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.EligibilityCriteria)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.EligibilityCriteria)
                .SetValidator(new EligibilityCriteriaValidator(_httpContextAccessor));

            RuleFor(x => x.Dictionaries)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError)
               .When(x => RulesHelper.GetConformanceRules(_httpContextAccessor.HttpContext.Request.Headers[IdFieldPropertyName.Common.UsdmVersion], nameof(StudyDesignValidator), nameof(StudyDesignDto.Dictionaries)), ApplyConditionTo.AllValidators)
               .Must(x => UniquenessArrayValidator.ValidateArrayV4(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);

            RuleForEach(x => x.Dictionaries)
                .SetValidator(new SyntaxTemplateDictionaryValidator(_httpContextAccessor));
        }
    }
}





