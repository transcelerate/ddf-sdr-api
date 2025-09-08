using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
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
        private readonly Mock<ICommonRepository> _mockCommonRepository = new(MockBehavior.Loose);

        private readonly Mock<IHelperV3> _mockHelperV3 = new(MockBehavior.Loose);
        private readonly Mock<IHelperV4> _mockHelperV4 = new(MockBehavior.Loose);
        private readonly Mock<IHelperV5> _mockHelperV5 = new(MockBehavior.Loose);

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

        public static Core.Entities.StudyV3.StudyDefinitionsEntity GetEntityDataFromStaticJsonV3()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            return JsonConvert.DeserializeObject<Core.Entities.StudyV3.StudyDefinitionsEntity>(jsonData);
        }
        public static Core.Entities.StudyV4.StudyDefinitionsEntity GetEntityDataFromStaticJsonV4()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");
            return JsonConvert.DeserializeObject<Core.Entities.StudyV4.StudyDefinitionsEntity>(jsonData);
        }
        public static Core.Entities.StudyV5.StudyDefinitionsEntity GetEntityDataFromStaticJsonV5()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
            return JsonConvert.DeserializeObject<Core.Entities.StudyV5.StudyDefinitionsEntity>(jsonData);
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
        #endregion

        #region UnitTesting
        #region ChangeAudit Controller UnitTesting
        [Test]
        public void ChangeAuditController_Success_UnitTesting()
        {
            ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>()))
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
            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>()))
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

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>()))
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
        public void ChangeAuditService_Get_UnitTesting()
        {
            ChangeAuditService changeAuditService = new(_mockChangeAuditRepository.Object, _mockCommonRepository.Object, _mockMapper, _mockLogHelper,
                _mockHelperV3.Object, _mockHelperV4.Object, _mockHelperV5.Object);

            // Arrange
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetChangeAuditEntityDataFromStaticJson()));

            // Act
            var method = changeAuditService.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            // Assert
            var expected = GetChangeAuditDtoDataFromStaticJson();
            var actual = result as ChangeAuditStudyDto;

            Assert.AreEqual(expected.ChangeAudit.StudyId, actual.ChangeAudit.StudyId);


            // Arrange
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(null as ChangeAuditStudyEntity));

            // Act
            method = changeAuditService.GetChangeAudit("sd");
            method.Wait();
            result = method.Result;

            // Assert
            Assert.IsNull(result);


            // Arrange
            _mockChangeAuditRepository.Setup(x => x.GetChangeAuditAsync(It.IsAny<string>()))
              .Throws(new Exception());

            // Act
            method = changeAuditService.GetChangeAudit("sd");

            // Assert
            Assert.Throws<AggregateException>(() => method.Wait());
        }
        #endregion
        #endregion
    }
}
