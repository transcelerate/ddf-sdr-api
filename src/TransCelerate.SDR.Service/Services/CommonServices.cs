using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public  class CommonServices:ICommonService
    {
        #region Variable
        private readonly ICommonRepository _commonRepository;
        private readonly ILogHelper _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CommonServices(ICommonRepository commonRepository, ILogHelper logger, IMapper mapper)
        {
            _commonRepository = commonRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged in user</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetRawJson(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GetRawJson)};");
                studyId = studyId.Trim();

                var study = await _commonRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    var jsonObject = JObject.Parse(JsonConvert.SerializeObject(study.ClinicalStudy));                    

                    if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                    {                                                                     
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1)
                    {
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2)
                    {                        
                        if (!await CheckAccessForAStudy(studyId, (string)jsonObject["studyType"]["decode"], user))
                            return Constants.ErrorMessages.Forbidden;
                    }

                    return study;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GetRawJson)};");
            }
        }

        public async Task<bool> CheckAccessForAStudy(string studyId, string studyTye, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _commonRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(studyId))
                            return true;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return true;
                        else if (groupFilters.Item1.Contains(studyTye))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        // Filter should not give any results
                        return false;
                    }
                }
                else
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }
        #endregion
    }
}
