using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class FromDateToDateHelper
    {
        public static Tuple<DateTime,DateTime> GetFromAndToDate(DateTime fromDate, DateTime toDate, int range)
        {
            if (fromDate != DateTime.MinValue)
            {
                fromDate = fromDate.Date;
            }
            else if (range != -1 && toDate == DateTime.MinValue)
            {
                fromDate = DateTime.UtcNow.AddDays(-range).Date;
            }
            else
            {
                fromDate = DateTime.MinValue.Date;
            }

            if (toDate == DateTime.MinValue)
            {
                toDate = DateTime.UtcNow;
            }

            if (toDate != DateTime.MinValue)
            {
                toDate = toDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }            

            return Tuple.Create(fromDate, toDate);
        }
    }
}
