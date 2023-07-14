using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransCelerate.SDR.AzureFunctions;
using TransCelerate.SDR.AzureFunctions.DataAccess;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;

namespace TransCelerate.SDR.UnitTesting.AzureFunctionsUnitTesting
{
    public class MessageProcessorUnitTesting
    {

        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV2> _mockHelperV2 = new(MockBehavior.Loose);
        private readonly Mock<IHelperV3> _mockHelperV3 = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private readonly ILogHelper _mockLogger1 = Mock.Of<ILogHelper>();
        private readonly Mock<IMessageProcessor> _messageProcessor = new(MockBehavior.Loose);

        public static Core.Entities.StudyV2.StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            return JsonConvert.DeserializeObject<Core.Entities.StudyV2.StudyDefinitionsEntity>(jsonData);
        }
        public static Core.Entities.StudyV3.StudyDefinitionsEntity GetEntityDataFromStaticJsonV3()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            return JsonConvert.DeserializeObject<Core.Entities.StudyV3.StudyDefinitionsEntity>(jsonData);
        }

        public static ChangeAuditStudyEntity GetChangeAuditDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyEntity>(jsonData);
        }
        [SetUp]
        public void SetUp()
        {
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        [Test]
        public void ProcessMessage_UnitTesting()
        {
            HelperV2 helper = new();

            var currentVersion = GetEntityDataFromStaticJson();
            var previousVersion = GetEntityDataFromStaticJson();
            currentVersion.AuditTrail.SDRUploadVersion = 2;
            currentVersion.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            previousVersion.AuditTrail.SDRUploadVersion = 1;
            previousVersion.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            
            List<Core.Entities.StudyV2.StudyDefinitionsEntity> studyEntities = new()
            {
                currentVersion,
                previousVersion

            };
            var difference = helper.GetChangedValues(currentVersion, previousVersion);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsyncV2(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            _mockChangeAuditRepository.Setup(x => x.GetAuditTrailsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(JsonConvert.DeserializeObject<List<AuditTrailEntity>>(JsonConvert.SerializeObject(studyEntities.Select(z => z.AuditTrail).ToList())));
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));
            _mockChangeAuditRepository.Setup(x => x.UpdateChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));

            _mockHelperV2.Setup(x => x.GetChangedValues(It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>(), It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>()))
                .Returns(difference);
            _mockHelperV3.Setup(x => x.GetChangedValues(It.IsAny<Core.Entities.StudyV3.StudyDefinitionsEntity>(), It.IsAny<Core.Entities.StudyV3.StudyDefinitionsEntity>()))
                .Returns(difference);


            MessageProcessor processor = new(_mockChangeAuditRepository.Object, _mockHelperV2.Object, _mockHelperV3.Object);

            string message = "{\"Study_uuid\":\"aaed3efe-7d70-4c9e-90e2-3446e936c291\",\"CurrentVersion\":2}";

            processor.ProcessMessage(message);

            var currentVersionV3 = GetEntityDataFromStaticJsonV3();
            var previousVersionV3 = GetEntityDataFromStaticJsonV3();
            currentVersionV3.AuditTrail.SDRUploadVersion = 2;
            currentVersionV3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            previousVersionV3.AuditTrail.SDRUploadVersion = 1;
            previousVersionV3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;

            List<Core.Entities.StudyV3.StudyDefinitionsEntity> studyEntitiesV3 = new()
            {
                currentVersionV3,
                previousVersionV3

            };
            _mockChangeAuditRepository.Setup(x => x.GetAuditTrailsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(JsonConvert.DeserializeObject<List<AuditTrailEntity>>(JsonConvert.SerializeObject(studyEntitiesV3.Select(z => z.AuditTrail).ToList())));
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsyncV3(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntitiesV3);

            processor.ProcessMessage(message);

            
            _mockChangeAuditRepository.Setup(x => x.GetAuditTrailsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(JsonConvert.DeserializeObject<List<AuditTrailEntity>>(JsonConvert.SerializeObject(studyEntities.Select(z => z.AuditTrail).ToList())));

            ChangeAuditStudyEntity changeAuditStudyEntity = null;
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(changeAuditStudyEntity);
            processor.ProcessMessage(message);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            var currentVersion1 = GetEntityDataFromStaticJson();
            var previousVersion1 = GetEntityDataFromStaticJson();
            List<Core.Entities.StudyV2.StudyDefinitionsEntity> studyEntities1 = new()
            {
                currentVersion,
                previousVersion

            };            
            currentVersion1.Study.StudyProtocolVersions[0].ProtocolStatus.CodeSystemVersion = "10";
            var difference1 = helper.GetChangedValues(currentVersion1, previousVersion1);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsyncV2(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities1);
            _mockHelperV2.Setup(x => x.GetChangedValues(It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>(), It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>()))
                .Returns(difference1);
            processor.ProcessMessage(message);

            previousVersion.AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsyncV2(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities1);
            processor.ProcessMessage(message);

            studyEntities[0].AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
            _mockChangeAuditRepository.Setup(x => x.GetAuditTrailsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(JsonConvert.DeserializeObject<List<AuditTrailEntity>>(JsonConvert.SerializeObject(studyEntities.Select(z => z.AuditTrail).ToList())));
            processor.ProcessMessage(message);
            studyEntities[0].AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
        }

        [Test]
        public void ChangeAuditFunction_UnitTesting()
        {
            ChangeAuditFunction changeAuditFunction = new(_messageProcessor.Object, _mockLogger1);
            _messageProcessor.Setup(x => x.ProcessMessage(It.IsAny<string>()));

            changeAuditFunction.Run("unit test");

            _messageProcessor.Setup(x => x.ProcessMessage(It.IsAny<string>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => changeAuditFunction.Run("unit"));

        }
        [Test]
        public void GetChangedValueUnitTesting()
        {
            HelperV2 helper = new();

            var currentVersion = GetEntityDataFromStaticJson();
            var previousVersion = GetEntityDataFromStaticJson();

            currentVersion.Study.StudyIdentifiers.Add(currentVersion.Study.StudyIdentifiers[0]);
            currentVersion.Study.StudyIdentifiers[0].StudyIdentifier = "1";
            currentVersion.Study.StudyDesigns.Add(currentVersion.Study.StudyDesigns[0]);

            currentVersion.Study.StudyDesigns[0].InterventionModel.Code = "1";
            currentVersion.Study.StudyDesigns[0].TrialType[0].Code = "1";
            currentVersion.Study.StudyDesigns[0].TrialIntentType[0].Code = "1";
            currentVersion.Study.StudyDesigns[0].StudyIndications[0].IndicationDescription = "Everything";
            currentVersion.Study.StudyDesigns[0].StudyInvestigationalInterventions[0].InterventionDescription = "intervention2";
            currentVersion.Study.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0].EndpointDescription = "Endpoint3";
            currentVersion.Study.StudyDesigns[0].StudyPopulations[0].PopulationDescription = "population 2";
            currentVersion.Study.StudyDesigns[0].StudyCells[0].StudyArm.StudyArmDataOriginType.CodeSystem = "8";
            currentVersion.Study.StudyDesigns[0].StudyCells[0].StudyArm.StudyArmType.Decode = "placebo arm 1";
            currentVersion.Study.StudyDesigns[0].StudyCells[0].StudyElements[0].StudyElementDescription = "Element 3";
            currentVersion.Study.StudyDesigns[0].Activities[0].DefinedProcedures.Add(currentVersion.Study.StudyDesigns[0].Activities[0].DefinedProcedures[0]);
            currentVersion.Study.StudyDesigns[0].Activities[0].DefinedProcedures[1].Id = "4";
            currentVersion.Study.StudyDesigns[0].Activities[0].ActivityName = "A2";
            currentVersion.Study.StudyDesigns[0].Encounters[0].EncounterContactModes[0].Code = "C126876";
            currentVersion.Study.StudyDesigns[0].Encounters[0].EncounterEnvironmentalSetting.Decode = "clinic2";
            currentVersion.Study.StudyDesigns[0].Encounters[0].EncounterName = "Encounter 3";
            currentVersion.Study.StudyDesigns[0].Encounters[0].TransitionEndRule.Id = "3";
            currentVersion.Study.StudyDesigns[0].Encounters[0].NextEncounterId = "34";
            currentVersion.Study.StudyDesigns[0].StudyEstimands[0].VariableOfInterest = "purpose1";
            currentVersion.Study.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0].IntercurrentEventDescription = "Event 1 desc";
            currentVersion.Study.StudyDesigns[0].StudyEstimands[0].Treatment = "model 2";




            currentVersion.AuditTrail.SDRUploadVersion = 2;
            currentVersion.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            previousVersion.AuditTrail.SDRUploadVersion = 1;
            previousVersion.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            List<Core.Entities.StudyV2.StudyDefinitionsEntity> studyEntities = new()
            {
                currentVersion,
                previousVersion

            };
            var difference = helper.GetChangedValues(currentVersion, previousVersion);
            _mockChangeAuditRepository.Setup(x => x.GetStudyItemsAsyncV2(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(studyEntities);

            _mockChangeAuditRepository.Setup(x => x.GetAuditTrailsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(JsonConvert.DeserializeObject<List<AuditTrailEntity>>(JsonConvert.SerializeObject(studyEntities.Select(z => z.AuditTrail).ToList())));
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(GetChangeAuditDataFromStaticJson());
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));
            _mockChangeAuditRepository.Setup(x => x.UpdateChangeAudit(It.IsAny<ChangeAuditStudyEntity>()));

            _mockHelperV2.Setup(x => x.GetChangedValues(It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>(), It.IsAny<Core.Entities.StudyV2.StudyDefinitionsEntity>()))
                .Returns(difference);


            MessageProcessor processor = new(_mockChangeAuditRepository.Object, _mockHelperV2.Object, _mockHelperV3.Object);

            string message = "{\"Study_uuid\":\"aaed3efe-7d70-4c9e-90e2-3446e936c291\",\"CurrentVersion\":2}";

            processor.ProcessMessage(message);

            Assert.IsNotEmpty(HelperV2.CheckDifferences<Core.Entities.StudyV2.StudyEntity>(currentVersion.Study, previousVersion.Study));
            Assert.IsEmpty(HelperV2.CheckForNumberOfElementsMismatch<Core.Entities.StudyV2.StudyIdentifierEntity>(currentVersion.Study.StudyIdentifiers, previousVersion.Study.StudyIdentifiers));

            currentVersion.Study.StudyProtocolVersions[0].BriefTitle = "tests";
            Assert.IsNotEmpty(HelperV2.CheckForNumberOfElementsMismatch<Core.Entities.StudyV2.StudyProtocolVersionEntity>(currentVersion.Study.StudyProtocolVersions, previousVersion.Study.StudyProtocolVersions));

        }
        [Test]
        public void ChangeAuditRepo_UnitTesting()
        {
            Mock<IMongoDatabase> mongoDatabase = new(MockBehavior.Loose);
            Mock<IMongoClient> mongoClient = new(MockBehavior.Loose);
            Mock<IMongoCollection<Core.Entities.StudyV2.StudyDefinitionsEntity>> mongoCollectionStudy = new(MockBehavior.Strict);
            Mock<IMongoCollection<ChangeAuditStudyEntity>> mongoCollectionChangeAudit = new(MockBehavior.Strict);
            Mock<IClientSessionHandle> mongoClientSessionHandle = new(MockBehavior.Loose);
            Mock<IFindFluent<ChangeAuditStudyEntity, ChangeAuditStudyEntity>> _fakeCollectionResult = new();
            var asyncCursor = new Mock<IAsyncCursor<ChangeAuditStudyEntity>>();

            mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), null))
                .Returns(mongoDatabase.Object);

            mongoDatabase.Setup(x => x.GetCollection<ChangeAuditStudyEntity>(It.IsAny<string>(), null))
                .Returns(mongoCollectionChangeAudit.Object);

            mongoDatabase.Setup(x => x.GetCollection<Core.Entities.StudyV2.StudyDefinitionsEntity>(It.IsAny<string>(), null))
                .Returns(mongoCollectionStudy.Object);

            string study_uuid = "aaed3efe-7d70-4c9e-90e2-3446e936c291";



            ChangeAuditRepository changeAuditRepository = new(mongoClient.Object, _mockLogger);

            Assert.Throws<Moq.MockException>(() => changeAuditRepository.GetChangeAuditAsync(study_uuid));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.GetStudyItemsAsyncV2(study_uuid, 1));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.InsertChangeAudit(GetChangeAuditDataFromStaticJson()));
            Assert.Throws<Moq.MockException>(() => changeAuditRepository.UpdateChangeAudit(GetChangeAuditDataFromStaticJson()));

        }

    }
}
