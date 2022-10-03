using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using ObjectsComparer;

namespace TransCelerate.SDR.AzureFunctions
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ILogHelper _logger;
        private readonly IChangeAuditReposotory _changeAuditReposotory;
        public MessageProcessor(ILogHelper logger,IChangeAuditReposotory changeAuditReposotory)
        {
            _logger = logger;
            _changeAuditReposotory = changeAuditReposotory;
        }
        public void ProcessMessage(string message)
        {
            _logger.LogInformation($"Logging from DI Processor. Message: {message}");

            ServiceBusMessageEntity serviceBusMessageEntity = JsonConvert.DeserializeObject<ServiceBusMessageEntity>(message);

            List<StudyEntity> studyEntities = _changeAuditReposotory.GetStudyItemsAsync(serviceBusMessageEntity.Study_uuid, serviceBusMessageEntity.CurrentVersion);

            StudyEntity currentStudyVersion = studyEntities.Where(x=>x.AuditTrail.SDRUploadVersion == serviceBusMessageEntity.CurrentVersion).FirstOrDefault();
            StudyEntity previousStudyVersion = studyEntities.Where(x=>x.AuditTrail.SDRUploadVersion == serviceBusMessageEntity.CurrentVersion-1).FirstOrDefault();

            var comparer = new ObjectsComparer.Comparer<ClinicalStudyEntity>();
            bool isEqual = comparer.Compare(currentStudyVersion.ClinicalStudy, previousStudyVersion.ClinicalStudy,out var differences);
            var changedValues = differences.Select(x => x.MemberPath);

            ChangeAuditEntity changeAuditEntity = _changeAuditReposotory.GetChangeAuditAsync(serviceBusMessageEntity.Study_uuid);

            if(changeAuditEntity is null)
            {
                changeAuditEntity = new ChangeAuditEntity();
                changeAuditEntity.Study_uuid = serviceBusMessageEntity.Study_uuid;
                changeAuditEntity.Changes = new List<ChangesEntity>();

                ChangesEntity change = new ChangesEntity
                {
                    Elements = changedValues.ToList(),
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
                    Elements = changedValues.ToList(),
                    EntryDateTime = currentStudyVersion.AuditTrail.EntryDateTime,
                    SDRUploadVersion = currentStudyVersion.AuditTrail.SDRUploadVersion
                };

                changeAuditEntity.Changes.Add(change);
                _changeAuditReposotory.UpdateChangeAudit(changeAuditEntity);
            }                        
        }
    }
}
