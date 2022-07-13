using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
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
            studyDto.AuditTrail = new AuditTrailDto { EntryDateTime = DateTime.Now.ToString(),SDRUploadVersion = 1 };
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
        #endregion
    }
}
