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
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Reports;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngine;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
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
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfies());
            });
            _mockMapper = new Mapper(mockMapper);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public static Core.DTO.Common.ChangeAuditStudyDto GetChangeAuditDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<Core.DTO.Common.ChangeAuditStudyDto>(jsonData);
        }
        public static StudyEntity GetPostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }
        public static PostStudyDTO PostDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            return JsonConvert.DeserializeObject<PostStudyDTO>(jsonData);
        }
        public static SDRGroupsDTO PostAGroupDto()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingDTO>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<SDRGroupsDTO>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups[0]));
            return groupDetails;
        }
        public static IEnumerable<GroupDetailsEntity> GetGroupDetails()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<IEnumerable<GroupDetailsEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }
        public static PostUserToGroupsDTO PostUser()
        {
            List<GroupsTaggedToUser> groupList = new();
            GroupsTaggedToUser groupsTaggedToUser = new()
            {
                GroupId = "0193a357-8519-4488-90e4-522f701658b9",
                GroupName = "OncologyRead",
                IsActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new()
            {
                GroupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new()
            {
                GroupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new()
            {
                Email = "user1@SDR.com",
                Oid = "aw2dq254wfdsf",
                Groups = groupList
            };

            return postUserToGroupsDTO;
        }
        public static IEnumerable<PostUserToGroupsDTO> UserList()
        {
            List<GroupsTaggedToUser> groupList = new();
            GroupsTaggedToUser groupsTaggedToUser = new()
            {
                GroupId = "0193a357-8519-4488-90e4-522f701658b9",
                GroupName = "OncologyRead",
                IsActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new()
            {
                GroupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new()
            {
                GroupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new()
            {
                Email = "user1@SDR.com",
                Oid = "aw2dq254wfdsf",
                Groups = groupList
            };
            List<PostUserToGroupsDTO> postUserToGroups = new();
            postUserToGroups.Add(postUserToGroupsDTO);
            IEnumerable<PostUserToGroupsDTO> postUserToGroupsIenum = JsonConvert.DeserializeObject<IEnumerable<PostUserToGroupsDTO>>(
                                                                    JsonConvert.SerializeObject(postUserToGroups));
            return postUserToGroupsIenum;
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

        #region PostStudyElementsCheck Unit Testing
        [Test]
        public void PostStudyElementsCheck_Unit_Testing()
        {
            StudyEntity study = GetPostDataFromStaticJson();
            var isSame = PostStudyElementsCheck.StudyComparison(study, study);

            //Assert

            Assert.IsTrue(isSame);
            study.ClinicalStudy.StudyTitle = "Changed";
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
            Assert.AreEqual(Config.InstrumentationKey, "true");
            Assert.AreEqual(Config.DateRange, "true");
            Assert.AreEqual(Config.Audience, "true");
            Assert.AreEqual(Config.Scope, "true");
            Assert.AreEqual(Config.TenantID, "true");
            Assert.AreEqual(Config.Authority, "true");
            Assert.AreEqual(Config.IsAuthEnabled, true);
            Assert.AreEqual(Config.IsGroupFilterEnabled, true);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            Assert.AreEqual(apiUsdmVersionMapping_NonStatic.SDRVersions.Count, ApiUsdmVersionMapping.SDRVersions.Count);
        }
        #endregion

        #region ErrorResponse Helper Unit Testing
        [Test]
        public void ErrorResponse_Helper_UnitTestng()
        {
            ErrorModel errorModel = ErrorResponseHelper.UnAuthorizedAccess();

            Assert.AreEqual("401", errorModel.StatusCode);
            Assert.AreEqual("Access Denied", errorModel.Message);

            errorModel = ErrorResponseHelper.MethodNotAllowed();

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

        #region Post Study Elements Check Testing
        [Test]
        public void PostStudyElements_Section_Check_UnitTesting()
        {
            var incomingpostStudyDTO = GetPostDataFromStaticJson();
            var existingpostStudyDTO = GetPostDataFromStaticJson();

            SectionIdGenerator.GenerateSectionId(incomingpostStudyDTO);
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO.ClinicalStudy.StudyIdentifiers.ForEach(x => x.StudyIdentifierId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);
            Assert.IsNotEmpty(incomingpostStudyDTO.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierId);
            Assert.IsNotEmpty(incomingpostStudyDTO.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierId);
            Assert.IsNotEmpty(incomingpostStudyDTO.ClinicalStudy.CurrentSections[0].CurrentSectionsId);
            Assert.IsNotEmpty(incomingpostStudyDTO.ClinicalStudy.CurrentSections[1].CurrentSectionsId);
            Assert.IsNotEmpty(incomingpostStudyDTO.ClinicalStudy.CurrentSections[2].CurrentSectionsId);

            //SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            //PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);



            //Study Section level
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            SectionIdGenerator.GenerateSectionId(incomingpostStudyDTO);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.InvestigationalInterventions != null).ForEach(x => x.InvestigationalInterventions = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.Objectives != null).ForEach(x => x.Objectives = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyIndications != null).ForEach(x => x.StudyIndications = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).ForEach(x => x.StudyDesigns = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            //Study Design Level
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));

            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.Objectives != null).Find(x => x.Objectives != null).Objectives.Find(x => x.Endpoints != null).Endpoints.ForEach(x => x.EndPointsId = "");
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections
                .FindAll(x => x.PlannedWorkflows != null).ForEach(x => x.PlannedWorkflows = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections
                .FindAll(x => x.StudyPopulations != null).ForEach(x => x.StudyPopulations = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections
                .FindAll(x => x.StudyCells != null).ForEach(x => x.StudyCells = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);

            //Planned WorkFlows and Study Cells
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.PlannedWorkflows != null)
                                                              .PlannedWorkflows.ForEach(x => x.PlannedWorkFlowId = "");
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyPopulations != null)
                                                              .StudyPopulations.ForEach(x => x.StudyPopulationId = "");
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.ForEach(x => x.StudyCellId = "");
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);


            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            var anotherMatrix = JsonConvert.DeserializeObject<List<MatrixEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix));
            PostStudyElementsCheck.MatrixSectionCheck(existingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix, anotherMatrix);
            var studyCells = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells));
            var anotherStudyCells = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(existingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells));
            PostStudyElementsCheck.StudyCellsSectionCheck(existingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0], studyCells, anotherStudyCells);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.PlannedWorkflows != null)
                                                              .PlannedWorkflows.Find(x => x.WorkflowItemMatrix != null).WorkflowItemMatrix.WorkFlowItemMatrixId = null;
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.PlannedWorkflows != null)
                                                              .PlannedWorkflows.Find(x => x.WorkflowItemMatrix != null).WorkflowItemMatrix.Matrix.ForEach(x =>
                                                              {
                                                                  x.MatrixId = null;
                                                                  x.Items.ForEach(i =>
                                                                  {
                                                                      i.ItemId = null;
                                                                      i.Activity.ActivityId = null;
                                                                      i.Encounter.EncounterId = null;
                                                                      i.Activity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = null);
                                                                      i.Encounter.Epoch.EpochId = null;
                                                                      i.Encounter.StartRule.RuleId = null;
                                                                      i.Encounter.EndRule.RuleId = null;
                                                                  });
                                                              });
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyArm != null).StudyArm.StudyArmId = null;
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyEpoch != null).StudyEpoch.StudyEpochId = null;
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyElements != null).StudyElements.ForEach(x => x.StudyElementId = null);
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyElements != null).StudyElements.ForEach(x => x.StartRule.RuleId = null);
            PostStudyElementsCheck.SectionCheck(incomingpostStudyDTO, existingpostStudyDTO);
            existingpostStudyDTO = GetPostDataFromStaticJson();
            SectionIdGenerator.GenerateSectionId(existingpostStudyDTO);
            incomingpostStudyDTO = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existingpostStudyDTO));
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.PlannedWorkflows != null)
                                                              .PlannedWorkflows.Find(x => x.WorkflowItemMatrix != null).WorkflowItemMatrix.Matrix.ForEach(x =>
                                                              {
                                                                  x.MatrixId = null;
                                                                  x.Items.ForEach(i =>
                                                                  {
                                                                      i.ItemId = null;
                                                                      i.Activity.ActivityId = null;
                                                                      i.Encounter.EncounterId = null;
                                                                      i.Activity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = null);
                                                                      i.Encounter.Epoch.EpochId = null;
                                                                      i.Encounter.StartRule.RuleId = null;
                                                                      i.Encounter.EndRule.RuleId = null;
                                                                  });
                                                              });
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyArm != null).StudyArm.StudyArmId = "";
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyEpoch != null).StudyEpoch.StudyEpochId = "";
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyElements != null).StudyElements.ForEach(x => x.StudyElementId = "");
            existingpostStudyDTO.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Find(x => x.StudyDesigns != null).StudyDesigns.Find(x => x.CurrentSections != null).CurrentSections.Find(x => x.StudyCells != null)
                                                              .StudyCells.Find(x => x.StudyElements != null).StudyElements.ForEach(x => x.StartRule.RuleId = "");
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
        public void FluentValidation_UnitTesting()
        {
            ValidationDependencies.AddValidationDependencies(serviceDescriptors);
            var incomingpostStudyDTO = PostDataFromStaticJson();
            PostStudyValidator postStudyValidator = new();
            Assert.IsTrue(postStudyValidator.Validate(incomingpostStudyDTO).IsValid);

            ClinicalStudyValidator clinicalStudyRules = new();
            var result = clinicalStudyRules.Validate(incomingpostStudyDTO.ClinicalStudy);
            Assert.IsTrue(result.IsValid);

            StudyIdentifiersValidator studyIdentifiersValidator = new();
            Assert.IsTrue(studyIdentifiersValidator.Validate(incomingpostStudyDTO.ClinicalStudy.StudyIdentifiers[0]).IsValid);

            StudyObjectivesValidator studyObjectivesValidator = new();
            Assert.IsTrue(studyObjectivesValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.Objectives != null).Objectives[0]).IsValid);

            EndpointsValidator endpointsValidator = new();
            Assert.IsTrue(endpointsValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.Objectives != null).Objectives[0].Endpoints[0]).IsValid);

            StudyIndicationValidator studyIndicationValidator = new();
            Assert.IsTrue(studyIndicationValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyIndications != null).StudyIndications[0]).IsValid);

            StudyPopulationValidator studyPopulationValidator = new();
            Assert.IsTrue(studyPopulationValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyPopulations != null).StudyPopulations[0]).IsValid);

            StudyCellsValidator studyCellsValidator = new();
            Assert.IsTrue(studyCellsValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells[0]).IsValid);

            PlannedWorkFlowValidator plannedWorkFlowValidator = new();
            Assert.IsTrue(plannedWorkFlowValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0]).IsValid);

            InvestigationalInterventionValidatior investigationalInterventionValidatior = new();
            Assert.IsTrue(investigationalInterventionValidatior.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.InvestigationalInterventions != null).InvestigationalInterventions[0]).IsValid);

            CodingValidator codingValidator = new();
            Assert.IsTrue(codingValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.InvestigationalInterventions != null).InvestigationalInterventions[0].Coding[0]).IsValid);

            PointInTimeValidator pointInTimeValidator = new();
            Assert.IsTrue(pointInTimeValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].StartPoint).IsValid);

            StudyElementsValidator studyElementsValidator = new();
            Assert.IsTrue(studyElementsValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells[0].StudyElements[0]).IsValid);

            RuleValidator ruleValidator = new();
            Assert.IsTrue(ruleValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells[0].StudyElements[0].EndRule).IsValid);

            StudyArmValidator studyArmValidator = new();
            Assert.IsTrue(studyArmValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells[0].StudyArm).IsValid);

            StudyEpochValidator studyEpochValidator = new();
            Assert.IsTrue(studyEpochValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.StudyCells != null).StudyCells[0].StudyEpoch).IsValid);

            StudyProtocolValidator studyProtocolValidator = new();
            Assert.IsTrue(studyProtocolValidator.Validate(incomingpostStudyDTO.ClinicalStudy.StudyProtocolReferences[0]).IsValid);

            WorkFlowItemMatrixValidator workFlowItemMatrixValidator = new();
            Assert.IsTrue(workFlowItemMatrixValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix).IsValid);

            MatrixValidator matrixValidator = new();
            Assert.IsTrue(matrixValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0]).IsValid);

            ItemValidator itemValidator = new();
            Assert.IsTrue(itemValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0]).IsValid);

            ActivityValidator activityValidator = new();
            Assert.IsTrue(activityValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0].Activity).IsValid);

            StudyDataCollectionValidator studyDataCollectionValidator = new();
            Assert.IsTrue(studyDataCollectionValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0].Activity.StudyDataCollection[0]).IsValid);

            DefinedProcedureValidator definedProcedureValidator = new();
            Assert.IsTrue(definedProcedureValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0].Activity.DefinedProcedures[0]).IsValid);

            EncounterValidator encounterValidator = new();
            Assert.IsTrue(encounterValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0].Encounter).IsValid);

            EpochValidator epochValidator = new();
            Assert.IsTrue(epochValidator.Validate(incomingpostStudyDTO.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns[0].CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows[0].WorkflowItemMatrix.Matrix[0].Items[0].Encounter.Epoch).IsValid);

            SearchParametersDTO searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                StudyId = "100",
                FromDate = "",
                ToDate = ""
            };
            SearchParametersValidator searchParametersValidator = new();
            var res = searchParametersValidator.Validate(searchParameters);
            Assert.IsTrue(searchParametersValidator.Validate(searchParameters).IsValid);

            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            UserGroupsQueryParametersValidator userGroupsQueryParametersValidator = new();
            Assert.IsTrue(userGroupsQueryParametersValidator.Validate(userGroupsQueryParameters).IsValid);

            GroupsValidator groupsValidator = new();
            Assert.IsTrue(groupsValidator.Validate(PostAGroupDto()).IsValid);

            PostUserToGroupValidator usersValidator = new();
            Assert.IsTrue(usersValidator.Validate(PostUser()).IsValid);

            GroupFilterValidator groupFilterValidator = new();
            Assert.IsTrue(groupFilterValidator.Validate(PostAGroupDto().GroupFilter[0]).IsValid);

            GroupFilterValuesValidator groupFilterValuesValidator = new();
            Assert.IsTrue(groupFilterValuesValidator.Validate(PostAGroupDto().GroupFilter[0].GroupFilterValues[0]).IsValid);

            UserLogin user = new()
            {
                Username = "user",
                Password = "password"
            };
            UserLoginValidator userLoginValidator = new();
            Assert.IsTrue(userLoginValidator.Validate(user).IsValid);

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

        #region UserGroup Sorting Unit Testing
        [Test]
        public void UserGroupSortingHelper_UnitTesting()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            string[] sortOrders = { SortOrder.asc.ToString(), SortOrder.desc.ToString() };
            foreach (var sortOrder in sortOrders)
            {
                userGroupsQueryParameters.SortOrder = sortOrder;
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0)
                        userGroupsQueryParameters.SortBy = "email";
                    if (i == 1)
                        userGroupsQueryParameters.SortBy = "modifiedon";
                    if (i == 2)
                        userGroupsQueryParameters.SortBy = "modifiedby";
                    if (i == 3)
                        userGroupsQueryParameters.SortBy = "createdby";
                    if (i == 4)
                        userGroupsQueryParameters.SortBy = "createdon";
                    if (i == 5)
                        userGroupsQueryParameters.SortBy = "name";
                    if (i == 6)
                        userGroupsQueryParameters.SortBy = "";
                    UserGroupSortingHelper.OrderGroups(GetGroupDetails(), userGroupsQueryParameters);
                    UserGroupSortingHelper.OrderUsers(UserList(), userGroupsQueryParameters);
                }
            }

        }
        #endregion


        #region HttpContext Response Helper UnitTesting
        [Test]
        public void HttpContextResponseHelper_UnitTesting()
        {
            //var mockHttpContext = Mock.Of<HttpContext>();
            var mockHttpContext = new DefaultHttpContext();
            string response = string.Empty;
            mockHttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var method = HttpContextResponseHelper.Response(mockHttpContext, response);
            method.Wait();
            response = method.Result;
            Assert.IsTrue(response.Contains(((int)HttpStatusCode.Forbidden).ToString()));
            mockHttpContext.Response.Headers.Remove("Content-Type");
            mockHttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            method = HttpContextResponseHelper.Response(mockHttpContext, response);
            method.Wait();
            response = method.Result;
            Assert.IsTrue(response.Contains(((int)HttpStatusCode.Unauthorized).ToString()));
            mockHttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            mockHttpContext.Response.Headers.Remove("Content-Type");
            method = HttpContextResponseHelper.Response(mockHttpContext, response);
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
            var splitStringList = SplitStringIntoArrayHelper.SplitString(JsonConvert.SerializeObject(PostAGroupDto()), 100);
            Assert.IsNotEmpty(splitStringList);
        }
        #endregion
        #region Token Controller
        [Test]
        public void TokenControllerUnitTesting()
        {
            UserLogin user = new()
            {
                Username = "user",
                Password = "password"
            };
            TokenController tokenController = new(_mockLogHelper);
            var method = tokenController.GetToken(user);
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/TokenRawResponse.json");
            var responseObject = JsonConvert.DeserializeObject<TokenSuccessResponseDTO>(jsonData);
            var tokenResponse = new { token = $"{responseObject.Token_type} {responseObject.Access_token}" };

            Assert.NotNull(tokenResponse);
        }

        [Test]
        public void ReportsControllerUnitTesting()
        {
            ReportBodyParameters reportBodyParameters = new()
            {
                Days = 10,
                Operation = "GET",
                PageSize = 10,
                RecordNumber = 1,
                ResponseCode = 200,
                SortBy = "requestdate",
                SortOrder = "asc"
            };
            ReportsController reportsController = new(_mockLogHelper, _mockMapper);
            var method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            //Expected
            var expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.GenericError);

            //Actual
            var actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            reportBodyParameters.PageSize = 0;
            reportBodyParameters.SortBy = "operation";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.SortBy = "api";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.SortBy = "callerip";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.SortBy = "responsecode";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.SortBy = "operationas";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.SortBy = "";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.FilterByTime = true;
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateMissingError);

            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            reportBodyParameters.FilterByTime = true;
            reportBodyParameters.FromDateTime = DateTime.Now;
            reportBodyParameters.ToDateTime = DateTime.Now.AddDays(-1);
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            expected = ErrorResponseHelper.BadRequest(Constants.ErrorMessages.DateErrorForReports);

            actual_result = (method.Result as BadRequestObjectResult).Value as ErrorModel;

            //Assert          
            Assert.IsNotNull((method.Result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (method.Result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), method.Result);

            Assert.AreEqual(expected.Message, actual_result.Message);
            Assert.AreEqual("400", actual_result.StatusCode);

            reportBodyParameters.FilterByTime = true;
            reportBodyParameters.FromDateTime = DateTime.Now.AddDays(-1);
            reportBodyParameters.ToDateTime = DateTime.Now;
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ReportsRawData.json");
            var rawReport = JsonConvert.DeserializeObject<SystemUsageRawReport>(jsonData);
            List<SystemUsageReportDTO> usageReport = new();
            rawReport.Tables[0].Rows.ForEach(rows => usageReport.Add(new SystemUsageReportDTO
            {
                RequestDate = rows[(int)UsageReportFields.timestamp],

                Api = rows[(int)UsageReportFields.name].Split(" ")[1],

                EmailId = JsonConvert.DeserializeObject<CustomDimension>(rows[(int)UsageReportFields.customDimensions1]).EmailAddress,

                UserName = JsonConvert.DeserializeObject<CustomDimension>(rows[(int)UsageReportFields.customDimensions1]).UserName,

                CallerIpAddress = rows[(int)UsageReportFields.client_IP],

                ResponseCode = rows[(int)UsageReportFields.resultCode],

                Operation = rows[(int)UsageReportFields.name].Split(" ")[0],

                ResponseCodeDescription = int.TryParse(rows[(int)UsageReportFields.resultCode], out int code) == true ?
                                                     Enum.IsDefined(typeof(HttpStatusCode), code) == true ?
                                                     $"{code} - {Enum.GetName(typeof(HttpStatusCode), code)}"
                                                     : null : null
            }));
            Assert.IsNotEmpty(usageReport);
        }
        #endregion

        #region ActionFilter
        [Test]
        public void ActionFilter_UnitTesting()
        {
            ActionFilter actionFilter = new(_mockLogHelper);

            Mock<ActionExecutionDelegate> actionExecutionDelegate = new();

            Core.DTO.Common.ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
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

        #region Header Validation Helper
        [Test]
        public void HeaderValidationHelperUnitTesting()
        {
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Path).Returns(Core.Utilities.Common.Route.PostElements);
            var MoqhttpContext = new DefaultHttpContext();
            MoqhttpContext.Request.Headers["usdmVersion"] = "mvp";

            var header = MoqhttpContext.Request.Headers;

            httpRequest.Setup(x => x.Headers).Returns(header);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
            var response = HeaderValidationHelper.ValidateUsdmVersionHeaderMvp(httpContext.Object, null);
            Assert.IsNull(response);

            MoqhttpContext.Request.Headers["usdmVersion"] = "v1";
            response = HeaderValidationHelper.ValidateUsdmVersionHeaderMvp(httpContext.Object, null);
            Assert.AreEqual(response, Constants.ErrorMessages.UsdmVersionMapError);

            MoqhttpContext.Request.Headers["usdmVersion"] = "";
            response = HeaderValidationHelper.ValidateUsdmVersionHeaderMvp(httpContext.Object, null);
            Assert.AreEqual(response, Constants.ErrorMessages.UsdmVersionMissing);

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
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchTitle(searchParameters, GetUserDataFromStaticJson().SDRGroups, user));

            Assert.IsNotNull(DataFilterCommon.GetFiltersForGetAudTrail("sd", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));

            Assert.IsNotNull(DataFilterCommon.GetFiltersForStudyHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "sd"));

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
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            var grps = GetUserDataFromStaticJson().SDRGroups;
            grps[0].GroupFilter[0].GroupFieldName = GroupFieldNames.studyType.ToString();
            grps[0].GroupFilter[0].GroupFilterValues[0].GroupFilterValueId = "ALL";
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity, grps, user));
            grps[0].GroupFilter[0].GroupFieldName = GroupFieldNames.studyType.ToString();
            grps[0].GroupFilter[0].GroupFilterValues[0].GroupFilterValueId = "interventional";
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity, grps, user));
            Config.IsGroupFilterEnabled = false;
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
        }

        #endregion
    }
}
