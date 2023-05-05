﻿using FluentValidation;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public class WorkFlowItemMatrixValidator : AbstractValidator<WorkflowItemMatrixDTO>
    {
        /// <summary>
        /// Validator for WorkFlowItemMatrix
        /// </summary>
        public WorkFlowItemMatrixValidator()
        {
            RuleFor(x => x.Matrix)
                .ForEach(y => y.SetValidator(new MatrixValidator()));
        }
    }
}
