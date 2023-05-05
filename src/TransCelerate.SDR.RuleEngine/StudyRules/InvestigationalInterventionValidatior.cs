﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class InvestigationalInterventionValidatior : AbstractValidator<InvestigationalInterventionDTO>
    {
        /// <summary>
        /// Validator for InvestigationalIntervention
        /// </summary>
        public InvestigationalInterventionValidatior()
        {
            RuleFor(x => x.Coding)
                .ForEach(y => y.SetValidator(new CodingValidator()));
        }
    }
}
