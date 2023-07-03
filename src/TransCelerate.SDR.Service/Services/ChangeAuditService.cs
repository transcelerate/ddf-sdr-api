using AutoMapper;
using System;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ChangeAuditService : IChangeAuditService
    {
        #region Variables
        private readonly IChangeAuditRepository _changeAuditRepository;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        #endregion

        #region Constructor
        public ChangeAuditService(IChangeAuditRepository changeAuditRepository, IMapper mapper, ILogHelper logger, ICommonService commonService)
        {
            _changeAuditRepository = changeAuditRepository;
            _commonService = commonService;
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
                    var checkAccessForStudy = await _commonService.GetRawJson(studyId, 0, user).ConfigureAwait(false);

                    return checkAccessForStudy is not null && checkAccessForStudy.ToString() != Constants.ErrorMessages.Forbidden
                                            ? _mapper.Map<ChangeAuditStudyDto>(changeAudit) : Constants.ErrorMessages.Forbidden;
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
