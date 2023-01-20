using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class RulesHelper
    {
        public static bool GetConformanceRules(string usdmVersion, string entity, string field)
        {
            entity = entity.Replace("Validator", "");
            return Conformance.ConformanceRules
                        .Where(x => x.Usdmversion == usdmVersion).FirstOrDefault()
                        .Rules.Where(x => x.Entity == entity).FirstOrDefault()
                        .Required.Any(x => x == field);
        }
    }
}
