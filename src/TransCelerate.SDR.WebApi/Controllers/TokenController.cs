using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    public class TokenController : ControllerBase
    {
        #region Variables
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public TokenController(ILogHelper logger)
        {
            _logger = logger;
        }
        #endregion

        #region Action Method
        /// <summary>
        /// GET Token for accessing API's
        /// </summary>
        /// <param name="user">logging user details</param>
        /// <response code="200">Returns Token</response>
        /// <response code="400">Bad Request</response>        
        [HttpPost]
        [Route(Route.Token)]
        [Route(Route.CommonToken)]
        [Produces("application/json")]
        public async Task<IActionResult> GetToken([FromBody] UserLogin user)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(TokenController)}; Method : {nameof(GetToken)};");

                HttpClient client = new();
                var values = new Dictionary<string, string>
                {
                    { Constants.TokenConstants.ClientId, Config.ClientId },
                    { Constants.TokenConstants.Grant_Type, Constants.TokenConstants.Grant_Type_Value },
                    { Constants.TokenConstants.Username, user.Username },
                    { Constants.TokenConstants.Password, user.Password },
                    { Constants.TokenConstants.Scope, Config.Scope },
                    { Constants.TokenConstants.Client_Secret, Config.ClientSecret }
                };


                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync($"{Config.Authority}/oauth2/v2.0/token",
                                            content);

                var responseString = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<TokenSuccessResponseDTO>(responseString);
                    var tokenResponse = new { token = $"{responseObject.Token_type} {responseObject.Access_token}" };
                    return Ok(tokenResponse);
                }

                else
                {
                    _logger.LogError($"Token Generation Error : {responseString}");
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.InvalidCredentials)).Value);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(TokenController)}; Method : {nameof(GetToken)};");
            }
        }
        #endregion
    }
}
