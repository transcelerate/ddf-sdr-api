using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
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
                    //var checkStudy = await CheckAccessForAStudy(study, user);
                    //if (checkStudy == null)
                    //    return Constants.ErrorMessages.Forbidden;
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

        #region UserGroups
        public async Task<bool> CheckPermissionForAUser(LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckPermissionForAUser)};");

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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyService)}; Method : {nameof(CheckPermissionForAUser)};");
            }
        }
        #endregion
    }
}
