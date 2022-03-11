using System;
using System.Linq;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Utilities;
using Microsoft.Extensions.Configuration;
using TransCelerate.SDR.Core.Utilities.Common;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.AppSettings;
using NUnit.Framework;
using System.Text;
using Moq;
using TransCelerate.SDR.Core.Utilities.Helpers;
using System.IO;
using Newtonsoft.Json;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.RuleEngine;
using Microsoft.Extensions.DependencyInjection;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.WebApi;
using TransCelerate.SDR.WebApi.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.UnitTesting
{
    public class CommonClassesUnitTesting
    {
        private Mock<ILoggerFactory> _mockLogger = new Mock<ILoggerFactory>();
        private Mock<ILogger> _mockSDRLogger = new Mock<ILogger>();
        private Mock<ILogger> _mockErrorSDRLogger = new Mock <ILogger>(MockBehavior.Strict);
        private Mock<IConfiguration> _mockConfig = new Mock<IConfiguration>();
        //private IConfiguration _mockConfiguration = Mock.Of<IConfiguration>();
        private IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        //private IApplicationBuilder app = Mock.Of<IApplicationBuilder>();
        //private IWebHostEnvironment env = Mock.Of<IWebHostEnvironment>();

        #region Setup
        public StudyEntity GetPostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");            
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData); 
        }
        public PostStudyDTO PostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            return JsonConvert.DeserializeObject<PostStudyDTO>(jsonData);            
        }
        #endregion

        #region LogHelper UnitTesting
        [Test]
        public void LogHelperInformation_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogInformation("This is Information");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogInformation(""));
        }
        [Test]
        public void LogHelperWarning_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);            
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogWarning("This is Warning");
            
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);
            
            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);           
            Assert.Throws<Moq.MockException>(() => logHelper2.LogWarning(""));
        }
        [Test]
        public void LogHelperError_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogError("This is Error");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogError(""));
        }
        [Test]
        public void LogHelperCritical_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogCriitical("This is Critical");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogCriitical(""));
        }
        [Test]
        public void LogHelperDebug_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogDebug("This is Debug");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogDebug(""));
        }
        [Test]
        public void LogHelperTrace_UnitTesting()
        {
            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockSDRLogger.Object);
            LogHelper logHelper = new LogHelper(_mockLogger.Object);
            logHelper.LogTrace("This is Trace");

            _mockLogger.Setup(x => x.CreateLogger(Constants.LogConstant.Application))
                       .Returns(_mockErrorSDRLogger.Object);

            LogHelper logHelper2 = new LogHelper(_mockLogger.Object);
            Assert.Throws<Moq.MockException>(() => logHelper2.LogTrace(""));
        }
        #endregion

        #region PostStudyElementsCheck Unit Testing
        [Test]
        public void PostStudyElementsCheck_Unit_Testing()
        {           
            StudyEntity study = GetPostDataFromStaticJson();
            var isSame= PostStudyElementsCheck.StudyComparison(study, study);

            //Assert

            Assert.IsTrue(isSame);
            study.clinicalStudy.studyTitle = "Changed";           
            StudyEntity studyCheck = GetPostDataFromStaticJson();


            isSame = PostStudyElementsCheck.StudyComparison(studyCheck, study);

            //Assert

            Assert.IsFalse(isSame);

        }
        #endregion

        #region Startup Library UnitTesting
        [Test]
        public void Startup_Library_UnitTesting()
        {
            _mockConfig.Setup(x => x.GetSection(It.IsAny<string>()).Value)
                .Returns("Value");
            StartupLib.SetConstants(_mockConfig.Object);       
            Assert.AreEqual(Config.connectionString, "Value");
            Assert.AreEqual(Config.databaseName, "Value");
            Assert.AreEqual(Config.instrumentationKey, "Value");    
        }
        #endregion

        #region ErrorResponse Helper Unit Testing
        [Test]
        public void ErrorResponse_Helper_UnitTestng()
        {
            ErrorModel errorModel = new ErrorModel();

            errorModel = ErrorResponseHelper.UnAuthorizedAccess();

            Assert.AreEqual("401", errorModel.statusCode);
            Assert.AreEqual("Access Denied", errorModel.message);

            errorModel = ErrorResponseHelper.MethodNotAllowed();

            Assert.AreEqual("405", errorModel.statusCode);
            Assert.AreEqual("Method Not Allowed", errorModel.message);

            errorModel = ErrorResponseHelper.GatewayError();

            Assert.AreEqual("500", errorModel.statusCode);
            Assert.AreEqual("Internal Server Error", errorModel.message);

            ValidationErrorModel validationErrorModel = new ValidationErrorModel();

            validationErrorModel = ErrorResponseHelper.BadRequest("Validation Error", "Conformance Error");
            Assert.AreEqual("Conformance Error", validationErrorModel.message);
            Assert.AreEqual("Validation Error", validationErrorModel.error);
            Assert.AreEqual("400", validationErrorModel.statusCode);
        }
        #endregion

        #region Post Study Elements Check Testing
        [Test]
        public void PostStudyElements_Section_Check_UnitTesting()
        {
            var incomingpostStudyDTO = GetPostDataFromStaticJson();
            var existingpostStudyDTO = GetPostDataFromStaticJson();

            SectionIdGenerator.GenerateSectionId(incomingpostStudyDTO);
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO.clinicalStudy.studyIdentifiers.ForEach(x => x.studyIdentifierId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);
            Assert.IsNotEmpty(incomingpostStudyDTO.clinicalStudy.studyIdentifiers[0].studyIdentifierId);
            Assert.IsNotEmpty(incomingpostStudyDTO.clinicalStudy.studyIdentifiers[0].studyIdentifierId);
            Assert.IsNotEmpty(incomingpostStudyDTO.clinicalStudy.currentSections[0].currentSectionsId);
            Assert.IsNotEmpty(incomingpostStudyDTO.clinicalStudy.currentSections[1].currentSectionsId);
            Assert.IsNotEmpty(incomingpostStudyDTO.clinicalStudy.currentSections[2].currentSectionsId);
            
            //SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            //PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);



            //Study Section level
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            SectionIdGenerator.GenerateSectionId(incomingpostStudyDTO);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.investigationalInterventions != null).ForEach(x => x.investigationalInterventions = null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.objectives != null).ForEach(x => x.objectives = null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyIndications != null).ForEach(x => x.studyIndications = null);            
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).ForEach(x => x.studyDesigns = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            //Study Design Level
            existingpostStudyDTO= GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));

            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.objectives != null).Find(x => x.objectives != null).objectives.Find(x => x.endpoints!=null).endpoints.ForEach(x=>x.endPointsId="");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections
                .FindAll(x => x.plannedWorkflows != null).ForEach(x => x.plannedWorkflows = null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections
                .FindAll(x => x.studyPopulations != null).ForEach(x => x.studyPopulations = null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections
                .FindAll(x => x.studyCells != null).ForEach(x => x.studyCells = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            //Planned WorkFlows and Study Cells
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO)); 
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x=>x.studyDesigns!=null).studyDesigns.Find(x=>x.currentSections!=null).currentSections.Find(x=>x.plannedWorkflows!=null)
                                                              .plannedWorkflows.ForEach(x => x.plannedWorkFlowId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyPopulations != null)
                                                              .studyPopulations.ForEach(x => x.studyPopulationId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.ForEach(x => x.studyCellId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            var anotherMatrix = JsonConvert.DeserializeObject<List<MatrixEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix));
            PostStudyElementsCheck.MatrixSectionCheck(existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix, anotherMatrix);
            var studyCells = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells));
            var anotherStudyCells = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells));            
            PostStudyElementsCheck.StudyCellsSectionCheck(existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0], studyCells, anotherStudyCells);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO)); 
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.workflowItemMatrix != null).workflowItemMatrix.workFlowItemMatrixId = null;
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.workflowItemMatrix != null).workflowItemMatrix.matrix.ForEach(x =>
                                                              {
                                                                  x.matrixId = null;
                                                                  x.items.ForEach(i =>
                                                                  {
                                                                      i.itemId = null;
                                                                      i.activity.activityId = null;
                                                                      i.encounter.encounterId = null;
                                                                      i.activity.studyDataCollection.ForEach(sdc => sdc.studyDataCollectionId = null);
                                                                      i.encounter.epoch.epochId = null;
                                                                      i.encounter.startRule.RuleId = null;
                                                                      i.encounter.endRule.RuleId = null;
                                                                  });
                                                              });           
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyArm != null).studyArm.studyArmId = null;
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyEpoch != null).studyEpoch.studyEpochId = null;
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyElements != null).studyElements.ForEach(x => x.studyElementId = null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyElements != null).studyElements.ForEach(x => x.startRule.RuleId = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.workflowItemMatrix != null).workflowItemMatrix.matrix.ForEach(x =>
                                                              {
                                                                  x.matrixId = null;
                                                                  x.items.ForEach(i =>
                                                                  {
                                                                      i.itemId = null;
                                                                      i.activity.activityId = null;
                                                                      i.encounter.encounterId = null;
                                                                      i.activity.studyDataCollection.ForEach(sdc => sdc.studyDataCollectionId = null);
                                                                      i.encounter.epoch.epochId = null;
                                                                      i.encounter.startRule.RuleId = null;
                                                                      i.encounter.endRule.RuleId = null;
                                                                  });
                                                              });
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyArm != null).studyArm.studyArmId = "";
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyEpoch != null).studyEpoch.studyEpochId = "";
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyElements != null).studyElements.ForEach(x => x.studyElementId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.Find(x => x.studyElements != null).studyElements.ForEach(x => x.startRule.RuleId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);
            PostStudyElementsCheck.RemoveId(incomingpostStudyDTO);            
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
        public void ClinicalStudyValidation_UnitTesting()
        {            
            ValidationDependencies.AddValidationDependencies(serviceDescriptors);
            var incomingpostStudyDTO = PostDataFromStaticJson();
            ClinicalStudyValidator clinicalStudyRules = new ClinicalStudyValidator();
            var result= clinicalStudyRules.Validate(incomingpostStudyDTO);
            Assert.IsTrue(result.IsValid);

            StudyIdentifiersValidator studyIdentifiersValidator = new StudyIdentifiersValidator();            
            Assert.IsTrue(studyIdentifiersValidator.Validate(incomingpostStudyDTO.clinicalStudy.studyIdentifiers[0]).IsValid);            

            StudyObjectivesValidator studyObjectivesValidator = new StudyObjectivesValidator();
            Assert.IsTrue(studyObjectivesValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x=>x.objectives!=null).objectives[0]).IsValid);

            EndpointsValidator endpointsValidator = new EndpointsValidator();
            Assert.IsTrue(endpointsValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.objectives != null).objectives[0].endpoints[0]).IsValid);

            StudyIndicationValidator studyIndicationValidator = new StudyIndicationValidator();
            Assert.IsTrue(studyIndicationValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyIndications != null).studyIndications[0]).IsValid);

            StudyPopulationValidator studyPopulationValidator = new StudyPopulationValidator();
            Assert.IsTrue(studyPopulationValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyPopulations != null).studyPopulations[0]).IsValid);            

            StudyCellsValidator studyCellsValidator = new StudyCellsValidator();
            Assert.IsTrue(studyCellsValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells[0]).IsValid);

            PlannedWorkFlowValidator plannedWorkFlowValidator = new PlannedWorkFlowValidator();
            Assert.IsTrue(plannedWorkFlowValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0]).IsValid);

            InvestigationalInterventionValidatior investigationalInterventionValidatior = new InvestigationalInterventionValidatior();
            Assert.IsTrue(investigationalInterventionValidatior.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions[0]).IsValid);
            
            CodingValidator codingValidator = new CodingValidator();
            Assert.IsTrue(codingValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions[0].coding[0]).IsValid);

            PointInTimeValidator pointInTimeValidator = new PointInTimeValidator();
            Assert.IsTrue(pointInTimeValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].startPoint).IsValid);

            StudyElementsValidator studyElementsValidator = new StudyElementsValidator();
            Assert.IsTrue(studyElementsValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells[0].studyElements[0]).IsValid);

            RuleValidator ruleValidator = new RuleValidator();
            Assert.IsTrue(ruleValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells[0].studyElements[0].endRule).IsValid);

            StudyArmValidator studyArmValidator = new StudyArmValidator();
            Assert.IsTrue(studyArmValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells[0].studyArm).IsValid);

            StudyEpochValidator studyEpochValidator = new StudyEpochValidator();
            Assert.IsTrue(studyEpochValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.studyCells != null).studyCells[0].studyEpoch).IsValid);

            StudyProtocolValidator studyProtocolValidator = new StudyProtocolValidator();
            Assert.IsTrue(studyProtocolValidator.Validate(incomingpostStudyDTO.clinicalStudy.studyProtocolReferences[0]).IsValid);

            WorkFlowItemMatrixValidator workFlowItemMatrixValidator = new WorkFlowItemMatrixValidator();
            Assert.IsTrue(workFlowItemMatrixValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix).IsValid);

            MatrixValidator matrixValidator = new MatrixValidator();
            Assert.IsTrue(matrixValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0]).IsValid);

            ItemValidator itemValidator = new ItemValidator();
            Assert.IsTrue(itemValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0]).IsValid);

            ActivityValidator activityValidator = new ActivityValidator();
            Assert.IsTrue(activityValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0].activity).IsValid);

            StudyDataCollectionValidator studyDataCollectionValidator = new StudyDataCollectionValidator();
            Assert.IsTrue(studyDataCollectionValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0].activity.studyDataCollection[0]).IsValid);

            DefinedProcedureValidator definedProcedureValidator = new DefinedProcedureValidator();
            Assert.IsTrue(definedProcedureValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0].activity.definedProcedures[0]).IsValid);

            EncounterValidator encounterValidator = new EncounterValidator();
            Assert.IsTrue(encounterValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0].encounter).IsValid);

            EpochValidator epochValidator = new EpochValidator();
            Assert.IsTrue(epochValidator.Validate(incomingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns[0].currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows[0].workflowItemMatrix.matrix[0].items[0].encounter.epoch).IsValid);

            SearchParametersDTO searchParameters = new SearchParametersDTO
            {         
                indication = "Bile",
                interventionModel = "CROSS_OVER",
                studyTitle = "Umbrella",
                pageNumber = 1,
                pageSize = 25,
                phase = "PHASE_1_TRAIL",
                studyId = "100",
                fromDate = "",
                toDate =""
            };
            SearchParametersValidator searchParametersValidator = new SearchParametersValidator();
            var res = searchParametersValidator.Validate(searchParameters);
            Assert.IsTrue(searchParametersValidator.Validate(searchParameters).IsValid);
        }
        #endregion
        
    }
}
