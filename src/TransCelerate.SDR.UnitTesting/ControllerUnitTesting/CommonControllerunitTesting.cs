﻿using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.Utilities.Common;
using AutoMapper;
using NUnit.Framework;
using System.Net;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.DTO.Common;
using Newtonsoft.Json.Serialization;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.WebApi.Mappers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Core.DTO.eCPT;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class CommonControllerunitTesting
    {
        #region Variables
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<ICommonService> _mockCommonService = new Mock<ICommonService>(MockBehavior.Loose);
        private IMapper _mockMapper;
        private Mock<ICommonRepository> _mockCommonRepository = new Mock<ICommonRepository>(MockBehavior.Loose);
        #endregion

        #region Setup

        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public CommonStudyEntity GetData(string usdmVersion)
        {
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
                var mvpData = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return mvpData;
            }
            else if (usdmVersion == Constants.USDMVersions.V1)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
                var v1 = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return v1;
            }
            else
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
                var v2 = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return v2;
            }
        }
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
            _mockMapper = new Mapper(mockMapper);
        }
        #endregion

        #region Get RawJson
        [Test]
        public void GetRawJsonUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new GetRawJsonDto()
            {
                StudyDefinitions = JsonConvert.SerializeObject(data,new JsonSerializerSettings
                            {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                })
            };

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetRawJsonDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyDefinitions,actual_result.StudyDefinitions);
        }
        [Test]
        public void GetRawJsonFailureUnitTesting()
        {

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
             .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(null as object));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound, statusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            method = commonController.GetRawJson("", 1);
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
        [Test]
        public void GeteCPTUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/DataeCPT.json");
            var data = JsonConvert.DeserializeObject<List<StudyeCPTDto>>(jsonData);
            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(),It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GeteCPT("sd", 1, "des");
            method.Wait();
            var result=method.Result;

            var expected = data;

            var actual_result = JsonConvert.DeserializeObject<List<StudyeCPTDto>>(
                                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.Count(), actual_result.Count());
        }
        [Test]
        public void GeteCPTData_FaulureUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/DataeCPT.json");
            var data = JsonConvert.DeserializeObject<List<StudyeCPTDto>>(jsonData);
            data = null;

            //Study NotFound Case
            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GeteCPT("sd", 1, "des");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound, statusCode = "404" };

            //Actual
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);


            //Forbidden  case
            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = commonController.GeteCPT("sd", 1,"des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            //eCPT Error
            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.eCPTError as object));

            method = commonController.GeteCPT("sd", 1, "des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.eCPTError , statusCode="400"};

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);


            //Exception case
            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GeteCPT("sd", 1,"des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(Constants.ErrorMessages.StudyInputError as object));
            method = commonController.GeteCPT("", 1,"des");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyInputError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GeteCPT(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
             .Returns(Task.FromResult(Constants.ErrorMessages.StudyDesignNotFound as object));
            method = commonController.GeteCPT("sd", 1, "");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyDesignNotFound, statusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Get AuditTrail
        [Test]
        public void GetAuditTrailSuccessUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            AuditTrailResponseDto auditTrailResponseDto = new AuditTrailResponseDto
            {
                StudyId = data.ClinicalStudy.ToString(),
                AuditTrail = new List<AuditTrailDto> { new AuditTrailDto { EntryDateTime = DateTime.Now.AddDays(-1), SDRUploadVersion = 1 }, new AuditTrailDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 2 } }
            };

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(auditTrailResponseDto as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = auditTrailResponseDto;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AuditTrailResponseDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }
        [Test]
        public void GetAuditTrailFailureUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound, statusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = commonController.GetAuditTrail("", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyInputError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = commonController.GetAuditTrail("sd", DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1));
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.DateError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

        }
        #endregion

        #region Study History
        [Test]
        public void GetStudyHistorySuccessUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<StudyHistoryResponseEntity>(jsonData);
            List<StudyHistoryResponseDto> studyHistories = new()
            {
                new StudyHistoryResponseDto { StudyId = data.StudyId, SDRUploadVersion = new List<UploadVersionDto>() { new UploadVersionDto
                {
                    ProtocolVersions = new List<string>(){"1","2"},
                    StudyIdentifiers = data.StudyIdentifiers,
                    StudyTitle = data.StudyTitle,
                    UploadVersion = 1,
                    StudyVersion = data.StudyVersion
                } } }
            };
            Config.DateRange = "20";
            _mockCommonService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyHistories));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
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
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            List<StudyHistoryResponseDto> studyHistory = null;
            _mockCommonService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(studyHistory));

            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound, statusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);


            _mockCommonService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = commonController.GetStudyHistory(DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1), "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.DateError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }
        #endregion

        #region Search Title
        [Test]
        public void SearchStudyTitleSuccessUnitTesting()
        {
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V2);
            List<SearchTitleResponseEntity> studyList = new() { new SearchTitleResponseEntity
            {
               StudyIdentifiers=mvp.ClinicalStudy.StudyIdentifiers,
               StudyId=mvp.ClinicalStudy.StudyId,
               StudyTitle=mvp.ClinicalStudy.StudyTitle,
               StudyType=mvp.ClinicalStudy.StudyType,
               SDRUploadVersion=mvp.AuditTrail.SDRUploadVersion,
               EntryDateTime=mvp.AuditTrail.EntryDateTime,
               HasAccess=true,
               UsdmVersion=mvp.AuditTrail.UsdmVersion,
            },new SearchTitleResponseEntity
             {
               StudyIdentifiers=v1.ClinicalStudy.StudyIdentifiers,
               StudyId=v1.ClinicalStudy.StudyId,
               StudyTitle=v1.ClinicalStudy.StudyTitle,
               StudyType=v1.ClinicalStudy.StudyType,
               SDRUploadVersion=v1.AuditTrail.SDRUploadVersion,
               EntryDateTime=v1.AuditTrail.EntryDateTime,
               HasAccess=true,
               UsdmVersion=v1.AuditTrail.UsdmVersion,
            },new SearchTitleResponseEntity
             {
               StudyIdentifiers=v2.ClinicalStudy.StudyIdentifiers,
               StudyId=v2.ClinicalStudy.StudyId,
               StudyTitle=v2.ClinicalStudy.StudyTitle,
               StudyType=v2.ClinicalStudy.StudyType,
               SDRUploadVersion=v2.AuditTrail.SDRUploadVersion,
               EntryDateTime=v2.AuditTrail.EntryDateTime,
               HasAccess=true,
               UsdmVersion=v2.AuditTrail.UsdmVersion,
            }};
            var searchTitleDTOList = _mockMapper.Map<List<SearchTitleResponseDto>>(studyList);
            CommonServices CommonService = new CommonServices(_mockCommonRepository.Object, _mockLogger, _mockMapper);
            searchTitleDTOList = CommonService.AssignStudyIdentifiers(searchTitleDTOList, studyList);

            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(searchTitleDTOList));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString()
            };


            var method = commonController.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

          //  Assert.AreEqual(expected[0].ClinicalStudy.StudyId, actual_result[0].ClinicalStudy.StudyId);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyTitleFailureUnitTesting()
        {
            var searchTitleDTOList = new List<SearchTitleResponseDto>();
            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(searchTitleDTOList));

            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                StudyId = "",
                FromDate = "",
                ToDate = ""
            };


            var method = commonController.SearchTitle(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = commonController.SearchTitle(null);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
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

            method = commonController.SearchTitle(searchParameters1);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
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
            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = commonController.SearchTitle(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            searchTitleDTOList = null;
            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(searchTitleDTOList));
            method = commonController.SearchTitle(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SearchNotFound);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Get API -> USDM Version Mapping
        [Test]
        public void GetApiUsdmVersionMappingUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json");            
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(jsonData);
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetApiUsdmMapping();

            var result = (method as OkObjectResult).Value;

            //Expected
            var expected = apiUsdmVersionMapping_NonStatic;
            
            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(JsonConvert.SerializeObject(result));

            Assert.AreEqual(expected.SDRVersions.Count, actual_result.SDRVersions.Count);

            Mock<ILogHelper> _mockLogg = new Mock<ILogHelper>(MockBehavior.Loose);
            _mockLogg.Setup(x => x.LogInformation(It.IsAny<string>())).Throws(new Exception("Error"));

            CommonController commonController1 = new CommonController(_mockCommonService.Object, _mockLogg.Object);
            Assert.Throws<System.Exception>(() => commonController1.GetApiUsdmMapping());
        }
        #endregion
    }
}
