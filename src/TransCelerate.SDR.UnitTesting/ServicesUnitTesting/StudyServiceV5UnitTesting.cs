using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV5;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class StudyServiceV5UnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV5> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<ServiceBusClient> _mockServiceBusClient = new(MockBehavior.Loose);
        private readonly Mock<IStudyRepositoryV5> _mockStudyRepository = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            return data;
        }
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public static SoADto GetSOAV5DataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<SoADto>(jsonData);
        }
        //public static List<ActivityEntity> GetActivitiesForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV5.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Activities;
        //}
        //public static List<EncounterEntity> GetEncountersForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV5.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).Encounters;
        //}
        //public static List<ScheduleTimelineEntity> GetTimelinesForSoADataFromStaticJson()
        //{
        //    string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleDataV5.json");
        //    return JsonConvert.DeserializeObject<StudyDesignEntity>(jsonData).ScheduleTimelines;
        //}
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        [SetUp]
        public void SetUp()
        {
            Config.IsGroupFilterEnabled = false;
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV5());
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
            studyEntity.AuditTrail = new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 0, UsdmVersion = Constants.USDMVersions.V3 };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V3 };
            _mockStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Id));
            _mockStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Id));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>(),It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V3 });
            StudyDefinitionsEntity studyEntity1 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson()); studyEntity1.AuditTrail.SDRUploadVersion = 1; studyEntity1.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            ServiceBusSender serviceBusSender = Mock.Of<ServiceBusSender>();

            _mockServiceBusClient.Setup(x => x.CreateSender(It.IsAny<string>()))
                .Returns(serviceBusSender);

            //POST Unit Testing
            #region POST
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);


            var method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);



            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(false);

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
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

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;



            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.Study.Id = null;

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
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
            method = studyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
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
            method = studyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
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

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                   .Returns(true);

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            #endregion


            var groups = GetUserDataFromStaticJson();
            groups.SDRGroups.ForEach(x => x.Permission = Permissions.READONLY.ToString());
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(groups.SDRGroups));
            user.UserRole = Constants.Roles.App_User;
            Config.IsGroupFilterEnabled = true;
            method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;
            Config.IsGroupFilterEnabled = false;

            //Actual            
            var actual_result1 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result1.ToString(), Constants.ErrorMessages.PostRestricted);

            _mockHelper.Setup(x => x.GetAuditTrail(user.UserName, Constants.USDMVersions.V3))
                 .Throws(new Exception("Error"));

            method = studyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);

            Assert.Throws<AggregateException>(method.Wait);




        }
        [Test]
        public void CheckPermissionForAUser_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.Org_Admin;
            method = studyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.App_User;
            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            method = studyService.CheckPermissionForAUser(user);
            method.Wait();
            Assert.IsFalse(method.Result);

            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = studyService.CheckPermissionForAUser(user);


            Assert.Throws<AggregateException>(method.Wait);

        }
        #endregion

        #region GET Study
        [Test]
        public void GetStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetStudy("1", 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetStudy("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudy("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GetStudy("1", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        [Test]
        public void CheckAccessForAStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());

            Assert.AreEqual(expected.Study.Id, method.Result.Study.Id);

            study.Study.Id = "studyId1";
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();


            Assert.AreEqual("studyId1", method.Result.Study.Id);

            study.Study.Id = "studyId5";
            study.Study.Versions.FirstOrDefault().StudyType.Decode = "Interventional";
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.Study.Id);

            Config.IsGroupFilterEnabled = false;
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.Study.Id);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.IsGroupFilterEnabled = true;
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            StudyServiceV5 studyService1 = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);
            method = studyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = studyService.CheckAccessForAStudy(study, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion        

        #region GET StudyDesign
        [Test]
        public void GetStudyDesign_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetStudyDesigns("1", null, 0, user, null);
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
            method = studyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudyDesigns("1", null, 0, user, Constants.StudyDesignElementsV5);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        #endregion

        #region Partial Study Elements Unit Testing
        [Test]
        public void GetPartialStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyElements("1", 0, user, Constants.StudyElementsV5);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyElements("1", 0, user, Constants.StudyElementsV5);
            method.Wait();
            result = method.Result;

            user.UserRole = Constants.Roles.App_User;
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyElements("1", 0, user, Constants.StudyElementsV5);
            method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyElements("1", 0, user, Constants.StudyElementsV5);


            Assert.Throws<AggregateException>(method.Wait);
        }

        [Test]
        public void GetPartialStudyDesigns_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV5);
            method.Wait();
            var result = method.Result;

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(null as StudyDefinitionsEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV5);
            method.Wait();
            result = method.Result;

            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id = "a";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV5);
            method.Wait();

            user.UserRole = Constants.Roles.App_User;
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "FAILURE STUDY TYPE";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV5);
            method.Wait();

            user.UserRole = Constants.Roles.Org_Admin;

            //method = studyService.GetPartialStudyDesigns("1", "a", 0, user, Constants.StudyDesignElements);
            //method.Wait();

            //method = studyService.GetPartialStudyDesigns("1", null, 0, user, Constants.StudyDesignElements);
            //method.Wait();


            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV5);


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

            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.DeleteStudy("1", user);
            method.Wait();
            var result = method.Result;

            count = 0;

            _mockStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(count));
            method = studyService.DeleteStudy("1", user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                  .Throws(new Exception("Error"));

            method = studyService.DeleteStudy("1", user);


            Assert.Throws<AggregateException>(method.Wait);

        }
        #endregion

        #region GetAccess For A Study
        [Test]
        public void GetAccessForAStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsForCheckingAccessAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetAccessForAStudy("1", 0, user);
            method.Wait();
            var result = method.Result;

            Assert.IsTrue(result);

            user.UserRole = Constants.Roles.App_User;

            method = studyService.GetAccessForAStudy("1", 0, user);
            method.Wait();
            result = method.Result;

            Assert.IsFalse(result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsForCheckingAccessAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetAccessForAStudy("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET SoA
        [Test]
        public void GetSOAV5_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";

            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            SoADto SoA = GetSOAV5DataFromStaticJson();
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id = "Sd_1";

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetSOAV5("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].ScheduleTimelines[0].Id, 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV5("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, "", 0, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = studyService.GetSOAV5("1", "Sd", "", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            method = studyService.GetSOAV5("1", studyEntity.Study.Versions.FirstOrDefault().StudyDesigns[0].Id, "Wf1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ScheduleTimelineNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetSOAV5("1", "StudyDesign_1", "ScheduleTimeline_4", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Throws(new Exception("Error"));

            method = studyService.GetSOAV5("1", "StudyDesign_1", "ScheduleTimeline_4", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            StudyDefinitionsEntity study = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(study));

            method = studyService.GetSOAV5("1", "StudyDesign_1", "ScheduleTimeline_4", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

            user.UserRole = Constants.Roles.Org_Admin;
            studyEntity.Study.Versions.FirstOrDefault().StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(studyEntity));

            method = studyService.GetSOAV5("1", "", "", 0, user);
            method.Wait();

            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);
        }
        #endregion

        #region Get eCPT UnitTesting
        [Test]
        public void GeteCPTUnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            studyEntity.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                 .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GeteCPTV5("a", 1, null, user);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            Config.IsGroupFilterEnabled = false;


            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GeteCPTV5("a", 1, "des", user);
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));
            method = studyService.GeteCPTV5("a", 1, "des", user);
            Assert.Throws<AggregateException>(method.Wait);
        }
        [Test]
        public void SexOfParticipants_UnitTesting()
        {
            //var jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
            //var data = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionsDto>(jsonData);
            //var malePopulation = data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[1];
            //data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV5.CodeDto> { data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[0], data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants[0] };

            //Assert.AreEqual(Constants.PlannedSexOfParticipants.Female, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV5(data.Study.StudyDesigns[0].Populations));

            //data.Study.StudyDesigns[0].Populations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV5.CodeDto> { malePopulation, malePopulation };
            //Assert.AreEqual(Constants.PlannedSexOfParticipants.Male, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV5(data.Study.StudyDesigns[0].Populations));
            //data.Study.StudyDesigns[0].Populations = null;
            //Assert.IsNull(Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV5(data.Study.StudyDesigns[0].Populations));
        }
        #endregion

        #region GET Study
        [Test]
        public void GetDifferences_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";

            var currentVersionV5 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            var previousVersionV5 = _mockMapper.Map<StudyDefinitionsEntity>(GetDtoDataFromStaticJson());
            currentVersionV5.AuditTrail.SDRUploadVersion = 2;
            currentVersionV5.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            previousVersionV5.AuditTrail.SDRUploadVersion = 1;
            previousVersionV5.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;

            currentVersionV5.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            previousVersionV5.Study.Versions.FirstOrDefault().StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 1))
                   .Returns(Task.FromResult(currentVersionV5));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                   .Returns(Task.FromResult(previousVersionV5));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV5 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = studyService.GetDifferences("1", 1, 2, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<VersionCompareDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetDifferences("1", 1, 2, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ForbiddenForAStudy);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetDifferences("1", 1, 2, user);


            Assert.Throws<AggregateException>(method.Wait);

            currentVersionV5.Study.Versions.FirstOrDefault().StudyType.Decode = "INTERVENTIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 1))
                  .Returns(Task.FromResult(currentVersionV5));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                  .Returns(Task.FromResult(previousVersionV5));

            method = studyService.GetDifferences("1", 1, 2, user);
            method.Wait();

            Assert.AreEqual(method.Result, Constants.ErrorMessages.ForbiddenForAStudy);


            previousVersionV5 = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 2))
                  .Returns(Task.FromResult(previousVersionV5));

            method = studyService.GetDifferences("1", 1, 2, user);
            method.Wait();

            Assert.AreEqual(method.Result, Constants.ErrorMessages.OneVersionNotFound);

            previousVersionV5 = null;
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(previousVersionV5));

            method = studyService.GetDifferences("1", 1, 2, user);
            method.Wait();

            Assert.IsNull(method.Result);            
        }
        #endregion        
        #endregion
    }
}
