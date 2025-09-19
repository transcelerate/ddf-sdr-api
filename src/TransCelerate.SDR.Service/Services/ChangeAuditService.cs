using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ChangeAuditService : IChangeAuditService
    {
        #region Variables
        private readonly IChangeAuditRepository _changeAuditRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;

        private readonly IHelperV3 _helperV3;
        private readonly IHelperV4 _helperV4;
        private readonly IHelperV5 _helperV5;
        #endregion

        #region Constructor
        public ChangeAuditService(IChangeAuditRepository changeAuditRepository, ICommonRepository commonRepository, IMapper mapper, ILogHelper logger,
            IHelperV3 helperV3, IHelperV4 helperV4, IHelperV5 helperV5)
        {
            _changeAuditRepository = changeAuditRepository;
            _commonRepository = commonRepository;

            _mapper = mapper;
            _logger = logger;

            _helperV3 = helperV3;
            _helperV4 = helperV4;
            _helperV5 = helperV5;
        }
        #endregion

        #region Services
        public async Task<object> GetChangeAudit(string studyId)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ChangeAuditService)}; Method : {nameof(GetChangeAudit)};");
                studyId = studyId.Trim();

                ChangeAuditStudyEntity changeAudit = await _changeAuditRepository.GetChangeAuditAsync(studyId: studyId).ConfigureAwait(false);

                return _mapper.Map<ChangeAuditStudyDto>(changeAudit);
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

        public async Task<string> ProcessChangeAudit(string studyId, int currentVersion)
        {
            // Check The AuditTrails
            List<AuditTrailEntity> auditTrailEntities = await _commonRepository.GetAuditTrailsAsync(studyId, currentVersion);
            string currentUsdmVersion = auditTrailEntities.Where(x => x.SDRUploadVersion == currentVersion).FirstOrDefault()?.UsdmVersion;
            string previousUsdmVersion = auditTrailEntities.Where(x => x.SDRUploadVersion == currentVersion - 1).FirstOrDefault()?.UsdmVersion;

            string currentApiVersion = ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(currentUsdmVersion)).Select(x => x.ApiVersion).First();
            string previousApiVersion = ApiUsdmVersionMapping.SDRVersions.Where(x => x.UsdmVersions.Contains(previousUsdmVersion)).Select(x => x.ApiVersion).First();

            if (currentApiVersion != previousApiVersion)
            {
                List<string> changedValues = [$"The usdmVersion have been changed from {previousUsdmVersion} to {currentUsdmVersion}"];

                // Update changeAudit if exist / create changeAudit if new
                return await _changeAuditRepository.AddOrUpdateChangeAuditAsync(studyId, changedValues, auditTrailEntities.Where(x => x.SDRUploadVersion == currentVersion).FirstOrDefault());
            }
            else
            {
                List<string> changedValues = new();

                if (currentApiVersion == Constants.ApiVersions.V3)
                {
                    // Get the studies with current and previous version
                    List<Core.Entities.StudyV3.StudyDefinitionsEntity> studyEntities = await _changeAuditRepository.GetStudyItemsAsyncV3(studyId, currentVersion);

                    Core.Entities.StudyV3.StudyDefinitionsEntity currentStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion).FirstOrDefault();
                    Core.Entities.StudyV3.StudyDefinitionsEntity previousStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion - 1).FirstOrDefault();

                    // Get the changes between current and previous version
                    changedValues = _helperV3.GetChangedValues(currentStudyVersion, previousStudyVersion);
                    changedValues = FormatChangeAuditElements(changedValues);
                }
                if (currentApiVersion == Constants.ApiVersions.V4)
                {
                    // Get the studies with current and previous version
                    List<Core.Entities.StudyV4.StudyDefinitionsEntity> studyEntities = await _changeAuditRepository.GetStudyItemsAsyncV4(studyId, currentVersion);

                    Core.Entities.StudyV4.StudyDefinitionsEntity currentStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion).FirstOrDefault();
                    Core.Entities.StudyV4.StudyDefinitionsEntity previousStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion - 1).FirstOrDefault();

                    // Get the changes between current and previous version
                    changedValues = _helperV4.GetChangedValues(currentStudyVersion, previousStudyVersion);
                    changedValues = FormatChangeAuditElements(changedValues);
                }
                if (currentApiVersion == Constants.ApiVersions.V5)
                {
                    // Get the studies with current and previous version
                    List<Core.Entities.StudyV5.StudyDefinitionsEntity> studyEntities = await _changeAuditRepository.GetStudyItemsAsyncV5(studyId, currentVersion);

                    Core.Entities.StudyV5.StudyDefinitionsEntity currentStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion).FirstOrDefault();
                    Core.Entities.StudyV5.StudyDefinitionsEntity previousStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == currentVersion - 1).FirstOrDefault();

                    // Get the changes between current and previous version
                    changedValues = _helperV5.GetChangedValues(currentStudyVersion, previousStudyVersion);
                    changedValues = FormatChangeAuditElements(changedValues);
                }

                // Update changeAudit if exist / create changeAudit if new
                return await _changeAuditRepository.AddOrUpdateChangeAuditAsync(studyId, changedValues, auditTrailEntities.Where(x => x.SDRUploadVersion == currentVersion).FirstOrDefault());
            }
        }

        /// <summary>
        /// Format the changes to store in database
        /// </summary>
        /// <param name="elements">List of changes</param>
        /// <returns></returns>
        private static List<string> FormatChangeAuditElements(List<string> elements)
        {
            List<string> formattedList = [];
            elements.ForEach(element =>
            {
                if (!element.EndsWith($".{nameof(IId.Id)}"))
                {
                    // Remove The index numbers
                    element = Regex.Replace(element, "[0-9]", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(1000));

                    // Remove [] from the element
                    Constants.ParanthesisToBeRemovedForAudit.ToList().ForEach(character =>
                    {
                        element = element.Replace(character, string.Empty);
                    });

                    // Remove Code
                    var stringSegments = element.Split(".");
                    if (Constants.CharactersToBeRemovedForAudit.ToList().Any(x => x == stringSegments.Last()))
                        stringSegments = stringSegments.SkipLast(1).ToArray();
                    element = string.Join(".", stringSegments);

                    // Change to camel case
                    element = string.Join(".", element?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}"));
                    formattedList.Add(element);
                }
            });
            return formattedList.Distinct().ToList();
        }
        #endregion
    }
}
