using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class UniquenessArrayValidator
    {
        public static bool ValidateArray<T>(List<T> arrayElement) where T : class, IUuid
        {
            if(arrayElement is not null && arrayElement.Any())
            {
                arrayElement.RemoveAll(x => String.IsNullOrWhiteSpace(x.Uuid));
                return arrayElement.Select(x => x.Uuid).ToList().Distinct().Count() == arrayElement.Select(x => x.Uuid).Count();
            }
            return true;
        }
    }
}
