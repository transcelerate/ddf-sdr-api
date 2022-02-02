using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
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
        public StudyEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\GetStudyData.json");
            study = JsonConvert.DeserializeObject<StudyEntity>(jsonData);
            return study;
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
        #region Depricated EndPoints UnitTesting
        //#region InterventionModel UnitTesting
        //[Test]
        //public void InterventionModel_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.InterventionModel("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = method.Result;

        //    //Assert                      

        //    Assert.AreEqual(expected.clinicalStudy.interventionModel, actual_result.interventionModel);
        //}

        //[Test]
        //public void InterventionModel_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.InterventionModel("2", "1", "New");
        //    method.Wait();

        //    //Actual

        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region InvestigationIntervention UnitTesting
        //[Test]
        //public void Investigationalinterventions_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.Investigationalinterventions("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = JsonConvert.DeserializeObject<InvestigationalInterventionResponse>(
        //        JsonConvert.SerializeObject(method.Result));

        //    //Assert          
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].description, actual_result.investigationalIntervention[0].description);
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].investigationalInterventionId, actual_result.investigationalIntervention[0].investigationalInterventionId);
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].coding[0].code, actual_result.investigationalIntervention[0].coding[0].code);
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].coding[0].codeSystem, actual_result.investigationalIntervention[0].coding[0].codeSystem);
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].coding[0].codeSystemVersion, actual_result.investigationalIntervention[0].coding[0].codeSystemVersion);
        //    Assert.AreEqual(expected.clinicalStudy.investigationalIntervention[0].coding[0].decode, actual_result.investigationalIntervention[0].coding[0].decode);
        //}

        //[Test]
        //public void Investigationalinterventions_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.Investigationalinterventions("2", "1", "New");
        //    method.Wait();

        //    //Actual

        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyIdentifier UnitTesting
        //[Test]
        //public void StudyIdentifiers_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyIdentifiers("1", "1", "New");
        //    method.Wait();


        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = JsonConvert.DeserializeObject<StudyIdentifierResponse>(
        //        JsonConvert.SerializeObject(method.Result));

        //    //Assert                  

        //    Assert.AreEqual(expected.clinicalStudy.studyIdentifier[0].studyIdentifierId, actual_result.studyIdentifier[0].id);
        //    Assert.AreEqual(expected.clinicalStudy.studyIdentifier[0].idType.code, actual_result.studyIdentifier[0].idType.code);
        //    Assert.AreEqual(expected.clinicalStudy.studyIdentifier[0].idType.name, actual_result.studyIdentifier[0].idType.name);
        //}

        //[Test]
        //public void StudyIdentifiers_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyIdentifiers("2", "1", "New");
        //    method.Wait();

        //    //Actual

        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyPhase UnitTesting
        //[Test]
        //public void StudyPhase_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyPhase("1", "1", "New");
        //    method.Wait();


        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = method.Result;

        //    //Assert                    
        //    Assert.AreEqual(expected.clinicalStudy.studyPhase.ToString(), actual_result.studyPhase);
        //}

        //[Test]
        //public void StudyPhase_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyPhase("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyProtocol UnitTesting
        //[Test]
        //public void StudyProtocol_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyProtocol("1", "1", "New");
        //    method.Wait();


        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = JsonConvert.DeserializeObject<StudyProtocolResponse>(
        //        JsonConvert.SerializeObject(method.Result));

        //    //Assert          
        //    Assert.AreEqual(expected.clinicalStudy.studyProtocol.protocolId, actual_result.studyProtocol.protocolId);
        //    Assert.AreEqual(expected.clinicalStudy.studyProtocol.briefTitle, actual_result.studyProtocol.briefTitle);
        //    Assert.AreEqual(expected.clinicalStudy.studyProtocol.officialTitle, actual_result.studyProtocol.officialTitle);
        //    Assert.AreEqual(expected.clinicalStudy.studyProtocol.version, actual_result.studyProtocol.version);
        //}

        //[Test]
        //public void StudyProtocol_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyProtocol("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}

        //#endregion

        //#region StudyObjectives UnitTesting
        //[Test]
        //public void StudyObjectives_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyObjectives("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = JsonConvert.DeserializeObject<StudyObjectivesResponse>(
        //        JsonConvert.SerializeObject(method.Result));

        //    //Assert                    
        //    Assert.AreEqual(expected.clinicalStudy.studyObjective[0].objectiveLevel, actual_result.studyObjective[0].objectiveLevel);
        //    Assert.AreEqual(expected.clinicalStudy.studyObjective[0].objective[0].description, actual_result.studyObjective[0].objective[0].description);
        //}

        //[Test]
        //public void StudyObjectives_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyObjectives("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyTargetPopulation UnitTesting
        //[Test]
        //public void StudyTargetPopulation_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyTargetPopulation("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = method.Result as StudyTargetPopulationResponse;

        //    //Assert                    
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetPopulation[0].studyTargetPopulationId, actual_result.studyTargetPopulation[0].id);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetPopulation[0].description, actual_result.studyTargetPopulation[0].description);
        //}

        //[Test]
        //public void StudyTargetPopulation_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyTargetPopulation("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyTitle UnitTesting
        //[Test]
        //public void StudyTitle_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyTitle("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.AreEqual(expected.clinicalStudy.studyTitle, actual_result.studyTitle);
        //}

        //[Test]
        //public void StudyTitle_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyTitle("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyType UnitTesting
        //[Test]
        //public void StudyType_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyType("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = method.Result;

        //    //Assert                     
        //    Assert.AreEqual(expected.clinicalStudy.studyType.ToString(), actual_result.studyType);
        //}

        //[Test]
        //public void StudyType_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyType("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion

        //#region StudyIndication UnitTesting
        //[Test]
        //public void StudyIndication_UnitTest_SuccessResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);

        //    var method = ClinicalStudyService.StudyIndication("1", "1", "New");
        //    method.Wait();

        //    //Expected
        //    var expected = GetDataFromStaticJson();

        //    //Actual            
        //    var actual_result = JsonConvert.DeserializeObject<StudyIndicationResponse>(
        //        JsonConvert.SerializeObject(method.Result));

        //    //Assert                
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.studyTargetIndicationId, actual_result.studyTargetIndication.id);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.description, actual_result.studyTargetIndication.description);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.coding[0].code, actual_result.studyTargetIndication.coding[0].code);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.coding[0].codeSystem, actual_result.studyTargetIndication.coding[0].codeSystem);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.coding[0].codeSystemVersion, actual_result.studyTargetIndication.coding[0].codeSystemVersion);
        //    Assert.AreEqual(expected.clinicalStudy.studyTargetIndication.coding[0].decode, actual_result.studyTargetIndication.coding[0].decode);
        //}

        //[Test]
        //public void StudyIndication_UnitTest_FailureResponse()
        //{
        //    var mockLogger = Mock.Of<ILogger<ClinicalStudyService>>();
        //    ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockGetClinicalStudyRepository.Object, _mockMapper, mockLogger);
        //    var method = ClinicalStudyService.StudyIndication("2", "1", "New");
        //    method.Wait();

        //    //Actual
        //    var actual_result = method.Result;

        //    //Assert          
        //    Assert.IsNull(actual_result);
        //}
        //#endregion 
        #endregion

        #region GET All Elements UnitTesting
        [Test]
        public void GetAllElments_UnitTest_SuccessResponse()
        {      
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);

            var method = ClinicalStudyService.GetAllElements("1",1, "1.0Draft");
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
            Assert.AreEqual(expected.clinicalStudy.currentSections[0].investigationalInterventions[0].investigationalInterventionId, actual_result.clinicalStudy.investigationalInterventions[0].id);
            Assert.AreEqual(expected.clinicalStudy.studyTitle, actual_result.clinicalStudy.studyTitle);
        }

        [Test]
        public void GetAllElments_UnitTest_FailureResponse()
        {           
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var method = ClinicalStudyService.GetAllElements("2",1,"New");
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
            string[] sections = {"study_objectives", "study_investigational_interventions", "study_protocol", "study_indications", "study_design" };
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);            

            var method = ClinicalStudyService.GetSections("1", 1, "1.0Draft",sections);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.AreEqual(expected.investigationalInterventions[0].interventionType, actual_result.investigationalInterventions[0].interventionType);     
            Assert.AreEqual(expected.objectives[0].description, actual_result.objectives[0].description);
            Assert.AreEqual(expected.studyProtocol.protocolId, actual_result.studyProtocol.protocolId);            
            Assert.AreEqual(expected.studyIndications[0].description, actual_result.studyIndications[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);                        
            Assert.AreEqual(expected.studyId, actual_result.studyId);                        
            Assert.AreEqual(expected.studyVersion, actual_result.studyVersion);

            sections = new string[] { };
            method = ClinicalStudyService.GetSections("1", 1, "1.0Draft", sections);
            method.Wait();
            result = method.Result;

            //Expected
            expected = GetStudySectionsDataFromStaticJson();

            //Actual            
            actual_result = JsonConvert.DeserializeObject<GetStudySectionsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert                      
            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.studyVersion, actual_result.studyVersion);
        }

        [Test]
        public void GetSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetSections("2", 1, "1.0Draft", sections);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
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

            var method = ClinicalStudyService.GetStudyDesignSections("1", "ad4393db-e076-4ca0-a02a-fdb6f930bf06", 1, "1.0Draft", sections);
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
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].transitions[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].transitions[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyPopulations[0].description, actual_result.studyDesigns[0].studyPopulations[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyEpoch.description, actual_result.studyDesigns[0].studyCells[0].studyEpoch.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyElements[0].description, actual_result.studyDesigns[0].studyCells[0].studyElements[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.studyVersion, actual_result.studyVersion);

            sections = new string[] { "study_target_populations", "study_cells" };
            method = ClinicalStudyService.GetStudyDesignSections("1", "ad4393db-e076-4ca0-a02a-fdb6f930bf06", 1, "1.0Draft", sections);
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
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyArm.description, actual_result.studyDesigns[0].studyCells[0].studyArm.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyEpoch.description, actual_result.studyDesigns[0].studyCells[0].studyEpoch.description);
            Assert.AreEqual(expected.studyDesigns[0].studyCells[0].studyElements[0].description, actual_result.studyDesigns[0].studyCells[0].studyElements[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.studyVersion, actual_result.studyVersion);

            sections = new string[] { "study_target_populations", "study_planned_workflow" };
            method = ClinicalStudyService.GetStudyDesignSections("1", "ad4393db-e076-4ca0-a02a-fdb6f930bf06", 1, "1.0Draft", sections);
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
            Assert.AreEqual(expected.studyDesigns[0].plannedWorkflows[0].transitions[0].description, actual_result.studyDesigns[0].plannedWorkflows[0].transitions[0].description);
            Assert.AreEqual(expected.studyDesigns[0].studyDesignId, actual_result.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected.studyId, actual_result.studyId);
            Assert.AreEqual(expected.studyVersion, actual_result.studyVersion);
        }

        [Test]
        public void GetDesignSections_UnitTest_FailureResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.GetStudyItemsAsync("1", 1, "1.0Draft"))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            string[] sections = { "study_cells", "study_objectives", "study_investigational_interventions", "study_planned_workflow", "study_target_populations", "study_indications", "study_design" };

            var method = ClinicalStudyService.GetStudyDesignSections("2","1", 1, "1.0Draft", sections);
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

            var method = ClinicalStudyService.GetAuditTrail(fromDate, toDate, GetAuditDataFromStaticJson().studyId);
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
            var method = ClinicalStudyService.GetAuditTrail(DateTime.Now, DateTime.Now.AddDays(1), "1");
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
        }
        #endregion      
        #endregion

        #region POST Method Unit Testing
        #region POST All Elements UnitTesting
        [Test]
        public void PostAllElments_UnitTest_SuccessResponse()
        {
            _mockClinicalStudyRepository.Setup(x => x.PostStudyItemsAsync(It.IsAny<StudyEntity>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson().clinicalStudy.studyId));
            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);
            var studyDTO = JsonConvert.DeserializeObject<PostStudyDTO>(
                JsonConvert.SerializeObject(GetPostDataFromStaticJson()));

            var method = ClinicalStudyService.PostAllElements(studyDTO,null,null);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson().clinicalStudy.studyId;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostStudyResponseDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(expected, actual_result.studyId);
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


            var method = ClinicalStudyService.SearchStudy(searchParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataForSearchFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GetStudyDTO>>(
               JsonConvert.SerializeObject((result)));

            //Assert           

            Assert.AreEqual(expected[0].clinicalStudy.investigationalInterventions[0].interventionType, actual_result[0].clinicalStudy.investigationalInterventions[0].interventionType);
            Assert.AreEqual(expected[0].clinicalStudy.objectives[0].description, actual_result[0].clinicalStudy.objectives[0].description);
            Assert.AreEqual(expected[0].clinicalStudy.studyProtocol.protocolId, actual_result[0].clinicalStudy.studyProtocol.protocolId);
            Assert.AreEqual(expected[0].clinicalStudy.studyIndications[0].description, actual_result[0].clinicalStudy.studyIndications[0].description);
            Assert.AreEqual(expected[0].clinicalStudy.studyDesigns[0].studyDesignId, actual_result[0].clinicalStudy.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected[0].clinicalStudy.studyId, actual_result[0].clinicalStudy.studyId);
            Assert.AreEqual(expected[0].clinicalStudy.studyVersion, actual_result[0].clinicalStudy.studyVersion);
            Assert.AreEqual(expected[1].clinicalStudy.investigationalInterventions[0].interventionType, actual_result[1].clinicalStudy.investigationalInterventions[0].interventionType);
            Assert.AreEqual(expected[1].clinicalStudy.objectives[0].description, actual_result[1].clinicalStudy.objectives[0].description);
            Assert.AreEqual(expected[1].clinicalStudy.studyProtocol.protocolId, actual_result[1].clinicalStudy.studyProtocol.protocolId);
            Assert.AreEqual(expected[1].clinicalStudy.studyIndications[0].description, actual_result[1].clinicalStudy.studyIndications[0].description);
            Assert.AreEqual(expected[1].clinicalStudy.studyDesigns[0].studyDesignId, actual_result[1].clinicalStudy.studyDesigns[0].studyDesignId);
            Assert.AreEqual(expected[1].clinicalStudy.studyId, actual_result[1].clinicalStudy.studyId);
            Assert.AreEqual(expected[1].clinicalStudy.studyVersion, actual_result[1].clinicalStudy.studyVersion);
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
            _mockClinicalStudyRepository.Setup(x => x.SearchStudy(searchParameters))
                    .Returns(Task.FromResult(GetListDataFromStaticJson()));
           

            ClinicalStudyService ClinicalStudyService = new ClinicalStudyService(_mockClinicalStudyRepository.Object, _mockMapper, _mockLogger);           
           
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

            var method = ClinicalStudyService.SearchStudy(searchParametersChanged);
            method.Wait();         

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);
        }
        #endregion
        #endregion

        #endregion
    }
}
