using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;

namespace TransCelerate.SDR.AzureFunctions
{
    public class MessageProcessor : IMessageProcessor
    {
        #region Variables
        private readonly ILogHelper _logger;
        private readonly IHelperV2 _helper;
        private readonly IChangeAuditRepository _changeAuditReposotory;
        #endregion
        #region Constructor
        public MessageProcessor(ILogHelper logger, IChangeAuditRepository changeAuditReposotory, IHelperV2 helper)
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

            //Get the changes between current and previous version
            List<string> changedValues = _helper.GetChangedValues(currentStudyVersion, previousStudyVersion);
            changedValues = FormatChangeAuditElements(changedValues);

            //Get the change audit data for studyId
            ChangeAuditStudyEntity changeAuditEntity = _changeAuditReposotory.GetChangeAuditAsync(serviceBusMessageEntity.Study_uuid);

            //Update changeAudit if exist/ create changeAudit if new
            AddChangeAuditInDatabase(changeAuditEntity, serviceBusMessageEntity, changedValues, currentStudyVersion);
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
                if(!element.EndsWith($".{nameof(StudyIdentifierEntity.Id)}"))
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
                }
            });
            return formattedList.Distinct().ToList();
        }

        /// <summary>
        /// Add or update the changes in change audit collection
        /// </summary>
        /// <param name="changeAuditStudyEntity">Change Audit Entity from database if exist</param>
        /// <param name="serviceBusMessageEntity">Service bus message after deserialization</param>
        /// <param name="changedValues">Changed values list</param>
        /// <param name="currentStudyVersion">Current study version</param>
        private void AddChangeAuditInDatabase(ChangeAuditStudyEntity changeAuditStudyEntity, ServiceBusMessageEntity serviceBusMessageEntity, List<string> changedValues, StudyEntity currentStudyVersion)
        {
            ChangesEntity change = new ChangesEntity
            {
                Elements = changedValues,
                EntryDateTime = currentStudyVersion.AuditTrail.EntryDateTime,
                SDRUploadVersion = currentStudyVersion.AuditTrail.SDRUploadVersion
            };
            if (changeAuditStudyEntity is null)
            {
                var changeAuditEntity = new ChangeAuditEntity();
                changeAuditEntity.Study_uuid = serviceBusMessageEntity.Study_uuid;
                changeAuditEntity.Changes = new List<ChangesEntity>();              
                changeAuditEntity.Changes.Add(change);


                changeAuditStudyEntity = new ChangeAuditStudyEntity { ChangeAudit = changeAuditEntity };

                _changeAuditReposotory.InsertChangeAudit(changeAuditStudyEntity);
            }
            else
            {                
                changeAuditStudyEntity.ChangeAudit.Changes.Add(change);
               
                _changeAuditReposotory.UpdateChangeAudit(changeAuditStudyEntity);
            }
        } 
        #endregion
    }
}
