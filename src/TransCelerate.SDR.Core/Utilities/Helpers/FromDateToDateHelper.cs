using System;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This is a helper class to format from and to date for the endpoints
    /// </summary>
    public static class FromDateToDateHelper
    {
        /// <summary>
        /// This method helps to return formatted from and to date
        /// </summary>
        /// <param name="fromDate">From Date</param>
        /// <param name="toDate">To Date</param>
        /// <param name="range">Date Range for which the from date have to be formatted (used for studyHistory endpoint).</param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetFromAndToDate(DateTime fromDate, DateTime toDate, int range)
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
