using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.DependencyInjection;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting
{
    public class CommonClassesUnitTesting
    {
        private IMapper _mockMapper;
        private readonly Mock<ILoggerFactory> _mockLogger = new();
        private readonly Mock<ILogger> _mockSDRLogger = new();
        private readonly ILogHelper _mockLogHelper = Mock.Of<ILogHelper>();
        private readonly Mock<ILogger> _mockErrorSDRLogger = new(MockBehavior.Strict);
        private readonly Mock<IConfiguration> _mockConfig = new();
        private readonly IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        private readonly Mock<IChangeAuditService> _mockChangeAuditService = new(MockBehavior.Loose);

        #region Setup
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfilesV3());
                cfg.AddProfile(new AutoMapperProfilesV4());
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
            _mockMapper = new Mapper(mockMapper);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        public static Core.DTO.Common.ChangeAuditStudyDto GetChangeAuditDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<Core.DTO.Common.ChangeAuditStudyDto>(jsonData);
        }
        #endregion

        #region LogHelper UnitTesting
        [Test]
        public void LogHelperInformation_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogInformation("This is Information");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogInformation(""));
        }
        [Test]
        public void LogHelperWarning_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogWarning("This is Warning");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogWarning(""));
        }
        [Test]
        public void LogHelperError_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogError("This is Error");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogError(""));
        }
        [Test]
        public void LogHelperCritical_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogCriitical("This is Critical");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogCriitical(""));
        }
        [Test]
        public void LogHelperDebug_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogDebug("This is Debug");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogDebug(""));
        }
        [Test]
        public void LogHelperTrace_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new(_mockLogger.Object);
            logHelper.LogTrace("This is Trace");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogTrace(""));
        }
        #endregion

        #region Startup Library UnitTesting
        [Test]
        public void Startup_Library_UnitTesting()
        {
            _mockConfig.Setup(x => x.GetSection(It.IsAny<string>()).Value)
                .Returns("true");
            _mockConfig.Setup(x => x.GetSection("ApiVersionUsdmVersionMapping").Value)
               .Returns(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            _mockConfig.Setup(x => x.GetSection("ConformanceRules").Value)
               .Returns(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ConformanceRules.json"));
            var file = "{\"SdrCptMasterDataMapping\":[{\"entity\":\"InterventionModel\",\"mapping\":[{\"code\":\"C142568\",\"CDISC\":\"SEQUENTIAL\",\"CPT\":\"Sequential\"},{\"code\":\"C82637\",\"CDISC\":\"CROSS-OVER\",\"CPT\":\"Cross-OverGroup\"},{\"code\":\"C82638\",\"CDISC\":\"FACTORIAL\",\"CPT\":\"Factorial\"},{\"code\":\"C82639\",\"CDISC\":\"PARALLEL\",\"CPT\":\"ParallelGroup\"},{\"code\":\"C82640\",\"CDISC\":\"SINGLEGROUP\",\"CPT\":\"SingleGroup\"}]},{\"entity\":\"Study Phase\",\"mapping\":[{\"code\":\"C48660\",\"CDISC\":\"NOTAPPLICABLE\",\"CPT\":\"\"},{\"code\":\"C54721\",\"CDISC\":\"PHASE0TRIAL\",\"CPT\":\"EarlyPhase1\"},{\"code\":\"C15600\",\"CDISC\":\"PHASEITRIAL\",\"CPT\":\"Phase1\"},{\"code\":\"C15693\",\"CDISC\":\"PHASEI/IITRIAL\",\"CPT\":\"Phase1/Phase2\"},{\"code\":\"C15601\",\"CDISC\":\"PHASEIITRIAL\",\"CPT\":\"Phase2\"},{\"code\":\"C15694\",\"CDISC\":\"PHASEII/IIITRIAL\",\"CPT\":\"Phase2/Phase3\"},{\"code\":\"C49686\",\"CDISC\":\"PHASEIIATRIAL\",\"CPT\":\"Phase2\"},{\"code\":\"C49688\",\"CDISC\":\"PHASEIIBTRIAL\",\"CPT\":\"Phase2\"},{\"code\":\"C15602\",\"CDISC\":\"PHASEIIITRIAL\",\"CPT\":\"Phase3\"},{\"code\":\"C49687\",\"CDISC\":\"PHASEIIIATRIAL\",\"CPT\":\"Phase3\"},{\"code\":\"C49689\",\"CDISC\":\"PHASEIIIBTRIAL\",\"CPT\":\"Phase3\"},{\"code\":\"C15603\",\"CDISC\":\"PHASEIVTRIAL\",\"CPT\":\"Phase4\"},{\"code\":\"C47865\",\"CDISC\":\"PHASEVTRIAL\",\"CPT\":\"Phase5\"}]},{\"entity\":\"TrialIntentType\",\"mapping\":[{\"code\":\"C15714\",\"CDISC\":\"BASICSCIENCE\",\"CPT\":\"BasicScience\"},{\"code\":\"C49654\",\"CDISC\":\"CURE\",\"CPT\":\"\"},{\"code\":\"C139174\",\"CDISC\":\"DEVICEFEASIBILITY\",\"CPT\":\"DeviceFeasibility\"},{\"code\":\"C49653\",\"CDISC\":\"DIAGNOSIS\",\"CPT\":\"Diagnostic\"},{\"code\":\"C170629\",\"CDISC\":\"DISEASEMODIFYING\",\"CPT\":\"\"},{\"code\":\"C15245\",\"CDISC\":\"HEALTHSERVICESRESEARCH\",\"CPT\":\"HealthServicesResearch\"},{\"code\":\"C49655\",\"CDISC\":\"MITIGATION\",\"CPT\":\"\"},{\"code\":\"\",\"CDISC\":\"\",\"CPT\":\"Other\"},{\"code\":\"C49657\",\"CDISC\":\"PREVENTION\",\"CPT\":\"Prevention\"},{\"code\":\"C71485\",\"CDISC\":\"SCREENING\",\"CPT\":\"Screening\"},{\"code\":\"C71486\",\"CDISC\":\"SUPPORTIVECARE\",\"CPT\":\"SupportiveCare\"},{\"code\":\"C49656\",\"CDISC\":\"TREATMENT\",\"CPT\":\"Treatment\"}]},{\"entity\":\"Objective Level\",\"mapping\":[{\"code\":\"C85826\",\"CDISC\":\"StudyPrimaryObjective\",\"CPT\":\"\"},{\"code\":\"C85827\",\"CDISC\":\"StudySecondaryObjective\",\"CPT\":\"\"}]}]}";
            _mockConfig.Setup(x => x.GetSection("SdrCptMasterDataMapping").Value)
              .Returns(file);
            StartupLib.SetConstants(_mockConfig.Object);
            Assert.AreEqual(Config.ConnectionString, "true");
            Assert.AreEqual(Config.DatabaseName, "true");
            Assert.AreEqual(Config.DateRange, "true");
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            Assert.AreEqual(apiUsdmVersionMapping_NonStatic.SDRVersions.Count, ApiUsdmVersionMapping.SDRVersions.Count);
        }
        #endregion

        #region ErrorResponse Helper Unit Testing
        [Test]
        public void ErrorResponse_Helper_UnitTestng()
        {
            ErrorModel errorModel = ErrorResponseHelper.MethodNotAllowed();

            Assert.AreEqual("405", errorModel.StatusCode);
            Assert.AreEqual("Method Not Allowed", errorModel.Message);

            errorModel = ErrorResponseHelper.GatewayError();

            Assert.AreEqual("500", errorModel.StatusCode);
            Assert.AreEqual("Internal Server Error", errorModel.Message);


            ValidationErrorModel validationErrorModel = ErrorResponseHelper.BadRequest("Validation Error", "Conformance Error");
            Assert.AreEqual("Conformance Error", validationErrorModel.Message);
            Assert.AreEqual("Validation Error", validationErrorModel.Error);
            Assert.AreEqual("400", validationErrorModel.StatusCode);

            errorModel = ErrorResponseHelper.InternalServerError();
            Assert.AreEqual("500", errorModel.StatusCode);
            Assert.AreEqual("Internal Server Error", errorModel.Message);
        }
        #endregion

        #region DateValidationHelper Unit Testing
        [Test]
        public void DateValidaitonHelper_UnitTesting()
        {
            Assert.IsTrue(DateValidationHelper.IsValid(""));
            Assert.IsTrue(DateValidationHelper.IsValid("2022-10-12"));
        }
        #endregion

        #region FluentValidation Unit Testing
        [Test]
        public void FluentValidation_UnitTesting()
        {
            TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon.AddValidationDependenciesCommon(serviceDescriptors);
            TransCelerate.SDR.Core.DTO.Common.SearchParametersDto searchParametersCommon = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE 1 TRAIL",
                SponsorId = "100",
                FromDate = "",
                ToDate = "",
                ValidateUsdmVersion = false
            };
            TransCelerate.SDR.RuleEngine.Common.SearchParametersValidator searchValidator = new();
            Assert.IsTrue(searchValidator.Validate(searchParametersCommon).IsValid);

            TransCelerate.SDR.Core.DTO.Common.SearchTitleParametersDto searchTitleParametersCommon = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                SponsorId = "100",
                FromDate = "",
                ToDate = ""
            };
            TransCelerate.SDR.RuleEngine.Common.SearchTitleParametersValidator searchTitleParametersValidator = new();
            Assert.IsTrue(searchTitleParametersValidator.Validate(searchTitleParametersCommon).IsValid);
        }
        #endregion

        #region HttpContext Response Helper UnitTesting
        [Test]
        public void HttpContextResponseHelper_UnitTesting()
        {
            var mockHttpContext = new DefaultHttpContext();
            string response = string.Empty;
            mockHttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            mockHttpContext.Response.Headers.Remove("Content-Type");
            var method = HttpContextResponseHelper.Response(mockHttpContext, response);
            method.Wait();
            response = method.Result;
            Assert.IsTrue(response.Contains(((int)HttpStatusCode.NotFound).ToString()));
            mockHttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            mockHttpContext.Response.Headers.Remove("Content-Type");
            method = HttpContextResponseHelper.Response(mockHttpContext, response);
            method.Wait();
            response = method.Result;
            Assert.IsTrue(response.Contains(((int)HttpStatusCode.MethodNotAllowed).ToString()));
        }
        #endregion

        #region Spit String Helper
        [Test]
        public void SplitStringIntoArrayHelperUnitTesting()
        {
            // Arrange
            string input = "TestString";
            int splitSize = 2;
            var expected = new List<string> { "Te", "st", "St", "ri", "ng" };

            // Act
            var splitStringList = SplitStringIntoArrayHelper.SplitString(input, splitSize);

            // Assert
            CollectionAssert.AreEqual(expected, splitStringList);
        }
        #endregion

        #region ActionFilter
        [Test]
        public void ActionFilter_UnitTesting()
        {
            ActionFilter actionFilter = new(_mockLogHelper);

            Mock<ActionExecutionDelegate> actionExecutionDelegate = new();

            Core.DTO.Common.ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>()))
                .Returns(Task.FromResult(study as object));
            ChangeAuditController changeAuditController = new(_mockChangeAuditService.Object, _mockLogHelper);
            var method = changeAuditController.GetChangeAudit("sd");
            method.Wait();
            var result = method.Result;

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                changeAuditController.ModelState
            );
            var actionExecutedContext = new ActionExecutedContext(
                actionContext,
                new List<IFilterMetadata>(),
                changeAuditController);
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                changeAuditController
            );

            actionExecutionDelegate.Setup(x => x())
                .Returns(Task.FromResult(actionExecutedContext));

            var method1 = actionFilter.OnActionExecutionAsync(actionExecutingContext, actionExecutionDelegate.Object);
            method1.Wait();
            var res = method.GetAwaiter();

            ActionExecutionDelegate actionExecutionDelegate1 = Mock.Of<ActionExecutionDelegate>();
            actionExecutionDelegate.Setup(x => x())
             .Throws(new Exception());
            method1 = actionFilter.OnActionExecutionAsync(actionExecutingContext, actionExecutionDelegate.Object);
            //Assert.Throws<NullReferenceException>(()=>method1.Wait());
            Assert.Throws(Is.InstanceOf<AggregateException>(), () => method1.Wait());
        }
        #endregion

        #region VersioningErrorResponseHelper
        [Test]
        public void VersioningErrorResponseHelperUnitTesting()
        {
            var errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.UnsupportedApiVersion,
                   "",
                   "");

            VersioningErrorResponseHelper versioningErrorResponseHelper = new();
            var result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            var actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).Message, Constants.ErrorMessages.UsdmVersionMapError);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.ApiVersionUnspecified,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).Message, Constants.ErrorMessages.UsdmVersionMissing);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.AmbiguousApiVersion,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).Message, Constants.ErrorMessages.UsdmVersionAmbiguous);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.InvalidApiVersion,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).Message, Constants.ErrorMessages.UsdmVersionMapError);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   "",
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).Message, Constants.ErrorMessages.UsdmVersionMapError);
        }
        #endregion

        #region DataFilter
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFilterCommon.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);

            SearchTitleParametersEntity searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                SortBy = "version",
                SortOrder = "asc",
                GroupByStudyId = true,
                SponsorId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
            };

            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchTitle(searchParameters));
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchTitleV4(searchParameters));

            Assert.IsNotNull(DataFilterCommon.GetFiltersForGetAudTrail("sd", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));


            Assert.IsNotNull(DataFilterCommon.GetFiltersForStudyHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "sd"));
            Assert.IsNotNull(DataFilterCommon.GetFiltersForStudyHistoryV4(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "sd"));


            Assert.IsNotNull(DataFilterCommon.GetFiltersForGetStudyBsonDocument("sd", 1));

            Assert.IsNotNull(DataFilterCommon.GetFiltersForGetStudyBsonDocument("sd", 0));

            Assert.IsNotNull(DataFilterCommon.GetSorterForBsonDocument());

            Assert.IsNotNull(DataFilterCommon.GetProjectorForBsonDocument());

            SearchParametersEntity searchParametersEntity = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                SponsorId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
                Asc = true,
                Header = "sdrversion",
                ValidateUsdmVersion = false
            };

            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity));
            searchParametersEntity.Header = "studytitle";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudy(searchParametersEntity));
            searchParametersEntity.Header = "sdrversion";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudy(searchParametersEntity));
            searchParametersEntity.Header = "lastmodifieddate";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudy(searchParametersEntity));
            searchParametersEntity.Header = "usdmversion";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudy(searchParametersEntity));

            searchParameters.SortBy = "studytitle";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
            searchParameters.SortBy = "version";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
            searchParameters.SortBy = "lastmodifieddate";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
            searchParameters.SortBy = "usdmversion";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));

            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchV3(searchParametersEntity));
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchV4(searchParametersEntity));
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchV5(searchParametersEntity));

            searchParameters.SortBy = "studytitle";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));
            searchParameters.SortBy = "version";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));
            searchParameters.SortBy = "lastmodifieddate";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));
            searchParameters.SortBy = "usdmversion";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));

            searchParameters.SortBy = "sponsorid";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));

            searchParameters.SortBy = "indication";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));

            searchParameters.SortBy = "interventionmodel";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));

            searchParameters.SortBy = "phase";
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, true));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV3(new List<Core.Entities.StudyV3.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV4(new List<Core.Entities.StudyV4.SearchResponseEntity>(), searchParameters.SortBy, false));
            Assert.IsNotNull(DataFilterCommon.SortSearchResultsV5(new List<Core.Entities.StudyV5.SearchResponseEntity>(), searchParameters.SortBy, false));
        }

        #endregion

        #region Dependency Injector Unit Testing
        [Test]
        public void ApplicationDependencyInjectorUnitTesting()
        {
            IServiceCollection services = Mock.Of<IServiceCollection>();
            Config.ConnectionString = "mongodb://localhost:password@localhost:10255/admin?ssl=true&retrywrites=false&maxIdleTimeMS=120000";
            ApplicationDependencyInjector.AddApplicationDependencies(services);
        }
        #endregion
    }
}
