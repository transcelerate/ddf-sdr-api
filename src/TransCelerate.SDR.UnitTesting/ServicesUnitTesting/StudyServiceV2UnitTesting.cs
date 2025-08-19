using AutoMapper;
using Azure.Messaging.ServiceBus;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class StudyServiceV2UnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV2> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<ServiceBusClient> _mockServiceBusClient = new(MockBehavior.Loose);
        private readonly Mock<IStudyRepositoryV2> _mockStudyRepository = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        public static StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsEntity>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            return data;
        }
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            return data;
        }
        public static SoADto GetSOAV2DataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<SoADto>(jsonData);
        }
        public static List<ActivityEntity> GetActivitiesForSoADataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Activities;
        }
        public static List<EncounterEntity> GetEncountersForSoADataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Encounters;
        }
        public static List<ScheduleTimelineEntity> GetTimelinesForSoADataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).StudyScheduleTimelines;
        }
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV2());
            });
            _mockMapper = new Mapper(mockMapper);
            _mockHelper.Setup(x => x.RemoveStudyElements(It.IsAny<string[]>(), It.IsAny<StudyDefinitionsDto>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockHelper.Setup(x => x.RemoveStudyDesignElements(It.IsAny<string[]>(), It.IsAny<List<StudyDesignDto>>(), It.IsAny<string>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson().Study.StudyId));
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), 0))
                .Returns(Task.FromResult(GetEntityDataFromStaticJson().AuditTrail));
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;

            SdrCptMapping_NonStatic sdrCptMapping_NonStatic = JsonConvert.DeserializeObject<SdrCptMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SdrCptMasterDataMapping.json"));
            SdrCptMapping.SdrCptMasterDataMapping = sdrCptMapping_NonStatic.SdrCptMasterDataMapping;
        }
        #endregion

        #region TestCases

        #region POST All Elements Unit Testing
        [Test]
        public void PostAllElementsUnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            StudyDefinitionsEntity entity = null;
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            studyDto.Study.StudyTitle = "New";
            studyDto.Study.StudyId = "";
            studyEntity.AuditTrail = new AuditTrailEntity { EntryDateTime = DateTime.Now, SDRUploadVersion = 0, UsdmVersion = Constants.USDMVersions.V1_9 };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V1_9 };
            _mockStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.StudyId));
            _mockStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.StudyId));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V1_9 });
            StudyDefinitionsEntity studyEntity1 = GetEntityDataFromStaticJson(); studyEntity1.AuditTrail.SDRUploadVersion = 1; studyEntity1.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            ServiceBusSender serviceBusSender = Mock.Of<ServiceBusSender>();

            _mockServiceBusClient.Setup(x => x.CreateSender(It.IsAny<string>()))
                .Returns(serviceBusSender);

            //POST Unit Testing
            #region POST
            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);


            var method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);



            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(false);

            method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.Study.StudyId = "New";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));

            method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;



            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.Study.StudyId = null;

            method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            #endregion


            #region PUT
            //PUT Changes Unit Testing
            studyDto.Study.StudyId = "112233";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.PostAllElements(studyDto, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.Study.StudyId = "112233";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(null as AuditTrailEntity));
            method = studyService.PostAllElements(studyDto, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.AreEqual(result.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                   .Returns(false);

            method = studyService.PostAllElements(studyDto, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                   .Returns(true);

            method = studyService.PostAllElements(studyDto, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            #endregion

            _mockHelper.Setup(x => x.GetAuditTrail(Constants.USDMVersions.V1_9))
                 .Throws(new Exception("Error"));

            method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET Study
        [Test]
        public void GetStudy_UnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetStudy("1", 0);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudy("1", 0);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GetStudy("1", 0);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        #endregion

        #region GET StudyDesign
        [Test]
        public void GetStudyDesign_UnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetStudyDesigns("1", null, 0, null);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyEntity.Study.StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetStudyDesigns("1", null, 0, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudyDesigns("1", null, 0, Constants.StudyDesignElementsV2);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GetStudyDesigns("1", null, 0, null);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        #endregion

        #region Partial Study Elements Unit Testing
        [Test]
        public void GetPartialStudy_UnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV2);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV2);
            method.Wait();
            result = method.Result;

            studyEntity.Study.StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV2);
            method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV2);


            Assert.Throws<AggregateException>(method.Wait);
        }

        [Test]
        public void GetPartialStudyDesigns_UnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV2);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV2);
            method.Wait();
            result = method.Result;

            studyEntity.Study.StudyDesigns[0].Id = "a";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV2);
            method.Wait();

            studyEntity.Study.StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV2);
            method.Wait();

            //method = studyService.GetPartialStudyDesigns("1", "a", 0, user, Constants.StudyDesignElements);
            //method.Wait();

            //method = studyService.GetPartialStudyDesigns("1", null, 0, user, Constants.StudyDesignElements);
            //method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV2);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region Delete Study
        [Test]
        public void DeleteStudy_UnitTesting()
        {
            long count = 1;

            var deletResultAcknowledge = new MongoDB.Driver.DeleteResult.Acknowledged(1);
            Mock<MongoDB.Driver.DeleteResult> deleteResult = new();


            _mockStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(count));
            _mockStudyRepository.Setup(x => x.DeleteStudyAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(deleteResult.Object));

            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.DeleteStudy("1");
            method.Wait();
            var result = method.Result;

            count = 0;

            _mockStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(count));
            method = studyService.DeleteStudy("1");
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                  .Throws(new Exception("Error"));

            method = studyService.DeleteStudy("1");


            Assert.Throws<AggregateException>(method.Wait);

        }
        #endregion

        #region GET SoA
        [Test]
        public void GetSOAV2_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            SoADto SoA = GetSOAV2DataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            studyEntity.Study.StudyDesigns[0].Id = "Sd_1";
            studyEntity.Study.StudyDesigns[0].Encounters = GetEncountersForSoADataFromStaticJson();
            studyEntity.Study.StudyDesigns[0].Activities = GetActivitiesForSoADataFromStaticJson();
            studyEntity.Study.StudyDesigns[0].StudyScheduleTimelines = GetTimelinesForSoADataFromStaticJson();

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetSOAV2("1", studyEntity.Study.StudyDesigns[0].Id, studyEntity.Study.StudyDesigns[0].StudyScheduleTimelines[0].Id, 0);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV2("1", studyEntity.Study.StudyDesigns[0].Id, "", 0);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV2("1", "Sd", "", 0);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            method = studyService.GetSOAV2("1", studyEntity.Study.StudyDesigns[0].Id, "Wf1", 0);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ScheduleTimelineNotFound);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Throws(new Exception("Error"));

            method = studyService.GetSOAV2("1", "Sd_1", "Wf1", 0);


            Assert.Throws<AggregateException>(method.Wait);

            StudyDefinitionsEntity study = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(study));

            method = studyService.GetSOAV2("1", "Sd_1", "Wf1", 0);
            method.Wait();

            Assert.IsNull(method.Result);

            studyEntity.Study.StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(studyEntity));

            method = studyService.GetSOAV2("1", "", "", 0);
            method.Wait();

            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);
        }
        #endregion

        #region GET eCPT
        #region Get eCPT UnitTesting
        [Test]
        public void GeteCPTUnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            StudyServiceV2 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GeteCPTV2("a", 1, null);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GeteCPTV2("a", 1, "des");
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));
            method = studyService.GeteCPTV2("a", 1, "des");
            Assert.Throws<AggregateException>(method.Wait);
        }
        [Test]
        public void SexOfParticipants_UnitTesting()
        {
            var jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.DTO.StudyV2.StudyDefinitionsDto>(jsonData);
            var malePopulation = data.Study.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[1];
            data.Study.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV2.CodeDto> { data.Study.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[0], data.Study.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[0] };

            Assert.AreEqual(Constants.PlannedSexOfParticipants.Female, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV2(data.Study.StudyDesigns[0].StudyPopulations));

            data.Study.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV2.CodeDto> { malePopulation, malePopulation };
            Assert.AreEqual(Constants.PlannedSexOfParticipants.Male, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV2(data.Study.StudyDesigns[0].StudyPopulations));
            data.Study.StudyDesigns[0].StudyPopulations = null;
            Assert.IsNull(Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV2(data.Study.StudyDesigns[0].StudyPopulations));
        }
        #endregion
        #endregion
        #endregion
    }
}
