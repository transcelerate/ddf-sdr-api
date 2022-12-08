using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
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
        private readonly IClinicalStudyService _clinicalStudyServiceMVP;
        private readonly IClinicalStudyServiceV1 _clinicalStudyServiceV1;
        private readonly IClinicalStudyServiceV2 _clinicalStudyServiceV2;
        #endregion

        #region Constructor
        public CommonServices(ICommonRepository commonRepository, ILogHelper logger, IMapper mapper, 
            IClinicalStudyService clinicalStudyServiceMVP, 
            IClinicalStudyServiceV1 clinicalStudyServiceV1, 
            IClinicalStudyServiceV2 clinicalStudyServiceV2)
        {
            _commonRepository = commonRepository;
            _logger = logger;
            _mapper = mapper;
            _clinicalStudyServiceMVP = clinicalStudyServiceMVP;
            _clinicalStudyServiceV1 = clinicalStudyServiceV1;
            _clinicalStudyServiceV2 = clinicalStudyServiceV2;
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
                    if(study.AuditTrail.UsdmVersion == Constants.USDMVersions.MVP)
                    {
                        var studyMVP = JsonConvert.DeserializeObject<Core.Entities.Study.StudyEntity>(JsonConvert.SerializeObject(study));
                        var checkStudy = await _clinicalStudyServiceMVP.CheckAccessForAStudy(studyMVP, user);
                        if (checkStudy == null)
                            return _mapper.Map<Core.DTO.Study.GetStudyDTO>(checkStudy);
                    }
                    else if (study.AuditTrail.UsdmVersion == Constants.USDMVersions.V1)
                    {
                        var studyV1 = JsonConvert.DeserializeObject<Core.Entities.StudyV1.StudyEntity>(JsonConvert.SerializeObject(study));
                        var checkStudy = await _clinicalStudyServiceV1.CheckAccessForAStudy(studyV1, user);
                        if (checkStudy == null)
                            return Constants.ErrorMessages.Forbidden;
                        else
                            return _mapper.Map<Core.DTO.StudyV1.StudyDto>(checkStudy);
                    }
                    else if(study.AuditTrail.UsdmVersion == Constants.USDMVersions.V2)
                    {
                        var studyV2 = JsonConvert.DeserializeObject<Core.Entities.StudyV2.StudyEntity>(JsonConvert.SerializeObject(study));
                        var checkStudy = await _clinicalStudyServiceV2.CheckAccessForAStudy(studyV2, user);
                        if (checkStudy == null)
                            return _mapper.Map<Core.DTO.StudyV2.StudyDto>(checkStudy);                        
                    }
                    else
                    {
                        return null;
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
        #endregion
    }
}
