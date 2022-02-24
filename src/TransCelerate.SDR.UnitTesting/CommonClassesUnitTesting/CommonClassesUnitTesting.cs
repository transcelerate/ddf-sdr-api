using System;
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

namespace TransCelerate.SDR.UnitTesting.CommonClassesUnitTesting
{
    public class CommonClassesUnitTesting
    {
        private Mock<ILoggerFactory> _mockLogger = new Mock<ILoggerFactory>();
        private Mock<ILogger> _mockSDRLogger = new Mock<ILogger>();
        private Mock<ILogger> _mockErrorSDRLogger = new Mock <ILogger>(MockBehavior.Strict);
        private Mock<IConfiguration> _mockConfig = new Mock<IConfiguration>();
        private Mock<IConfigurationSection> _mockConfigSections = new Mock<IConfigurationSection>();       

        #region Setup
        public StudyEntity GetPostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Data\PostStudyData.json");            
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData); 
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
        public void PostStudyElementsCheck_UnitTesting()
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
            existingpostStudyDTO.clinicalStudy.currentSections.Find(x => x.studyProtocol != null).studyProtocol.protocolId=null;
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).ForEach(x => x.studyDesigns = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            //Study Design Level
            existingpostStudyDTO= GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));

            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.objectives != null).Find(x => x.objectives != null).objectives.Find(x => x.endpoints!=null).endpoints.ForEach(x=>x.EndPointsId="");
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
            incomingpostStudyDTO = incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO)); 
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x=>x.studyDesigns!=null).studyDesigns.Find(x=>x.currentSections!=null).currentSections.Find(x=>x.plannedWorkflows!=null)
                                                              .plannedWorkflows.ForEach(x => x.plannedWorkFlowId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyPopulations != null)
                                                              .studyPopulations.ForEach(x => x.studyPopulationId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.studyCells != null)
                                                              .studyCells.ForEach(x => x.studyCellId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO)); 
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).startPoint.pointInTimeId = null;
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).transitions.Find(x=>x.transitionCriteria!=null).transitionCriteria.ForEach(x=>x.transitionCriteriaId= null);
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).transitions.Find(x => x.transitionCriteria != null).transitionRule.transitionRuleId= null;
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
            incomingpostStudyDTO = incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).startPoint.pointInTimeId = "";
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).transitions.Find(x => x.transitionCriteria != null).transitionCriteria.ForEach(x => x.transitionCriteriaId = "");
            existingpostStudyDTO.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).FindAll(x => x.studyDesigns != null).Find(x => x.studyDesigns != null).studyDesigns.Find(x => x.currentSections != null).currentSections.Find(x => x.plannedWorkflows != null)
                                                              .plannedWorkflows.Find(x => x.transitions != null).transitions.Find(x => x.transitionCriteria != null).transitionRule.transitionRuleId = "";
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
            DateValidationHelper dateValidationHelper = new DateValidationHelper();

            Assert.IsTrue(dateValidationHelper.IsValid(""));
            Assert.IsTrue(dateValidationHelper.IsValid("2022-10-12"));
        }
        #endregion
    }
}
