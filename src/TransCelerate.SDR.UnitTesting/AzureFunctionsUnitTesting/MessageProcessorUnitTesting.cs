using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using TransCelerate.SDR.AzureFunctions;
using NUnit.Framework;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ObjectsComparer;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TransCelerate.SDR.UnitTesting.AzureFunctionsUnitTesting
{
    public class MessageProcessorUnitTesting
    {

        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IHelper> _mockHelper = new Mock<IHelper>(MockBehavior.Loose);
        private Mock<IChangeAuditRepository> _mockChangeAuditRepository = new Mock<IChangeAuditRepository>(MockBehavior.Loose);
        private Mock<IMessageProcessor> _messageProcessor = new Mock<IMessageProcessor>(MockBehavior.Loose);

        public StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }

        public ChangeAuditStudyEntity  GetChangeAuditDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyEntity>(jsonData);
        }
        [Test]
        public void ProcessMessage_UnitTesting()
        {
            Helper helper =new Helper();

            var currentVersion=GetEntityDataFromStaticJson();
            var previousVersion=GetEntityDataFromStaticJson();
            List<StudyEntity> studyEntities = new List<StudyEntity>
            {
                currentVersion,previousVersion

            };
            var difference = helper.GetChangedValues(currentVersion, previousVersion);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities);
            
            _mockChangeAuditRepository.Setup(x=>x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));
            _mockChangeAuditRepository.Setup(x => x.UpdateChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));

            _mockHelper.Setup(x => x.GetChangedValues(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                .Returns(difference);


            MessageProcessor processor = new MessageProcessor(_mockLogger,_mockChangeAuditRepository.Object,_mockHelper.Object);

            string message = "{\"Study_uuid\":\"aaed3efe-7d70-4c9e-90e2-3446e936c291\",\"CurrentVersion\":2}";

            processor.ProcessMessage(message);

            ChangeAuditStudyEntity changeAuditStudyEntity = null;
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(changeAuditStudyEntity);
            processor.ProcessMessage(message);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            var currentVersion1 = GetEntityDataFromStaticJson();
            var previousVersion1 = GetEntityDataFromStaticJson();
            List<StudyEntity> studyEntities1 = new List<StudyEntity>
            {
                currentVersion,previousVersion

            };
            currentVersion1.ClinicalStudy.StudyProtocolVersions[0].ProtocolStatus.Add(currentVersion1.ClinicalStudy.StudyProtocolVersions[0].ProtocolStatus[0]);
            currentVersion1.ClinicalStudy.StudyProtocolVersions[0].ProtocolStatus[0].CodeSystemVersion = "10";
            var difference1 = helper.GetChangedValues(currentVersion1, previousVersion1);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities1);
            _mockHelper.Setup(x => x.GetChangedValues(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                .Returns(difference1);
            processor.ProcessMessage(message);

        }
        [Test]
        public void ChangeAuditRepo_UnitTesting()
        {
            Mock<IMongoDatabase> mongoDatabase = new Mock<IMongoDatabase>(MockBehavior.Loose);
            Mock<IMongoClient> mongoClient = new Mock<IMongoClient>(MockBehavior.Loose);
            Mock<IMongoCollection<StudyEntity>> mongoCollectionStudy = new Mock<IMongoCollection<StudyEntity>>(MockBehavior.Strict);
            Mock<IMongoCollection<ChangeAuditStudyEntity>> mongoCollectionChangeAudit = new Mock<IMongoCollection<ChangeAuditStudyEntity>>(MockBehavior.Strict);            
            Mock<IClientSessionHandle> mongoClientSessionHandle = new Mock<IClientSessionHandle>(MockBehavior.Loose);
            Mock<IFindFluent<ChangeAuditStudyEntity, ChangeAuditStudyEntity>> _fakeCollectionResult = new Mock<IFindFluent<ChangeAuditStudyEntity, ChangeAuditStudyEntity>>();
            var asyncCursor = new Mock<IAsyncCursor<ChangeAuditStudyEntity>>();

            mongoClient.Setup(x=>x.GetDatabase(It.IsAny<string>(),null))
                .Returns(mongoDatabase.Object);

            mongoDatabase.Setup(x => x.GetCollection<ChangeAuditStudyEntity>(It.IsAny<string>(), null))
                .Returns(mongoCollectionChangeAudit.Object);

            mongoDatabase.Setup(x => x.GetCollection<StudyEntity>(It.IsAny<string>(), null))
                .Returns(mongoCollectionStudy.Object);

            string study_uuid = "aaed3efe-7d70-4c9e-90e2-3446e936c291";



            ChangeAuditRepository changeAuditRepository = new ChangeAuditRepository(mongoClient.Object, _mockLogger);            

            Assert.Throws<Moq.MockException>(() => changeAuditRepository.GetChangeAuditAsync(study_uuid));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.GetStudyItemsAsync(study_uuid, 1));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.InsertChangeAudit(GetChangeAuditDataFromStaticJson()));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.UpdateChangeAudit(GetChangeAuditDataFromStaticJson()));

        }


    }
}
