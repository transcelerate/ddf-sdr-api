using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ChangeAudit
{
    public class ChangeAuditUnitTesting
    {
        #region Variables
        private readonly Mock<IChangeAuditService> _mockChangeAuditService = new(MockBehavior.Loose);
        private readonly Mock<IChangeAuditRepository> _mockChangeAuditRepository = new(MockBehavior.Loose);
        private readonly Mock<ICommonService> _mockCommonService = new(MockBehavior.Loose);
        private readonly ILogHelper _mockLogHelper = Mock.Of<ILogHelper>();
        private IMapper _mockMapper;
        #endregion

        #region Setup
        public static ChangeAuditStudyEntity GetChangeAuditEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyEntity>(jsonData);
        }
        public static ChangeAuditStudyDto GetChangeAuditDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyDto>(jsonData);
        }
        public static Core.Entities.StudyV2.StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<Core.Entities.StudyV2.StudyDefinitionsEntity>(jsonData);
            data.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            return data;
        }
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SharedAutoMapperProfiles());
                cfg.AddProfile(new AutoMapperProfilesV2());
                cfg.AddProfile(new AutoMapperProfilesV3());
            });
            _mockMapper = new Mapper(mockMapper);
        }
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        #endregion


        #region UnitTesting
        #region ChangeAudit Controller UnitTesting
        [Test]
        public void ChangeAuditController_Success_UnitTesting()
        {
            ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ChangeAuditController changeAuditController = new(_mockChangeAuditService.Object, _mockLogHelper);

            var method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ChangeAuditStudyDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.ChangeAudit.StudyId, actual_result.ChangeAudit.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }
        [Test]
        public void ChangeAuditController_Failure_UnitTesting()
        {
            ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ChangeAuditController changeAuditController = new(_mockChangeAuditService.Object, _mockLogHelper);

            var method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = $"{Constants.ErrorMessages.ChangeAuditNotFound} sd", StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = changeAuditController.GetChangeAudit("");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region ChangeAudit Service Unit Testing
        [Test]
        public void ChangeAuditService_UnitTesting()
        {
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetChangeAuditEntityDataFromStaticJson()));
            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(GetEntityDataFromStaticJson() as object));
            ChangeAuditService changeAuditService = new(_mockChangeAuditRepository.Object, _mockMapper, _mockLogHelper, _mockCommonService.Object);
            var method = changeAuditService.GetChangeAudit("sd", user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetChangeAuditDtoDataFromStaticJson();

            //Actual
            var actual = result as ChangeAuditStudyDto;

            Assert.AreEqual(expected.ChangeAudit.StudyId, actual.ChangeAudit.StudyId);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetChangeAuditEntityDataFromStaticJson()));
            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));

            method = changeAuditService.GetChangeAudit("sd", user);
            method.Wait();
            result = method.Result;

            Assert.AreEqual(Constants.ErrorMessages.Forbidden, result as string);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(null as ChangeAuditStudyEntity));
            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));

            method = changeAuditService.GetChangeAudit("sd", user);
            method.Wait();
            result = method.Result;

            Assert.IsNull(result);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
              .Throws(new Exception());


            method = changeAuditService.GetChangeAudit("sd", user);

            Assert.Throws<AggregateException>(() => method.Wait());
        }
        #endregion
        #endregion
    }
}
