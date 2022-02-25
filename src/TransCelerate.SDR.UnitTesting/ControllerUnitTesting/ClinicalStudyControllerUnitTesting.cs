using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.Entities.Study;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TransCelerate.SDR.WebApi.Mappers;
using TransCelerate.SDR.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Core.Utilities.Helpers;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Reflection;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Entities;

namespace TransCelerate.SDR.UnitTesting
{
    public class ClinicalStudyControllerUnitTesting
    {
        #region Variables        
        private IMapper _mockMapper;
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private ILogHelper _mockControllerLogger = Mock.Of<ILogHelper>();
        private Mock<IClinicalStudyRepository> _mockClinicalStudyRepository = new Mock<IClinicalStudyRepository>(MockBehavior.Loose);
        private Mock<IClinicalStudyService> _mockClinicalStudyService = new Mock<IClinicalStudyService>(MockBehavior.Loose);

        StudyEntity study = new StudyEntity();
        List<StudyEntity> studyList = new List<StudyEntity>();
        GetStudyAuditDTO auditTrail = new GetStudyAuditDTO();
        List<GetStudyDTO> studyDTO = new List<GetStudyDTO>();
        GetStudySectionsDTO studySectionsDTO = new GetStudySectionsDTO();
        PostStudyDTO postStudyDTO = new PostStudyDTO();
        #endregion

