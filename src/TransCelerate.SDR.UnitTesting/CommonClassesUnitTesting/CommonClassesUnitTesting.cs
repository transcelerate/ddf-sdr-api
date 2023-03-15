﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Reports;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc.Versioning;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.DataAccess.Filters;

namespace TransCelerate.SDR.UnitTesting
{
    public class CommonClassesUnitTesting
    {
        private IMapper _mockMapper;
        private Mock<ILoggerFactory> _mockLogger = new Mock<ILoggerFactory>();
        private Mock<ILogger> _mockSDRLogger = new Mock<ILogger>();
        private ILogHelper _mockLogHelper = Mock.Of<ILogHelper>();
        private Mock<ILogger> _mockErrorSDRLogger = new Mock<ILogger>(MockBehavior.Strict);
        private Mock<IConfiguration> _mockConfig = new Mock<IConfiguration>();
        //private IConfiguration _mockConfiguration = Mock.Of<IConfiguration>();
        private IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        private Mock<IChangeAuditService> _mockChangeAuditService = new Mock<IChangeAuditService>(MockBehavior.Loose);
        //private IApplicationBuilder app = Mock.Of<IApplicationBuilder>();
        //private IWebHostEnvironment env = Mock.Of<IWebHostEnvironment>();

        #region Setup
        public UserGroupMappingEntity GetUserDataFromStaticJson()
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
        }
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public ChangeAuditStudyDto GetChangeAuditDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ChangeAuditData.json");
            return JsonConvert.DeserializeObject<ChangeAuditStudyDto>(jsonData);
        }
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
        public SDRGroupsDTO PostAGroupDto()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingDTO>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<SDRGroupsDTO>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups[0]));
            return groupDetails;
        }
        public IEnumerable<GroupDetailsEntity> GetGroupDetails()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<IEnumerable<GroupDetailsEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }
        public PostUserToGroupsDTO PostUser()
        {
            List<GroupsTaggedToUser> groupList = new List<GroupsTaggedToUser>();
            GroupsTaggedToUser groupsTaggedToUser = new GroupsTaggedToUser
            {
                groupId = "0193a357-8519-4488-90e4-522f701658b9",
                groupName = "OncologyRead",
                isActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new GroupsTaggedToUser
            {
                groupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                groupName = "AmnesiaReadWrite",
                isActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new GroupsTaggedToUser
            {
                groupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                groupName = "AmnesiaReadWrite",
                isActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new PostUserToGroupsDTO
            {
                email = "user1@SDR.com",
                oid = "aw2dq254wfdsf",
                groups = groupList
            };
            
            return postUserToGroupsDTO;
        }
        public IEnumerable<PostUserToGroupsDTO> UserList()
        {
            List<GroupsTaggedToUser> groupList = new List<GroupsTaggedToUser>();
            GroupsTaggedToUser groupsTaggedToUser = new GroupsTaggedToUser
            {
                groupId = "0193a357-8519-4488-90e4-522f701658b9",
                groupName = "OncologyRead",
                isActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new GroupsTaggedToUser
            {
                groupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                groupName = "AmnesiaReadWrite",
                isActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new GroupsTaggedToUser
            {
                groupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                groupName = "AmnesiaReadWrite",
                isActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new PostUserToGroupsDTO
            {
                email = "user1@SDR.com",
                oid = "aw2dq254wfdsf",
                groups = groupList
            };
            List<PostUserToGroupsDTO> postUserToGroups = new List<PostUserToGroupsDTO>();
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
            Assert.AreEqual(Config.isAuthEnabled, true);               
            Assert.AreEqual(Config.isGroupFilterEnabled, true);
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            Assert.AreEqual(apiUsdmVersionMapping_NonStatic.SDRVersions.Count, ApiUsdmVersionMapping.SDRVersions.Count);
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

            errorModel = ErrorResponseHelper.InternalServerError();
            Assert.AreEqual("500", errorModel.statusCode);
            Assert.AreEqual("Internal Server Error", errorModel.message);
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
        public void FluentValidation_UnitTesting()
        {            
            ValidationDependencies.AddValidationDependencies(serviceDescriptors);            
            var incomingpostStudyDTO = PostDataFromStaticJson();
            PostStudyValidator postStudyValidator = new PostStudyValidator();
            Assert.IsTrue(postStudyValidator.Validate(incomingpostStudyDTO).IsValid);

            ClinicalStudyValidator clinicalStudyRules = new ClinicalStudyValidator();
            var result= clinicalStudyRules.Validate(incomingpostStudyDTO.clinicalStudy);
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

            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            UserGroupsQueryParametersValidator userGroupsQueryParametersValidator = new UserGroupsQueryParametersValidator();
            Assert.IsTrue(userGroupsQueryParametersValidator.Validate(userGroupsQueryParameters).IsValid);

            GroupsValidator groupsValidator = new GroupsValidator();
            Assert.IsTrue(groupsValidator.Validate(PostAGroupDto()).IsValid);

            PostUserToGroupValidator usersValidator = new PostUserToGroupValidator();
            Assert.IsTrue(usersValidator.Validate(PostUser()).IsValid);

            GroupFilterValidator groupFilterValidator = new GroupFilterValidator();
            Assert.IsTrue(groupFilterValidator.Validate(PostAGroupDto().groupFilter[0]).IsValid);

            GroupFilterValuesValidator groupFilterValuesValidator = new GroupFilterValuesValidator();
            Assert.IsTrue(groupFilterValuesValidator.Validate(PostAGroupDto().groupFilter[0].groupFilterValues[0]).IsValid);

            UserLogin user = new UserLogin
            {
                username="user",password="password"
            };
            UserLoginValidator userLoginValidator = new UserLoginValidator();
            Assert.IsTrue(userLoginValidator.Validate(user).IsValid);

            TransCelerate.SDR.RuleEngine.Common.ValidationDependenciesCommon.AddValidationDependenciesCommon(serviceDescriptors);
            TransCelerate.SDR.Core.DTO.Common.SearchParametersDto searchParametersCommon = new TransCelerate.SDR.Core.DTO.Common.SearchParametersDto
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
            TransCelerate.SDR.RuleEngine.Common.SearchParametersValidator searchValidator = new RuleEngine.Common.SearchParametersValidator();
            Assert.IsTrue(searchValidator.Validate(searchParametersCommon).IsValid);
        }
        #endregion

        #region UserGroup Sorting Unit Testing
        [Test]
        public void UserGroupSortingHelper_UnitTesting()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            string[] sortOrders = { SortOrder.asc.ToString(), SortOrder.desc.ToString() };
            foreach (var sortOrder in sortOrders)
            {
                userGroupsQueryParameters.sortOrder = sortOrder;
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0)
                        userGroupsQueryParameters.sortBy = "email";
                    if (i == 1)
                        userGroupsQueryParameters.sortBy = "modifiedon";
                    if (i == 2)
                        userGroupsQueryParameters.sortBy = "modifiedby";
                    if (i == 3)
                        userGroupsQueryParameters.sortBy = "createdby";
                    if (i == 4)
                        userGroupsQueryParameters.sortBy = "createdon";
                    if (i == 5)
                        userGroupsQueryParameters.sortBy = "name";
                    if (i == 6)
                        userGroupsQueryParameters.sortBy = "";
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
            UserLogin user = new UserLogin
            {
                username = "user",
                password = "password"
            };
            TokenController tokenController = new TokenController(_mockLogHelper);
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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/TokenRawResponse.json");
            var responseObject = JsonConvert.DeserializeObject<TokenSuccessResponseDTO>(jsonData);
            var tokenResponse = new { token = $"{responseObject.token_type} {responseObject.access_token}" };

            Assert.NotNull(tokenResponse);
        }

        [Test]
        public void ReportsControllerUnitTesting()
        {
            ReportBodyParameters reportBodyParameters = new ReportBodyParameters
            {
                days = 10,
                operation = "GET",
                pageSize = 10,
                recordNumber = 1,
                responseCode = 200,
                sortBy = "requestdate",
                sortOrder = "asc"
            };
            ReportsController reportsController = new ReportsController(_mockLogHelper,_mockMapper);
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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            reportBodyParameters.pageSize = 0;
            reportBodyParameters.sortBy = "operation";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.sortBy = "api";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.sortBy = "callerip";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.sortBy = "responsecode";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.sortBy = "operationas";
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            reportBodyParameters.sortBy = "";
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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

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

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.AreEqual("400", actual_result.statusCode);

            reportBodyParameters.FilterByTime = true;
            reportBodyParameters.FromDateTime = DateTime.Now.AddDays(-1);
            reportBodyParameters.ToDateTime = DateTime.Now;
            method = reportsController.GetUsageReport(reportBodyParameters);
            method.Wait();

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ReportsRawData.json");
            var rawReport = JsonConvert.DeserializeObject<SystemUsageRawReport>(jsonData);
            List<SystemUsageReportDTO> usageReport = new List<SystemUsageReportDTO>();
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
            ActionFilter actionFilter = new ActionFilter(_mockLogHelper);

            Mock<ActionExecutionDelegate> actionExecutionDelegate = new Mock<ActionExecutionDelegate>();

            ChangeAuditStudyDto study = GetChangeAuditDtoDataFromStaticJson();

            _mockChangeAuditService.Setup(x => x.GetChangeAudit(It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(study as object));
            ChangeAuditController changeAuditController = new ChangeAuditController(_mockChangeAuditService.Object, _mockLogHelper);
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
            
            VersioningErrorResponseHelper versioningErrorResponseHelper = new VersioningErrorResponseHelper();
            var result  = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            var actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).message, Constants.ErrorMessages.UsdmVersionMapError);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.ApiVersionUnspecified,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).message, Constants.ErrorMessages.UsdmVersionMissing);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.AmbiguousApiVersion,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).message, Constants.ErrorMessages.UsdmVersionAmbiguous);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   Constants.ApiVersionErrorCodes.InvalidApiVersion,
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).message, Constants.ErrorMessages.UsdmVersionMapError);

            errorResponseContext = new ErrorResponseContext(
                   new Mock<HttpRequest>().Object,
                   400,
                   "",
                   "",
                   "");
            result = versioningErrorResponseHelper.CreateResponse(errorResponseContext);
            actualResult = (result as BadRequestObjectResult).Value;
            Assert.AreEqual((actualResult as ErrorModel).message, Constants.ErrorMessages.UsdmVersionMapError);
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
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
            };
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchTitle(searchParameters, GetUserDataFromStaticJson().SDRGroups,user));

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
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
                Asc = true,
                Header = "sdrversion"
            };

            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity));
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            var grps = GetUserDataFromStaticJson().SDRGroups;
            grps[0].groupFilter[0].groupFieldName = GroupFieldNames.studyType.ToString();
            grps[0].groupFilter[0].groupFilterValues[0].groupFilterValueId = "ALL";
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity, grps, user));
            grps[0].groupFilter[0].groupFieldName = GroupFieldNames.studyType.ToString();
            grps[0].groupFilter[0].groupFilterValues[0].groupFilterValueId = "interventional";
            Assert.IsNotNull(DataFilterCommon.GetFiltersForSearchStudy(searchParametersEntity, grps, user));
            Config.isGroupFilterEnabled = false;
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
            searchParameters.SortBy = "sdrversion";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
            searchParameters.SortBy = "lastmodifieddate";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
            searchParameters.SortBy = "usdmversion";
            Assert.IsNotNull(DataFilterCommon.GetSorterForSearchStudyTitle(searchParameters));
        }

        #endregion
    }
}
