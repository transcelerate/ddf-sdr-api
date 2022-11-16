﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Analysis Population
    /// </summary>
    public class AnalysisPopulationValidator : AbstractValidator<AnalysisPopulationDto>
    {
        public AnalysisPopulationValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.AnalysisPopulationId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
                .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.AnalysisPopulationId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);


            RuleFor(x => x.PopulationDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);    
        }
    }
}





