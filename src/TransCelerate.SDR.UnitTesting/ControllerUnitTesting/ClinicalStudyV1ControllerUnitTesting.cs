using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class ClinicalStudyV1ControllerUnitTesting
    {
        #region Variables
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IClinicalStudyServiceV1> _mockClinicalStudyService = new Mock<IClinicalStudyServiceV1>(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup               
        public StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
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
        public void PostAllElementsSuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new ClinicalStudyV1Controller(_mockClinicalStudyService.Object, _mockLogger);

            var method = clinicalStudyV1Controller.PostAllElements(study);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.ClinicalStudy.Uuid, actual_result.ClinicalStudy.Uuid);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PostAllElementsFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new ClinicalStudyV1Controller(_mockClinicalStudyService.Object, _mockLogger);

            var method = clinicalStudyV1Controller.PostAllElements(study);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));            

            method = clinicalStudyV1Controller.PostAllElements(study);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);

            method = clinicalStudyV1Controller.PostAllElements(null);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            ////Error BadRequest
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>()))
                        .Throws(new Exception("Error"));

            method = clinicalStudyV1Controller.PostAllElements(study);
            method.Wait();
            result = method.Result;

            //Expected
            var expected1= ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual            
            var actual_result1 = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected1.message, actual_result1.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }
        #endregion

        #region GET StudyDefinitions
        [Test]
        public void GetStudySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new ClinicalStudyV1Controller(_mockClinicalStudyService.Object, _mockLogger);

            var method = clinicalStudyV1Controller.GetStudy("sd",1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.ClinicalStudy.Uuid, actual_result.ClinicalStudy.Uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new ClinicalStudyV1Controller(_mockClinicalStudyService.Object, _mockLogger);

            var method = clinicalStudyV1Controller.GetStudy("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound ,statusCode = "400"};

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));           

            method = clinicalStudyV1Controller.GetStudy("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403,(result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = clinicalStudyV1Controller.GetStudy("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = clinicalStudyV1Controller.GetStudy("", 1);
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

        #endregion
    }
}
