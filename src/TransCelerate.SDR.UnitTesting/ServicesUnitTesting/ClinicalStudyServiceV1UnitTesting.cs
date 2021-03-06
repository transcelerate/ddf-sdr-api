using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class ClinicalStudyServiceV1UnitTesting
    {
        #region Variables
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IHelper> _mockHelper = new Mock<IHelper>(MockBehavior.Loose);
        private Mock<IClinicalStudyRepositoryV1> _mockClinicalStudyRepository = new Mock<IClinicalStudyRepositoryV1>(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        public StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }
        public StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
        }
        public UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        [SetUp]
        public void SetUp()
        {
            Config.isGroupFilterEnabled = false;
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV1());
            });
            _mockMapper = new Mapper(mockMapper);
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
            studyEntity.AuditTrail = new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now , SDRUploadVersion = 0 };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now,SDRUploadVersion = 1 };
            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(studyDto.ClinicalStudy.Uuid));
            _mockClinicalStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(studyDto.ClinicalStudy.Uuid));
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(),0))
                    .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockHelper.Setup(x=>x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { CreatedBy = user.UserName,EntryDateTime = DateTime.Now, SDRUploadVersion = 1});
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger,_mockHelper.Object);
            

            var method = ClinicalStudyService.PostAllElements(studyDto, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);    
            


            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                    .Returns(false);
            _mockHelper.Setup(x => x.CheckForSections(It.IsAny<StudyEntity>(), It.IsAny<StudyEntity>()))
                    .Returns(studyEntity);

            method = ClinicalStudyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));

            method = ClinicalStudyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            var actual_result1 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result1.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockHelper.Setup(x => x.GeneratedSectionId(It.IsAny<StudyEntity>()))
                    .Returns(studyEntity);
            studyDto.ClinicalStudy.Uuid = null;

            method = ClinicalStudyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            var groups = GetUserDataFromStaticJson();
            groups.SDRGroups.ForEach(x => x.permission = Permissions.READONLY.ToString());
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(groups.SDRGroups));
            user.UserRole = Constants.Roles.App_User;
            Config.isGroupFilterEnabled = true;
            method = ClinicalStudyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;
            Config.isGroupFilterEnabled = false;

            //Actual            
            actual_result1 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result1.ToString(), Constants.ErrorMessages.PostRestricted);

            _mockHelper.Setup(x => x.GetAuditTrail(user.UserName))
                 .Throws(new Exception("Error"));

            method = ClinicalStudyService.PostAllElements(studyDto, user);
            
            Assert.Throws<AggregateException>(method.Wait);

        }
        [Test]
        public void CheckPermissionForAUser_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

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
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(),It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.GetStudy("1",0,user);
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
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetEntityDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = GetEntityDataFromStaticJson();

            Assert.AreEqual(expected.ClinicalStudy.Uuid, method.Result.ClinicalStudy.Uuid);

            study.ClinicalStudy.Uuid = "studyId1";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();


            Assert.AreEqual("studyId1", method.Result.ClinicalStudy.Uuid);

            study.ClinicalStudy.Uuid = "studyId5";
            study.ClinicalStudy.StudyType.Decode = "Interventional";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.Uuid);

            Config.isGroupFilterEnabled = false;
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.Uuid);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.isGroupFilterEnabled = true;
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            ClinicalStudyServiceV1 ClinicalStudyService1 = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);
            method = ClinicalStudyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.CheckAccessForAStudy(study, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region Search Study
        [Test]
        public void SearchStudyUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<StudyDto> studyList = new() { study };
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            SearchResponseEntity searchResponseEntity = new()
            {
                EntryDateTime = DateTime.Now,
                SDRUploadVersion = 2,
                InterventionModel = studyEntity.ClinicalStudy.StudyDesigns.Select(x=>x.InterventionModel),
                StudyIdentifiers = studyEntity.ClinicalStudy.StudyIdentifiers,
                StudyIndications = studyEntity.ClinicalStudy.StudyDesigns.Select(x=>x.StudyIndications),
                StudyPhase = studyEntity.ClinicalStudy.StudyPhase,
                StudyTitle = studyEntity.ClinicalStudy.StudyTitle,
                StudyType = studyEntity.ClinicalStudy.StudyType,
                Uuid = studyEntity.ClinicalStudy.Uuid
            };
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                  .Returns(Task.FromResult(new List<SearchResponseEntity>() { searchResponseEntity }));

            SearchParametersDto searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
                Asc = true,
                Header  = "sdrversion"
            };

            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.SearchStudy(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                .Throws(new Exception("Error"));            

            method = ClinicalStudyService.SearchStudy(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);

            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as List<SearchResponseEntity>));

            method = ClinicalStudyService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsNull(result);
        }
        #endregion

        #region GET Study Audit
        [Test]
        public void GetStudyAudit_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.ClinicalStudy.StudyType
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now.AddDays(1),
                    SDRUploadVersion = 2,
                    StudyType = studyEntity.ClinicalStudy.StudyType
                }
            };
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(),It.IsAny<DateTime>()))
                   .Returns(Task.FromResult(auditTrailResponseEntities));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.GetAuditTrail("1", DateTime.MinValue,DateTime.MinValue, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AuditTrailResponseEntity>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user); 


            Assert.Throws<AggregateException>(method.Wait);

            auditTrailResponseEntities = null;
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Returns(Task.FromResult(auditTrailResponseEntities));

            method = ClinicalStudyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        [Test]
        public void CheckAccessForAStudyAudit_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";           
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            //studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.ClinicalStudy.StudyType
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now.AddDays(1),
                    SDRUploadVersion = 2,
                    StudyType = studyEntity.ClinicalStudy.StudyType
                }
            };
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            var expected = auditTrailResponseEntities;

            Assert.AreEqual(expected[0].SDRUploadVersion, method.Result[0].SDRUploadVersion);

            studyEntity.ClinicalStudy.Uuid = "studyId1";
            method = ClinicalStudyService.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);
            method.Wait();


            Assert.IsNotNull(method.Result);

            studyEntity.ClinicalStudy.Uuid = "studyId5";
            auditTrailResponseEntities.ForEach(x => x.StudyType.Decode = "Observational");
            method = ClinicalStudyService.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNull(method.Result);

            auditTrailResponseEntities.ForEach(x => x.StudyType.Decode = "Interventional");
            Config.isGroupFilterEnabled = false;
            method = ClinicalStudyService.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.isGroupFilterEnabled = true;
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            ClinicalStudyServiceV1 ClinicalStudyService1 = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);
            method = ClinicalStudyService1.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.CheckAccessForStudyAudit(studyEntity.ClinicalStudy.Uuid, auditTrailResponseEntities, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET StudyDesign
        [Test]
        public void GetStudyDesign_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDto studyDto = GetDtoDataFromStaticJson();
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.ClinicalStudy.StudyType.Decode = "OBSERVATIONAL";
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.GetStudyDesigns("1", 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDesignDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyEntity.ClinicalStudy.StudyDesigns = null;
           _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            method = ClinicalStudyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetStudyDesigns("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = ClinicalStudyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        #endregion

        #region GET Study History
        [Test]
        public void GetStudyHistory_UnitTesting()
        {            
            StudyEntity studyEntity = GetEntityDataFromStaticJson();
            List<StudyHistoryResponseEntity> studyHistories = new()
            {
                new StudyHistoryResponseEntity
                {
                    Uuid = studyEntity.ClinicalStudy.Uuid,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = studyEntity.ClinicalStudy.StudyIdentifiers,
                    StudyTitle = studyEntity.ClinicalStudy.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = studyEntity.ClinicalStudy.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.ClinicalStudy.StudyType
                }
            };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<string>(),It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(studyHistories));
            
            ClinicalStudyServiceV1 ClinicalStudyService = new ClinicalStudyServiceV1(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyService.GetStudyHistory(DateTime.Now,DateTime.MinValue, "", user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyHistories = null;
            _mockClinicalStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(studyHistories));
            method = ClinicalStudyService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();

            Assert.IsNull(method.Result);       

            _mockClinicalStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                  .Throws(new Exception("Error"));

            method = ClinicalStudyService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);


            Assert.Throws<AggregateException>(method.Wait);

        }
        #endregion
        #endregion
    }
}
