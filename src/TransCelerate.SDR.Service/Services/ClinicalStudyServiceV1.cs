using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ClinicalStudyServiceV1 : IClinicalStudyServiceV1
    {
        #region Variables
        private readonly IClinicalStudyRepositoryV1 _clinicalStudyRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        private readonly IHelper _helper;
        #endregion

        #region Constructor
        public ClinicalStudyServiceV1(IClinicalStudyRepositoryV1 clinicalStudyRepository, IMapper mapper, ILogHelper logger,IHelper helper)
        {
            _clinicalStudyRepository = clinicalStudyRepository;
            _mapper = mapper;
            _logger = logger;
            _helper = helper;
        }
        #endregion

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="version">Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudy(string studyId, int version, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, version: version).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDto>(study);  //Mapping Entity to Dto                                                  
                    return studyDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(GetStudy)};");
            }
        }
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<object> PostAllElements(StudyDto studyDTO, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                StudyEntity incomingStudyEntity = new StudyEntity
                {
                    ClinicalStudy = _mapper.Map<ClinicalStudyEntity>(studyDTO.ClinicalStudy),
                    AuditTrail = _helper.GetAuditTrail(user?.UserName),
                    _id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                if(String.IsNullOrWhiteSpace(incomingStudyEntity.ClinicalStudy.Uuid))
                {
                    incomingStudyEntity = _helper.GeneratedSectionId(incomingStudyEntity);
                    incomingStudyEntity.AuditTrail.SDRUploadVersion = 1;
                    await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);
                    studyDTO = _mapper.Map<StudyDto>(incomingStudyEntity);
                }
                else
                {
                    StudyEntity existingStudyEntity = await _clinicalStudyRepository.GetStudyItemsAsync(incomingStudyEntity.ClinicalStudy.Uuid, 0);

                    if(existingStudyEntity is null)
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }                    

                    if (_helper.IsSameStudy(incomingStudyEntity, existingStudyEntity))
                    {
                        
                        existingStudyEntity.AuditTrail.EntryDateTime = incomingStudyEntity.AuditTrail.EntryDateTime;
                        await _clinicalStudyRepository.UpdateStudyItemsAsync(existingStudyEntity);
                        studyDTO = _mapper.Map<StudyDto>(existingStudyEntity);
                    }
                    else
                    {
                        incomingStudyEntity = _helper.CheckForSections(incomingStudyEntity,existingStudyEntity);
                        incomingStudyEntity.AuditTrail.SDRUploadVersion = existingStudyEntity.AuditTrail.SDRUploadVersion + 1;
                        await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);
                        studyDTO = _mapper.Map<StudyDto>(incomingStudyEntity);
                    }
                }                

                return studyDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(PostAllElements)};");
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search Study Elements with search criteria
        /// </summary>
        /// <param name="searchParametersDto">Parameters to search in database</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{StudyDto}"/> which matches serach criteria <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<List<StudyDto>> SearchStudy(SearchParametersDto searchParametersDto, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(SearchStudy)};");
                _logger.LogInformation($"Search Parameters : {JsonConvert.SerializeObject(searchParametersDto)}");

                var searchParameters = _mapper.Map<SearchParameters>(searchParametersDto);

                List<SearchResponseEntity> studies = await _clinicalStudyRepository.SearchStudy(searchParameters, user);

                if(studies is null || !studies.Any())
                {
                    return null;
                }

                List<StudyDto> studyDtos = _mapper.Map<List<StudyDto>>(studies);

                studies.ForEach(study =>
                {                   
                    List<CodeDto> interventionModel = _mapper.Map<List<CodeDto>>(study.InterventionModel?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList());
                    List<IndicationDto> studyIndication = _mapper.Map<List<IndicationDto>>(study.StudyIndications?.Where(x => x != null && x.Any()).SelectMany(x => x).ToList());

                    List<StudyDesignDto> studyDesigns = new()
                    {
                        new StudyDesignDto {StudyIndications = studyIndication, InterventionModel = interventionModel}
                    };

                    studyDtos[studies.IndexOf(study)].ClinicalStudy.StudyDesigns = studyDesigns;
                });

                return studyDtos;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(SearchStudy)};");
            }
        }

        #endregion
        #region UserGroups
        /// <summary>
        /// Check access for the study
        /// </summary>
        /// <param name="study">Study for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<StudyEntity> CheckAccessForAStudy(StudyEntity study, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(study.ClinicalStudy.Uuid))
                            return study;
                        else if (groupFilters.Item1.Contains(study.ClinicalStudy.StudyType?.Decode?.ToLower()))
                            return study;
                        else
                            return null;
                    }
                    else
                    {
                        // Filter should not give any results
                        return null;
                    }
                }
                else
                    return study;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }


        /// <summary>
        /// Check READ_WRITE Permission for a user
        /// </summary>    
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// <see langword="true"/> If the user have READ_WRITE access in any of the groups <br></br> <br></br>
        /// <see langword="false"/> If the user does not have READ_WRITE access in any of the groups
        /// </returns>
        public async Task<bool> CheckPermissionForAUser(LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckPermissionForAUser)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        if (groups.Any(x => x.permission == Permissions.READ_WRITE.ToString()))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV1)}; Method : {nameof(CheckPermissionForAUser)};");
            }
        }
        #endregion
    }
}
