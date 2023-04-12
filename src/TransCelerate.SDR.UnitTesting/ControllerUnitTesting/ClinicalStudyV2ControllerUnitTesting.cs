using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class ClinicalStudyV2ControllerUnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV2> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<IClinicalStudyServiceV2> _mockClinicalStudyService = new(MockBehavior.Loose);
        private string[] studyElements = Constants.ClinicalStudyElements;
        private string[] studyDesignElements = Constants.StudyDesignElements;
        #endregion

        #region Setup               
        public static StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
        }
        public static SoADto GetSoADataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/SoASampleData.json");
            return JsonConvert.DeserializeObject<SoADto>(jsonData);
        }
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV2());
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
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));

            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.ClinicalStudy.StudyId, actual_result.ClinicalStudy.StudyId);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PostAllElementsFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));

            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = ClinicalStudyV2Controller.PostAllElements(null, Constants.USDMVersions.V2);
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
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                        .Throws(new Exception("Error"));

            method = ClinicalStudyV2Controller.PostAllElements(study, Constants.USDMVersions.V2);
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

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = ClinicalStudyV2Controller.PostAllElements(study, Constants.USDMVersions.V2);
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
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDto>(), out errors))
               .Returns(true);

            method = ClinicalStudyV2Controller.PostAllElements(study, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDto>(), out errors))
               .Returns(false);
        }
        #endregion

        #region PUT All Elements Unit Testing
        [Test]
        public void PutStudySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));

            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.PutStudy(study, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.ClinicalStudy.StudyId, actual_result.ClinicalStudy.StudyId);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PutStudyFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));

            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.PutStudy(study, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = ClinicalStudyV2Controller.PutStudy(null, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
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
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
                        .Throws(new Exception("Error"));

            method = ClinicalStudyV2Controller.PutStudy(study, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
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

            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDto>(), It.IsAny<LoggedInUser>(), It.IsAny<string>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = ClinicalStudyV2Controller.PutStudy(study, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
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
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDto>(), out errors))
               .Returns(true);

            method = ClinicalStudyV2Controller.PutStudy(study, study.ClinicalStudy.StudyId, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = study;

            //Actual            
            actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            _mockHelper.Setup(x => x.ReferenceIntegrityValidation(It.IsAny<StudyDto>(), out errors))
               .Returns(false);
        }
        #endregion

        #region GET StudyDefinitions
        [Test]
        public void GetStudySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);
            string[] nullStudyElements = null;
            _mockHelper.Setup(x => x.AreValidStudyElements(It.IsAny<string>(), out nullStudyElements))
                .Returns(true);

            var method = ClinicalStudyV2Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.ClinicalStudy.StudyId, actual_result.ClinicalStudy.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetPartialStudyElements(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.ClinicalStudyElements);
            var method = ClinicalStudyV2Controller.GetStudy("sd", 1, listofelements, Constants.USDMVersions.V2);
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
            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = ClinicalStudyV2Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.GetStudy("sd", 1, null, Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = ClinicalStudyV2Controller.GetStudy("", 1, null, Constants.USDMVersions.V2);
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
            method = ClinicalStudyV2Controller.GetStudy("a", 1, null, Constants.USDMVersions.V2);
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

        #region GET AuditTrail
        [Test]
        public void GetAuditSuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            AudiTrailResponseDto audTrailResponseDto = new()
            {
                StudyId = study.ClinicalStudy.StudyId,
                AuditTrail = new List<AuditTrailDto> { new AuditTrailDto { EntryDateTime = DateTime.Now.AddDays(-1), SDRUploadVersion = 1 }, new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 2 } }
            };

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(audTrailResponseDto as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = audTrailResponseDto;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AudiTrailResponseDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetAuditFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = ClinicalStudyV2Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = ClinicalStudyV2Controller.GetAuditTrail("", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = ClinicalStudyV2Controller.GetAuditTrail("sd", DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1));
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.DateError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region GET StudyHistory
        [Test]
        public void GetStudyHistorySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<StudyHistoryResponseDto> studyHistories = new()
            {
                new StudyHistoryResponseDto
                {
                    StudyId = study.ClinicalStudy.StudyId,
                    SDRUploadVersion = new List<UploadVersionDto>() { new UploadVersionDto
                {
                    ProtocolVersions = new List<string>(){"1","2"},
                    StudyIdentifiers = study.ClinicalStudy.StudyIdentifiers,
                    StudyTitle = study.ClinicalStudy.StudyTitle,
                    UploadVersion = 1,
                    StudyVersion = study.ClinicalStudy.StudyVersion
                } }
                }
            };
            Config.DateRange = "20";
            _mockClinicalStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyHistories));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = studyHistories;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected[0].StudyId, actual_result[0].StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyHistoryFailureUnitTesting()
        {
            Config.DateRange = "20";
            StudyDto study = GetDtoDataFromStaticJson();
            List<StudyHistoryResponseDto> studyHistory = null;
            _mockClinicalStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(studyHistory));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);


            _mockClinicalStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = ClinicalStudyV2Controller.GetStudyHistory(DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1), "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.DateError, StatusCode = "400" };

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
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(study.ClinicalStudy.StudyDesigns as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyDesignElements);
            var method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", listofelements, Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study.ClinicalStudy.StudyDesigns;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDesignDto>>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected[0].Id, actual_result[0].Id);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyDesignsFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>(), It.IsAny<string[]>()))
               .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = ClinicalStudyV2Controller.GetStudyDesigns("", 1, "des", "list", Constants.USDMVersions.V2);
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
            method = ClinicalStudyV2Controller.GetStudyDesigns("sd", 1, "des", "list", Constants.USDMVersions.V2);
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
            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(deleteResult));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.DeleteStudy("sd");
            method.Wait();
            var result = method.Result;

            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void DeleteStudyFailureUnitTesting()
        {
            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.DeleteStudy("sd");
            method.Wait();
            var result = method.Result;

            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.NotValidStudyId as object));

            method = ClinicalStudyV2Controller.DeleteStudy("");
            method.Wait();
            result = method.Result;
            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
           .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));
            method = ClinicalStudyV2Controller.DeleteStudy("sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);


            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));

            method = ClinicalStudyV2Controller.DeleteStudy("sd");
            method.Wait();
            result = method.Result;
            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.DeleteStudy(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.DeleteStudy("sd");
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
        public void GetSoASuccessUnitTesting()
        {
            SoADto SoA = GetSoADataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(SoA as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
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
        public void GetSoAFailureUnitTesting()
        {
            SoADto SoA = GetSoADataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV2Controller ClinicalStudyV2Controller = new(_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
              .Throws(new Exception(""));

            method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = ClinicalStudyV2Controller.GetSOA("", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.ScheduleTimelineNotFound as object));
            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = ClinicalStudyV2Controller.GetSOA("sd", "sd_1", "des", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.ScheduleTimelineNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetSOA(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.ScheduleTimelineNotFound as object));
            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);


            method = ClinicalStudyV2Controller.GetSOA("sd", "", "WF1", 1);
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
        #endregion
    }
}
