using System.Collections.Generic;
using System.Linq;

namespace TransCelerate.SDR.RuleEngine.Common
{
    public static class UniquenessArrayValidator
    {
        public static bool ValidateArrayV3<T>(List<T> arrayElement) where T : class, Core.DTO.StudyV3.IId
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                var ids = arrayElement.Select(x => x.Id).ToList();
                ids.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return ids.Distinct().Count() == ids.Count;
            }
            return true;
        }

        public static bool ValidateArrayV4<T>(List<T> arrayElement) where T : class, Core.DTO.StudyV4.IId
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                var ids = arrayElement.Select(x => x.Id).ToList();
                ids.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return ids.Distinct().Count() == ids.Count;
            }
            return true;
        }

        public static bool ValidateArrayV5<T>(List<T> arrayElement) where T : class, Core.DTO.StudyV5.IId
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                var ids = arrayElement.Select(x => x.Id).ToList();
                ids.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return ids.Distinct().Count() == ids.Count;
            }
            return true;
        }

        public static bool ValidateStringList(List<string> arrayElement)
        {
            if (arrayElement is not null && arrayElement.Any())
            {
                arrayElement.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                return arrayElement.Select(x => x).ToList().Distinct().Count() == arrayElement.Select(x => x).Count();
            }
            return true;
        }
    }
}
