using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TransCelerate.SDR.WebApi.Mappers;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Entities;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.Core.DTO.Token;

namespace TransCelerate.SDR.UnitTesting
{
    public class ClinicalStudyServiceUnitTesting
    {
        #region Variables        
        private IMapper _mockMapper;
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IClinicalStudyRepository> _mockClinicalStudyRepository = new Mock<IClinicalStudyRepository>(MockBehavior.Loose);           
        
        StudyEntity study = new StudyEntity();
        List<StudyEntity> studyList = new List<StudyEntity>();
        GetStudyAuditDTO auditTrail = new GetStudyAuditDTO();
        List<GetStudyDTO> studyDTO = new List<GetStudyDTO>();
        GetStudySectionsDTO studySectionsDTO = new GetStudySectionsDTO();
        #endregion

        #region Setup
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public StudyEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            study.auditTrail.UsdmVersion = "mvp";
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
        public List<SearchResponse> GetListForSearchDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");           
            return JsonConvert.DeserializeObject<List<SearchResponse>>(jsonData); 
        }
        public List<SearchTitleEntity> GetListForSearchTitleDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/GetSearchStudyData.json");
            return JsonConvert.DeserializeObject<List<SearchTitleEntity>>(jsonData); 
        }
        public List<SearchTitleDTO> GetListForSearchTitleDTODataFromStaticJson()
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
                .Returns(Task.FromResult(GetDataFromStaticJson().auditTrail));
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

            var method = ClinicalStudyService.GetAllElements("1",1, "1.0Draft", user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.AreEqual(expected.clinicalStudy.studyId, actual_result.clinicalStudy.studyId);
            Assert.AreEqual(expected.clinicalStudy.studyPhase, actual_result.clinicalStudy.studyPhase);
            Assert.NotNull(actual_result.clinicalStudy.objectives);
            Assert.NotNull(actual_result.clinicalStudy.studyIdentifiers);
            Assert.NotNull(actual_result.clinicalStudy.studyIndications);
            Assert.NotNull(actual_result.clinicalStudy.studyDesigns);
            Assert.AreEqual(expected.clinicalStudy.studyTitle, actual_result.clinicalStudy.studyTitle);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse()
        {           
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var method = ClinicalStudyService.GetAllElements("2",1,"New", user);
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
            string[] sections = {"study_objectives", "study_indications", "study_design" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);            

            var method = ClinicalStudyService.GetSections("1", 1, "1.0Draft",sections, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.objectives[0].description, actual_result.objectives[0].description);            
            Assert.AreEqual(expected.studyIndications[0].description, actual_result.studyIndications[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);                        
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].description);                        
            Assert.AreEqual(expected.studyDesigns[0].investigationalInterventions[0].description, actual_result.studyDesigns[0].investigationalInterventions[0].description);                        
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);                        
            Assert.AreEqual(expected.studyDesigns[0].studyPopulations[0].description, actual_result.studyDesigns[0].studyPopulations[0].description);                        
            Assert.AreEqual(expected.studyId, actual_result.studyId);                                    

            sections = new string[] { };
            method = ClinicalStudyService.GetSections("1", 1, "1.0Draft", sections, user);
            method.Wait();
            result = method.Result;

            //Expected
            expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.studyId, actual_result.studyId);            
        }

        [Test]
        public void GetSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetSections("2", 1, null, sections, user);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);

            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>(), "New"))
                   .Throws(new Exception("Error"));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);            

            method = ClinicalStudyService1.GetSections("1", 1, "New", sections, user);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET Design Sections UnitTesting
        [Test]
        public void GetDesignSections_UnitTest_SuccessResponse()
        {
            string[] sections = { "study_planned_workflow", "study_target_populations", "study_cells"};
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetStudyDesignSections("1", "02ab88b2-b3bd-427d-bb1a-6f9966d7e6dd", 1, "1.0Draft", sections, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].description);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping, actual_result.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping);            
            Assert.AreEqual(expected.studyDesigns[0].studyPopulations[0].description, actual_result.studyDesigns[0].studyPopulations[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyEpoch.description, actual_result.studyDesigns[0].studyCells[0].studyEpoch.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyElements[0].description, actual_result.studyDesigns[0].studyCells[0].studyElements[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);            

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
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyDesigns[0].investigationalInterventions[0].description, actual_result.studyDesigns[0].investigationalInterventions[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyEpoch.description, actual_result.studyDesigns[0].studyCells[0].studyEpoch.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyElements[0].description, actual_result.studyDesigns[0].studyCells[0].studyElements[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);    

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
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyDesigns[0].studyPopulations[0].description, actual_result.studyDesigns[0].studyPopulations[0].description);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].description);
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping, actual_result.studyDesigns[0].plannedWorkflows[0].startPoint.subjectStatusGrouping);            
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);            
        }

        [Test]
        public void GetDesignSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, null))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetStudyDesignSections("2","1", 1, null, sections, user);
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
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(fromDate,toDate, GetAuditDataFromStaticJson().studyId))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));           
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().studyId, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetAuditDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudyAuditDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.auditTrail[0].studyVersion, actual_result.auditTrail[0].studyVersion);
            Assert.AreEqual(expected.auditTrail[0].entryDateTime, actual_result.auditTrail[0].entryDateTime);
            Assert.AreEqual(expected.auditTrail[1].studyVersion, actual_result.auditTrail[1].studyVersion);
            Assert.AreEqual(expected.auditTrail[1].entryDateTime, actual_result.auditTrail[1].entryDateTime);
        }

        [Test]
        public void GetAuditTrail_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetAuditTrail(DateTime.Now, DateTime.Now, "1"))
                   .Returns(Task.FromResult(GetListDataFromStaticJson()));            
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
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
            studyHistory.studyType = studyList[0].clinicalStudy.studyType;
            studyHistory.studyTitle = studyList[0].clinicalStudy.studyTitle;
            studyHistory.studyVersion = studyList[0].auditTrail.studyVersion;
            studyHistories.Add(studyHistory);
            studyHistory.studyId = studyList[1].clinicalStudy.studyId;
            studyHistory.studyTitle = studyList[1].clinicalStudy.studyTitle;
            studyHistory.studyVersion = studyList[1].auditTrail.studyVersion;
            studyHistories.Add(studyHistory);
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(),null, user))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);           


            var method = ClinicalStudyService.GetAllStudyId(fromDate, toDate, null, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AllStudyResponse>(
                JsonConvert.SerializeObject(result));

            var studyElements = JsonConvert.DeserializeObject<List<AllStudyElementsResponse>>(
                JsonConvert.SerializeObject(actual_result.study));

            //Assert                    

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
            _mockClinicalStudyRepository.Setup(x => x.GetAllStudyId(fromDate, toDate, null, user))
                    .Returns(Task.FromResult(studyHistories));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);            

            var method = ClinicalStudyService.GetAllStudyId(It.IsAny<DateTime>(), It.IsAny<DateTime>(),null, user);
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
                    .Returns(Task.FromResult(GetPostDataFromStaticJson().clinicalStudy.studyId));
            _mockClinicalStudyRepository.Setup(x => x.UpdateStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetPostDataFromStaticJson().clinicalStudy.studyId));
            StudyEntity studyEntity1 = GetPostDataFromStaticJson(); studyEntity1.auditTrail.studyVersion = 1; studyEntity1.auditTrail.UsdmVersion = "mvp";
            _mockClinicalStudyRepository.Setup(x => x.GetUsdmVersionAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(studyEntity1.auditTrail));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var studyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));
            studyDTO.clinicalStudy.studyId = null;
            user.UserRole = Constants.Roles.Org_Admin;

            var method = ClinicalStudyService.PostAllElements(studyDTO,"A", user);
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

           
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(),It.IsAny<int>()))
                   .Returns(Task.FromResult(GetDataFromStaticJson()));
            newStudyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));
            newStudyDTO.clinicalStudy.studyId = GetDataFromStaticJson().clinicalStudy.studyId;
            newStudyDTO.clinicalStudy.studyTitle = "A";

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
            newStudyDTO.clinicalStudy.studyId = GetDataFromStaticJson().clinicalStudy.studyId;            

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
            SearchParametersDTO searchParameters = new SearchParametersDTO
            {            
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


            var method = ClinicalStudyService.SearchStudy(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataForSearchFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GetStudyDTO>>(
               JsonConvert.SerializeObject((result)));

            //Assert           
                        
            Assert.AreEqual(expected[0].clinicalStudy.studyIndications[0].description, actual_result[0].clinicalStudy.studyIndications[0].description);                                          
            Assert.AreEqual(expected[1].clinicalStudy.studyIndications[0].description, actual_result[1].clinicalStudy.studyIndications[0].description);            
        }

        [Test]
        public void SearchStudy_UnitTest_FailureResponse()
        {
            SearchParameters searchParameters = new SearchParameters
            {
                briefTitle = "Umbrella",
                indication = "Bile",
                interventionModel = "CROSS_OVER",
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,
                phase = "PHASE_1_TRAIL",
                studyId = "100",
                fromDate = DateTime.Now.AddDays(-5),
                toDate = DateTime.Now
            };       
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(searchParameters, user))
                    .Returns(Task.FromResult(GetListForSearchDataFromStaticJson()));
           

            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);           
           
            SearchParametersDTO searchParametersChanged = new SearchParametersDTO
            {       
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
            SearchTitleParametersDTO searchParameters = new SearchTitleParametersDTO
            {               
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,             
                fromDate = DateTime.Now.AddDays(-5).ToString(),
                toDate = DateTime.Now.ToString(),
                groupByStudyId = true
            };
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);


            var method = ClinicalStudyService.SearchTitle(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListForSearchTitleDTODataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleDTO>>(
               JsonConvert.SerializeObject((result)));

            //Assert           
            Assert.AreEqual(expected[0].clinicalStudy.studyTitle, actual_result[0].clinicalStudy.studyTitle);

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
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetPostDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            var expected = GetPostDataFromStaticJson();

            Assert.AreEqual(expected.clinicalStudy.studyId, method.Result.clinicalStudy.studyId);

            study.clinicalStudy.studyId = "studyId1";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();
            

            Assert.AreEqual("studyId1", method.Result.clinicalStudy.studyId);

            study.clinicalStudy.studyId = "studyId5";
            study.clinicalStudy.studyType = "EXPANDED_ACCESS";
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.clinicalStudy.studyId);            

            Config.isGroupFilterEnabled = false;
            method = ClinicalStudyService.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.AreEqual("studyId5", method.Result.clinicalStudy.studyId);

            var noGroups = GetUserDataFromStaticJson().SDRGroups;
            noGroups.Clear();
            Config.isGroupFilterEnabled = true;
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(noGroups));
            ClinicalStudyService ClinicalStudyService1 = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            method = ClinicalStudyService1.CheckAccessForAStudy(study, user);
            method.Wait();

            Assert.IsNull(method.Result);
        }
        [Test]
        public void CheckAccessForAudit_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            var study = GetListDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            var expected = GetListDataFromStaticJson();

            Assert.IsTrue(expected.Any(x => x.clinicalStudy.studyId == method.Result[0].clinicalStudy.studyId));

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

            Assert.IsTrue(expected.Any(x => x.clinicalStudy.studyId == method.Result[0].clinicalStudy.studyId));
            

            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var groups = GetUserDataFromStaticJson().SDRGroups;
            groups.ForEach(x => x.groupFilter.Where(x => x.groupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(x => x.groupFilterValues.ForEach(x=>x.groupFilterValueId= "ef70fb0f-0504-4f30-8173-34491d8326f1")));
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            method = ClinicalStudyService.CheckAccessForAuditTrail(study, user);
            method.Wait();

            Assert.IsTrue(expected.Any(x => x.clinicalStudy.studyId == method.Result[0].clinicalStudy.studyId));

        }
        [Test]
        public void CheckPermissionForAUser_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            var study = GetPostDataFromStaticJson();
            _mockClinicalStudyRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

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
