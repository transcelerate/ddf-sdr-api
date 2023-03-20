using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class ClinicalStudyV1ControllerUnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV1> _mockHelper = new (MockBehavior.Loose);
        private readonly Mock<IClinicalStudyServiceV1> _mockClinicalStudyService = new (MockBehavior.Loose);        
        #endregion

        #region Setup               
        public static StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
        }

        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV1());
            });            
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
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
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.PostAllElements(study, "1.0");
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
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.PostAllElements(study, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = clinicalStudyV1Controller.PostAllElements(null, "1.0");
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

            method = clinicalStudyV1Controller.PostAllElements(study, "1.0");
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
        }
        #endregion

        #region GET StudyDefinitions
        [Test]
        public void GetStudySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetStudy("sd", 1, "1.0");
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

            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.ClinicalStudyElements);
            var method = clinicalStudyV1Controller.GetStudy("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = clinicalStudyV1Controller.GetStudy("sd", 1, "1.0");
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

            method = clinicalStudyV1Controller.GetStudy("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = clinicalStudyV1Controller.GetStudy("", 1, "1.0");
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

        #region Search StudyDefintions
        [Test]
        public void SearchStudySuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<StudyDto> studyList = new() { study };

            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);
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
                ToDate = DateTime.Now.ToString()
            };


            var method = clinicalStudyV1Controller.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDto>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(expected[0].ClinicalStudy.Uuid, actual_result[0].ClinicalStudy.Uuid);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<StudyDto> studyList = new() { study };

            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchParametersDto searchParameters = new ()
            {
                Indication = "",
                InterventionModel = "",
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                Phase = "",
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };


            var method = clinicalStudyV1Controller.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = clinicalStudyV1Controller.SearchStudy(null);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            SearchParametersDto searchParameters1 = new ()
            {
                Indication = "",
                InterventionModel = "",
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                Phase = "",
                StudyId = "",
                FromDate = DateTime.Now.AddDays(1).ToString(),
                ToDate = DateTime.Now.ToString()
            };

            method = clinicalStudyV1Controller.SearchStudy(searchParameters1);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            SearchParametersDto searchParameters2 = new ()
            {
                Indication = "Alzheimer",
                InterventionModel = "",
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                Phase = "",
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };
            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = clinicalStudyV1Controller.SearchStudy(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            studyList = null;
            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(studyList));
            method = clinicalStudyV1Controller.SearchStudy(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SearchNotFound);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Search Study Title
        [Test]
        public void SearchStudyTitleSuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<SearchTitleResponseDto> studyList = new()
            {
                new SearchTitleResponseDto
                {
                    ClinicalStudy = new SearchTitleClinicalStudy { StudyIdentifiers = study.ClinicalStudy.StudyIdentifiers, StudyTitle = study.ClinicalStudy.StudyTitle, Uuid = study.ClinicalStudy.Uuid },
                    AuditTrail = new SearchTitleAuditTrail { EntryDateTime = DateTime.Now, SDRUploadVersion = 1 }
                }
            };

            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString()
            };


            var method = clinicalStudyV1Controller.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(expected[0].ClinicalStudy.Uuid, actual_result[0].ClinicalStudy.Uuid);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyTitleFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            List<SearchTitleResponseDto> studyList = new()
            {
                new SearchTitleResponseDto
                {
                    ClinicalStudy = new SearchTitleClinicalStudy { StudyIdentifiers = study.ClinicalStudy.StudyIdentifiers, StudyTitle = study.ClinicalStudy.StudyTitle, Uuid = study.ClinicalStudy.Uuid },
                    AuditTrail = new SearchTitleAuditTrail { EntryDateTime = DateTime.Now, SDRUploadVersion = 1 }
                }
            };

            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };


            var method = clinicalStudyV1Controller.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = clinicalStudyV1Controller.SearchTitle(null);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            SearchTitleParametersDto searchParameters1 = new()
            {
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                StudyId = "",
                FromDate = DateTime.Now.AddDays(1).ToString(),
                ToDate = DateTime.Now.ToString()
            };

            method = clinicalStudyV1Controller.SearchTitle(searchParameters1);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            SearchTitleParametersDto searchParameters2 = new()
            {
                StudyTitle = "Study",
                PageNumber = 0,
                PageSize = 0,
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };
            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = clinicalStudyV1Controller.SearchTitle(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            studyList = null;
            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(studyList));
            method = clinicalStudyV1Controller.SearchTitle(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SearchNotFound);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region GET AuditTrail
        [Test]
        public void GetAuditSuccessUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();
            AudiTrailResponseDto audTrailResponseDto = new ()
            {
                Uuid = study.ClinicalStudy.Uuid,
                AuditTrail = new List<AuditTrailDto> { new AuditTrailDto { EntryDateTime = DateTime.Now.AddDays(-1), SDRUploadVersion = 1 }, new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 2 } }
            };

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(audTrailResponseDto as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = audTrailResponseDto;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AudiTrailResponseDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.Uuid, actual_result.Uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetAuditFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
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

            method = clinicalStudyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
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

            method = clinicalStudyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = clinicalStudyV1Controller.GetAuditTrail("", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = clinicalStudyV1Controller.GetAuditTrail("sd", DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1));
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
                    StudyId = study.ClinicalStudy.Uuid,
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
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
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
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
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

            method = clinicalStudyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = clinicalStudyV1Controller.GetStudyHistory(DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1), "sd");
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

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study.ClinicalStudy.StudyDesigns as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyDesignElements);
            var method = clinicalStudyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study.ClinicalStudy.StudyDesigns;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDesignDto>>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected[0].Uuid, actual_result[0].Uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyDesignsFailureUnitTesting()
        {
            StudyDto study = GetDtoDataFromStaticJson();

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            ClinicalStudyV1Controller clinicalStudyV1Controller = new (_mockClinicalStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = clinicalStudyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = clinicalStudyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = clinicalStudyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockClinicalStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = clinicalStudyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = clinicalStudyV1Controller.GetStudyDesigns("", 1, "1.0");
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
        #endregion
    }
}
