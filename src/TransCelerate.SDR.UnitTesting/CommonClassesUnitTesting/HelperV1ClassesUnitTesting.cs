using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngineV1;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV1ClassesUnitTesting
    {
        #region Variables
        private readonly IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        #endregion
        #region Setup
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public static StudyDefinitionsEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsEntity>(jsonData);
        }

        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
        }
        #endregion

        #region Test Cases
        #region HelperV1 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            HelperV1 helper = new();
            AuditTrailEntity auditTrailEntity = helper.GetAuditTrail(user.UserName);
            Assert.IsInstanceOf(typeof(DateTime), auditTrailEntity.EntryDateTime);

            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            studyEntity = helper.GeneratedSectionId(studyEntity);
            Assert.IsNotNull(studyEntity);

            StudyDefinitionsEntity studyEntity1 = GetEntityDataFromStaticJson();
            StudyDefinitionsEntity studyEntity2 = GetEntityDataFromStaticJson();
            var isSameStudy = helper.IsSameStudy(studyEntity1, studyEntity2);
            Assert.IsTrue(isSameStudy);

            studyEntity = helper.CheckForSections(studyEntity1, studyEntity2);
            Assert.IsNotNull(studyEntity);
            studyEntity2.Study.StudyType = null;
            studyEntity2.Study.StudyPhase = null;


            studyEntity = helper.CheckForSections(studyEntity1, studyEntity2);
            Assert.IsNotNull(studyEntity);

        }

        [Test]
        public void ApiBehaviourOptionsHelper()
        {
            ApiBehaviourOptionsHelper apiBehaviourOptionsHelper = new(_mockLogger);
            ActionContext context = new();
            var studyDto = GetDtoDataFromStaticJson();
            studyDto.Study = null;
            StudyDefinitionsValidator studyDefinitionsValidator = new();
            var errors = studyDefinitionsValidator.Validate(studyDto).Errors;
            context.ModelState.AddModelError("study", errors[0].ErrorMessage);
            var response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);


            context.ModelState.Clear();
            studyDto = GetDtoDataFromStaticJson();
            studyDto.Study.StudyTitle = null;
            StudyValidator studyValidator = new();
            errors = studyValidator.Validate(studyDto.Study).Errors;
            context.ModelState.AddModelError("Conformance", errors[0].ErrorMessage);
            response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);
        }
        #endregion

        #region Individual Study Elements Unit Testing
        [Test]
        public void CheckForCodeSection_unitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson(); //this is  json file

            List<CodeEntity> mockCodefromSomeRequest = JsonConvert.DeserializeObject<List<CodeEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].InterventionModel));
            Assert.IsNotNull(mockCodefromSomeRequest);
            List<CodeEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<CodeEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].InterventionModel));
            Assert.IsNotNull(mockCodeFromDatabase);
            mockCodefromSomeRequest.Add(studyEntity.Study.StudyType);
            HelperV1 helper = new();
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
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyIdentifierEntity> mockFromRequest = JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyIdentifiers));

            List<StudyIdentifierEntity> mockfromdatabase = JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyIdentifiers));

            mockFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyIdentifierEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyIdentifiers)));
            HelperV1 helper = new();
            var result = helper.CheckForStudyIdentifierSection(mockFromRequest, mockfromdatabase);
            Assert.IsNotNull(result);

            mockfromdatabase = null;
            result = helper.CheckForStudyIdentifierSection(mockFromRequest, mockfromdatabase);
            Assert.AreEqual(4, result.Count);
        }

        //852
        [Test]
        public void CheckForStudyProtocolSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyProtocolVersionEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyProtocolVersions));

            List<StudyProtocolVersionEntity> mockcodeFromDatabase = JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyProtocolVersions));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyProtocolVersionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyProtocolVersions)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyProtocolSection(mockCodeFromRequest, mockcodeFromDatabase);
            Assert.IsNotNull(result);
            mockcodeFromDatabase = null;
            result = helper.CheckForStudyProtocolSection(mockCodeFromRequest, mockcodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }


        //931
        [Test]
        public void CheckForStudyDesignSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDesignEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns));

            List<StudyDesignEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDesignEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyDesignSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyDesignSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //963
        [Test]
        public void CheckForStudyIndicationsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<IndicationEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyIndications));

            List<IndicationEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyIndications));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<IndicationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyIndications)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyIndicationsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyIndicationsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //995
        [Test]
        public void CheckForInvestigationalInterventionsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<InvestigationalInterventionEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyInvestigationalInterventions));

            List<InvestigationalInterventionEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyInvestigationalInterventions));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<InvestigationalInterventionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyInvestigationalInterventions)));

            HelperV1 helper = new();
            var result = helper.CheckForInvestigationalInterventionsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForInvestigationalInterventionsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //1027-1030
        [Test]
        public void CheckForStudyDesignPopulationsSection_unitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDesignPopulationEntity> fromRequest = JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyPopulations));

            List<StudyDesignPopulationEntity> fromDatabase = JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyPopulations));

            fromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDesignPopulationEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyPopulations)));
            HelperV1 helper = new();
            var result = helper.CheckForStudyDesignPopulationsSection(fromRequest, fromDatabase);
            Assert.IsNotNull(result);
            fromDatabase = null;
            result = helper.CheckForStudyDesignPopulationsSection(fromRequest, fromDatabase);
            Assert.AreEqual(2, result.Count);
        }


        //1060-1063
        [Test]
        public void CheckForStudyObjectivesSection_unitTetsting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<ObjectiveEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives));

            List<ObjectiveEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<ObjectiveEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives)));
            HelperV1 helper = new();
            var result = helper.CheckForStudyObjectivesSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyObjectivesSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

        }

        //1095
        [Test]
        public void CheckForStudyObjectivesEndpointsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<EndpointEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints));

            List<EndpointEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<EndpointEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyObjectivesEndpointsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyObjectivesEndpointsSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }

        //1132
        [Test]
        public void CheckForStudyCellsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyCellEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));

            List<StudyCellEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase[0].StudyArm = null;
            mockCodeFromDatabase[0].StudyEpoch = null;
            mockCodeFromDatabase[0].StudyElements = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyCellEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells));
            mockCodeFromDatabase[0].StudyArm.Uuid = null;
            mockCodeFromDatabase[0].StudyEpoch.Uuid = null;
            mockCodeFromRequest[0].StudyArm.Uuid = null;
            mockCodeFromRequest[0].StudyEpoch.Uuid = null;
            result = helper.CheckForStudyCellsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
        }


        //1024
        [Test]
        public void CheckForStudyElementsSection_unitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyElementEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyElements));

            List<StudyElementEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyElements));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyElementEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyElements)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyElementsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyElementsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1254
        [Test]
        public void CheckForStudyDataCollectionSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<StudyDataCollectionEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection));

            List<StudyDataCollectionEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<StudyDataCollectionEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyDataCollectionSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyDataCollectionSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1280
        [Test]
        public void CheckForStudyWorkflowSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<WorkflowEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows));

            List<WorkflowEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<WorkflowEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyWorkflowSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyWorkflowSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        //1310
        [Test]
        public void CheckForStudyWorkflowItemsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<WorkFlowItemEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));

            List<WorkFlowItemEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems)));

            HelperV1 helper = new();
            var result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase[0].WorkflowItemEncounter = null;
            mockCodeFromDatabase[0].WorkflowItemActivity = null;

            result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromRequest = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase = JsonConvert.DeserializeObject<List<WorkFlowItemEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems));
            mockCodeFromDatabase[0].WorkflowItemEncounter.Uuid = null;
            mockCodeFromRequest[0].WorkflowItemActivity.Uuid = null;
            mockCodeFromDatabase[0].WorkflowItemEncounter.Uuid = null;
            mockCodeFromRequest[0].WorkflowItemActivity.Uuid = null;

            result = helper.CheckForStudyWorkflowItemsSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
        }

        //1483
        [Test]
        public void CheckForDefinedProceduresSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<DefinedProcedureEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures));

            List<DefinedProcedureEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<DefinedProcedureEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures)));

            HelperV1 helper = new();
            var result = helper.CheckForDefinedProceduresSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForDefinedProceduresSection(mockCodeFromRequest, mockCodeFromDatabase);

            Assert.AreEqual(2, result.Count);
        }


        //1583
        [Test]
        public void CheckForStudyEstimandSection_unitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<EstimandEntity> mockcodefromRequest = JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands));


            List<EstimandEntity> mockFromTheDatabse = JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands));

            mockcodefromRequest.AddRange(JsonConvert.DeserializeObject<List<EstimandEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands)));
            HelperV1 helper = new();
            var result = helper.CheckForStudyEstimandSection(mockcodefromRequest, mockFromTheDatabse);

            Assert.IsNotNull(result);
            mockFromTheDatabse = null;
            result = helper.CheckForStudyEstimandSection(mockcodefromRequest, mockFromTheDatabse);
            Assert.AreEqual(2, result.Count);


        }


        //1618
        [Test]
        public void CheckForIntercurrentEventsSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<InterCurrentEventEntity> mockfromRequest = JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents));

            List<InterCurrentEventEntity> mockfromDatabase = JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents));

            mockfromRequest.AddRange(JsonConvert.DeserializeObject<List<InterCurrentEventEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents)));

            HelperV1 helper = new();
            var result = helper.CheckForIntercurrentEventsSection(mockfromRequest, mockfromDatabase);
            Assert.IsNotNull(result);
            mockfromDatabase = null;
            result = helper.CheckForIntercurrentEventsSection(mockfromRequest, mockfromDatabase);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void CheckForEncounterListSection_UnitTesting()
        {
            StudyDefinitionsEntity studyEntity = GetEntityDataFromStaticJson();

            List<EncounterEntity> mockCodeFromRequest = JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters));

            List<EncounterEntity> mockCodeFromDatabase = JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters));

            mockCodeFromRequest.AddRange(JsonConvert.DeserializeObject<List<EncounterEntity>>(JsonConvert.SerializeObject(studyEntity.Study.StudyDesigns[0].StudyCells[0].StudyEpoch.Encounters)));

            HelperV1 helper = new();
            var result = helper.CheckForEncounterListSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.IsNotNull(result);
            mockCodeFromDatabase = null;
            result = helper.CheckForEncounterListSection(mockCodeFromRequest, mockCodeFromDatabase);
            Assert.AreEqual(2, result.Count);

        }











        #endregion

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFiltersV1.GetFiltersForGetStudy("1", 1);
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
            filter = DataFiltersV1.GetFiltersForSearchStudy(searchParameters);
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
            Assert.IsNotNull(DataFiltersV1.GetProjectionForCheckAccessForAStudy());

            Assert.IsNotNull(DataFiltersV1.GetFiltersForSearchTitle(searchParameters1));

            Assert.IsNotNull(DataFiltersV1.GetFiltersForGetAudTrail("sd", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));

            Assert.IsNotNull(DataFiltersV1.GetFiltersForStudyHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "sd"));

            Assert.IsNotNull(DataFiltersV1.GetProjectionForPartialStudyElements(Constants.StudyElementsV2.Select(x => x.ToLower()).ToArray()));

            Assert.IsNotNull(DataFiltersV1.GetProjectionForPartialStudyDesignElementsFullStudy());
        }
        #endregion

        #region Validation Classes
        [Test]
        public void ValidationDependenciesUnitTesting()
        {
            ValidationDependenciesV1.AddValidationDependenciesV1(serviceDescriptors);
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
            SearchParametersValidator searchValidationRules = new();
            Assert.IsTrue((searchValidationRules.Validate(searchParameters)).IsValid);
        }
        #endregion

        #region Conformance V1 UnitTesting
        [Test]
        public void ConformanceV1UnitTesting()
        {
            ValidationDependenciesV1.AddValidationDependenciesV1(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity));
            Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(), studyDto.Study.StudyDesigns[0].StudyEstimands[0].AnalysisPopulation));
            Assert.IsTrue(Validator<StudyDto>(new StudyValidator(), studyDto.Study));
            Assert.IsTrue(Validator<CodeDto>(new CodeValidator(), studyDto.Study.StudyPhase));
            Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter));
            Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(), studyDto.Study.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0]));
            Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(), studyDto.Study.StudyDesigns[0].StudyIndications[0]));
            Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(), studyDto.Study.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0]));
            Assert.IsTrue(Validator<InvestigationalInterventionDto>(new InvestigationalInterventionValidator(), studyDto.Study.StudyDesigns[0].StudyInvestigationalInterventions[0]));
            Assert.IsTrue(Validator<DefinedProcedureDto>(new ProcedureValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.DefinedProcedures[0]));
            Assert.IsTrue(Validator<StudyArmDto>(new StudyArmValidator(), studyDto.Study.StudyDesigns[0].StudyCells[0].StudyArm));
            Assert.IsTrue(Validator<StudyCellDto>(new StudyCellsValidator(), studyDto.Study.StudyDesigns[0].StudyCells[0]));
            Assert.IsTrue(Validator<StudyDataCollectionDto>(new StudyDataCollectionValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity.StudyDataCollection[0]));
            Assert.IsTrue(Validator<StudyDesignPopulationDto>(new StudyDesignPopulationValidator(), studyDto.Study.StudyDesigns[0].StudyPopulations[0]));
            Assert.IsTrue(Validator<StudyDesignDto>(new StudyDesignValidator(), studyDto.Study.StudyDesigns[0]));
            Assert.IsTrue(Validator<StudyElementDto>(new StudyElementsValidator(), studyDto.Study.StudyDesigns[0].StudyCells[0].StudyElements[0]));
            Assert.IsTrue(Validator<StudyEpochDto>(new StudyEpochValidator(), studyDto.Study.StudyDesigns[0].StudyCells[0].StudyEpoch));
            Assert.IsTrue(Validator<EstimandDto>(new StudyEstimandsValidator(), studyDto.Study.StudyDesigns[0].StudyEstimands[0]));
            Assert.IsTrue(Validator<StudyIdentifiersScopeDto>(new StudyIdentifierScopeValidator(), studyDto.Study.StudyIdentifiers[0].StudyIdentifierScope));
            Assert.IsTrue(Validator<StudyIdentifierDto>(new StudyIdentifiersValidator(), studyDto.Study.StudyIdentifiers[0]));
            Assert.IsTrue(Validator<ObjectiveDto>(new StudyObjectiveValidator(), studyDto.Study.StudyDesigns[0].StudyObjectives[0]));
            Assert.IsTrue(Validator<StudyProtocolVersionDto>(new StudyProtocolVersionsValidator(), studyDto.Study.StudyProtocolVersions[0]));
            Assert.IsTrue(Validator<StudyDefinitionsDto>(new StudyDefinitionsValidator(), studyDto));
            Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.TransitionStartRule));
            Assert.IsTrue(Validator<WorkflowDto>(new WorkflowValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0]));
            Assert.IsTrue(Validator<WorkflowItemDto>(new WorkflowItemValidator(), studyDto.Study.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0]));
        }

        public static bool Validator<T>(AbstractValidator<T> validator, T value)
        {
            return validator.Validate(value).IsValid;
        }

        #endregion

        #region UUID Conformance Helper
        [Test]
        public void UniquenessValidationHelper_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV1(studyDto.Study.StudyIdentifiers));
            studyDto.Study.StudyIdentifiers.Add(studyDto.Study.StudyIdentifiers[0]);
            Assert.IsFalse(UniquenessArrayValidator.ValidateArrayV1(studyDto.Study.StudyIdentifiers));
            studyDto.Study.StudyIdentifiers = null;
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV1(studyDto.Study.StudyIdentifiers));
        }
        #endregion
        #endregion
    }
}
