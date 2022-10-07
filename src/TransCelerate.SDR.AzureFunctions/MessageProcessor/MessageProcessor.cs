using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using ObjectsComparer;
using System.Text.RegularExpressions;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.AzureFunctions
{
    public class MessageProcessor : IMessageProcessor
    {
        #region Variables
        private readonly ILogHelper _logger;
        private readonly IHelper _helper;
        private readonly IChangeAuditReposotory _changeAuditReposotory;
        #endregion
        #region Constructor
        public MessageProcessor(ILogHelper logger, IChangeAuditReposotory changeAuditReposotory, IHelper helper)
        {
            _logger = logger;
            _changeAuditReposotory = changeAuditReposotory;
            _helper = helper;
        }
        #endregion
        #region Process Message For Change Audit
        /// <summary>
        /// Process the Message for Change Audit
        /// </summary>
        /// <param name="message">Message from service bus</param>
        public void ProcessMessage(string message)
        {            
            //Deserialize the message
            ServiceBusMessageEntity serviceBusMessageEntity = JsonConvert.DeserializeObject<ServiceBusMessageEntity>(message);

            //Get the studies with current and previous version
            List<StudyEntity> studyEntities = _changeAuditReposotory.GetStudyItemsAsync(serviceBusMessageEntity.Study_uuid, serviceBusMessageEntity.CurrentVersion);

            StudyEntity currentStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == serviceBusMessageEntity.CurrentVersion).FirstOrDefault();
            StudyEntity previousStudyVersion = studyEntities.Where(x => x.AuditTrail.SDRUploadVersion == serviceBusMessageEntity.CurrentVersion - 1).FirstOrDefault();

            //Remove Id before comparison
            currentStudyVersion = _helper.RemovedSectionId(currentStudyVersion);
            previousStudyVersion = _helper.RemovedSectionId(previousStudyVersion);

            //Get the changes between current and previous version
            List<string> changedValues = GetChangedValues(currentStudyVersion, previousStudyVersion);
            changedValues = FormatChangeAuditElements(changedValues);

            //Get the change audit data for studyId
            ChangeAuditEntity changeAuditEntity = _changeAuditReposotory.GetChangeAuditAsync(serviceBusMessageEntity.Study_uuid);

            //Update changeAudit if exist/ create changeAudit if new
            AddChangeAuditInDatabase(changeAuditEntity, serviceBusMessageEntity, changedValues, currentStudyVersion);
        }
        /// <summary>
        /// Get the differences between two studies
        /// </summary>
        /// <param name="currentStudyVersion">Current study version</param>
        /// <param name="previousStudyVersion">Previous study version</param>
        /// <returns></returns>
        private List<string> GetChangedValues(StudyEntity currentStudyVersion, StudyEntity previousStudyVersion)
        {
            var comparer = new ObjectsComparer.Comparer<ClinicalStudyEntity>();
            bool isEqual = comparer.Compare(currentStudyVersion.ClinicalStudy, previousStudyVersion.ClinicalStudy, out var differences);
            var changedValues = differences.Select(x => x.MemberPath).ToList();
            return changedValues;
        }
        /// <summary>
        /// Format the changes to store in database
        /// </summary>
        /// <param name="elements">List of changes</param>
        /// <returns></returns>
        private List<string> FormatChangeAuditElements(List<string> elements)
        {
            List<string> formattedList = new List<string>();
            elements.ForEach(element =>
            {
                // Remove The index numbers
                element = Regex.Replace(element, "[0-9]", string.Empty);

                // Remove [] from the element
                Constants.ParanthesisToBeRemovedForAudit.ToList().ForEach(character =>
                {
                    element = element.Replace(character, string.Empty);
                });

                //Remove Code
                var stringSegments = element.Split(".");
                if (Constants.CharactersToBeRemovedForAudit.ToList().Any(x => x == stringSegments.Last()))
                    stringSegments = stringSegments.SkipLast(1).ToArray();
                element = string.Join(".", stringSegments);

                //Change to camel case
                element = string.Join(".", element?.Split(".").Select(key => key?.Substring(0, 1)?.ToLower() + key?.Substring(1)));
                formattedList.Add(element);
            });
            return formattedList.Distinct().ToList();
        }

        /// <summary>
        /// Add or update the changes in change audit collection
        /// </summary>
        /// <param name="changeAuditEntity">Change Audit Entity from database if exist</param>
        /// <param name="serviceBusMessageEntity">Service bus message after deserialization</param>
        /// <param name="changedValues">Changed values list</param>
        /// <param name="currentStudyVersion">Current study version</param>
        private void AddChangeAuditInDatabase(ChangeAuditEntity changeAuditEntity, ServiceBusMessageEntity serviceBusMessageEntity, List<string> changedValues, StudyEntity currentStudyVersion)
        {
            if (changeAuditEntity is null)
            {
                changeAuditEntity = new ChangeAuditEntity();
                changeAuditEntity.Study_uuid = serviceBusMessageEntity.Study_uuid;
                changeAuditEntity.Changes = new List<ChangesEntity>();

                ChangesEntity change = new ChangesEntity
                {
                    Elements = changedValues,
                    EntryDateTime = currentStudyVersion.AuditTrail.EntryDateTime,
                    SDRUploadVersion = currentStudyVersion.AuditTrail.SDRUploadVersion
                };

                changeAuditEntity.Changes.Add(change);

                _changeAuditReposotory.InsertChangeAudit(changeAuditEntity);
            }
            else
            {
                ChangesEntity change = new ChangesEntity
                {
                    Elements = changedValues,
                    EntryDateTime = currentStudyVersion.AuditTrail.EntryDateTime,
                    SDRUploadVersion = currentStudyVersion.AuditTrail.SDRUploadVersion
                };

                changeAuditEntity.Changes.Add(change);
                _changeAuditReposotory.UpdateChangeAudit(changeAuditEntity);
            }
        } 
        #endregion
    }
}
