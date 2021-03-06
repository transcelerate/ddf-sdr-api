using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Reports;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class UsageReportQueryHelper
    {
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

            if (!String.IsNullOrWhiteSpace(reportBodyParameters.operation))
            {
                query += $"| where {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.name)} startswith \"{reportBodyParameters.operation}\"";
            }
            if (reportBodyParameters.responseCode != 0)
            {
                query += $"| where {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.resultCode)} == {reportBodyParameters.responseCode}";
            }

            if (!String.IsNullOrWhiteSpace(reportBodyParameters.sortBy))
            {
                switch (reportBodyParameters.sortBy.ToLower())
                {
                    case "requestdate":
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ";
                        break;
                    case "operation":
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.name)} ";
                        break;
                    case "api":
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.url)} ";
                        break;
                    case "callerip":
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.client_IP)} ";
                        break;
                    case "responsecode":
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.resultCode)} ";
                        break;
                    default:
                        query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ";
                        break;
                }
                query += reportBodyParameters.sortOrder == SortOrder.asc.ToString() ? $"{SortOrder.asc}" : $"{SortOrder.desc}";
            }
            else
            {
                query += $"| order by  {Enum.GetName(typeof(UsageReportFields), (int)UsageReportFields.timestamp)} ";
                query += reportBodyParameters.sortOrder == SortOrder.asc.ToString() ? $"{SortOrder.asc}" : $"{SortOrder.desc}";
            }
            
            if(reportBodyParameters.pageSize == 0)
            {
                query += $"| serialize Num = row_number() " +
                         $"| where Num > {reportBodyParameters.recordNumber}" +                   
                         $"&timespan=P{reportBodyParameters.days}D";
            }
            else
            {
                query += $"| serialize Num = row_number() " +
                         $"| where Num > {reportBodyParameters.recordNumber}" +
                         $"| take {reportBodyParameters.pageSize}" +
                         $"&timespan=P{reportBodyParameters.days}D";
            }

            return query;
        }
    }
}
