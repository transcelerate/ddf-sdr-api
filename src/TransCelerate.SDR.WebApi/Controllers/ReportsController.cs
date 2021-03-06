using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.DTO.Reports;
using Newtonsoft.Json;
using System.Net.Http;
using AutoMapper;
using System.Net;
using TransCelerate.SDR.Core.Utilities.Enums;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize(Roles = Constants.Roles.Org_Admin)]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region Variables
        private readonly ILogHelper _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReportsController(ILogHelper logger, IMapper mapper)
        {            
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// GET System Usage Report
        /// </summary>        
        /// <response code="200">Returns List of System Usage</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There are reports</response>
        [HttpPost]
        [Route(Route.SystemUsageReport)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<SystemUsageReportDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetUsageReport([FromBody] ReportBodyParameters reportBodyParameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(ReportsController)}; Method : {nameof(GetUsageReport)};");
                if (reportBodyParameters.days == 0)
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.InValidDays)).Value);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(Constants.DefaultHeaders.AppInsightsApiKey, Config.AppInsightsApiKey);

                string url = $"{Config.AppInsightsRESTApiUrl}/{Config.AppInsightsAppId}/query?" + UsageReportQueryHelper.FormattedQuery(reportBodyParameters);                     

                var response = await client.GetAsync(url);
                var responseString = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {                    
                    var rawReport = JsonConvert.DeserializeObject<SystemUsageRawReport>(responseString);

                    List<SystemUsageReportDTO> usageReport = new List<SystemUsageReportDTO>();
                    if (rawReport.Tables[0].Rows.Count > 0)
                    {                        
                        rawReport.Tables[0].Rows.ForEach(rows => usageReport.Add(new SystemUsageReportDTO
                        {
                            RequestDate = rows[(int)UsageReportFields.timestamp],

                            Api = rows[(int)UsageReportFields.name].Split(" ")[1],

                            EmailId = JsonConvert.DeserializeObject<CustomDimension>(rows[(int)UsageReportFields.customDimensions1]).EmailAddress,

                            UserName = JsonConvert.DeserializeObject<CustomDimension>(rows[(int)UsageReportFields.customDimensions1]).UserName,

                            CallerIpAddress = rows[(int)UsageReportFields.client_IP],

                            ResponseCode = rows[(int)UsageReportFields.resultCode],

                            Operation = rows[(int)UsageReportFields.name].Split(" ")[0],

                            ResponseCodeDescription = int.TryParse(rows[(int)UsageReportFields.resultCode], out int code) == true ?
                                                      Enum.IsDefined(typeof(HttpStatusCode), code) == true ?
                                                      $"{code} - {Enum.GetName(typeof(HttpStatusCode), code)}"
                                                      : null : null                                                                                                                                         
                        }));
                        return Ok(new JsonResult(usageReport).Value);
                    }
                    else
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.UsageReportNotAvailable)).Value);
                    }                    
                }

                else
                {
                    _logger.LogError($"Exception occured. Exception : {responseString}");
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError)).Value);                                        
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(ReportsController)}; Method : {nameof(GetUsageReport)};");
            }
        }        
        #endregion
    }
}
