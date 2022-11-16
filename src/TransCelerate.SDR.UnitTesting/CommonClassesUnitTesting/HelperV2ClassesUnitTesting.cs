﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.Core.Utilities.Common;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TransCelerate.SDR.RuleEngineV2;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.WebApi.DependencyInjection;
using FluentValidation;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV2ClassesUnitTesting
    {
        #region Variables
        private IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private IClinicalStudyServiceV1 _mockClinicalStudyService = Mock.Of<IClinicalStudyServiceV1>();
        #endregion
        #region Setup
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }

        public StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
        }
        #endregion

        #region Test Cases
        #region HelperV2 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            ApplicationDependencyInjector.AddApplicationDependencies(serviceDescriptors);
            HelperV2 helper = new HelperV2();
            AuditTrailEntity auditTrailEntity = helper.GetAuditTrail(user.UserName);
            Assert.IsInstanceOf(typeof(DateTime), auditTrailEntity.EntryDateTime);

            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            studyEntity = helper.GeneratedSectionId(studyEntity);
            Assert.IsNotNull(studyEntity);

            StudyEntity studyEntity1 = GetEntityDataFromStaticJson();
            StudyEntity studyEntity2 = GetEntityDataFromStaticJson();
            var isSameStudy = helper.IsSameStudy(studyEntity1, studyEntity2);
            Assert.IsTrue(isSameStudy);

            studyEntity = helper.CheckForSections(studyEntity1, studyEntity2);

            studyEntity2.ClinicalStudy.StudyType = null;
            studyEntity2.ClinicalStudy.StudyPhase = null;
           

            studyEntity = helper.CheckForSections(studyEntity1, studyEntity2);
       
        }

        [Test]
        public void ApiBehaviourOptionsHelper()
        {
            ApiBehaviourOptionsHelper apiBehaviourOptionsHelper = new ApiBehaviourOptionsHelper(_mockLogger);            
            ActionContext  context = new ActionContext();            
            var studyDto = GetDtoDataFromStaticJson();
            studyDto.ClinicalStudy = null;
            StudyValidator studyValidator = new StudyValidator();
            var errors = studyValidator.Validate(studyDto).Errors;
            context.ModelState.AddModelError("clinicalStudy", errors[0].ErrorMessage);            
            var response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);


            context.ModelState.Clear();
            studyDto = GetDtoDataFromStaticJson();
            studyDto.ClinicalStudy.StudyTitle = null;
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>();
            ClinicalStudyValidator clinicalStudyValidator = new ClinicalStudyValidator(httpContextAccessor);
            errors = clinicalStudyValidator.Validate(studyDto.ClinicalStudy).Errors;
            context.ModelState.AddModelError("Conformance", errors[0].ErrorMessage);
            response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);
        }
        #endregion

        #region Individual Study Elements Unit Testing
        [Test]
        public void CheckForCodeSection_unitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson(); //this is  json file

            List<CodeEntity> mockCodefromSomeRequest = new List<CodeEntity>();
            mockCodefromSomeRequest = JsonConvert.DeserializeObject<List<CodeEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].InterventionModel));

            List<CodeEntity> mockCodeFromDatabase = new List<CodeEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<CodeEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].InterventionModel));

            mockCodefromSomeRequest.Add(studyEntity.ClinicalStudy.StudyType);
            HelperV2 helper = new HelperV2();
            var result = helper.CheckForCodeSection(mockCodefromSomeRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(mockCodefromSomeRequest[0].Decode, result[0].Decode);


            mockCodeFromDatabase = null;
            result = helper.CheckForCodeSection(mockCodefromSomeRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //820
        [Test]
        public void CheckForStudyIdentifierSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyIdentifierEntity> mockFromRequest = new List<StudyIdentifierEntity>();
            mockFromRequest = JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyIdentifiers));

            List<StudyIdentifierEntity> mockfromdatabase = new List<StudyIdentifierEntity>();
            mockfromdatabase = JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyIdentifiers));

            mockFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyIdentifiers)));
            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyIdentifierSection(mockFromRequest, mockfromdatabase);


            mockfromdatabase = null;
            result = helper.CheckForStudyIdentifierSection(mockFromRequest, mockfromdatabase);
            Assert.AreEqual(4, result.Count);
        }

        //852
        [Test]
        public void CheckForStudyProtocolSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyProtocolVersionEntity> mockCodeFromRequest = new List<StudyProtocolVersionEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyProtocolVersions));

            List<StudyProtocolVersionEntity> mockcodeFromDatabase = new List<StudyProtocolVersionEntity>();
            mockcodeFromDatabase = JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyProtocolVersions));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyProtocolVersions)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyProtocolSection(mockCodeFromRequest, mockcodeFromDatabase);

            mockcodeFromDatabase = null;
            result = helper.CheckForStudyProtocolSection(mockCodeFromRequest, mockcodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }


        //931
        [Test]
        public void CheckForStudyDesignSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDesignEntity> mockCodeFromRequest = new List<StudyDesignEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns));

            List<StudyDesignEntity> mockCodeFromDatabase = new List<StudyDesignEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyDesignSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyDesignSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //963
        [Test]
        public void CheckForStudyIndicationsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<IndicationEntity> mockCodeFromRequest = new List<IndicationEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyIndications));

            List<IndicationEntity> mockCodeFromDatabase = new List<IndicationEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyIndications));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyIndications)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyIndicationsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyIndicationsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //995
        [Test]
        public void CheckForInvestigationalInterventionsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<InvestigationalInterventionEntity> mockCodeFromRequest = new List<InvestigationalInterventionEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions));

            List<InvestigationalInterventionEntity> mockCodeFromDatabase = new List<InvestigationalInterventionEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForInvestigationalInterventionsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForInvestigationalInterventionsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //1027-1030
        [Test]
        public void CheckForStudyDesignPopulationsSection_unitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDesignPopulationEntity> fromRequest = new List<StudyDesignPopulationEntity>();
            fromRequest = JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyPopulations));

            List<StudyDesignPopulationEntity> fromDatabase = new List<StudyDesignPopulationEntity>();
            fromDatabase = JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyPopulations));

            fromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyPopulations)));
            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyDesignPopulationsSection(fromRequest, fromDatabase);

            fromDatabase = null;
            result = helper.CheckForStudyDesignPopulationsSection(fromRequest, fromDatabase);
            Assert.AreEqual(2, result.Count);
        }


        //1060-1063
        [Test]
        public void CheckForStudyObjectivesSection_unitTetsting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<ObjectiveEntity> mockCodeFromRequest = new List<ObjectiveEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives));

            List<ObjectiveEntity> mockCodeFromDatabase = new List<ObjectiveEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives)));
            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyObjectivesSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyObjectivesSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

        }

        //1095
        [Test]
        public void CheckForStudyObjectivesEndpointsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<EndpointEntity> mockCodeFromRequest = new List<EndpointEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints));

            List<EndpointEntity> mockCodeFromDatabase = new List<EndpointEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyObjectivesEndpointsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyObjectivesEndpointsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //1132
        [Test]
        public void CheckForStudyCellsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyCellEntity> mockCodeFromRequest = new List<StudyCellEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));

            List<StudyCellEntity> mockCodeFromDatabase = new List<StudyCellEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase[0].StudyArm = null;
            mockCodeFromDatabase[0].StudyEpoch = null;
            mockCodeFromDatabase[0].StudyElements = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase[0].StudyArm.Uuid = null;
            mockCodeFromDatabase[0].StudyEpoch.Uuid = null;
            mockCodeFromRequest[0].StudyArm.Uuid = null;
            mockCodeFromRequest[0].StudyEpoch.Uuid = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
        }


        //1024
        [Test]
        public void CheckForStudyElementsSection_unitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyElementEntity> mockCodeFromRequest = new List<StudyElementEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements));

            List<StudyElementEntity> mockCodeFromDatabase = new List<StudyElementEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyElementsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyElementsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1254
        [Test]
        public void CheckForStudyDataCollectionSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDataCollectionEntity> mockCodeFromRequest = new List<StudyDataCollectionEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection));

            List<StudyDataCollectionEntity> mockCodeFromDatabase = new List<StudyDataCollectionEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyDataCollectionSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyDataCollectionSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1280
        [Test]
        public void CheckForStudyWorkflowSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<WorkflowEntity> mockCodeFromRequest = new List<WorkflowEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows));

            List<WorkflowEntity> mockCodeFromDatabase = new List<WorkflowEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyWorkflowSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyWorkflowSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1310
        [Test]
        public void CheckForStudyWorkflowItemsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<WorkFlowItemEntity> mockCodeFromRequest = new List<WorkFlowItemEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));

            List<WorkFlowItemEntity> mockCodeFromDatabase = new List<WorkFlowItemEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase[0].WorkflowItemEncounter = null;
            mockCodeFromDatabase[0].WorkflowItemActivity = null;

            result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase[0].WorkflowItemEncounter.Uuid = null;
            mockCodeFromRequest[0].WorkflowItemActivity.Uuid = null;
            mockCodeFromDatabase[0].WorkflowItemEncounter.Uuid = null;
            mockCodeFromRequest[0].WorkflowItemActivity.Uuid = null;

            result=helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest,mockCodeFromDatabase);

        }

        //1483
        [Test]
        public void CheckForDefinedProceduresSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<DefinedProcedureEntity> mockCodeFromRequest = new List<DefinedProcedureEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures));

            List<DefinedProcedureEntity> mockCodeFromDatabase = new List<DefinedProcedureEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForDefinedProceduresSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForDefinedProceduresSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }


        //1583
        [Test]
        public void CheckForStudyEstimandSection_unitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<EstimandEntity> mockcodefromRequest = new List<EstimandEntity>();
            mockcodefromRequest = JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands));


            List<EstimandEntity> mockFromTheDatabse = new List<EstimandEntity>();
            mockFromTheDatabse = JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands));

            mockcodefromRequest.AddRange(JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands)));
            HelperV2 helper = new HelperV2();
            var result = helper.CheckForStudyEstimandSection(mockcodefromRequest, mockFromTheDatabse);


            mockFromTheDatabse = null;
            result = helper.CheckForStudyEstimandSection(mockcodefromRequest, mockFromTheDatabse);
            Assert.AreEqual(2, result.Count);


        }


        //1618
        [Test]
        public void CheckForIntercurrentEventsSection_UnitTesting()
        {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<InterCurrentEventEntity> mockfromRequest = new List<InterCurrentEventEntity>();
            mockfromRequest = JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents));

            List<InterCurrentEventEntity> mockfromDatabase = new List<InterCurrentEventEntity>();
            mockfromDatabase = JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents));

            mockfromRequest.AddRange(JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForIntercurrentEventsSection(mockfromRequest, mockfromDatabase);

            mockfromDatabase = null;
            result = helper.CheckForIntercurrentEventsSection(mockfromRequest, mockfromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void CheckForEncounterListSection_UnitTesting()
            {
            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            List<EncounterEntity> mockCodeFromRequest= new List<EncounterEntity>();
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters));

            List<EncounterEntity> mockCodeFromDatabase = new List<EncounterEntity>();
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters)));

            HelperV2 helper = new HelperV2();
            var result = helper.CheckForEncounterListSection(mockCodeFromRequest, mockCodeFromDatabase);

            mockCodeFromDatabase = null;
            result = helper.CheckForEncounterListSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2,result.Count);

        }











        #endregion

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFiltersV2.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);

            SearchParameters searchParameters = new()
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
            filter = DataFiltersV2.GetFiltersForSearchStudy(searchParameters);
            Assert.IsNotNull(filter);
            SearchTitleParameters searchParameters1 = new()
            {               
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,              
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
                SortOrder = "asc",
                SortBy = "version"
            };
            Assert.IsNotNull(DataFiltersV2.GetProjectionForCheckAccessForAStudy());

            Assert.IsNotNull(DataFiltersV2.GetFiltersForChangeAudit("sd"));

            Assert.IsNotNull(DataFiltersV2.GetFiltersForSearchTitle(searchParameters1));

            Assert.IsNotNull(DataFiltersV2.GetFiltersForGetAudTrail("sd",DateTime.Now.AddDays(-1),DateTime.Now.AddDays(1)));

            Assert.IsNotNull(DataFiltersV2.GetFiltersForStudyHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1),"sd"));

            Assert.IsNotNull(DataFiltersV2.GetProjectionForPartialStudyElements(Constants.ClinicalStudyElements.Select(x=>x.ToLower()).ToArray()));

            Assert.IsNotNull(DataFiltersV2.GetProjectionForPartialStudyDesignElementsFullStudy());
        }
        #endregion

        #region Partial Study Elements
        [Test]
        public void AreValidStudyElementsUnitTesting()
        {
            HelperV2 helper = new HelperV2();
            var listofelements = string.Join(",", Constants.ClinicalStudyElements);
            Assert.IsTrue(helper.AreValidStudyElements(listofelements));
            Assert.IsFalse(helper.AreValidStudyElements("a,b"));
        }
        [Test]
        public void AreValidStudyDesignElementsUnitTesting()
        {
            HelperV2 helper = new HelperV2();
            var listofelements = string.Join(",", Constants.StudyDesignElements);
            Assert.IsTrue(helper.AreValidStudyDesignElements(listofelements));
            Assert.IsFalse(helper.AreValidStudyDesignElements("a,b"));
        }
        [Test]
        public void RemoveStudyElementsUnitTesting()
        {
            HelperV2 helper = new HelperV2();
            var stringArray = Constants.ClinicalStudyElements.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
            stringArray = Constants.ClinicalStudyElements.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
        }
        [Test]
        public void RemoveStudyDesignElementsUnitTesting()
        {
            HelperV2 helper = new HelperV2();
            var stringArray = Constants.ClinicalStudyElements.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().ClinicalStudy.StudyDesigns,"a"));
            stringArray = Constants.ClinicalStudyElements.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().ClinicalStudy.StudyDesigns, "a"));
        }
        #endregion
        #region Validation Classes
        [Test]
        public void ValidationDependenciesUnitTesting()
        {
            ValidationDependenciesV2.AddValidationDependenciesV2(serviceDescriptors);
            SearchParametersDto searchParameters = new()
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
            SearchParametersValidator searchValidationRules = new SearchParametersValidator();
            Assert.IsTrue((searchValidationRules.Validate(searchParameters)).IsValid);
        }
        #endregion

        #region Conformance V1 UnitTesting
        [Test]
        public void ConformanceV1UnitTesting()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>();
            ValidationDependenciesV2.AddValidationDependenciesV2(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity));
            Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].AnalysisPopulation));
            Assert.IsTrue(Validator<ClinicalStudyDto>(new ClinicalStudyValidator(httpContextAccessor), studyDto.ClinicalStudy));
            Assert.IsTrue(Validator<CodeDto>(new CodeValidator(), studyDto.ClinicalStudy.StudyPhase));
            Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter));
            Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0]));
            Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyIndications[0]));
            Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0]));
            Assert.IsTrue(Validator<InvestigationalInterventionDto>(new InvestigationalInterventionValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions[0]));
            Assert.IsTrue(Validator<DefinedProcedureDto>(new ProcedureValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures[0]));
            Assert.IsTrue(Validator<StudyArmDto>(new StudyArmValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyArm));
            Assert.IsTrue(Validator<StudyCellDto>(new StudyCellsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0]));
            Assert.IsTrue(Validator<StudyDataCollectionDto>(new StudyDataCollectionValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection[0]));
            Assert.IsTrue(Validator<StudyDesignPopulationDto>(new StudyDesignPopulationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyPopulations[0]));
            Assert.IsTrue(Validator<StudyDesignDto>(new StudyDesignValidator(), studyDto.ClinicalStudy.StudyDesigns[0]));
            Assert.IsTrue(Validator<StudyElementDto>(new StudyElementsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements[0]));
            Assert.IsTrue(Validator<StudyEpochDto>(new StudyEpochValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch));
            Assert.IsTrue(Validator<EstimandDto>(new StudyEstimandsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0]));
            Assert.IsTrue(Validator<StudyIdentifiersScopeDto>(new StudyIdentifierScopeValidator(), studyDto.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierScope));
            Assert.IsTrue(Validator<StudyIdentifierDto>(new StudyIdentifiersValidator(), studyDto.ClinicalStudy.StudyIdentifiers[0]));
            Assert.IsTrue(Validator<ObjectiveDto>(new StudyObjectiveValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0]));
            Assert.IsTrue(Validator<StudyProtocolVersionDto>(new StudyProtocolVersionsValidator(), studyDto.ClinicalStudy.StudyProtocolVersions[0]));
            Assert.IsTrue(Validator<StudyDto>(new StudyValidator(), studyDto));
            Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.TransitionStartRule));
            Assert.IsTrue(Validator<WorkflowDto>(new WorkflowValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0]));
            Assert.IsTrue(Validator<WorkflowItemDto>(new WorkflowItemValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0]));
        }

        public bool Validator<T>(AbstractValidator<T> validator,T value)
        {            
            return validator.Validate(value).IsValid;
        }

        #endregion

        #region UUID Conformance Helper
        [Test]
        public void UUIDConformanceValidationHelper_UnitTesting()
        {
            Assert.IsTrue(UUIDConformanceValidationHelper.CheckForUUIDConformance("123", "POST"));
            Assert.IsTrue(UUIDConformanceValidationHelper.CheckForUUIDConformance("123", "PUT"));
            Assert.IsFalse(UUIDConformanceValidationHelper.CheckForUUIDConformance("", "PUT"));
            Assert.IsFalse(UUIDConformanceValidationHelper.CheckForUUIDConformance(null, "PUT"));
            Assert.AreEqual(UUIDConformanceValidationHelper.GetMessageForUUIDConformance(""), Constants.ValidationErrorMessage.PropertyEmptyError);
            Assert.IsNotEmpty(UUIDConformanceValidationHelper.GetMessageForUUIDConformance(null), Constants.ValidationErrorMessage.PropertyMissingError);
        }
        #endregion

        #region UUID Conformance Helper
        [Test]
        public void UniquenessValidationHelper_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV2(studyDto.ClinicalStudy.StudyIdentifiers));
            studyDto.ClinicalStudy.StudyIdentifiers.Add(studyDto.ClinicalStudy.StudyIdentifiers[0]);
            Assert.IsFalse(UniquenessArrayValidator.ValidateArrayV2(studyDto.ClinicalStudy.StudyIdentifiers));
            studyDto.ClinicalStudy.StudyIdentifiers = null;
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV2(studyDto.ClinicalStudy.StudyIdentifiers));
        }
        #endregion

        #region ReferenceIntegrity
        [Test]
        public void RefernceIntegrity_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters.Add(JsonConvert.DeserializeObject<EncounterDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[0].Uuid = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[0].PreviousEncounterId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[0].NextEncounterId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[1].Uuid = "456";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[1].PreviousEncounterId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters[1].NextEncounterId = "123";


            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems.Add(JsonConvert.DeserializeObject<WorkflowItemDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].Uuid = "677";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].NextWorkflowItemId = "678";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].PreviousWorkflowItemId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].Uuid = "678";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].NextWorkflowItemId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].PreviousWorkflowItemId = "677";


         
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.Uuid = "777";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.NextActivityId = "778";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.PreviousActivityId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemActivity.Uuid = "778";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemActivity.NextActivityId = "678";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemActivity.PreviousActivityId = "777";

         
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.Uuid = "888";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.NextEncounterId = "889";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.PreviousEncounterId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemEncounter.Uuid = "889";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemEncounter.NextEncounterId = "778";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].WorkflowItemEncounter.PreviousEncounterId = "888";


            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells.Add(JsonConvert.DeserializeObject<StudyCellDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Uuid = "998";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.NextStudyEpochId = "999";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.PreviousStudyEpochId = "888";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.Uuid = "999";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.NextStudyEpochId = "888";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.PreviousStudyEpochId = "998";




            HelperV2 helper = new HelperV2();
            var result = helper.ReferenceIntegrityValidation(studyDto, out object referenceErrors);
            Assert.IsTrue(result);

        }
        #endregion
        #endregion
    }
}
