using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.eCPT;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class CommonControllerunitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<ICommonService> _mockCommonService = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        private readonly Mock<ICommonRepository> _mockCommonRepository = new(MockBehavior.Loose);
        #endregion

        #region Setup

        public static CommonStudyDefinitionsEntity GetData(string usdmVersion)
        {
            if (usdmVersion == Constants.USDMVersions.V1)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
                jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
                var v1 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
                return v1;
            }
            else if (usdmVersion == Constants.USDMVersions.V1_9)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
                jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
                var v2 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
                return v2;
            }
            else
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
                var v2 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
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
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
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
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new GetRawJsonDto()
            {
                StudyDefinitions = JsonConvert.SerializeObject(data, new JsonSerializerSettings
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

            Assert.AreEqual(expected.StudyDefinitions, actual_result.StudyDefinitions);
        }
        [Test]
        public void GetRawJsonFailureUnitTesting()
        {

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
             .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(null as object));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            method = commonController.GetRawJson("", 1);
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

        #region Get AuditTrail
        [Test]
        public void GetAuditTrailSuccessUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            AuditTrailResponseDto auditTrailResponseDto = new()
            {
                StudyId = data.Study.ToString(),
                RevisionHistory = new List<AuditTrailResponseWithLinksDto> { new AuditTrailResponseWithLinksDto { EntryDateTime = DateTime.Now.AddDays(-1), SDRUploadVersion = 1 }, new AuditTrailResponseWithLinksDto { EntryDateTime = DateTime.Now, SDRUploadVersion = 2 } }
            };

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(auditTrailResponseDto as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

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
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));

            method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.Forbidden, StatusCode = "403" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetAuditTrail("sd", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = commonController.GetAuditTrail("", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.StudyInputError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = commonController.GetAuditTrail("sd", DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1));
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

        #region Study History
        [Test]
        public void GetStudyHistorySuccessUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<StudyHistoryResponseEntity>(jsonData);
            List<StudyHistoryResponseDto> studyHistories = new()
            {
                new StudyHistoryResponseDto
                {
                    StudyId = data.StudyId,
                    SDRUploadVersion = new List<UploadVersionDto>() { new UploadVersionDto
                {
                    ProtocolVersions = new List<string>(){"1","2"},
                    StudyIdentifiers = data.StudyIdentifiers,
                    StudyTitle = data.StudyTitle,
                    UploadVersion = 1,
                    StudyVersion = data.StudyVersion
                } }
                }
            };
            Config.DateRange = "20";
            _mockCommonService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyHistories));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

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

            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "400" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);


            _mockCommonService.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetStudyHistory(DateTime.MinValue, DateTime.MinValue, "sd");
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            method = commonController.GetStudyHistory(DateTime.MinValue.AddDays(2), DateTime.MinValue.AddDays(1), "sd");
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

        #region Search Title
        [Test]
        public void SearchStudyTitleSuccessUnitTesting()
        {
            CommonStudyDefinitionsEntity mvp = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<SearchTitleResponseEntity> studyList = new()
            {
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = mvp.Study.StudyIdentifiers,
                    StudyId = mvp.Study.StudyId,
                    StudyTitle = mvp.Study.StudyTitle,
                    StudyType = mvp.Study.StudyType,
                    SDRUploadVersion = mvp.AuditTrail.SDRUploadVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyId = v1.Study.StudyId,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyType = v1.Study.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyType = v2.Study.StudyType,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                }
            };
            var searchTitleDTOList = _mockMapper.Map<List<SearchTitleResponseDto>>(studyList);
            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);
            searchTitleDTOList = CommonServices.AssignStudyIdentifiers(searchTitleDTOList, studyList);

            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(searchTitleDTOList));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                SponsorId = "100",
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
            
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyTitleFailureUnitTesting()
        {
            var searchTitleDTOList = new List<SearchTitleResponseDto>();
            _mockCommonService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(searchTitleDTOList));

            CommonController commonController = new(_mockCommonService.Object, _mockLogger);
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                SponsorId = "",
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

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = commonController.SearchTitle(null);
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
                SponsorId = "",
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

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            SearchTitleParametersDto searchParameters2 = new()
            {
                StudyTitle = "Study",
                PageNumber = 0,
                PageSize = 0,
                SponsorId = "",
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

            Assert.AreEqual(expected.Message, actual_result.Message);
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

            Assert.AreEqual(expected.Message, actual_result.Message);
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
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetApiUsdmMapping();

            var result = (method as OkObjectResult).Value;

            //Expected
            var expected = apiUsdmVersionMapping_NonStatic;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(JsonConvert.SerializeObject(result));

            Assert.AreEqual(expected.SDRVersions.Count, actual_result.SDRVersions.Count);

            Mock<ILogHelper> _mockLogg = new(MockBehavior.Loose);
            _mockLogg.Setup(x => x.LogInformation(It.IsAny<string>())).Throws(new Exception("Error"));

            CommonController commonController1 = new(_mockCommonService.Object, _mockLogg.Object);
            Assert.Throws<System.Exception>(() => commonController1.GetApiUsdmMapping());
        }
        #endregion

        #region Search StudyDefintions
        [Test]
        public void SearchStudySuccessUnitTesting()
        {            
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            List<SearchResponseEntity> studyList = new()
            {
                new SearchResponseEntity
                {
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyId = v1.Study.StudyId,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyType = v1.Study.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyType = v2.Study.StudyType,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = v3.Study.StudyIdentifiers,
                    StudyId = v3.Study.StudyId,
                    StudyTitle = v3.Study.StudyTitle,
                    StudyType = v3.Study.StudyType,
                    SDRUploadVersion = v3.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v3.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v3.AuditTrail.UsdmVersion,
                }
            };

            _mockCommonService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(studyList as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);
            SearchParametersDto searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                SponsorId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
                ValidateUsdmVersion = false
            };


            var method = commonController.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = studyList;

            //Actual Result            
            var actual_result = JsonConvert.DeserializeObject<List<SearchResponseEntity>>(
                 JsonConvert.SerializeObject((result as ObjectResult).Value));

            Assert.AreEqual(expected[0].StudyId, actual_result[0].StudyId);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
        }

        [Test]
        public void SearchStudyFailureUnitTesting()
        {


            _mockCommonService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);
            SearchParametersDto searchParameters = new()
            {
                Indication = "",
                InterventionModel = "",
                StudyTitle = "",
                PageNumber = 0,
                PageSize = 0,
                Phase = "",
                SponsorId = "",
                FromDate = "",
                ToDate = "",
                ValidateUsdmVersion = false
            };


            var method = commonController.SearchStudy(searchParameters);
            method.Wait();

            var result = method.Result;

            //Expected Result
            var expected = ErrorResponseHelper.BadRequest(Constants.ValidationErrorMessage.AnyOneFieldError);

            //Actual Result            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);



            method = commonController.SearchStudy(null);
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
                SponsorId = "",
                FromDate = DateTime.Now.AddDays(1).ToString(),
                ToDate = DateTime.Now.ToString()
            };

            method = commonController.SearchStudy(searchParameters1);
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
                SponsorId = "",
                FromDate = "",
                ToDate = "",
                ValidateUsdmVersion = false
            };
            _mockCommonService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception("Error"));

            method = commonController.SearchStudy(searchParameters2);
            method.Wait();

            result = method.Result;

            //Expected Result
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual Result            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);


            _mockCommonService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDto>(), It.IsAny<LoggedInUser>()))
              .Returns(Task.FromResult(null as object));
            method = commonController.SearchStudy(searchParameters2);
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

        #region Get Links 
        [Test]
        public void GetLinksSuccessUnitTesting()
        {
            LinksResponseDto links = new()
            {
                StudyId = "1",
                SDRUploadVersion = 1,
                UsdmVersion = Constants.USDMVersions.V1_9,
                Links = LinksHelper.GetLinksForEndpoint("1", Constants.USDMVersions.V1, 1)
            };
            links.Links = LinksHelper.GetLinksForEndpoint("1", Constants.USDMVersions.MVP, 1);
            var link = LinksHelper.GetLinks("1", null, Constants.USDMVersions.MVP, 1);
            _mockCommonService.Setup(x => x.GetLinks(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(links as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetLinks("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = links;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<LinksResponseDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
        }

        [Test]
        public void GetLinksFailureUnitTesting()
        {
            _mockCommonService.Setup(x => x.GetLinks(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(null as object));
            CommonController commonController = new(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetLinks("sd", 1);
            method.Wait();
            var result = method.Result;

            var expected = new ErrorModel { Message = Constants.ErrorMessages.StudyNotFound, StatusCode = "404" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetLinks(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetLinks("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { Message = Constants.ErrorMessages.GenericError, StatusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            method = commonController.GetLinks("", 1);
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
    }
}
