using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting
{
    public class ClinicalStudyControllerUnitTesting
    {
        #region Variables        
        private IMapper _mockMapper;
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly ILogHelper _mockControllerLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IClinicalStudyRepository> _mockClinicalStudyRepository = new(MockBehavior.Loose);
        private readonly Mock<IClinicalStudyService> _mockClinicalStudyService = new(MockBehavior.Loose);

        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        StudyEntity study = new();
        List<StudyEntity> studyList = new();
        GetStudyAuditDTO auditTrail = new();
        List<GetStudyDTO> studyDTO = new();
        GetStudySectionsDTO studySectionsDTO = new();
        PostStudyDTO postStudyDTO = new();
        #endregion

        #region Setup
        public StudyEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            study.AuditTrail.UsdmVersion = "mvp";
            return study;
        }
        public PostStudyDTO PostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            postStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(jsonData);
            postStudyDTO.AuditTrail.UsdmVersion = "mvp";
            return postStudyDTO;
        }
        public List<GetStudyDTO> GetDataForSearchFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyListData.json");
            studyDTO = JsonConvert.DeserializeObject<List<GetStudyDTO>>(jsonData);
            studyDTO.ForEach(x => x.AuditTrail.UsdmVersion = "mvp");
            return studyDTO;
        }
        public GetStudySectionsDTO GetStudySectionsDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudySectionsData.json");
            studySectionsDTO = JsonConvert.DeserializeObject<GetStudySectionsDTO>(jsonData);
            return studySectionsDTO;
        }
        public GetStudyAuditDTO GetAuditDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyAuditData.json");
            auditTrail = JsonConvert.DeserializeObject<GetStudyAuditDTO>(jsonData);
            return auditTrail;
        }
        public List<StudyEntity> GetListDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyListData.json");
            studyList = JsonConvert.DeserializeObject<List<StudyEntity>>(jsonData);
            studyList.ForEach(x => x.AuditTrail.UsdmVersion = "mvp");
            return studyList;
        }
        public static List<SearchResponse> GetListForSearchDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");
            var data = JsonConvert.DeserializeObject<List<SearchResponse>>(jsonData);
            data.ForEach(x => x.UsdmVersion = "mvp");
            return data;
        }
        public static List<SearchTitleEntity> GetListForSearchTitleDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");
            var data = JsonConvert.DeserializeObject<List<SearchTitleEntity>>(jsonData);
            data.ForEach(x => x.UsdmVersion = "mvp");
            return data;
        }
        public static List<SearchTitleDTO> GetListForSearchTitleDTODataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyListData.json");
            var data = JsonConvert.DeserializeObject<List<SearchTitleDTO>>(jsonData);
            data.ForEach(x => x.AuditTrail.UsdmVersion = "mvp");
            return data;
        }

        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfies());
            });
            _mockMapper = new Mapper(mockMapper);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        #endregion

        #region TestCases
        #region GET Methods Unit Testing

        #region GET All Elements UnitTesting
        [Test]
        public void GetAllElments_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson() as object));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudy("1", 1, "1.0Draft", null, "mvp");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);


            Assert.AreEqual(expected.ClinicalStudy.StudyId, actual_result.ClinicalStudy.StudyId);
            Assert.AreEqual(expected.ClinicalStudy.StudyPhase, actual_result.ClinicalStudy.StudyPhase);
            Assert.AreEqual(expected.ClinicalStudy.StudyTitle, actual_result.ClinicalStudy.StudyTitle);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, null, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, null, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudy("2", 1, null, sections, "mvp");
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("404", actual_result.StatusCode);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse_BadRequest()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = "study_objectives,studyinvestigational_interventions,study_protocol,study_indications,study_design";

            var method = clinicalStudyController.GetStudy("2", 1, "New", sections, "mvp");
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SectionNotValid);

            //Actual
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            method = clinicalStudyController.GetStudy(null, 1, "New", sections, "mvp");
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                     .Throws(new Exception("Error"));
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()))
                     .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController1 = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            sections = string.Empty;
            method = clinicalStudyController1.GetStudy("1", 1, "1.0Draft", sections, "mvp");
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            method = clinicalStudyController1.GetStudy("1", 1, "1.0Draft", sections, "");
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMissing);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            method = clinicalStudyController1.GetStudy("1", 1, "1.0Draft", sections, "1.0");
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
        }
        #endregion

        #region GET Design Sections UnitTesting
        [Test]
        public void GetDesignSections_UnitTest_SuccessResponse()
        {
            string[] sectionArray = { "study_planned_workflow", "study_target_populations", "study_cells" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sectionArray, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sectionArray, user));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = "study_planned_workflow,study_target_populations,study_cells";

            var method = clinicalStudyController.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections, "mvp");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);


            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].Description, actual_result.StudyDesigns[0].PlannedWorkflows[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping, actual_result.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping);
            Assert.AreEqual(expected.StudyDesigns[0].StudyPopulations[0].Description, actual_result.StudyDesigns[0].StudyPopulations[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyArm.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyArm.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyEpoch.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyEpoch.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyElements[0].Description, actual_result.StudyDesigns[0].StudyCells[0].StudyElements[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
        }

        [Test]
        public void GetDesignSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft", It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudyDesignSections("2", "1", 1, "New", sections, "mvp");
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("404", actual_result.StatusCode);

            method = clinicalStudyController.GetStudy(null, 1, "New", sections, "mvp");
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);


            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] section = Array.Empty<string>();
            _mockClinicalStudyService.Setup(x => x.GetStudyDesignSections("1", "1", 1, "1.0Draft", section, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService1.GetStudyDesignSections("1", "1", 1, "1.0Draft", section, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController1 = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            sections = string.Empty;

            method = clinicalStudyController1.GetStudyDesignSections("1", "1", 1, "1.0Draft", sections, "mvp");
            method.Wait();
            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            method = clinicalStudyController1.GetStudyDesignSections("1", "1", 1, "1.0Draft", sections, "1.0");
            method.Wait();
            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
            method = clinicalStudyController1.GetStudyDesignSections("1", "1", 1, "1.0Draft", sections, "1.0");
            method.Wait();
            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.UsdmVersionMapError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
        }
        #endregion

        #region GET AuditTrail UnitTesting
        [Test]
        public void GetAuditTrail_UnitTest_SuccessResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId, user));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAuditTrail(GetAuditDataFromStaticJson().StudyId, fromDate, toDate);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetAuditDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyAuditDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);


            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
            Assert.AreEqual(expected.AuditTrail[0].StudyVersion, actual_result.AuditTrail[0].StudyVersion);
            Assert.AreEqual(expected.AuditTrail[0].EntryDateTime, actual_result.AuditTrail[0].EntryDateTime);
            Assert.AreEqual(expected.AuditTrail[1].StudyVersion, actual_result.AuditTrail[1].StudyVersion);
            Assert.AreEqual(expected.AuditTrail[1].EntryDateTime, actual_result.AuditTrail[1].EntryDateTime);
        }

        [Test]
        public void GetAuditTrail_UnitTest_FailureResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().StudyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().StudyId, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().StudyId, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAuditTrail("1", fromDate, toDate);
            method.Wait();


            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("404", actual_result.StatusCode);

            method = clinicalStudyController.GetAuditTrail(null, DateTime.Now.AddDays(1), DateTime.Now);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService1.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().StudyId, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController1 = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            method = clinicalStudyController1.GetAuditTrail(GetAuditDataFromStaticJson().StudyId, DateTime.MinValue, DateTime.MinValue);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
        }
        #endregion

        #region GET All StudyId UnitTesting
        public class AllStudyElementsResponse
        {
            public string StudyId { get; set; }
            public string StudyTitle { get; set; }
            public int[] StudyVersion { get; set; }
        }
        public class AllStudyResponse
        {
            public object Study { get; set; }
        }

        [Test]
        public void GetAllStudy_UnitTest_SuccessResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<StudyHistoryEntity> studyHistories = new();
            studyList = GetListDataFromStaticJson();
            StudyHistoryEntity studyHistory = new()
            {
                StudyId = studyList[0].ClinicalStudy.StudyId,
                StudyTitle = studyList[0].ClinicalStudy.StudyTitle,
                StudyVersion = studyList[0].AuditTrail.StudyVersion
            };
            studyHistories.Add(studyHistory);
            studyHistory.StudyId = studyList[1].ClinicalStudy.StudyId;
            studyHistory.StudyTitle = studyList[1].ClinicalStudy.StudyTitle;
            studyHistory.StudyVersion = studyList[1].AuditTrail.StudyVersion;
            studyHistories.Add(studyHistory);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAllStudyId(fromDate, toDate, null);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AllStudyResponse>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            var studyElements = JsonConvert.DeserializeObject<List<AllStudyElementsResponse>>(
                JsonConvert.SerializeObject(actual_result.Study));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.AreEqual(expected[0].ClinicalStudy.StudyId, studyElements[0].StudyId);
            Assert.AreEqual(expected[0].ClinicalStudy.StudyTitle, studyElements[0].StudyTitle);
            Assert.AreEqual(expected[1].AuditTrail.StudyVersion, studyElements[0].StudyVersion[0]);
            //Assert.AreEqual(expected[0].auditTrail.studyVersion, studyElements[0].studyVersion[1]);

        }

        [Test]
        public void GetAllStudy_UnitTest_FailureResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<StudyHistoryEntity> studyHistories = new();
            studyList = GetListDataFromStaticJson();
            StudyHistoryEntity studyHistory = new()
            {
                StudyId = studyList[0].ClinicalStudy.StudyId,
                StudyTitle = studyList[0].ClinicalStudy.StudyTitle,
                StudyVersion = studyList[0].AuditTrail.StudyVersion
            };
            studyHistories.Add(studyHistory);
            studyHistory.StudyId = studyList[1].ClinicalStudy.StudyId;
            studyHistory.StudyTitle = studyList[1].ClinicalStudy.StudyTitle;
            studyHistory.StudyVersion = studyList[1].AuditTrail.StudyVersion;
            studyHistories.Add(studyHistory);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(fromDate, toDate, null, It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(fromDate, toDate, null, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.GetAllStudyId(fromDate, toDate, null, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAllStudyId(fromDate, toDate, null);
            method.Wait();


            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("404", actual_result.StatusCode);

            method = clinicalStudyController.GetAuditTrail(null, DateTime.Now.AddDays(1), DateTime.Now);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService1.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController1 = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            method = clinicalStudyController1.GetAllStudyId(DateTime.MinValue, DateTime.MinValue, null);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
        }
        #endregion 
        #endregion.

        #region POST Method Unit Testing
        #region POST All Elements UnitTesting
        [Test]
        public void PostAllElments_UnitTest_SuccessResponse()
        {
            PostStudyDTO postStudyResponseDTO = new() { ClinicalStudy = PostDataFromStaticJson().ClinicalStudy, };

            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson().ClinicalStudy.StudyId));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<PostStudyDTO>(), null, It.IsAny<LoggedInUser>()))
                    //.Returns(ClinicalStudyService.PostAllElements(PostDataFromStaticJson(), null, null));
                    .Returns(Task.FromResult(postStudyResponseDTO as object));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);

            var method = clinicalStudyController.PostAllElements(PostDataFromStaticJson(), null, "mvp");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson().ClinicalStudy.StudyId;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostStudyDTO>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            //Assert          
            Assert.IsNotNull((result as CreatedResult).Value);
            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
            Assert.IsInstanceOf(typeof(CreatedResult), result);

            Assert.AreEqual(expected, actual_result.ClinicalStudy.StudyId);
        }
        [Test]
        public void PostAllElments_UnitTest_FailureResponse()
        {
            PostStudyDTO postStudyResponseDTO = new() { ClinicalStudy = PostDataFromStaticJson().ClinicalStudy };

            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.PostAllElements(PostDataFromStaticJson(), null, It.IsAny<LoggedInUser>()))
                    //.Returns(ClinicalStudyService.PostAllElements(PostDataFromStaticJson(), null, null));
                    .Returns(ClinicalStudyService.PostAllElements(PostDataFromStaticJson(), null, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);

            var method = clinicalStudyController.PostAllElements(PostDataFromStaticJson(), null, "mvp");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual            
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);
        }

        #endregion

        #region Search Study Unit Testing
        [Test]
        public void SearchStudy_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));
            SearchParametersDTO searchParameters = new()
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
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(searchParameters, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.SearchStudy(searchParameters, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.SearchStudy(searchParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataForSearchFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GetStudyDTO>>(
               JsonConvert.SerializeObject(((result as OkObjectResult).Value)));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.AreEqual(expected[0].ClinicalStudy.StudyIndications[0].Description, actual_result[0].ClinicalStudy.StudyIndications[0].Description);
            Assert.AreEqual(expected[1].ClinicalStudy.StudyIndications[0].Description, actual_result[1].ClinicalStudy.StudyIndications[0].Description);
        }

        [Test]
        public void SearchStudy_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));
            SearchParametersDTO searchParameters = new()
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
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(searchParameters, It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService.SearchStudy(searchParameters, It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);

            SearchParametersDTO searchParametersChanged = new()
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

            var method = clinicalStudyController.SearchStudy(searchParametersChanged);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.SearchNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("404", actual_result.StatusCode);

            method = clinicalStudyController.SearchStudy(null);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), It.IsAny<LoggedInUser>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDTO>(), It.IsAny<LoggedInUser>()))
                    .Returns(ClinicalStudyService1.SearchStudy(It.IsAny<SearchParametersDTO>(), It.IsAny<LoggedInUser>()));
            ClinicalStudyController clinicalStudyController1 = new(_mockClinicalStudyService.Object, _mockControllerLogger);
            searchParameters.FromDate = null;
            searchParameters.ToDate = null;

            method = clinicalStudyController1.SearchStudy(searchParameters);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

        }
        #endregion

        #region Search Study Title Unit Testing
        [Test]
        public void SearchStudyTitle_UnitTest_SuccessResponse()
        {
            SearchTitleParametersDTO searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
                GroupByStudyId = true
            };
            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDTO>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(GetListForSearchTitleDTODataFromStaticJson()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.SearchTitle(searchParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListForSearchTitleDTODataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleDTO>>(
               JsonConvert.SerializeObject(((result as OkObjectResult).Value)));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.AreEqual(expected[0].ClinicalStudy.StudyTitle, actual_result[0].ClinicalStudy.StudyTitle);
        }

        [Test]
        public void SearchStudyTitle_UnitTest_FailureResponse()
        {
            SearchTitleParametersDTO searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                FromDate = "",
                ToDate = "",
                GroupByStudyId = true
            };
            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDTO>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(GetListForSearchTitleDTODataFromStaticJson()));
            ClinicalStudyController clinicalStudyController = new(_mockClinicalStudyService.Object, _mockControllerLogger);

            SearchTitleParametersDTO searchParametersChanged = new()
            {
                StudyTitle = "",
                PageNumber = 1,
                PageSize = 25,
                FromDate = "",
                ToDate = "",
                GroupByStudyId = true
            };
            var method = clinicalStudyController.SearchTitle(searchParametersChanged);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = Constants.ValidationErrorMessage.AnyOneFieldError;
            //Actual            
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            _mockClinicalStudyService.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersDTO>(), It.IsAny<LoggedInUser>()))
                    .Throws(new Exception("Error"));


            method = clinicalStudyController.SearchTitle(searchParameters);
            method.Wait();

            //Expected
            var expected_error = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected_error.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            method = clinicalStudyController.SearchTitle(null);
            method.Wait();

            //Expected
            expected_error = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected_error.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

        }
        #endregion
        #endregion
        #endregion
    }
}
