using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV3;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;
using static TransCelerate.SDR.Core.Utilities.Common.Constants;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class StudyV3ControllerUnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV3> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<IStudyServiceV3> _mockStudyService = new(MockBehavior.Loose);
        private string[] studyElements = Constants.StudyElementsV3;
        private string[] studyDesignElements = Constants.StudyDesignElementsV3;
        #endregion

        #region Setup               
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
        }
        public static StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsEntity>(jsonData);
        }
        public static SoADto GetSOAV3DataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<SoADto>(jsonData);
        }
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV3());
            });
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;

            _mockHelper.Setup(x => x.AreValidStudyElements(It.IsAny<string>(), out studyElements))
                .Returns(true);
            Assert.IsNotNull(studyElements);
            _mockHelper.Setup(x => x.AreValidStudyDesignElements(It.IsAny<string>(), out studyDesignElements))
                .Returns(true);
            Assert.IsNotNull(studyDesignElements);
        }
        #endregion

        #region TestCases

        #region POST All Elements Unit Testing
        [Test]
        public void PostAllElementsSuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.Study.StudyId, actual_result.Study.StudyId);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PostAllElementsFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = studyV3Controller.PostAllElements(null, Constants.USDMVersions.V2);
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
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                        .Throws(new Exception("Error"));

            method = studyV3Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            var expected1 = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual            
            var actual_result1 = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected1.Message, actual_result1.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = studyV3Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);

            object errors = null;
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(true);

            method = studyV3Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(false);
        }
        #endregion

        #region PUT All Elements Unit Testing
        [Test]
        public void PutStudySuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.PutStudy(study, study.Study.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.Study.StudyId, actual_result.Study.StudyId);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PutStudyFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.PutStudy(study, study.Study.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = studyV3Controller.PutStudy(null, study.Study.StudyId, Constants.USDMVersions.V2);
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
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                        .Throws(new Exception("Error"));

            method = studyV3Controller.PutStudy(study, study.Study.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            var expected1 = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual            
            var actual_result1 = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected1.Message, actual_result1.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = studyV3Controller.PutStudy(study, study.Study.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);

            object errors = null;
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(true);

            method = studyV3Controller.PutStudy(study, study.Study.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(false);
        }
        #endregion

        #region GET StudyDefinitions
        [Test]
        public void GetStudySuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);
            string[] nullStudyElements = null;
            _mockHelper.Setup(x => x.AreValidStudyElements(It.IsAny<string>(), out nullStudyElements))
                .Returns(true);

            var method = studyV3Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.Study.StudyId, actual_result.Study.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetPartialStudyElements(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(null as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyElementsV3);
            var method = studyV3Controller.GetStudy("sd", 1, listofelements, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            string[] nullStudyElements = null;
            _mockHelper.Setup(x => x.AreValidStudyElements(It.IsAny<string>(), out nullStudyElements))
                .Returns(true);
            _mockStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV3Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV3Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetStudy("", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockHelper.Setup(x => x.AreValidStudyElements(It.IsAny<string>(), out studyElements))
                .Returns(false);
            method = studyV3Controller.GetStudy("a", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyElementNotValid, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region GET StudyDesigns
        [Test]
        public void GetStudyDesignsSuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(study.Study.StudyDesigns as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyDesignElementsV3);
            var method = studyV3Controller.GetStudyDesigns("sd", 1, "des", listofelements, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study.Study.StudyDesigns;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDesignDto>>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected[0].Id, actual_result[0].Id);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyDesignsFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(null as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV3Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = studyV3Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Throws(new Exception(""));

            method = studyV3Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetStudyDesigns("", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockHelper.Setup(x => x.AreValidStudyDesignElements(It.IsAny<string>(), out studyDesignElements))
                .Returns(false);
            method = studyV3Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignElementNotValid, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region Delete StudyDefinitions
        [Test]
        public void DeleteStudySuccessUnitTesting()
        {
            object deleteResult = "Delete Success";
            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(deleteResult));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.DeleteStudy("sd");
            method.Wait();
            var result = method.Result;

            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void DeleteStudyFailureUnitTesting()
        {
            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.DeleteStudy("sd");
            method.Wait();
            var result = method.Result;

            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = studyV3Controller.DeleteStudy("");
            method.Wait();
            result = method.Result;
            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);

            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
           .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));
            method = studyV3Controller.DeleteStudy("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);


            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));

            method = studyV3Controller.DeleteStudy("sd");
            method.Wait();
            result = method.Result;
            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);

            _mockStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = studyV3Controller.DeleteStudy("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region GET SoA
        [Test]
        public void GetSOAV3SuccessUnitTesting()
        {
            SoADto SoA = GetSOAV3DataFromStaticJson();

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(SoA as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = SoA;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SoADto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(SoA.StudyId, SoA.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetSOAV3FailureUnitTesting()
        {
            SoADto SoA = GetSOAV3DataFromStaticJson();

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetSOAV3("", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.ScheduleTimelineNotFound as object));
            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = studyV3Controller.GetSOAV3("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ScheduleTimelineNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetSOAV3(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.ScheduleTimelineNotFound as object));
            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);


            method = studyV3Controller.GetSOAV3("sd", "", "WF1", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.EnterDesignIdError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region GET eCPT Data
        [Test]
        public void GeteCPTUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/DataeCPT.json");
            var data = JsonConvert.DeserializeObject<Core.DTO.eCPT.ECPTDto>(jsonData);
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GeteCPTV3("sd", 1, "des");
            method.Wait();
            var result = method.Result;

            var expected = data;

            var actual_result = JsonConvert.DeserializeObject<Core.DTO.eCPT.ECPTDto>(
                                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyDesign, actual_result.StudyDesign);
        }
        [Test]
        public void GeteCPTData_FaulureUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/DataeCPT.json");
            var data = JsonConvert.DeserializeObject<Core.DTO.eCPT.ECPTDto>(jsonData);
            data = null;

            //Study NotFound Case
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GeteCPTV3("sd", 1, "des");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "404" };

            //Actual
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);


            //Forbidden  case
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV3Controller.GeteCPTV3("sd", 1, "des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            //StudyDesignNotFound
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignIdNotFoundCPT as object));

            method = studyV3Controller.GeteCPTV3("sd", 1, "");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignIdNotFoundCPT, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            //eCPT Error
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.eCPTError as object));

            method = studyV3Controller.GeteCPTV3("sd", 1, "des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.eCPTError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);

            //Exception case
            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV3Controller.GeteCPTV3("sd", 1, "des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(Constants.ErrorMessages.StudyInputError as object));
            method = studyV3Controller.GeteCPTV3("", 1, "des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GeteCPTV3(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
             .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFoundCPT as object));
            method = studyV3Controller.GeteCPTV3("sd", 1, "");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFoundCPT, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region Validation USDM Conformance rules Unit testing
        [Test]
        public void ValidateUsdmConformanceSuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.ValidateUsdmConformance(study, Constants.USDMVersions.V2);

            //Expected
            var expected = new ErrorModel { Message = $"{SuccessMessages.ValidationSuccess}{USDMVersions.V2}" };

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((method as OkObjectResult).Value));

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(OkObjectResult), method);
        }
        [Test]
        public void ValidateUsdmConformanceFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));

            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.ValidateUsdmConformance(null, Constants.USDMVersions.V2);


            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((method as ObjectResult).Value));

            Assert.AreEqual(400, (method as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), method);


            ////Error BadRequest
            object errors = null;
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
                    .Throws(new Exception("Error"));

            method = studyV3Controller.ValidateUsdmConformance(study, Constants.USDMVersions.V2);

            //Expected
            var expected1 = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual            
            var actual_result1 = (method as BadRequestObjectResult).Value as ErrorModel;

            //Assert
            Assert.IsNotNull((method as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method);

            Assert.AreEqual(expected1.Message, actual_result1.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
            
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(true);

            method = studyV3Controller.ValidateUsdmConformance(study, Constants.USDMVersions.V2);

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((method as ObjectResult).Value));

            Assert.AreEqual(400, (method as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), method);
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDefinitionsDto>(), out errors))
               .Returns(false);
        }
        #endregion

        #region Version Comparison
        [Test]
        public void GetDifferencesSuccessUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            var currentVersionV3 = GetEntityDataFromStaticJson();
            var previousVersionV3 = GetEntityDataFromStaticJson();
            currentVersionV3.AuditTrail.SDRUploadVersion = 2;
            currentVersionV3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            previousVersionV3.AuditTrail.SDRUploadVersion = 1;
            previousVersionV3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;

            List<StudyDefinitionsEntity> studyEntitiesV3 = new()
            {
                currentVersionV3,
                previousVersionV3

            };
            HelperV3 helperV3 = new();

            _mockStudyService.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(new VersionCompareDto 
                {
                    StudyId = currentVersionV3.Study.StudyId,
                    LHS = new VersionDetails { EntryDateTime = currentVersionV3.AuditTrail.EntryDateTime},
                    RHS = new VersionDetails { EntryDateTime = previousVersionV3.AuditTrail.EntryDateTime},
                    ElementsChanged = helperV3.GetChangedValuesForStudyComparison(currentVersionV3, previousVersionV3)
                } as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV3Controller.GetDifferences("sd", 2, 1, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<VersionCompareDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.Study.StudyId, actual_result.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetDifferencesFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(null as object));
            StudyV3Controller studyV3Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyElementsV3);
            var method = studyV3Controller.GetDifferences("sd", 1, 2, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "404" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.OneVersionNotFound as object));

            method = studyV3Controller.GetDifferences("sd", 1, 2, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.OneVersionNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.ForbiddenForAStudy as object));

            method = studyV3Controller.GetDifferences("sd", 1, 2, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ForbiddenForAStudy, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);


            method = studyV3Controller.GetDifferences("sd", 1, 1, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ProvideDifferentVersion, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            _mockStudyService.Setup(x => x.GetDifferences(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV3Controller.GetDifferences("sd", 1, 2, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetDifferences("", 1, 2, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetDifferences("a", 0, 1, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ProvideValidVersion[0] + " sdrUploadVersionOne" + Constants.ErrorMessages.ProvideValidVersion[1] ,StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetDifferences("a", 1, 0, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ProvideValidVersion[0] + " sdrUploadVersionTwo" + Constants.ErrorMessages.ProvideValidVersion[1], StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV3Controller.GetDifferences("a", 0, 0, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ProvideValidVersion[0] + " sdrUploadVersionOne and sdrUploadVersionTwo" + Constants.ErrorMessages.ProvideValidVersion[1], StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion
        #endregion
    }
}
