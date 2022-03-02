using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine
{
    public class StudyProtocolValidator : AbstractValidator<StudyProtocolDTO>
    {
        public StudyProtocolValidator()
        {                              
        }
    }
}
