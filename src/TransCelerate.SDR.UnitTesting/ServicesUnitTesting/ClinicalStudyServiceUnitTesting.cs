using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting
{
    public class ClinicalStudyServiceUnitTesting
    {
        #region Variables        
        private IMapper _mockMapper;
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IClinicalStudyRepository> _mockClinicalStudyRepository = new(MockBehavior.Loose);

        StudyEntity study = new();
        List<StudyEntity> studyList = new();
        GetStudyAuditDTO auditTrail = new();
        List<GetStudyDTO> studyDTO = new();
        GetStudySectionsDTO studySectionsDTO = new();
        #endregion

        #region Setup
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public StudyEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            study.AuditTrail.UsdmVersion = "mvp";
            return study;
        }
        public List<GetStudyDTO> GetDataForSearchFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyListData.json");
            studyDTO = JsonConvert.DeserializeObject<List<GetStudyDTO>>(jsonData);
            return studyDTO;
        }
        public GetStudySectionsDTO GetStudySectionsDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudySectionsData.json");
            studySectionsDTO = JsonConvert.DeserializeObject<GetStudySectionsDTO>(jsonData);
            return studySectionsDTO;
        }
        public StudyEntity GetPostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            return study;
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
            return studyList;
        }
        public static List<SearchResponse> GetListForSearchDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");
            return JsonConvert.DeserializeObject<List<SearchResponse>>(jsonData);
        }
        public static List<SearchTitleEntity> GetListForSearchTitleDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");
            return JsonConvert.DeserializeObject<List<SearchTitleEntity>>(jsonData);
        }
        public static List<SearchTitleDTO> GetListForSearchTitleDTODataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyListData.json");
            return JsonConvert.DeserializeObject<List<SearchTitleDTO>>(jsonData);
        }

        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfies());
            });
            _mockMapper = new Mapper(mockMapper);
            user.UserName = "user1@SDR.com";
            user.UserRole = Constants.Roles.Org_Admin;
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), 0))
                .Returns(Task.FromResult(GetDataFromStaticJson().AuditTrail));
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

            var method = ClinicalStudyService.GetAllElements("1", 1, "1.0Draft", user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.AreEqual(expected.ClinicalStudy.StudyId, actual_result.ClinicalStudy.StudyId);
            Assert.AreEqual(expected.ClinicalStudy.StudyPhase, actual_result.ClinicalStudy.StudyPhase);
            Assert.NotNull(actual_result.ClinicalStudy.Objectives);
            Assert.NotNull(actual_result.ClinicalStudy.StudyIdentifiers);
            Assert.NotNull(actual_result.ClinicalStudy.StudyIndications);
            Assert.NotNull(actual_result.ClinicalStudy.StudyDesigns);
            Assert.AreEqual(expected.ClinicalStudy.StudyTitle, actual_result.ClinicalStudy.StudyTitle);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var method = ClinicalStudyService.GetAllElements("2", 1, "New", user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
        }
        #endregion

        #region GET Sections UnitTesting
        [Test]
        public void GetSections_UnitTest_SuccessResponse()
        {
            string[] sections = { "study_objectives", "study_indications", "study_design" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetSections("1", 1, "1.0Draft", sections, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.Objectives[0].Description, actual_result.Objectives[0].Description);
            Assert.AreEqual(expected.StudyIndications[0].Description, actual_result.StudyIndications[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].Description, actual_result.StudyDesigns[0].PlannedWorkflows[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].InvestigationalInterventions[0].Description, actual_result.StudyDesigns[0].InvestigationalInterventions[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyArm.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyArm.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyPopulations[0].Description, actual_result.StudyDesigns[0].StudyPopulations[0].Description);
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);

            sections = Array.Empty<string>();
            method = ClinicalStudyService.GetSections("1", 1, "1.0Draft", sections, user);
            method.Wait();
            result = method.Result;

            //Expected
            expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
        }

        [Test]
        public void GetSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetSections("2", 1, null, sections, user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), "New"))
                   .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            method = ClinicalStudyService1.GetSections("1", 1, "New", sections, user);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET Design Sections UnitTesting
        [Test]
        public void GetDesignSections_UnitTest_SuccessResponse()
        {
            string[] sections = { "study_planned_workflow", "study_target_populations", "study_cells" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].Description, actual_result.StudyDesigns[0].PlannedWorkflows[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping, actual_result.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping);
            Assert.AreEqual(expected.StudyDesigns[0].StudyPopulations[0].Description, actual_result.StudyDesigns[0].StudyPopulations[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyArm.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyArm.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyEpoch.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyEpoch.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyElements[0].Description, actual_result.StudyDesigns[0].StudyCells[0].StudyElements[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);

            sections = new string[] { "study_investigational_interventions", "study_cells" };
            method = ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections, user);
            method.Wait();
            result = method.Result;

            //Expected
            expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyDesigns[0].InvestigationalInterventions[0].Description, actual_result.StudyDesigns[0].InvestigationalInterventions[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyArm.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyArm.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyEpoch.Description, actual_result.StudyDesigns[0].StudyCells[0].StudyEpoch.Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyCells[0].StudyElements[0].Description, actual_result.StudyDesigns[0].StudyCells[0].StudyElements[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);

            sections = new string[] { "study_target_populations", "study_planned_workflow" };
            method = ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections, user);
            method.Wait();
            result = method.Result;

            //Expected
            expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyDesigns[0].StudyPopulations[0].Description, actual_result.StudyDesigns[0].StudyPopulations[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].Description, actual_result.StudyDesigns[0].PlannedWorkflows[0].Description);
            Assert.AreEqual(expected.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping, actual_result.StudyDesigns[0].PlannedWorkflows[0].StartPoint.SubjectStatusGrouping);
            Assert.AreEqual(expected.StudyDesigns[0].StudyDesignId, actual_result.StudyDesigns[0].StudyDesignId);
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
        }

        [Test]
        public void GetDesignSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetStudyDesignSections("2", "1", 1, null, sections, user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
        }
        #endregion

        #region GET AuditTrail UnitTesting
        [Test]
        public void GetAuditTrail_UnitTest_SuccessResponse()
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().StudyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().StudyId, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetAuditDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyAuditDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.AreEqual(expected.StudyId, actual_result.StudyId);
            Assert.AreEqual(expected.AuditTrail[0].StudyVersion, actual_result.AuditTrail[0].StudyVersion);
            Assert.AreEqual(expected.AuditTrail[0].EntryDateTime, actual_result.AuditTrail[0].EntryDateTime);
            Assert.AreEqual(expected.AuditTrail[1].StudyVersion, actual_result.AuditTrail[1].StudyVersion);
            Assert.AreEqual(expected.AuditTrail[1].EntryDateTime, actual_result.AuditTrail[1].EntryDateTime);
        }

        [Test]
        public void GetAuditTrail_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(DateTime.Now, DateTime.Now, "1"))
                   .Returns(Task.FromResult(GetListDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var method = ClinicalStudyService.GetAuditTrail(DateTime.Now, DateTime.Now.AddDays(1), "1", user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
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
            StudyHistoryEntity studyHistory = new();
            studyHistory.StudyId = studyList[0].ClinicalStudy.StudyId;
            studyHistory.StudyType = studyList[0].ClinicalStudy.StudyType;
            studyHistory.StudyTitle = studyList[0].ClinicalStudy.StudyTitle;
            studyHistory.StudyVersion = studyList[0].AuditTrail.StudyVersion;
            studyHistories.Add(studyHistory);
            studyHistory.StudyId = studyList[1].ClinicalStudy.StudyId;
            studyHistory.StudyTitle = studyList[1].ClinicalStudy.StudyTitle;
            studyHistory.StudyVersion = studyList[1].AuditTrail.StudyVersion;
            studyHistories.Add(studyHistory);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, user))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);


            var method = ClinicalStudyService.GetAllStudyId(fromDate, toDate, null, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AllStudyResponse>(
                JsonConvert.SerializeObject(result));

            var studyElements = JsonConvert.DeserializeObject<List<AllStudyElementsResponse>>(
                JsonConvert.SerializeObject(actual_result.Study));

            //Assert                    

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
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(fromDate, toDate, null, user))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, user);
            method.Wait();

            Assert.IsNull(method.Result);
        }
        #endregion 

        #endregion

        #region POST Method Unit Testing
        #region POST All Elements UnitTesting
        [Test]
        public void PostAllElments_UnitTest_SuccessResponse()
        {

            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetPostDataFromStaticJson().ClinicalStudy.StudyId));
            _mockClinicalStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetPostDataFromStaticJson().ClinicalStudy.StudyId));
            StudyEntity studyEntity1 = GetPostDataFromStaticJson(); studyEntity1.AuditTrail.StudyVersion = 1; studyEntity1.AuditTrail.UsdmVersion = "mvp";
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.AuditTrail));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var studyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));
            studyDTO.ClinicalStudy.StudyId = null;
            user.UserRole = Constants.Roles.Org_Admin;

            var method = ClinicalStudyService.PostAllElements(studyDTO, "A", user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(null as AuditTrailEntity));
            var newStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));

            method = ClinicalStudyService.PostAllElements(newStudyDTO, null, user);
            method.Wait();

            //Expected
            var expected = Constants.ErrorMessages.NotValidStudyId;

            //Actual            
            var newActual_result = method.Result;

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(expected, newActual_result);


            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(GetDataFromStaticJson()));
            newStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));
            newStudyDTO.ClinicalStudy.StudyId = GetDataFromStaticJson().ClinicalStudy.StudyId;
            newStudyDTO.ClinicalStudy.StudyTitle = "A";

            method = ClinicalStudyService.PostAllElements(newStudyDTO, null, user);
            method.Wait();

            //Actual            
            newActual_result = method.Result;

            //Assert          
            Assert.IsNotNull(newActual_result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(GetDataFromStaticJson()));
            newStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetDataFromStaticJson()));
            newStudyDTO.ClinicalStudy.StudyId = GetDataFromStaticJson().ClinicalStudy.StudyId;

            method = ClinicalStudyService.PostAllElements(newStudyDTO, null, user);
            method.Wait();

            //Actual            
            newActual_result = method.Result;

            //Assert          
            Assert.IsNotNull(newActual_result);
        }
        #endregion

        #region Search Study Unit Testing
        [Test]
        public void SearchStudy_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParameters>(), user))
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


            var method = ClinicalStudyService.SearchStudy(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataForSearchFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GetStudyDTO>>(
               JsonConvert.SerializeObject((result)));

            //Assert           

            Assert.AreEqual(expected[0].ClinicalStudy.StudyIndications[0].Description, actual_result[0].ClinicalStudy.StudyIndications[0].Description);
            Assert.AreEqual(expected[1].ClinicalStudy.StudyIndications[0].Description, actual_result[1].ClinicalStudy.StudyIndications[0].Description);
        }

        [Test]
        public void SearchStudy_UnitTest_FailureResponse()
        {
            SearchParameters searchParameters = new()
            {
                BriefTitle = "Umbrella",
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now
            };
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(searchParameters, user))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));


            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

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

            var method = ClinicalStudyService.SearchStudy(searchParametersChanged, user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
        }

        [Test]
        public void SearchTitle_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), user))
                    .Returns(Task.FromResult(GetListForSearchTitleDataFromStaticJson()));
            SearchTitleParametersDTO searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
                GroupByStudyId = true
            };
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);


            var method = ClinicalStudyService.SearchTitle(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListForSearchTitleDTODataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleDTO>>(
               JsonConvert.SerializeObject((result)));

            //Assert           
            Assert.AreEqual(expected[0].ClinicalStudy.StudyTitle, actual_result[0].ClinicalStudy.StudyTitle);

            user.UserRole = Constants.Roles.App_User;
            method = ClinicalStudyService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;

            Assert.IsEmpty(result);

            _mockClinicalStudyRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParameters>(), user))
                    .Throws(new Exception("Error"));
            user.UserRole = Constants.Roles.Org_Admin;
            method = ClinicalStudyService.SearchTitle(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion
        #endregion


        #region User Group Mapping
        [Test]
        public void CheckAccessForAStudy_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetPostDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = GetPostDataFromStaticJson();

            Assert.AreEqual(expected.ClinicalStudy.StudyId, method.Result.ClinicalStudy.StudyId);

            study.ClinicalStudy.StudyId = "studyId1";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();


            Assert.AreEqual("studyId1", method.Result.ClinicalStudy.StudyId);

            study.ClinicalStudy.StudyId = "studyId5";
            study.ClinicalStudy.StudyType = "EXPANDED_ACCESS";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.StudyId);

            Config.IsGroupFilterEnabled = false;
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.ClinicalStudy.StudyId);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.IsGroupFilterEnabled = true;
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            ClinicalStudyService ClinicalStudyService1 = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            method = ClinicalStudyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);
        }
        [Test]
        public void CheckAccessForAudit_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            var study = GetListDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            var expected = GetListDataFromStaticJson();

            Assert.IsTrue(expected.Any(x => x.ClinicalStudy.StudyId == method.Result[0].ClinicalStudy.StudyId));

            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(noGroups));
            method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            Assert.IsNull(method.Result);

            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";

            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            Assert.IsTrue(expected.Any(x => x.ClinicalStudy.StudyId == method.Result[0].ClinicalStudy.StudyId));


            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var groups = GetUserDataFromStaticJson().SDRGroups;
            groups.ForEach(x => x.GroupFilter.Where(x => x.GroupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(x => x.GroupFilterValues.ForEach(x => x.GroupFilterValueId = "ef70fb0f-0504-4f30-8173-34491d8326f1")));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            Assert.IsTrue(expected.Any(x => x.ClinicalStudy.StudyId == method.Result[0].ClinicalStudy.StudyId));

        }
        [Test]
        public void CheckPermissionForAUser_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetPostDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.Org_Admin;
            method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();

            Assert.IsTrue(method.Result);

            user.UserRole = Constants.Roles.App_User;
            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            method = ClinicalStudyService.CheckPermissionForAUser(user);
            method.Wait();
            Assert.IsFalse(method.Result);

        }
        #endregion
        #endregion
    }
}
