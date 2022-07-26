using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Core.Utilities.Common;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TransCelerate.SDR.RuleEngineV1;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.WebApi.DependencyInjection;
using FluentValidation;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV1ClassesUnitTesting
    {
        #region Variables
        private IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        #endregion
        #region Setup
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }

        public StudyDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyDto>(jsonData);
        }
        #endregion

        #region Test Cases
        #region HelperV1 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            ApplicationDependencyInjector.AddApplicationDependencies(serviceDescriptors);
            Helper helper = new Helper();
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
        }
        #endregion

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFilters.GetFiltersForGetStudy("1", 1);
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
            filter = DataFilters.GetFiltersForSearchStudy(searchParameters);
            Assert.IsNotNull(filter);
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
            SearchParametersValidator searchValidationRules = new SearchParametersValidator();
            Assert.IsTrue((searchValidationRules.Validate(searchParameters)).IsValid);
        }
        #endregion

        #region Conformance V1 UnitTesting
        [Test]
        public void ConformanceV1UnitTesting()
        {
            ValidationDependenciesV1.AddValidationDependenciesV1(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemActivity));
            Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].AnalysisPopulation));
            Assert.IsTrue(Validator<ClinicalStudyDto>(new ClinicalStudyValidator(), studyDto.ClinicalStudy));
            Assert.IsTrue(Validator<CodeDto>(new CodeValidator(), studyDto.ClinicalStudy.StudyPhase));
            Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter));
            Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0]));
            Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyIndications[0]));
            Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].InterCurrentEvents[0]));
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
            Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].WorkflowItemEncounter.StartRule));
            Assert.IsTrue(Validator<WorkflowDto>(new WorkflowValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0]));
            Assert.IsTrue(Validator<WorkflowItemDto>(new WorkflowItemValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0]));
        }

        public bool Validator<T>(AbstractValidator<T> validator,T value)
        {            
            return validator.Validate(value).IsValid;
        }

        #endregion
        #endregion
    }
}
