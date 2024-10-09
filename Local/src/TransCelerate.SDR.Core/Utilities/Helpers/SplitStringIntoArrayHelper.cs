using System.Collections.Generic;
using System.Linq;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class SplitStringIntoArrayHelper
    {
        /// <summary>
        /// This method helps to split the array based on index value
        /// </summary>
        /// <param name="value">String which needs to be split</param>
        /// <param name="index">The number of parts the string have to be split into</param>
        /// <returns></returns>
        public static List<string> SplitString(string value, int index)
        {
            return value.Select((c, index) => new { c, index })
                               .GroupBy(x => x.index / index)
                               .Select(group => group.Select(elem => elem.c))
                               .Select(chars => new string(chars.ToArray())).ToList();
        }
    }
}
