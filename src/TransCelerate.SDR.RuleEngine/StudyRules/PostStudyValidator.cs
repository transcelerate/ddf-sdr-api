using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class PostStudyValidator : AbstractValidator<PostStudyDTO>
    {
        /// <summary>
        /// Validator for ClinicalStudy
        /// </summary>
        public PostStudyValidator()
        {

            RuleFor(x => x.clinicalStudy)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(Constants.ValidationErrorMessage.RootElementMissing);                          

        }
    }
}
