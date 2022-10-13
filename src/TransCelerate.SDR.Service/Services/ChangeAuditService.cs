using AutoMapper;
using System;
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
    public class ChangeAuditService : IChangeAuditService
    {
        #region Variables
        private readonly IChangeAuditRepository _changeAuditRepository;
        private readonly IClinicalStudyServiceV1 _clinicalStudyServiceV1;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public ChangeAuditService(IChangeAuditRepository changeAuditRepository, IMapper mapper, ILogHelper logger, IClinicalStudyServiceV1 clinicalStudyServiceV1)
        {
            _changeAuditRepository = changeAuditRepository;
            _clinicalStudyServiceV1 = clinicalStudyServiceV1;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Services
        public async Task<object> GetChangeAudit(string studyId, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ChangeAuditService)}; Method : {nameof(GetChangeAudit)};");
                studyId = studyId.Trim();

                ChangeAuditStudyEntity changeAudit = await _changeAuditRepository.GetChangeAuditAsync(studyId: studyId).ConfigureAwait(false);

                if (changeAudit == null)
                {
                    return null;
                }
                else
                {
                    bool checkAccessForStudy = await _clinicalStudyServiceV1.GetAccessForAStudy(studyId, 0, user).ConfigureAwait(false);

                    return checkAccessForStudy ? _mapper.Map<ChangeAuditStudyDto>(changeAudit) : Constants.ErrorMessages.Forbidden;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ChangeAuditService)}; Method : {nameof(GetChangeAudit)};");
            }
        }
        #endregion
    }
}
