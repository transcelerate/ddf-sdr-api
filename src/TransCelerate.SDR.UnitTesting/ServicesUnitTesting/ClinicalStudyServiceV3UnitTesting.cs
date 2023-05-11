﻿using AutoMapper;
using Azure.Messaging.ServiceBus;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV3;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class ClinicalStudyServiceV3UnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV3> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<ServiceBusClient> _mockServiceBusClient = new(MockBehavior.Loose);
        private readonly Mock<IClinicalStudyRepositoryV3> _mockClinicalStudyRepository = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        public static StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            var data = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            return data;
        }
        public static StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            var data = JsonConvert.DeserializeObject<StudyDto>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            return data;
        }
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public static SoADto GetSOAV3DataFromStaticJson()
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
                cfg.AddProfile(new AutoMapperProfilesV3());
            });
            _mockMapper = new Mapper(mockMapper);
            _mockHelper.Setup(x => x.RemoveStudyElements(It.IsAny<string[]>(), It.IsAny<StudyDto>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockHelper.Setup(x => x.RemoveStudyDesignElements(It.IsAny<string[]>(), It.IsAny<List<StudyDesignDto>>(), It.IsAny<string>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson()));
            _mockChangeAuditRepository.Setup(x => x.InsertChangeAudit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(GetDtoDataFromStaticJson().ClinicalStudy.StudyId));
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), 0))
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
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            StudyEntity entity = null;
            StudyDto studyDto = GetDtoDataFromStaticJson();
            studyDto.ClinicalStudy.StudyTitle = "New";
            studyDto.ClinicalStudy.StudyId = "";
            studyEntity.AuditTrail = new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 0, UsdmVersion = Constants.USDMVersions.V1_9 };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V1_9 };
            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(studyDto.ClinicalStudy.StudyId));
            _mockClinicalStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(studyDto.ClinicalStudy.StudyId));
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = Constants.USDMVersions.V1_9 });
            StudyEntity studyEntity1 = GetEntityDataFromStaticJson(); studyEntity1.AuditTrail.SDRUploadVersion = 1; studyEntity1.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            ServiceBusSender serviceBusSender = Mock.Of<ServiceBusSender>();

            _mockServiceBusClient.Setup(x => x.CreateSender(It.IsAny<string>()))
                .Returns(serviceBusSender);

            //POST Unit Testing
            #region POST
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);


            var method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);



            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                    .Returns(false);

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.ClinicalStudy.StudyId = "New";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;



            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.ClinicalStudy.StudyId = null;

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            #endregion


            #region PUT
            //PUT Changes Unit Testing
            studyDto.ClinicalStudy.StudyId = "112233";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyDto.ClinicalStudy.StudyId = "112233";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(null as AuditTrailEntity));
            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.AreEqual(result.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                   .Returns(false);

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                   .Returns(true);

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Put.Method);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            #endregion


            var groups = GetUserDataFromStaticJson();
            groups.SDRGroups.ForEach(x => x.Permission = Permissions.READONLY.ToString());
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(groups.SDRGroups));
            user.UserRole = Constants.Roles.App_User;
            Config.IsGroupFilterEnabled = true;
            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);
            method.Wait();
            result = method.Result;
            Config.IsGroupFilterEnabled = false;

            //Actual            
            var actual_result1 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result1.ToString(), Constants.ErrorMessages.PostRestricted);

            _mockHelper.Setup(x => x.GetAuditTrail(user.UserName))
                 .Throws(new Exception("Error"));

            method = ClinicalStudyService.PostAllElements(studyDto, user, HttpMethod.Post.Method);

            Assert.Throws<AggregateException>(method.Wait);




        }
        [Test]
        public void CheckPermissionForAUser_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.Org_Admin;
            method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.App_User;
            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();
            Assert.IsFalse(method.Result);

            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.CheckPermissionForAUser(user);


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
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetStudy("1", 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.GetStudy("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetStudy("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = ClinicalStudyService.GetStudy("1", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        [Test]
        public void CheckAccessForAStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetEntityDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = GetEntityDataFromStaticJson();

            Assert.AreEqual(expected.ClinicalStudy.StudyId, method.Result.ClinicalStudy.StudyId);

            study.ClinicalStudy.StudyId = "studyId1";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();


            Assert.AreEqual("studyId1", method.Result.ClinicalStudy.StudyId);

            study.ClinicalStudy.StudyId = "studyId5";
            study.ClinicalStudy.StudyType.Decode = "Interventional";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.StudyId);

            Config.IsGroupFilterEnabled = false;
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.StudyId);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.IsGroupFilterEnabled = true;
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            ClinicalStudyServiceV3 ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);
            method = ClinicalStudyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.CheckAccessForAStudy(study, user);


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
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyEntity.ClinicalStudy.StudyDesigns = null;
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.GetStudyDesigns("1", null, 0, user, null);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetStudyDesigns("1", null, 0, user, Constants.StudyDesignElementsV3);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = ClinicalStudyService.GetStudyDesigns("1", null, 0, user, null);
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
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetPartialStudyElements("1", 0, user, Constants.ClinicalStudyElementsV3);
            method.Wait();
            var result = method.Result;

            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                 .Returns(Task.FromResult(null as StudyEntity));
            method = ClinicalStudyService.GetPartialStudyElements("1", 0, user, Constants.ClinicalStudyElementsV3);
            method.Wait();
            result = method.Result;

            user.UserRole = Constants.Roles.App_User;
            studyEntity.ClinicalStudy.StudyType.Decode = "FAILURE STUDY TYPE";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                   .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.GetPartialStudyElements("1", 0, user, Constants.ClinicalStudyElementsV3);
            method.Wait();


            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetPartialStudyElements("1", 0, user, Constants.ClinicalStudyElementsV3);


            Assert.Throws<AggregateException>(method.Wait);
        }

        [Test]
        public void GetPartialStudyDesigns_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV3);
            method.Wait();
            var result = method.Result;

            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(null as StudyEntity));
            method = ClinicalStudyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV3);
            method.Wait();
            result = method.Result;

            studyEntity.ClinicalStudy.StudyDesigns[0].Id = "a";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV3);
            method.Wait();

            user.UserRole = Constants.Roles.App_User;
            studyEntity.ClinicalStudy.StudyType.Decode = "FAILURE STUDY TYPE";
            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV3);
            method.Wait();

            user.UserRole = Constants.Roles.Org_Admin;

            //method = ClinicalStudyService.GetPartialStudyDesigns("1", "a", 0, user, Constants.StudyDesignElements);
            //method.Wait();

            //method = ClinicalStudyService.GetPartialStudyDesigns("1", null, 0, user, Constants.StudyDesignElements);
            //method.Wait();


            _mockClinicalStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetPartialStudyDesigns("1", "b", 0, user, Constants.StudyDesignElementsV3);


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


            _mockClinicalStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(count));
            _mockClinicalStudyRepository.Setup(x => x.DeleteStudyAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(deleteResult.Object));

            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.DeleteStudy("1", user);
            method.Wait();
            var result = method.Result;

            count = 0;

            _mockClinicalStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                   .Returns(Task.FromResult(count));
            method = ClinicalStudyService.DeleteStudy("1", user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockClinicalStudyRepository.Setup(x => x.CountAsync(It.IsAny<string>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.DeleteStudy("1", user);


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
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsForCheckingAccessAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetAccessForAStudy("1", 0, user);
            method.Wait();
            var result = method.Result;

            Assert.IsTrue(result);

            user.UserRole = Constants.Roles.App_User;

            method = ClinicalStudyService.GetAccessForAStudy("1", 0, user);
            method.Wait();
            result = method.Result;

            Assert.IsFalse(result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsForCheckingAccessAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetAccessForAStudy("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET SoA
        [Test]
        public void GetSOAV3_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";

            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            SoADto SoA = GetSOAV3DataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            studyEntity.ClinicalStudy.StudyDesigns[0].Id = "Sd_1";
            studyEntity.ClinicalStudy.StudyDesigns[0].Encounters = GetEncountersForSoADataFromStaticJson();
            studyEntity.ClinicalStudy.StudyDesigns[0].Activities = GetActivitiesForSoADataFromStaticJson();
            studyEntity.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines = GetTimelinesForSoADataFromStaticJson();

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));

            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GetSOAV3("1", studyEntity.ClinicalStudy.StudyDesigns[0].Id, studyEntity.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0].Id, 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = ClinicalStudyService.GetSOAV3("1", studyEntity.ClinicalStudy.StudyDesigns[0].Id, "", 0, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<SoADto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsInstanceOf(typeof(SoADto), result);

            method = ClinicalStudyService.GetSOAV3("1", "Sd", "", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            method = ClinicalStudyService.GetSOAV3("1", studyEntity.ClinicalStudy.StudyDesigns[0].Id, "Wf1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.ScheduleTimelineNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.GetSOAV3("1", "Sd_1", "Wf1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetSOAV3("1", "Sd_1", "Wf1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            StudyEntity study = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(study));

            method = ClinicalStudyService.GetSOAV3("1", "Sd_1", "Wf1", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

            user.UserRole = Constants.Roles.Org_Admin;
            studyEntity.ClinicalStudy.StudyDesigns = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                 .Returns(Task.FromResult(studyEntity));

            method = ClinicalStudyService.GetSOAV3("1", "", "", 0, user);
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
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                 .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            ClinicalStudyServiceV3 ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object, _mockServiceBusClient.Object, _mockChangeAuditRepository.Object);

            var method = ClinicalStudyService.GeteCPTV3("a", 1, null, user);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            Config.IsGroupFilterEnabled = false;


            studyEntity = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = ClinicalStudyService.GeteCPTV3("a", 1, "des", user);
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));
            method = ClinicalStudyService.GeteCPTV3("a", 1, "des", user);
            Assert.Throws<AggregateException>(method.Wait);
        }
        [Test]
        public void SexOfParticipants_UnitTesting()
        {
            var jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            var data = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.DTO.StudyV3.StudyDto>(jsonData);
            var malePopulation = data.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[1];
            data.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV3.CodeDto> { data.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[0], data.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants[0] };

            Assert.AreEqual(Constants.PlannedSexOfParticipants.Female, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV3(data.ClinicalStudy.StudyDesigns[0].StudyPopulations));

            data.ClinicalStudy.StudyDesigns[0].StudyPopulations[0].PlannedSexOfParticipants = new List<Core.DTO.StudyV3.CodeDto> { malePopulation, malePopulation };
            Assert.AreEqual(Constants.PlannedSexOfParticipants.Male, Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV3(data.ClinicalStudy.StudyDesigns[0].StudyPopulations));
            data.ClinicalStudy.StudyDesigns[0].StudyPopulations = null;
            Assert.IsNull(Core.Utilities.Helpers.ECPTHelper.GetPlannedSexOfParticipantsV3(data.ClinicalStudy.StudyDesigns[0].StudyPopulations));
        }
        #endregion
        #endregion
    }
}