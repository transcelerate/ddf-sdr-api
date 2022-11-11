using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV2;
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
        private Mock<IChangeAuditService> _mockChangeAuditService = new Mock<IChangeAuditService>(MockBehavior.Loose);
        private Mock<IChangeAuditRepository> _mockChangeAuditRepository = new Mock<IChangeAuditRepository>(MockBehavior.Loose);
        private Mock<IClinicalStudyServiceV2> _mockClinicalStudyServiceV2 = new Mock<IClinicalStudyServiceV2>(MockBehavior.Loose);
        private ILogHelper _mockLogHelper = Mock.Of<ILogHelper>();
        private IMapper _mockMapper;
        #endregion

        #region Setup
        public ChangeAuditStudyEntity GetChangeAuditEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyEntity>(jsonData);
        }
        public ChangeAuditStudyDto GetChangeAuditDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyDto>(jsonData);
        }
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV2());
            });
            _mockMapper = new Mapper(mockMapper);
        }
        LoggedInUser user = new LoggedInUser
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
            ChangeAuditController changeAuditController = new ChangeAuditController(_mockChangeAuditService.Object, _mockLogHelper);

            var method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ChangeAuditStudyDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.ChangeAudit.Study_uuid, actual_result.ChangeAudit.Study_uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }
        [Test]
        public void ChangeAuditController_Failure_UnitTesting()
        {
            ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ChangeAuditController changeAuditController = new ChangeAuditController(_mockChangeAuditService.Object, _mockLogHelper);

            var method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = $"{Constants.ErrorMessages.ChangeAuditNotFound} sd", statusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = changeAuditController.GetChangeAudit("");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyInputError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
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
            _mockClinicalStudyServiceV2.Setup(x => x.GetAccessForAStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(true));
            ChangeAuditService changeAuditService = new ChangeAuditService(_mockChangeAuditRepository.Object, _mockMapper, _mockLogHelper, _mockClinicalStudyServiceV2.Object);
            var method = changeAuditService.GetChangeAudit("sd", user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetChangeAuditDtoDataFromStaticJson();

            //Actual
            var actual = result as ChangeAuditStudyDto;

            Assert.AreEqual(expected.ChangeAudit.Study_uuid,actual.ChangeAudit.Study_uuid);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetChangeAuditEntityDataFromStaticJson()));
            _mockClinicalStudyServiceV2.Setup(x => x.GetAccessForAStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(false));
            
            method = changeAuditService.GetChangeAudit("sd", user);
            method.Wait();
            result = method.Result;

            Assert.AreEqual(Constants.ErrorMessages.Forbidden, result as string);

            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(null as ChangeAuditStudyEntity));
            _mockClinicalStudyServiceV2.Setup(x => x.GetAccessForAStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(false));

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
