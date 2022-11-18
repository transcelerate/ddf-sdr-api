﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for WorkFlows
    /// </summary>
    public class WorkflowValidator : AbstractValidator<WorkflowDto>
    {
        public WorkflowValidator()
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotNull().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().OverridePropertyName(IdFieldPropertyName.StudyV2.WorkflowItemId).WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.WorkflowDescription)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(Constants.ValidationErrorMessage.PropertyMissingError)
               .NotEmpty().WithMessage(Constants.ValidationErrorMessage.PropertyEmptyError);

            RuleFor(x => x.WorkflowItems)
               .Must(x => UniquenessArrayValidator.ValidateArrayV2(x)).WithMessage(Constants.ValidationErrorMessage.UniquenessArrayError);
        }
    }
}




