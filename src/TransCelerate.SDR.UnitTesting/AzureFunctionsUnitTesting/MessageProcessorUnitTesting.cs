using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using TransCelerate.SDR.AzureFunctions;
using NUnit.Framework;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ObjectsComparer;
using MongoDB.Driver;

namespace TransCelerate.SDR.UnitTesting.AzureFunctionsUnitTesting
{
    public class MessageProcessorUnitTesting
    {

        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IHelperV2> _mockHelper = new Mock<IHelperV2>(MockBehavior.Loose);
        private Mock<IChangeAuditRepository> _mockChangeAuditRepository = new Mock<IChangeAuditRepository>(MockBehavior.Loose);
        private ILogHelper _mockLogger1 = Mock.Of<ILogHelper>();
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
            HelperV2 helper =new HelperV2();

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
        public void ChangeAuditFunction_UnitTesting()
        {
            ChangeAuditFunction changeAuditFunction = new ChangeAuditFunction(_messageProcessor.Object, _mockLogger1);
            _messageProcessor.Setup(x => x.ProcessMessage(It.IsAny<string>()));

            changeAuditFunction.Run("unit test");

            _messageProcessor.Setup(x => x.ProcessMessage(It.IsAny<string>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => changeAuditFunction.Run("unit"));

        }
        [Test]
        public void GetChangedValueUnitTesting()
        {
            HelperV2 helper = new HelperV2();

            var currentVersion = GetEntityDataFromStaticJson();
            var previousVersion = GetEntityDataFromStaticJson();
            List<StudyEntity> studyEntities = new List<StudyEntity>
            {
                currentVersion,previousVersion

            };
            currentVersion.ClinicalStudy.StudyIdentifiers.Add(currentVersion.ClinicalStudy.StudyIdentifiers[0]);
            currentVersion.ClinicalStudy.StudyIdentifiers[0].StudyIdentifier = "1";
            currentVersion.ClinicalStudy.StudyDesigns.Add(currentVersion.ClinicalStudy.StudyDesigns[0]);
      
            currentVersion.ClinicalStudy.StudyDesigns[0].InterventionModel[0].Code = "1";
            currentVersion.ClinicalStudy.StudyDesigns[0].TrialType[0].Code = "1";
            currentVersion.ClinicalStudy.StudyDesigns[0].TrialIntentType[0].Code = "1";
        
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyIndications[0].IndicationDescription = "Everything";
         
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions[0].InterventionDescription = "intervention2";
        
        
       
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0].EndpointDescription = "Endpoint3";
          
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PopulationDescription = "population 2";
        
        
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyArm.StudyArmDataOriginType[0].CodeSystem = "8";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyArm.StudyArmType[0].Decode = "placebo arm 1";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[0].EncounterDescription = "desc1";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements[0].StudyElementDescription = "Element 3";        
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemDescription = "sample 2";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems.Add(currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0]);
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].Id = "1";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection.Add(currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection[0]);
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection[1].Id = "4";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures.Add(currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures[0]);
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures[1].Id = "4";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection[0].StudyDataName = "study2";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.ActivityName = "A2";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.EncounterContactMode[0].Code = "C126876";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.EncounterEnvironmentalSetting[0].Decode = "clinic2";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.EncounterName = "Encounter 3";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.TransitionEndRule.Id = "3";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.NextEncounterId = "34";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].VariableOfInterest.EndpointLevel[0].Decode = "purpose1";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0].IntercurrentEventDescription = "Event 1 desc";
            currentVersion.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].Treatment.Codes[0].Decode = "model 2";




            var difference = helper.GetChangedValues(currentVersion, previousVersion);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));
            _mockChangeAuditRepository.Setup(x => x.UpdateChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));

            _mockHelper.Setup(x => x.GetChangedValues(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                .Returns(difference);


            MessageProcessor processor = new MessageProcessor(_mockLogger, _mockChangeAuditRepository.Object, _mockHelper.Object);

            string message = "{\"Study_uuid\":\"aaed3efe-7d70-4c9e-90e2-3446e936c291\",\"CurrentVersion\":2}";

            processor.ProcessMessage(message);

            Assert.IsNotEmpty(helper.CheckDifferences<ClinicalStudyEntity>(currentVersion.ClinicalStudy, previousVersion.ClinicalStudy));
            Assert.IsEmpty(helper.CheckForNumberOfElementsMismatch<StudyIdentifierEntity>(currentVersion.ClinicalStudy.StudyIdentifiers, previousVersion.ClinicalStudy.StudyIdentifiers));

            currentVersion.ClinicalStudy.StudyProtocolVersions[0].BriefTitle = "tests";
            Assert.IsNotEmpty(helper.CheckForNumberOfElementsMismatch<StudyProtocolVersionEntity>(currentVersion.ClinicalStudy.StudyProtocolVersions, previousVersion.ClinicalStudy.StudyProtocolVersions));

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

            mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null))
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
