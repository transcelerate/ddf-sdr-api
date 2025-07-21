using AutoMapper;
using Azure.Messaging.ServiceBus;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Entities.StudyV4;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class StudyServiceV4UnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV4> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<ServiceBusClient> _mockServiceBusClient = new(MockBehavior.Loose);
        private readonly Mock<IStudyRepositoryV4> _mockStudyRepository = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            return data;
        }
        public static SoADto GetSOAV4DataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<SoADto>(jsonData);
        }
        //public static List<ActivityEntity> GetActivitiesForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV4.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Activities;
        //}
        //public static List<EncounterEntity> GetEncountersForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV4.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Encounters;
        //}
        //public static List<ScheduleTimelineEntity> GetTimelinesForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV4.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).ScheduleTimelines;
        //}
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV4());
            });
            _mockMapper = new Mapper(mockMapper);
            _mockHelper.Setup(x => x.RemoveStudyElements(It.IsAny<string[]>(), It.IsAny<StudyDefinitionsDto>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockHelper.Setup(x => x.RemoveStudyDesignElements(It.IsAny<string[]>(), It.IsAny<List<StudyDesignDto>>(), It.IsAny<string>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson().Study.Id));
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), 0))
                .Returns(Task.FromResult(_mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson()).AuditTrail));
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
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            StudyDefinitionsEntity entity = null;
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();            
            studyDto.Study.Id = "";
            studyEntity.AuditTrail = new AuditTrailEntity { EntryDateTime = DateTime.Now, SDRUploadVersion = 0, UsdmVersion = Constants.USDMVersions.V3 };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V3 };
            _mockStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Id));
            _mockStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Id));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V3 });
            StudyDefinitionsEntity studyEntity1 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson()); studyEntity1.AuditTrail.SDRUploadVersion = 1; studyEntity1.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            ServiceBusSender serviceBusSender = Mock.Of<ServiceBusSender>();

            _mockServiceBusClient.Setup(x => x.CreateSender(It.IsAny<string>()))
                .Returns(serviceBusSender);

            //POST Unit Testing
            #region POST
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);


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

            studyDto.Study.Id = "New";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));

            method = studyService.PostAllElements(studyDto, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;



            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.Study.Id = null;

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
            studyDto.Study.Id = "112233";
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

            studyDto.Study.Id = "112233";
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

            _mockHelper.Setup(x => x.GetAuditTrail(Constants.USDMVersions.V3))
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
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

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
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetStudyDesigns("1", null, 0, null);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetStudyDesigns("1", null, 0, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudyDesigns("1", null, 0, Constants.StudyDesignElementsV4);


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
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV4);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV4);
            method.Wait();
            result = method.Result;

            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV4);
            method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyElements("1", 0, Constants.StudyElementsV4);


            Assert.Throws<AggregateException>(method.Wait);
        }

        [Test]
        public void GetPartialStudyDesigns_UnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV4);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV4);
            method.Wait();
            result = method.Result;

            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id = "a";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV4);
            method.Wait();

            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV4);
            method.Wait();

            //method = studyService.GetPartialStudyDesigns("1", "a", 0, user, Constants.StudyDesignElements);
            //method.Wait();

            //method = studyService.GetPartialStudyDesigns("1", null, 0, user, Constants.StudyDesignElements);
            //method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyDesigns("1", "b", 0, Constants.StudyDesignElementsV4);


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

            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

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
        public void GetSOAV4_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            SoADto SoA = GetSOAV4DataFromStaticJson();
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id = "Sd_1";

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetSOAV4("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].ScheduleTimelines[0].Id, 0);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV4("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, "", 0);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV4("1", "Sd", "", 0);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            method = studyService.GetSOAV4("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, "Wf1", 0);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ScheduleTimelineNotFound);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Throws(new Exception("Error"));

            method = studyService.GetSOAV4("1", "StudyDesign_1", "ScheduleTimeline_4", 0);


            Assert.Throws<AggregateException>(method.Wait);

            StudyDefinitionsEntity study = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(study));

            method = studyService.GetSOAV4("1", "StudyDesign_1", "ScheduleTimeline_4", 0);
            method.Wait();

            Assert.IsNull(method.Result);

            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(studyEntity));

            method = studyService.GetSOAV4("1", "", "", 0);
            method.Wait();

            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);
        }
        #endregion

        #region Get eCPT UnitTesting
        [Test]
        public void GeteCPTUnitTesting()
        {
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GeteCPTV4("a", 1, null);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GeteCPTV4("a", 1, "des");
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));
            method = studyService.GeteCPTV4("a", 1, "des");
            Assert.Throws<AggregateException>(method.Wait);
        }
        [Test]
        public void SexOfParticipants_UnitTesting()
        {
            //var jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");
            //var data = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.DTO.StudyV4.StudyDefinitionsDto>(jsonData);
            //var malePopulation = data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[1];
            //data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV4.CodeDto> { data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[0], data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[0] };

            //Assert.AreEqual(Constants.PlannedSexOfParticipants.Female, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV4(data.Study.StudyDesigns[0].Populations));

            //data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV4.CodeDto> { malePopulation, malePopulation };
            //Assert.AreEqual(Constants.PlannedSexOfParticipants.Male, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV4(data.Study.StudyDesigns[0].Populations));
            //data.Study.StudyDesigns[0].Populations = null;
            //Assert.IsNull(Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV4(data.Study.StudyDesigns[0].Populations));
        }
        #endregion

        #region GET Study
        [Test]
        public void GetDifferences_UnitTesting()
        {
            var currentVersionV4 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            var previousVersionV4 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            currentVersionV4.AuditTrail.SDRUploadVersion = 2;
            currentVersionV4.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            previousVersionV4.AuditTrail.SDRUploadVersion = 1;
            previousVersionV4.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;

            currentVersionV4.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            previousVersionV4.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 1))
                   .Returns(Task.FromResult(currentVersionV4));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                   .Returns(Task.FromResult(previousVersionV4));
            StudyServiceV4 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetDifferences("1", 1, 2);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<VersionCompareDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            method = studyService.GetDifferences("1", 1, 2);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ForbiddenForAStudy);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetDifferences("1", 1, 2);


            Assert.Throws<AggregateException>(method.Wait);

            currentVersionV4.Study.Versions.FirstOrDefault().StudyType.Decode = "INTERVENTIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 1))
                  .Returns(Task.FromResult(currentVersionV4));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                  .Returns(Task.FromResult(previousVersionV4));

            method = studyService.GetDifferences("1", 1, 2);
            method.Wait();

            Assert.AreEqual(method.Result, Constants.ErrorMessages.ForbiddenForAStudy);


            previousVersionV4 = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                  .Returns(Task.FromResult(previousVersionV4));

            method = studyService.GetDifferences("1", 1, 2);
            method.Wait();

            Assert.AreEqual(method.Result, Constants.ErrorMessages.OneVersionNotFound);

            previousVersionV4 = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(previousVersionV4));

            method = studyService.GetDifferences("1", 1, 2);
            method.Wait();

            Assert.IsNull(method.Result);            
        }
        #endregion        
        #endregion
    }
}
