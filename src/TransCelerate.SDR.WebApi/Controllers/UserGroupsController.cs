using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Authorize(Roles = Constants.Roles.Org_Admin)]
    [ApiController]
    [ApiVersionNeutral]
    public class UserGroupsController : ControllerBase
    {
        #region Variables
        private readonly IUserGroupMappingService _userGroupMappingService;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public UserGroupsController(IUserGroupMappingService userGroupMappingService, ILogHelper logger)
        {
            _userGroupMappingService = userGroupMappingService;
            _logger = logger;
        }
        #endregion

        #region Action Methods

        #region GET
        /// <summary>
        /// GET All Groups
        /// </summary>        
        /// <response code="200">Returns List of Groups</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There are no groups in database</response>
        [HttpPost]
        [Route(Route.GetGroups)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserGroupMappingDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetUserGroups([FromBody] UserGroupsQueryParameters userGroupsQueryParameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUserGroups)};");
                var groups = await _userGroupMappingService.GetUserGroups(userGroupsQueryParameters);
                if (groups == null)
                {
                    return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.GroupsNotFound)).Value);
                }
                else if (groups.Count == 0)
                {
                    return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.GroupsNotFound)).Value);
                }
                else
                {
                    return Ok(groups);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUserGroups)};");
            }
        }

        /// <summary>
        /// GET All Users 
        /// </summary>        
        /// <response code="200">Returns List of users in a group</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">There are no users tagged to groups</response>
        [HttpPost]
        [Route(Route.GetUsers)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<UsersDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetUsersList([FromBody] UserGroupsQueryParameters userGroupsQueryParameters)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUsersList)};");
                var users = await _userGroupMappingService.GetUsersList(userGroupsQueryParameters);
                if (users == null)
                {
                    return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.UsersNotFound)).Value);
                }
                else
                {
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUsersList)};");
            }
        }

        /// <summary>
        /// GET group list
        /// </summary>        
        /// <response code="200">Returns List of users in a group</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The users for the groupId is Not Found</response>
        [HttpGet]
        [Route(Route.GetGroupList)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<UsersDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetGroupList()
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(GetGroupList)};");
                var groups = await _userGroupMappingService.ListGroups();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(GetGroupList)};");
            }
        }


        /// <summary>
        /// GET user list from AD
        /// </summary>        
        /// <response code="200">Returns List of users in a AD</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The users for the groupId is Not Found</response>
        [HttpGet]
        [Route(Route.GetUsersFromAD)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUserList)};");

                ClientSecretCredential clientSecretCredential = new(
                    Config.TenantID, Config.ClientId, Config.ClientSecret);
                Microsoft.Graph.GraphServiceClient graphServiceClient =
                    new(clientSecretCredential);

                var users = await graphServiceClient.Users.Request().GetAsync();

                if (users == null)
                {
                    return NotFound(new JsonResult(ErrorResponseHelper.NotFound(Constants.ErrorMessages.GroupsNotFound)).Value);
                }
                else
                {
                    var userList = users.CurrentPage.ToList();
                    while (users.NextPageRequest != null)
                    {
                        users = await users.NextPageRequest.GetAsync();
                        userList.AddRange(users.CurrentPage);
                    }
                    var userListResponse = userList.Select(x => new
                    {
                        x.Id,
                        x.DisplayName,
                        x.Mail
                    }).ToList();
                    return Ok(userListResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(GetUserList)};");
            }
        }

        /// <summary>
        /// Check Group name
        /// </summary>        
        /// <response code="200">Returns a object with the group existance boolean value</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The users for the groupId is Not Found</response>
        [HttpGet]
        [Route(Route.CheckGroupName)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> CheckGroupName(string groupName)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(CheckGroupName)};");
                if (!String.IsNullOrWhiteSpace(groupName))
                {
                    return Ok(await _userGroupMappingService.CheckGroupName(groupName));
                }
                else
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest()).Value);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(CheckGroupName)};");
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// POST a group
        /// </summary>
        /// <param name="groupDTO">Group which needs to be added/modified</param> 
        /// <response code="200">Returns List of users in a group</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The users for the groupId is Not Found</response>
        [HttpPost]
        [Route(Route.PostAGroup)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserGroupMappingDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> PostGroup(SDRGroupsDTO groupDTO)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(PostGroup)};");
                LoggedInUser user = new()
                {
                    UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                if (groupDTO == null)
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.PostGroupDataNotValid)).Value);
                }
                else
                {
                    var response = await _userGroupMappingService.PostGroup(groupDTO, user);
                    if (response == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError)).Value);
                    }
                    else if (Convert.ToString(response) == Constants.ErrorMessages.GroupNameExists)
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GroupNameExists)).Value);
                    }
                    else if (Convert.ToString(response) == Constants.ErrorMessages.GroupIdError)
                    {
                        return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GroupIdError)).Value);
                    }
                    else
                    {
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(PostGroup)};");
            }
        }

        /// <summary>
        /// POST a user to groups
        /// </summary>
        /// <param name="userToGroupsDTO">User which needs to be added/modified to groups</param> 
        /// <response code="200">Returns List of users in a group</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">The users for the groupId is Not Found</response>
        [HttpPost]
        [Route(Route.PostUserToGroups)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserGroupMappingDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<IActionResult> PostUserToGroups(PostUserToGroupsDTO userToGroupsDTO)
        {
            try
            {
                _logger.LogInformation($"Started Controller : {nameof(UserGroupsController)}; Method : {nameof(PostGroup)};");
                LoggedInUser user = new()
                {
                    UserName = User?.FindFirst(ClaimTypes.Email)?.Value,
                    UserRole = User?.FindFirst(ClaimTypes.Role)?.Value
                };
                if (userToGroupsDTO == null)
                {
                    return BadRequest(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.PostGroupDataNotValid)).Value);
                }
                else
                {
                    var response = await _userGroupMappingService.PostUserToGroups(userToGroupsDTO, user);
                    if (response == null)
                    {
                        return NotFound(new JsonResult(ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError)).Value);
                    }
                    else
                    {
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured. Exception : {ex}");
                return BadRequest(new JsonResult(ErrorResponseHelper.ErrorResponseModel(ex)).Value);
            }
            finally
            {
                _logger.LogInformation($"Ended Controller : {nameof(UserGroupsController)}; Method : {nameof(PostGroup)};");
            }
        }
        #endregion
        #endregion
    }
}
