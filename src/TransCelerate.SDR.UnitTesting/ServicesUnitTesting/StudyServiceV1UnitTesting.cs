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
    public class StudyServiceV1UnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV1> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<IStudyRepositoryV1> _mockStudyRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup        
        public static StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsEntity>(jsonData);
            data.AuditTrail.UsdmVersion = "1.0";
            return data;
        }
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            var data = JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
            data.AuditTrail.UsdmVersion = "1.0";
            return data;
        }
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
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
                cfg.AddProfile(new AutoMapperProfilesV1());
            });
            _mockMapper = new Mapper(mockMapper);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
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
            studyEntity.AuditTrail = new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 0, UsdmVersion = "1.0" };
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 1, UsdmVersion = "1.0" };
            _mockStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Uuid));
            _mockStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(Task.FromResult(studyDto.Study.Uuid));
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(studyEntity));
            StudyDefinitionsEntity studyEntity1 = GetEntityDataFromStaticJson(); studyEntity1.AuditTrail.SDRUploadVersion = 1; studyEntity1.AuditTrail.UsdmVersion = "1.0";
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(true);
            _mockHelper.Setup(x => x.GetAuditTrail(It.IsAny<string>()))
                    .Returns(new AuditTrailEntity { CreatedBy = user.UserName, EntryDateTime = DateTime.Now, SDRUploadVersion = 1 });
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);


            var method = studyService.PostAllElements(studyDto, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);



            _mockHelper.Setup(x => x.IsSameStudy(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(false);
            _mockHelper.Setup(x => x.CheckForSections(It.IsAny<StudyDefinitionsEntity>(), It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(studyEntity);

            method = studyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), 0))
                    .Returns(Task.FromResult(entity));
            _mockStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(null as AuditTrailEntity));

            method = studyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            var actual_result1 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result1.ToString(), Constants.ErrorMessages.NotValidStudyId);

            _mockHelper.Setup(x => x.GeneratedSectionId(It.IsAny<StudyDefinitionsEntity>()))
                    .Returns(studyEntity);
            studyDto.Study.Uuid = null;

            method = studyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);


            var groups = GetUserDataFromStaticJson();
            groups.SDRGroups.ForEach(x => x.Permission = Permissions.READONLY.ToString());
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(groups.SDRGroups));
            user.UserRole = Constants.Roles.App_User;
            Config.IsGroupFilterEnabled = true;
            method = studyService.PostAllElements(studyDto, user);
            method.Wait();
            result = method.Result;
            Config.IsGroupFilterEnabled = false;

            //Actual            
            var actual_result2 = result.ToString();

            //Assert          
            Assert.AreEqual(actual_result2.ToString(), Constants.ErrorMessages.PostRestricted);

            _mockHelper.Setup(x => x.GetAuditTrail(user.UserName))
                 .Throws(new Exception("Error"));

            method = studyService.PostAllElements(studyDto, user);

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
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

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
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

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
            var study = GetEntityDataFromStaticJson();
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = GetEntityDataFromStaticJson();

            Assert.AreEqual(expected.Study.Uuid, method.Result.Study.Uuid);

            study.Study.Uuid = "studyId1";
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();


            Assert.AreEqual("studyId1", method.Result.Study.Uuid);

            study.Study.Uuid = "studyId5";
            study.Study.StudyType.Decode = "Interventional";
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.Study.Uuid);

            Config.IsGroupFilterEnabled = false;
            method = studyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.Study.Uuid);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.IsGroupFilterEnabled = true;
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            StudyServiceV1 studyService1 = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);
            method = studyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = studyService.CheckAccessForAStudy(study, user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region Search Study
        [Test]
        public void SearchStudyUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyDefinitionsDto> studyList = new() { study };
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            SearchResponseEntity searchResponseEntity = new()
            {
                EntryDateTime = DateTime.Now,
                SDRUploadVersion = 2,
                InterventionModel = studyEntity.Study.StudyDesigns.Select(x => x.InterventionModel),
                StudyIdentifiers = studyEntity.Study.StudyIdentifiers,
                StudyIndications = studyEntity.Study.StudyDesigns.Select(x => x.StudyIndications),
                StudyPhase = studyEntity.Study.StudyPhase,
                StudyTitle = studyEntity.Study.StudyTitle,
                StudyType = studyEntity.Study.StudyType,
                StudyId = studyEntity.Study.Uuid
            };
            _mockStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
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
                Header = "sdrversion"
            };

            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.SearchStudy(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDefinitionsDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            _mockStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                .Throws(new Exception("Error"));

            method = studyService.SearchStudy(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);

            _mockStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as List<SearchResponseEntity>));

            method = studyService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsNull(result);
        }
        #endregion

        #region GET Study Audit
        [Test]
        public void GetStudyAudit_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsDto studyDto = GetDtoDataFromStaticJson();
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.Study.StudyType
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now.AddDays(1),
                    SDRUploadVersion = 2,
                    StudyType = studyEntity.Study.StudyType
                }
            };
            _mockStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .Returns(Task.FromResult(auditTrailResponseEntities));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AuditTrailResponseEntity>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);


            Assert.Throws<AggregateException>(method.Wait);

            auditTrailResponseEntities = null;
            _mockStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Returns(Task.FromResult(auditTrailResponseEntities));

            method = studyService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        [Test]
        public void CheckAccessForAStudyAudit_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();            
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.Study.StudyType
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = DateTime.Now.AddDays(1),
                    SDRUploadVersion = 2,
                    StudyType = studyEntity.Study.StudyType
                }
            };
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            var expected = auditTrailResponseEntities;

            Assert.AreEqual(expected[0].SDRUploadVersion, method.Result[0].SDRUploadVersion);

            studyEntity.Study.Uuid = "studyId1";
            method = studyService.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);
            method.Wait();


            Assert.IsNotNull(method.Result);

            studyEntity.Study.Uuid = "studyId5";
            auditTrailResponseEntities.ForEach(x => x.StudyType.Decode = "Observational");
            method = studyService.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNull(method.Result);

            auditTrailResponseEntities.ForEach(x => x.StudyType.Decode = "Interventional");
            Config.IsGroupFilterEnabled = false;
            method = studyService.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.IsGroupFilterEnabled = true;
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            StudyServiceV1 studyService1 = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);
            method = studyService1.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception("Error"));

            method = studyService.CheckAccessForStudyAudit(studyEntity.Study.Uuid, auditTrailResponseEntities, user);


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
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.GetStudyDesigns("1", 0, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDesignsResposeDto>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyEntity.Study.StudyDesigns = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(studyEntity));
            method = studyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.StudyDesignNotFound);

            user.UserRole = Constants.Roles.App_User;
            method = studyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudyDesigns("1", 0, user);


            Assert.Throws<AggregateException>(method.Wait);

            studyEntity = null;
            _mockStudyRepository.Setup(x => x.GetPartialStudyDesignItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(studyEntity));

            method = studyService.GetStudyDesigns("1", 0, user);
            method.Wait();

            Assert.IsNull(method.Result);

        }
        #endregion

        #region GET Study History
        [Test]
        public void GetStudyHistory_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            List<StudyHistoryResponseEntity> studyHistories = new()
            {
                new StudyHistoryResponseEntity
                {
                    Uuid = studyEntity.Study.Uuid,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = studyEntity.Study.StudyIdentifiers,
                    StudyTitle = studyEntity.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = studyEntity.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = studyEntity.Study.StudyType
                }
            };
            _mockStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(studyHistories));

            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            studyHistories = null;
            _mockStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(studyHistories));
            method = studyService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockStudyRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                  .Throws(new Exception("Error"));

            method = studyService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);


            Assert.Throws<AggregateException>(method.Wait);

        }
        #endregion

        #region Search Study Title
        [Test]
        public void SearchStudyTitleUnitTesting()
        {
            user.UserRole = Constants.Roles.Org_Admin;
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyDefinitionsDto> studyList = new() { study };
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            SearchResponseEntity searchResponseEntity = new()
            {
                EntryDateTime = DateTime.Now,
                SDRUploadVersion = 2,
                StudyIdentifiers = studyEntity.Study.StudyIdentifiers,
                StudyPhase = studyEntity.Study.StudyPhase,
                StudyTitle = studyEntity.Study.StudyTitle,
                StudyType = studyEntity.Study.StudyType,
                StudyId = studyEntity.Study.Uuid
            };
            _mockStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), It.IsAny<LoggedInUser>()))
                  .Returns(Task.FromResult(new List<SearchResponseEntity>() { searchResponseEntity }));

            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
            };

            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

            var method = studyService.SearchTitle(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            _mockStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), It.IsAny<LoggedInUser>()))
                .Throws(new Exception("Error"));

            method = studyService.SearchTitle(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);

            _mockStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as List<SearchResponseEntity>));

            method = studyService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsEmpty(result);

            _mockStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), It.IsAny<LoggedInUser>()))
                 .Returns(Task.FromResult(new List<SearchResponseEntity>() { searchResponseEntity }));
            searchParameters.GroupByStudyId = true;
            method = studyService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsNotEmpty(result);

            string[] sortByFields = { "studytitle", "sponsorid", "lastmodifieddate", "version", "" };
            string[] sortOrder = { "asc", "desc" };
            sortOrder.ToList().ForEach(sortOrderItem =>
            {
                sortByFields.ToList().ForEach(sortByField =>
                {
                    searchParameters.SortOrder = sortOrderItem;
                    searchParameters.SortBy = sortByField;
                    var method_sort = StudyServiceV1.SortStudyTitle(result, searchParameters);
                    Assert.IsNotEmpty(method_sort);
                });
            });

            user.UserRole = Constants.Roles.App_User;
            method = studyService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsEmpty(result);
            user.UserRole = Constants.Roles.Org_Admin;

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
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();
            studyEntity.Study.StudyType.Decode = "OBSERVATIONAL";
            _mockStudyRepository.Setup(x => x.GetStudyItemsForCheckingAccessAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity));
            _mockStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            StudyServiceV1 studyService = new(_mockStudyRepository.Object, _mockMapper, _mockLogger, _mockHelper.Object);

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

        #endregion
    }
}
