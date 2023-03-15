using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngineV2;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.DependencyInjection;

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
        [SetUp]
        public void Setup()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ConformanceRules.json");
            ConformanceNonStatic conformanceNonStatic = JsonConvert.DeserializeObject<ConformanceNonStatic>(jsonData);
            Conformance.ConformanceRules = conformanceNonStatic.ConformanceRules;
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
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var contextAccessor = new DefaultHttpContext();
            var usdmVersion = Constants.USDMVersions.V2;
            contextAccessor.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(contextAccessor);


            StudyValidator studyValidator = new StudyValidator(httpContextAccessor.Object);
            var errors = studyValidator.Validate(studyDto).Errors;
            context.ModelState.AddModelError("clinicalStudy", errors[0].ErrorMessage);            
            var response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);


            context.ModelState.Clear();
            studyDto = GetDtoDataFromStaticJson();
            studyDto.ClinicalStudy.StudyTitle = null;
            
            ClinicalStudyValidator clinicalStudyValidator = new ClinicalStudyValidator(httpContextAccessor.Object);
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

        #region Conformance V2 UnitTesting
        [Test]
        public void ConformanceV2UnitTesting()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            var usdmVersion = Constants.USDMVersions.V2;
            context.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            ValidationDependenciesV2.AddValidationDependenciesV2(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0]));
            Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].AnalysisPopulation));
            Assert.IsTrue(Validator<ClinicalStudyDto>(new ClinicalStudyValidator(httpContextAccessor.Object), studyDto.ClinicalStudy));
            Assert.IsTrue(Validator<CodeDto>(new CodeValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyType));
            Assert.IsTrue(Validator<AliasCodeDto>(new AliasCodeValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyPhase));
            Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0]));
            Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0].ObjectiveEndpoints[0]));
            Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyIndications[0]));
            Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].IntercurrentEvents[0]));
            Assert.IsTrue(Validator<InvestigationalInterventionDto>(new InvestigationalInterventionValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyInvestigationalInterventions[0]));
            Assert.IsTrue(Validator<ProcedureDto>(new ProcedureValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].DefinedProcedures[0]));
            Assert.IsTrue(Validator<StudyArmDto>(new StudyArmValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyArm));
            Assert.IsTrue(Validator<StudyCellDto>(new StudyCellsValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0]));            
            Assert.IsTrue(Validator<StudyDesignPopulationDto>(new StudyDesignPopulationValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyPopulations[0]));
            Assert.IsTrue(Validator<StudyDesignDto>(new StudyDesignValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0]));
            Assert.IsTrue(Validator<StudyElementDto>(new StudyElementsValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyElements[0]));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.NextStudyEpochId = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.PreviousStudyEpochId = "124";
            Assert.IsTrue(Validator<StudyEpochDto>(new StudyEpochValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch));
            Assert.IsTrue(Validator<EstimandDto>(new StudyEstimandsValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0]));
            Assert.IsTrue(Validator<OrganisationDto>(new OrganisationValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierScope));
            Assert.IsTrue(Validator<StudyIdentifierDto>(new StudyIdentifiersValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyIdentifiers[0]));
            Assert.IsTrue(Validator<ObjectiveDto>(new ObjectiveValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyObjectives[0]));
            Assert.IsTrue(Validator<StudyProtocolVersionDto>(new StudyProtocolVersionsValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyProtocolVersions[0]));
            Assert.IsTrue(Validator<StudyDto>(new StudyValidator(httpContextAccessor.Object), studyDto));
            Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0].TransitionStartRule));            
            Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Activities[0]));
            Assert.IsTrue(Validator<BiomedicalConceptDto>(new BiomedicalConceptValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].BiomedicalConcepts[0]));
            Assert.IsTrue(Validator<BiomedicalConceptCategoryDto>(new BiomedicalConceptCategoryValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].BcCategories[0]));
            Assert.IsTrue(Validator<BiomedicalConceptPropertyDto>(new BiomedicalConceptPropertyValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].BiomedicalConcepts[0].BcProperties[0]));
            Assert.IsTrue(Validator<BiomedicalConceptSurrogateDto>(new BiomedicalConceptSurrogateValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].BcSurrogates[0]));
            Assert.IsTrue(Validator<ResponseCodeDto>(new ResponseCodeValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].BiomedicalConcepts[0].BcProperties[0].BcPropertyResponseCodes[0]));                
            Assert.IsTrue(Validator<ScheduleTimelineDto>(new ScheduleTimelinesValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0]));                
            Assert.IsTrue(Validator<ScheduledInstanceDto>(new ScheduledInstanceValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0].ScheduledTimelineInstances[0]));                
            Assert.IsTrue(Validator<ScheduledInstanceDto>(new ScheduledInstanceValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0].ScheduledTimelineInstances[1]));                
            Assert.IsTrue(Validator<TimingDto>(new TimingValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0].ScheduledTimelineInstances[0].ScheduledInstanceTimings[0]));                
            Assert.IsTrue(Validator<ScheduleTimelineExitDto>(new ScheduleTimelineExitValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].StudyScheduleTimelines[0].ScheduleTimelineExits[0]));                
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].ActivityIsConditional = true;
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[1].ActivityIsConditional = "entity";
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].DefinedProcedures[0].ProcedureIsConditional = true;
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[1].DefinedProcedures[0].ProcedureIsConditional = "12";
            Assert.IsFalse(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyDesigns[0].Activities[1]));
            Assert.IsTrue(Validator<AddressDto>(new AddressValidator(httpContextAccessor.Object), studyDto.ClinicalStudy.StudyIdentifiers[0].StudyIdentifierScope.OrganizationLegalAddress));
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
            Assert.IsTrue(UUIDConformanceValidationHelper.CheckForUUIDConformance("123", "POST",Route.PostElementsV2));
            Assert.IsTrue(UUIDConformanceValidationHelper.CheckForUUIDConformance("123", "PUT", Route.PostElementsV2));
            Assert.IsFalse(UUIDConformanceValidationHelper.CheckForUUIDConformance("", "PUT", Route.PostElementsV2));
            Assert.IsFalse(UUIDConformanceValidationHelper.CheckForUUIDConformance(null, "PUT", Route.PostElementsV2));
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
            Assert.IsTrue(UniquenessArrayValidator.ValidateStringList(new List<string>()));
        }
        #endregion

        #region ReferenceIntegrity
        [Test]
        public void RefernceIntegrity_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();            

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells.Add(JsonConvert.DeserializeObject<StudyCellDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.Id = "998";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.NextStudyEpochId = "999";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[0].StudyEpoch.PreviousStudyEpochId = "888";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.Id = "999";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.NextStudyEpochId = "888";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyCells[1].StudyEpoch.PreviousStudyEpochId = "998";

            studyDto.ClinicalStudy.StudyDesigns[0].Activities.Add(JsonConvert.DeserializeObject<ActivityDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].Activities[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].Id = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].NextActivityId = "124";
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[0].PreviousActivityId = "127";

            studyDto.ClinicalStudy.StudyDesigns[0].Activities[1].Id = "124";
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[1].NextActivityId = "234";
            studyDto.ClinicalStudy.StudyDesigns[0].Activities[1].PreviousActivityId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].Encounters.Add(JsonConvert.DeserializeObject<EncounterDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0].Id = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0].NextEncounterId = "124";
            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[0].PreviousEncounterId = "127";

            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[1].Id = "124";
            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[1].NextEncounterId = "234";
            studyDto.ClinicalStudy.StudyDesigns[0].Encounters[1].PreviousEncounterId = "123";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands.Add(JsonConvert.DeserializeObject<EstimandDto>(JsonConvert.SerializeObject(studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0])));
            studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].Id = "123";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].Treatment = "124";

            studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].Id = "124";
            studyDto.ClinicalStudy.StudyDesigns[0].StudyEstimands[0].Treatment = "124";

            HelperV2 helper = new HelperV2();
            var result = helper.ReferenceIntegrityValidation(studyDto, out object referenceErrors);
            Assert.IsTrue(result);

        }
        #endregion

        #region Make Ids Null UnitTesting
        [Test]
        public void RemoveIdsUnitTesting()
        {
            var study = GetEntityDataFromStaticJson();
            HelperV2 helperV2 = new HelperV2();
            helperV2.RemovedSectionId(study);
            Assert.IsNull(study.ClinicalStudy.StudyIdentifiers[0].Id);
        }
        #endregion
        #endregion
    }
}
