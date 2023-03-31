using System.Collections.Generic;

namespace TransCelerate.SDR.Core.Utilities.Common
{
    public static class Conformance
    {
        public static List<ConformanceRules> ConformanceRules { get; set; }
    }

    public class ConformanceRules
    {
        public string Usdmversion { get; set; }
        public List<Rules> Rules { get; set; }
    }

    public class ConformanceNonStatic
    {
        public List<ConformanceRules> ConformanceRules { get; set; }
    }

    public class Rules
    {
        public string Entity { get; set; }
        public List<string> Required { get; set; }
    }
}
