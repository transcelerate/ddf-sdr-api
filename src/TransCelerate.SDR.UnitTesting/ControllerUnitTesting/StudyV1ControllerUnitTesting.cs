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
    public class StudyV1ControllerUnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IHelperV1> _mockHelper = new(MockBehavior.Loose);
        private readonly Mock<IStudyServiceV1> _mockStudyService = new(MockBehavior.Loose);
        #endregion

        #region Setup               
        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.PostAllElements(study, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            Assert.AreEqual(expected.Study.Uuid, actual_result.Study.Uuid);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }
        [Test]
        public void PostAllElementsFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            /////Restricted
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.PostRestricted as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.PostAllElements(study, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ErrorModel>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(401, (result as ObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            method = studyV1Controller.PostAllElements(null, "1.0");
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
            _mockStudyService.Setup(x => x.PostAllElements(It.IsAny<StudyDefinitionsDto>(), It.IsAny<LoggedInUser>()))
                        .Throws(new Exception("Error"));

            method = studyV1Controller.PostAllElements(study, "1.0");
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetStudy("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<StudyDefinitionsDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.Study.Uuid, actual_result.Study.Uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyElementsV2);
            var method = studyV1Controller.GetStudy("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetStudy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV1Controller.GetStudy("sd", 1, "1.0");
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

            method = studyV1Controller.GetStudy("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV1Controller.GetStudy("", 1, "1.0");
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyDefinitionsDto> studyList = new() { study };

            _mockStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);
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


            var method = studyV1Controller.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDefinitionsDto>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(expected[0].Study.Uuid, actual_result[0].Study.Uuid);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyDefinitionsDto> studyList = new() { study };

            _mockStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchParametersDto searchParameters = new()
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


            var method = studyV1Controller.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = studyV1Controller.SearchStudy(null);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            SearchParametersDto searchParameters1 = new()
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

            method = studyV1Controller.SearchStudy(searchParameters1);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            SearchParametersDto searchParameters2 = new()
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
            _mockStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = studyV1Controller.SearchStudy(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            studyList = null;
            _mockStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(studyList));
            method = studyV1Controller.SearchStudy(searchParameters2);
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<SearchTitleResponseDto> studyList = new()
            {
                new SearchTitleResponseDto
                {
                    Study = new SearchTitleStudy { StudyIdentifiers = study.Study.StudyIdentifiers, StudyTitle = study.Study.StudyTitle, Uuid = study.Study.Uuid },
                    AuditTrail = new SearchTitleAuditTrail { EntryDateTime = DateTime.Now, SDRUploadVersion = 1 }
                }
            };

            _mockStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString()
            };


            var method = studyV1Controller.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(expected[0].Study.Uuid, actual_result[0].Study.Uuid);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyTitleFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<SearchTitleResponseDto> studyList = new()
            {
                new SearchTitleResponseDto
                {
                    Study = new SearchTitleStudy { StudyIdentifiers = study.Study.StudyIdentifiers, StudyTitle = study.Study.StudyTitle, Uuid = study.Study.Uuid },
                    AuditTrail = new SearchTitleAuditTrail { EntryDateTime = DateTime.Now, SDRUploadVersion = 1 }
                }
            };

            _mockStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };


            var method = studyV1Controller.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = studyV1Controller.SearchTitle(null);
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

            method = studyV1Controller.SearchTitle(searchParameters1);
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
            _mockStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = studyV1Controller.SearchTitle(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            studyList = null;
            _mockStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(studyList));
            method = studyV1Controller.SearchTitle(searchParameters2);
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            AudiTrailResponseDto audTrailResponseDto = new()
            {
                Uuid = study.Study.Uuid,
                AuditTrail = new List<AuditTrailDto> { new AuditTrailDto { EntryDateTime = DateTime.Now.AddDays(-1), SDRUploadVersion = 1 }, new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 2 } }
            };

            _mockStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(audTrailResponseDto as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV1Controller.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV1Controller.GetAuditTrail("", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV1Controller.GetAuditTrail("sd", DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1));
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyHistoryResponseDto> studyHistories = new()
            {
                new StudyHistoryResponseDto
                {
                    StudyId = study.Study.Uuid,
                    SDRUploadVersion = new List<UploadVersionDto>() { new UploadVersionDto
                {
                    ProtocolVersions = new List<string>(){"1","2"},
                    StudyIdentifiers = study.Study.StudyIdentifiers,
                    StudyTitle = study.Study.StudyTitle,
                    UploadVersion = 1,
                    StudyVersion = study.Study.StudyVersion
                } }
                }
            };
            Config.DateRange = "20";
            _mockStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyHistories));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();
            List<StudyHistoryResponseDto> studyHistory = null;
            _mockStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(studyHistory));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);


            _mockStudyService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV1Controller.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = studyV1Controller.GetStudyHistory(DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1), "sd");
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
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study.Study.StudyDesigns as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var listofelements = string.Join(",", Constants.StudyDesignElementsV2);
            var method = studyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = study.Study.StudyDesigns;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyDesignDto>>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected[0].Uuid, actual_result[0].Uuid);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void GetStudyDesignsFailureUnitTesting()
        {
            StudyDefinitionsDto study = GetDtoDataFromStaticJson();

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            StudyV1Controller studyV1Controller = new(_mockStudyService.Object, _mockLogger, _mockHelper.Object);

            var method = studyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = studyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));

            method = studyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyDesignNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockStudyService.Setup(x => x.GetStudyDesigns(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = studyV1Controller.GetStudyDesigns("sd", 1, "1.0");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = studyV1Controller.GetStudyDesigns("", 1, "1.0");
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