        #region Setup
        public StudyEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            return study;
        }
        public PostStudyDTO PostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\PostStudyData.json");
            postStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(jsonData);
            return postStudyDTO;
        }
        public List<GetStudyDTO> GetDataForSearchFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudyListData.json");
            studyDTO = JsonConvert.DeserializeObject<List<GetStudyDTO>>(jsonData);
            return studyDTO;
        }
        public GetStudySectionsDTO GetStudySectionsDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudySectionsData.json");
            studySectionsDTO = JsonConvert.DeserializeObject<GetStudySectionsDTO>(jsonData);
            return studySectionsDTO;
        }
        public StudyEntity GetPostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\PostStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            return study;
        }
        public GetStudyAuditDTO GetAuditDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudyAuditData.json");
            auditTrail = JsonConvert.DeserializeObject<GetStudyAuditDTO>(jsonData);
            return auditTrail;
        }
        public List<StudyEntity> GetListDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudyListData.json");
            studyList = JsonConvert.DeserializeObject<List<StudyEntity>>(jsonData);
            return studyList;
        }
        public List<StudyEntity> GetListForSearchDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetSearchStudyData.json");
            studyList = JsonConvert.DeserializeObject<List<StudyEntity>>(jsonData);
            return studyList;
        }

        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfies());
            });
            _mockMapper = new Mapper(mockMapper);
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
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft"))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft")); 
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudy("1", 1, "1.0Draft",null);
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


            Assert.AreEqual(expected.clinicalStudy.studyId, actual_result.clinicalStudy.studyId);
            Assert.AreEqual(expected.clinicalStudy.studyPhase, actual_result.clinicalStudy.studyPhase);            
            Assert.AreEqual(expected.clinicalStudy.studyTitle, actual_result.clinicalStudy.studyTitle);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, null))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1,null));          
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudy("2", 1, null, sections);
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);     
            Assert.AreEqual("404", actual_result.statusCode);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse_BadRequest()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft"))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft"));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = "study_objectives,studyinvestigational_interventions,study_protocol,study_indications,study_design";

            var method = clinicalStudyController.GetStudy("2", 1, "New", sections);
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.SectionNotValid);

            //Actual
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            method = clinicalStudyController.GetStudy(null, 1, "New", sections);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                     .Throws(new Exception("Error"));
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft"))
                     .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft"));
            ClinicalStudyController clinicalStudyController1 = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            sections = string.Empty;
            method = clinicalStudyController1.GetStudy("1", 1, "1.0Draft", sections);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }
        #endregion

        #region GET Design Sections UnitTesting
        [Test]
        public void GetDesignSections_UnitTest_SuccessResponse()
        {
            string[] sectionArray = { "study_planned_workflow", "study_target_populations", "study_cells" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sectionArray))
                    .Returns(ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sectionArray)); 
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = "study_planned_workflow,study_target_populations,study_cells";

            var method = clinicalStudyController.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections);
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


            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].description);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping, actual_result.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping);           
            Assert.AreEqual(expected.studyDesigns[0].studyPopulations[0].description, actual_result.studyDesigns[0].studyPopulations[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyEpoch.description, actual_result.studyDesigns[0].studyCells[0].studyEpoch.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyElements[0].description, actual_result.studyDesigns[0].studyCells[0].studyElements[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);            
        }

        [Test]
        public void GetDesignSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllElements("1", 1, "1.0Draft"))
                    .Returns(ClinicalStudyService.GetAllElements("1", 1, "1.0Draft"));          
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            string sections = string.Empty;

            var method = clinicalStudyController.GetStudyDesignSections("2","1", 1, "New", sections);
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);     
            Assert.AreEqual("404", actual_result.statusCode);

            method = clinicalStudyController.GetStudy(null, 1, "New", sections);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.StudyInputError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);


            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] section = new string[] { };
            _mockClinicalStudyService.Setup(x => x.GetStudyDesignSections("1", "1",1, "1.0Draft", section))
                    .Returns(ClinicalStudyService1.GetStudyDesignSections("1","1", 1, "1.0Draft", section));
            ClinicalStudyController clinicalStudyController1 = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            sections = string.Empty;

            method = clinicalStudyController1.GetStudyDesignSections("1", "1", 1, "1.0Draft", sections);
            method.Wait();
            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }
        #endregion

        #region GET AuditTrail UnitTesting
        [Test]
        public void GetAuditTrail_UnitTest_SuccessResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId))
                    .Returns(ClinicalStudyService.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAuditTrail(GetAuditDataFromStaticJson().studyId, fromDate, toDate);
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


            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.auditTrail[0].studyVersion, actual_result.auditTrail[0].studyVersion);
            Assert.AreEqual(expected.auditTrail[0].entryDateTime, actual_result.auditTrail[0].entryDateTime);
            Assert.AreEqual(expected.auditTrail[1].studyVersion, actual_result.auditTrail[1].studyVersion);
            Assert.AreEqual(expected.auditTrail[1].entryDateTime, actual_result.auditTrail[1].entryDateTime);
        }

        [Test]
        public void GetAuditTrail_UnitTest_FailureResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().studyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().studyId))
                    .Returns(ClinicalStudyService.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().studyId));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


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

            Assert.AreEqual(expected.message, actual_result.message);            
            Assert.AreEqual("404", actual_result.statusCode);

            method = clinicalStudyController.GetAuditTrail(null,DateTime.Now.AddDays(1),DateTime.Now);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateError);

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId))
                    .Returns(ClinicalStudyService1.GetAuditTrail(It.IsAny<DateTime>(), It.IsAny<DateTime>(), GetAuditDataFromStaticJson().studyId));
            ClinicalStudyController clinicalStudyController1 = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


            method = clinicalStudyController1.GetAuditTrail(GetAuditDataFromStaticJson().studyId, DateTime.MinValue, DateTime.MinValue);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }
        #endregion

        #region GET All StudyId UnitTesting
        public class AllStudyElementsResponse
        {
            public string studyId { get; set; }
            public string studyTitle { get; set; }
            public int[] studyVersion { get; set; }
        }
        public class AllStudyResponse
        {
            public object study { get; set; }
        }

        [Test]
        public void GetAllStudy_UnitTest_SuccessResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<StudyHistoryEntity> studyHistories = new List<StudyHistoryEntity>();
            studyList = GetListDataFromStaticJson();
            StudyHistoryEntity studyHistory = new StudyHistoryEntity();
            studyHistory.studyId = studyList[0].clinicalStudy.studyId;
            studyHistory.studyTitle = studyList[0].clinicalStudy.studyTitle;
            studyHistory.studyVersion = studyList[0].auditTrail.studyVersion;
            studyHistories.Add(studyHistory);
            studyHistory.studyId = studyList[1].clinicalStudy.studyId;
            studyHistory.studyTitle = studyList[1].clinicalStudy.studyTitle;
            studyHistory.studyVersion = studyList[1].auditTrail.studyVersion;
            studyHistories.Add(studyHistory);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .Returns(Task.FromResult(studyHistories as object));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .Returns(ClinicalStudyService.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAllStudyId(fromDate, toDate);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AllStudyResponse>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            var studyElements = JsonConvert.DeserializeObject<List<AllStudyElementsResponse>>(
                JsonConvert.SerializeObject(actual_result.study));

            //Assert          
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.AreEqual(expected[0].clinicalStudy.studyId, studyElements[0].studyId);
            Assert.AreEqual(expected[0].clinicalStudy.studyTitle, studyElements[0].studyTitle);
            Assert.AreEqual(expected[1].auditTrail.studyVersion, studyElements[0].studyVersion[0]);
            //Assert.AreEqual(expected[0].auditTrail.studyVersion, studyElements[0].studyVersion[1]);
            
        }

        [Test]
        public void GetAllStudy_UnitTest_FailureResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(fromDate, toDate))
                    .Returns(Task.FromResult(GetListDataFromStaticJson() as object));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(fromDate, toDate))
                    .Returns(ClinicalStudyService.GetAllStudyId(fromDate, toDate));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


            var method = clinicalStudyController.GetAllStudyId(fromDate, toDate);
            method.Wait();


            //Expected
            var expected = ErrorResponseHelper.NotFound(Constants.ErrorMessages.StudyNotFound);

            //Actual
            var actual_result = (method.Result as NotFoundObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (method.Result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("404", actual_result.statusCode);

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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .Returns(ClinicalStudyService1.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            ClinicalStudyController clinicalStudyController1 = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


            method = clinicalStudyController1.GetAllStudyId(DateTime.MinValue, DateTime.MinValue);
            method.Wait();

            //Expected
            expected = ErrorResponseHelper.BadRequest("An Error Occured");

            //Actual
            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }
        #endregion 
        #endregion.

        #region POST Method Unit Testing
        #region POST All Elements UnitTesting
        [Test]
        public void PostAllElments_UnitTest_SuccessResponse()
        {
            PostStudyResponseDTO postStudyResponseDTO = new PostStudyResponseDTO { studyId = GetDataFromStaticJson().clinicalStudy.studyId };

            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson().clinicalStudy.studyId));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<PostStudyDTO>(), null))
                    //.Returns(ClinicalStudyService.PostAllElements(PostDataFromStaticJson(), null, null));
                    .Returns(Task.FromResult(postStudyResponseDTO as object));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);            

            var method = clinicalStudyController.PostAllElements(PostDataFromStaticJson(), null);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson().clinicalStudy.studyId;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostStudyResponseDTO>(
                 JsonConvert.SerializeObject((result as CreatedResult).Value));

            //Assert          
            Assert.IsNotNull((result as CreatedResult).Value);
            Assert.AreEqual(201, (result as CreatedResult).StatusCode);
            Assert.IsInstanceOf(typeof(CreatedResult), result);

            Assert.AreEqual(expected, actual_result.studyId);
        }
        [Test]
        public void PostAllElments_UnitTest_FailureResponse()
        {
            PostStudyResponseDTO postStudyResponseDTO = new PostStudyResponseDTO { studyId = GetDataFromStaticJson().clinicalStudy.studyId };

            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.PostAllElements(It.IsAny<PostStudyDTO>(), null))
                    //.Returns(ClinicalStudyService.PostAllElements(PostDataFromStaticJson(), null, null));
                    .Returns(ClinicalStudyService.PostAllElements(It.IsAny<PostStudyDTO>(), null));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);

            var method = clinicalStudyController.PostAllElements(PostDataFromStaticJson(), null);
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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);
        }      

        #endregion

        #region Search Study Unit Testing
        [Test]
        public void SearchStudy_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>()))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));
            SearchParametersDTO searchParameters = new SearchParametersDTO
            {
                briefTitle = "Umbrella",
                indication = "Bile",
                interventionModel = "CROSS_OVER",
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,
                phase = "PHASE_1_TRAIL",
                studyId = "100",
                fromDate = DateTime.Now.AddDays(-5).ToString(),
                toDate = DateTime.Now.ToString()
            };
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(searchParameters))
                    .Returns(ClinicalStudyService.SearchStudy(searchParameters));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);


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

            Assert.AreEqual(expected[0].clinicalStudy.studyDesigns[0].investigationalInterventions[0].interventionType, actual_result[0].clinicalStudy.studyDesigns[0].investigationalInterventions[0].interventionType);
            Assert.AreEqual(expected[0].clinicalStudy.objectives[0].description, actual_result[0].clinicalStudy.objectives[0].description);
            Assert.AreEqual(expected[0].clinicalStudy.studyIndications[0].description, actual_result[0].clinicalStudy.studyIndications[0].description);
            Assert.AreEqual(expected[0].clinicalStudy.studyDesigns[0].studyDesignId, actual_result[0].clinicalStudy.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected[0].clinicalStudy.studyId, actual_result[0].clinicalStudy.studyId);
            Assert.AreEqual(expected[1].clinicalStudy.studyDesigns[0].investigationalInterventions[0].interventionType, actual_result[1].clinicalStudy.studyDesigns[0].investigationalInterventions[0].interventionType);
            Assert.AreEqual(expected[1].clinicalStudy.objectives[0].description, actual_result[1].clinicalStudy.objectives[0].description);
            Assert.AreEqual(expected[1].clinicalStudy.studyIndications[0].description, actual_result[1].clinicalStudy.studyIndications[0].description);
            Assert.AreEqual(expected[1].clinicalStudy.studyDesigns[0].studyDesignId, actual_result[1].clinicalStudy.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected[1].clinicalStudy.studyId, actual_result[1].clinicalStudy.studyId);
        }

        [Test]
        public void SearchStudy_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>()))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));
            SearchParametersDTO searchParameters = new SearchParametersDTO
            {
                briefTitle = "Umbrella",
                indication = "Bile",
                interventionModel = "CROSS_OVER",
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,
                phase = "PHASE_1_TRAIL",
                studyId = "100",
                fromDate = DateTime.Now.AddDays(-5).ToString(),
                toDate = DateTime.Now.ToString()
            };
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(searchParameters))
                    .Returns(ClinicalStudyService.SearchStudy(searchParameters));
            ClinicalStudyController clinicalStudyController = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);

            SearchParametersDTO searchParametersChanged = new SearchParametersDTO
            {
                briefTitle = "Umbrella1",
                indication = "Bile",
                interventionModel = "CROSS_OVER",
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,
                phase = "PHASE_1_TRAIL",
                studyId = "100",
                fromDate = DateTime.Now.AddDays(-5).ToString(),
                toDate = DateTime.Now.ToString()
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

            Assert.AreEqual(expected.message, actual_result.message);           
            Assert.AreEqual("404", actual_result.statusCode);

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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>()))
                    .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            _mockClinicalStudyService.Setup(x => x.SearchStudy(It.IsAny<SearchParametersDTO>()))
                    .Returns(ClinicalStudyService1.SearchStudy(It.IsAny<SearchParametersDTO>()));
            ClinicalStudyController clinicalStudyController1 = new ClinicalStudyController(_mockClinicalStudyService.Object, _mockControllerLogger);
            searchParameters.fromDate = null;
            searchParameters.toDate = null;

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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

        }
        #endregion
        #endregion
        #endregion
    }
}
