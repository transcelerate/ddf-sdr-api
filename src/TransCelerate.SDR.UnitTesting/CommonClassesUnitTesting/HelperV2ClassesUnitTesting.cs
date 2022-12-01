using NUnit.Framework;
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

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFiltersV2.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);
           
            Assert.IsNotNull(DataFiltersV2.GetProjectionForCheckAccessForAStudy());

            Assert.IsNotNull(DataFiltersV2.GetFiltersForChangeAudit("sd"));            

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
            Assert.IsTrue(helper.AreValidStudyElements(listofelements, out string[] listofelementsArray));
            Assert.IsFalse(helper.AreValidStudyElements("a,b", out listofelementsArray));
        }
        [Test]
        public void AreValidStudyDesignElementsUnitTesting()
        {
            HelperV2 helper = new HelperV2();
            var listofelements = string.Join(",", Constants.StudyDesignElements);
            Assert.IsTrue(helper.AreValidStudyDesignElements(listofelements, out string[] listofelementsArray));
            Assert.IsFalse(helper.AreValidStudyDesignElements("a,b", out listofelementsArray));
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

        #region Conformance V1 UnitTesting
        [Test]
        public void ConformanceV1UnitTesting()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>();
            ValidationDependenciesV2.AddValidationDependenciesV2(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0]));
            Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].AnalysisPopulation));
            Assert.IsTrue(Validator<ClinicalStudyDto>(new ClinicalStudyValidator(httpContextAccessor), studyDto.ClinicalStudy));
            Assert.IsTrue(Validator<CodeDto>(new CodeValidator(), studyDto.ClinicalStudy.StudyPhase));
            Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(), studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0]));
            Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0]));
            Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyIndications[0]));
            Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0]));
            Assert.IsTrue(Validator<InvestigationalInterventionDto>(new InvestigationalInterventionValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions[0]));
            Assert.IsTrue(Validator<ProcedureDto>(new ProcedureValidator(), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].DefinedProcedures[0]));
            Assert.IsTrue(Validator<StudyArmDto>(new StudyArmValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyArm));
            Assert.IsTrue(Validator<StudyCellDto>(new StudyCellsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0]));
            Assert.IsTrue(Validator<StudyDataDto>(new StudyDataCollectionValidator(), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].StudyDataCollection[0]));
            Assert.IsTrue(Validator<StudyDesignPopulationDto>(new StudyDesignPopulationValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyPopulations[0]));
            Assert.IsTrue(Validator<StudyDesignDto>(new StudyDesignValidator(), studyDto.ClinicalStudy.StudyDesigns[0]));
            Assert.IsTrue(Validator<StudyElementDto>(new StudyElementsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements[0]));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.NextStudyEpochId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.PreviousStudyEpochId= "124";
            Assert.IsTrue(Validator<StudyEpochDto>(new StudyEpochValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch));
            Assert.IsTrue(Validator<EstimandDto>(new StudyEstimandsValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0]));
            Assert.IsTrue(Validator<OrganisationDto>(new OrganisationValidator(), studyDto.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierScope));
            Assert.IsTrue(Validator<StudyIdentifierDto>(new StudyIdentifiersValidator(), studyDto.ClinicalStudy.StudyIdentifiers[0]));
            Assert.IsTrue(Validator<ObjectiveDto>(new StudyObjectiveValidator(), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0]));
            Assert.IsTrue(Validator<StudyProtocolVersionDto>(new StudyProtocolVersionsValidator(), studyDto.ClinicalStudy.StudyProtocolVersions[0]));
            Assert.IsTrue(Validator<StudyDto>(new StudyValidator(), studyDto));
            Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(), studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0].TransitionStartRule));
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

            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems.Add(JsonConvert.DeserializeObject<WorkflowItemDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].Id = "677";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].NextWorkflowItemId = "678";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[0].PreviousWorkflowItemId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].Id = "678";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].NextWorkflowItemId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyWorkflows[0].WorkflowItems[1].PreviousWorkflowItemId = "677";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells.Add(JsonConvert.DeserializeObject<StudyCellDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Id = "998";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.NextStudyEpochId = "999";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.PreviousStudyEpochId = "888";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.Id = "999";
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
