﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngineV2
{
    /// <summary>
    /// This Class is the validator for Study
    /// </summary>
    public class StudyValidator : AbstractValidator<StudyDto>
    {       
        public StudyValidator()
        {
            RuleFor(x => x.ClinicalStudy)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.RootElementMissing);
        }
    }
}