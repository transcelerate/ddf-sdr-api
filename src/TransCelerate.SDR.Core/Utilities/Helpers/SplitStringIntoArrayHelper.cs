using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class SplitStringIntoArrayHelper
    {
        public static List<string> SplitString(string value,int index)
        {
            return value.Select((c, index) => new { c, index })
                               .GroupBy(x => x.index / index) 
                               .Select(group => group.Select(elem => elem.c))
                               .Select(chars => new string(chars.ToArray())).ToList();
        }
    }
}
