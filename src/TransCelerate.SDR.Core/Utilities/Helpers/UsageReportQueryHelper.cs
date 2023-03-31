using System;
using TransCelerate.SDR.Core.DTO.Reports;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This is a helper class to format the KQL query
    /// </summary>
    public static class UsageReportQueryHelper
    {
        /// <summary>
        /// This method helps to format the KQL query for system usage report
        /// </summary>
        /// <param name="reportBodyParameters">Body parameters for usage report</param>
        /// <returns></returns>
        public static string FormattedQuery(ReportBodyParameters reportBodyParameters)
        {
            string query = $"query=" +
                                 $"requests " +
                                 $"| join (traces) on operation_Id ,$left.operation_Id == $right.operation_Id " +
                                 $"| where url has \"apim\" and cloud_RoleName1 has \"apim\"" +
                                 $"| project {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)}," +
                                           $"{Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.name)}," +
                                           $"{Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.customDimensions1)}," +
                                           $"{Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.client_IP)}," +
                                           $"{Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.resultCode)}," +
                                           $"{Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.url)} ";

            if (!String.IsNullOrWhiteSpace(reportBodyParameters.Operation))
            {
                query += $"| where {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.name)} startswith \"{reportBodyParameters.Operation}\"";
            }
            if (reportBodyParameters.ResponseCode != 0)
            {
                query += $"| where {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.resultCode)} == {reportBodyParameters.ResponseCode}";
            }

            if (!String.IsNullOrWhiteSpace(reportBodyParameters.SortBy))
            {
                query += reportBodyParameters.SortBy.ToLower() switch
                {
                    "requestdate" => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ",
                    "operation" => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.name)} ",
                    "api" => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.url)} ",
                    "callerip" => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.client_IP)} ",
                    "responsecode" => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.resultCode)} ",
                    _ => $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ",
                };
                query += reportBodyParameters.SortOrder == SortOrder.asc.ToString() ? $"{SortOrder.asc}" : $"{SortOrder.desc}";
            }
            else
            {
                query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ";
                query += reportBodyParameters.SortOrder == SortOrder.asc.ToString() ? $"{SortOrder.asc}" : $"{SortOrder.desc}";
            }

            if (reportBodyParameters.PageSize == 0)
            {
                query += $"| serialize Num = row_number() " +
                         $"| where Num > {reportBodyParameters.RecordNumber}";
            }
            else
            {
                query += $"| serialize Num = row_number() " +
                         $"| where Num > {reportBodyParameters.RecordNumber}" +
                         $"| take {reportBodyParameters.PageSize}";
            }

            if (reportBodyParameters.FilterByTime)
            {
                query += $"&timespan={reportBodyParameters.FromDateTime:O}/{reportBodyParameters.ToDateTime:O}";
            }
            else
            {
                query += $"&timespan=P{reportBodyParameters.Days}D";
            }

            return query;
        }
    }
}
