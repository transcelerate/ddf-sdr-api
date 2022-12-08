using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region Variables
        private readonly ILogHelper _logger;
        private readonly ICommonService _commonService;
        #endregion

        #region Constructor
        public CommonController(ICommonService commonService, ILogHelper logger)
        {
            _logger = logger;
            _commonService = commonService;
        }
        #endregion

        #region Action Methods

        #region Get Method
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param> 
        /// <response code="200">Returns Study</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The Study for the studyId is Not Found</response>
        [HttpGet]
        [Route(Route.GetRawJson)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [Produces("application/json")]
        public async Task<IActionResult> GetRawJson(string studyId,int sdruploadversion)
        {
            

            return Ok(studyId);
        }
        #endregion
        #endregion



    }
}
