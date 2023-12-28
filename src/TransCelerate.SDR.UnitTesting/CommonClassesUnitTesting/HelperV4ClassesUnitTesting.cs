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
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV4;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngineV4;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV4ClassesUnitTesting
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
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsEntity>(jsonData);
        }

        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");
            return JsonConvert.DeserializeObject<StudyDefinitionsDto>(jsonData);
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
        #region HelperV3 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            HelperV4 helper = new();
            AuditTrailEntity auditTrailEntity = helper.GetAuditTrail(user.UserName);
            Assert.IsInstanceOf(typeof(DateTime), auditTrailEntity.EntryDateTime);
        }

        [Test]
        public void ApiBehaviourOptionsHelper()
        {
            ApiBehaviourOptionsHelper apiBehaviourOptionsHelper = new(_mockLogger);
            ActionContext context = new();
            var studyDto = GetDtoDataFromStaticJson();
            studyDto.Study = null;
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var contextAccessor = new DefaultHttpContext();
            var usdmVersion = Constants.USDMVersions.V3;
            contextAccessor.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(contextAccessor);


            StudyDefinitionsValidator studyDefinitionsValidator = new(httpContextAccessor.Object);
            var errors = studyDefinitionsValidator.Validate(studyDto).Errors;
            context.ModelState.AddModelError("study", errors[0].ErrorMessage);
            var response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);


            context.ModelState.Clear();
            studyDto = GetDtoDataFromStaticJson();
            studyDto.Study.Versions.FirstOrDefault().StudyTitle = null;

            StudyVersionValidator studyValidator = new(httpContextAccessor.Object);
            errors = studyValidator.Validate(studyDto.Study.Versions.FirstOrDefault()).Errors;
            context.ModelState.AddModelError("Conformance", errors[0].ErrorMessage);
            response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);
        }
        #endregion

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFiltersV3.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);

            Assert.IsNotNull(DataFiltersV3.GetProjectionForCheckAccessForAStudy());

            Assert.IsNotNull(DataFiltersV3.GetFiltersForChangeAudit("sd"));

            Assert.IsNotNull(DataFiltersV3.GetFiltersForGetAudTrail("sd", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));

            Assert.IsNotNull(DataFiltersV3.GetFiltersForStudyHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "sd"));

            Assert.IsNotNull(DataFiltersV3.GetProjectionForPartialStudyElements(Constants.StudyElementsV3.Select(x => x.ToLower()).ToArray()));

            Assert.IsNotNull(DataFiltersV3.GetProjectionForPartialStudyDesignElementsFullStudy());
        }
        #endregion

        #region Partial Study Elements
        [Test]
        public void AreValidStudyElementsUnitTesting()
        {
            HelperV4 helper = new();
            var listofelements = string.Join(",", Constants.StudyElementsV4);
            Assert.IsTrue(helper.AreValidStudyElements(listofelements, out string[] _));
            Assert.IsFalse(helper.AreValidStudyElements("a,b", out _));
        }
        [Test]
        public void AreValidStudyDesignElementsUnitTesting()
        {
            HelperV4 helper = new();
            var listofelements = string.Join(",", Constants.StudyDesignElementsV4);
            Assert.IsTrue(helper.AreValidStudyDesignElements(listofelements, out string[] _));
            Assert.IsFalse(helper.AreValidStudyDesignElements("a,b", out _));
        }
        [Test]
        public void RemoveStudyElementsUnitTesting()
        {
            HelperV4 helper = new();
            var stringArray = Constants.StudyElementsV4.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
            stringArray = Constants.StudyElementsV4.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
        }
        [Test]
        public void RemoveStudyDesignElementsUnitTesting()
        {
            HelperV4 helper = new();
            var stringArray = Constants.StudyElementsV4.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().Study.Versions.FirstOrDefault().StudyDesigns, "a"));
            stringArray = Constants.StudyElementsV4.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().Study.Versions.FirstOrDefault().StudyDesigns, "a"));
        }
        #endregion

        #region Conformance V3 UnitTesting
        [Test]
        public void ConformanceV4UnitTesting()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            var usdmVersion = Constants.USDMVersions.V3;
            context.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            ValidationDependenciesV4.AddValidationDependenciesV4(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();

            //Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Activities[0]));
            //Assert.IsTrue(Validator<AnalysisPopulationDto>(new AnalysisPopulationValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Estimands[0].AnalysisPopulation));
            //Assert.IsTrue(Validator<StudyVersionDto>(new StudyValidator(httpContextAccessor.Object), studyDto.Study));
            //Assert.IsTrue(Validator<CodeDto>(new CodeValidator(httpContextAccessor.Object), studyDto.Study.Type));
            //Assert.IsTrue(Validator<AliasCodeDto>(new AliasCodeValidator(httpContextAccessor.Object), studyDto.Study.StudyPhase));
            //Assert.IsTrue(Validator<EncounterDto>(new EncounterValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Encounters[0]));
            //Assert.IsTrue(Validator<EndpointDto>(new EndpointValidator(httpContextAccessor.Object), (studyDto.Study.StudyDesigns[0].Objectives[0] as ObjectiveDto).Endpoints[0] as EndpointDto));
            //Assert.IsTrue(Validator<IndicationDto>(new IndicationValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Indications[0]));
            //Assert.IsTrue(Validator<InterCurrentEventDto>(new InterCurrentEventsValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Estimands[0].IntercurrentEvents[0]));
            //Assert.IsTrue(Validator<StudyInterventionDto>(new StudyInterventionValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].StudyInterventions[0]));
            //Assert.IsTrue(Validator<ProcedureDto>(new ProcedureValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Activities[0].DefinedProcedures[0]));            
            //Assert.IsTrue(Validator<StudyCellDto>(new StudyCellsValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].StudyCells[0]));
            //Assert.IsTrue(Validator<StudyDesignPopulationDto>(new StudyDesignPopulationValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Populations[0]));
            //Assert.IsTrue(Validator<StudyDesignDto>(new StudyDesignValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0]));
            //Assert.IsTrue(Validator<EstimandDto>(new StudyEstimandsValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Estimands[0]));
            //Assert.IsTrue(Validator<OrganisationDto>(new OrganisationValidator(httpContextAccessor.Object), studyDto.Study.StudyIdentifiers[0].StudyIdentifierScope));
            //Assert.IsTrue(Validator<StudyIdentifierDto>(new StudyIdentifiersValidator(httpContextAccessor.Object), studyDto.Study.StudyIdentifiers[0]));
            //Assert.IsTrue(Validator<ObjectiveDto>(new ObjectiveValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Objectives[0] as ObjectiveDto));
            //Assert.IsTrue(Validator<StudyProtocolDocumentVersionDto>(new StudyProtocolVersionsValidator(httpContextAccessor.Object), studyDto.Study.StudyProtocolVersions[0]));
            //Assert.IsTrue(Validator<StudyDefinitionsDto>(new StudyDefinitionsValidator(httpContextAccessor.Object), studyDto));
            //Assert.IsTrue(Validator<TransitionRuleDto>(new TransitionRuleValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Encounters[0].TransitionStartRule));
            //Assert.IsTrue(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Activities[0]));
            //Assert.IsTrue(Validator<BiomedicalConceptDto>(new BiomedicalConceptValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].BiomedicalConcepts[0]));
            //Assert.IsTrue(Validator<BiomedicalConceptCategoryDto>(new BiomedicalConceptCategoryValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].BcCategories[0]));
            //Assert.IsTrue(Validator<BiomedicalConceptPropertyDto>(new BiomedicalConceptPropertyValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].BiomedicalConcepts[0].Properties[0]));
            //Assert.IsTrue(Validator<BiomedicalConceptSurrogateDto>(new BiomedicalConceptSurrogateValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].BcSurrogates[0]));
            //Assert.IsTrue(Validator<ResponseCodeDto>(new ResponseCodeValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].BiomedicalConcepts[0].Properties[0].ResponseCodes[0]));
            //Assert.IsTrue(Validator<ScheduleTimelineDto>(new ScheduleTimelinesValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].ScheduleTimelines[0]));
            //Assert.IsTrue(Validator<ScheduledInstanceDto>(new ScheduledInstanceValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].ScheduleTimelines[0].Instances[0]));
            //Assert.IsTrue(Validator<ScheduledInstanceDto>(new ScheduledInstanceValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].ScheduleTimelines[0].Instances[1]));
            //Assert.IsTrue(Validator<TimingDto>(new TimingValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].ScheduleTimelines[0].Instances[0].Timings[0]));
            //Assert.IsTrue(Validator<ScheduleTimelineExitDto>(new ScheduleTimelineExitValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].ScheduleTimelines[0].Exits[0]));
            //studyDto.Study.StudyDesigns[0].Activities[0].IsConditional = true;
            //studyDto.Study.StudyDesigns[0].Activities[1].IsConditional = "entity";
            //studyDto.Study.StudyDesigns[0].Activities[0].DefinedProcedures[0].IsConditional = true;
            //studyDto.Study.StudyDesigns[0].Activities[1].DefinedProcedures[0].IsConditional = "12";
            //Assert.IsFalse(Validator<ActivityDto>(new ActivityValidator(httpContextAccessor.Object), studyDto.Study.StudyDesigns[0].Activities[1]));
            //Assert.IsTrue(Validator<AddressDto>(new AddressValidator(httpContextAccessor.Object), studyDto.Study.StudyIdentifiers[0].StudyIdentifierScope.OrganizationLegalAddress));
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
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV4(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
            studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers.Add(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers[0]);
            Assert.IsFalse(UniquenessArrayValidator.ValidateArrayV4(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
            studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers = null;
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV4(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
            Assert.IsTrue(UniquenessArrayValidator.ValidateStringList(new List<string>()));
        }
        #endregion

        #region ReferenceIntegrity
        [Test]
        public void RefernceIntegrity_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();

            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].StudyCells.Add(JsonConvert.DeserializeObject<StudyCellDto>(JsonConvert.SerializeObject(studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].StudyCells[0])));
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities.Add(JsonConvert.DeserializeObject<ActivityDto>(JsonConvert.SerializeObject(studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[0])));
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[0].Id = "123";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[0].NextId = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[0].PreviousId = "127";
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[1].Id = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[1].NextId = "234";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Activities[1].PreviousId = "123";
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters.Add(JsonConvert.DeserializeObject<EncounterDto>(JsonConvert.SerializeObject(studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[0])));
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[0].Id = "123";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[0].NextId = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[0].PreviousId = "127";
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[1].Id = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[1].NextId = "234";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Encounters[1].PreviousId = "123";
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands.Add(JsonConvert.DeserializeObject<EstimandDto>(JsonConvert.SerializeObject(studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0])));
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].Id = "123";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].TreatmentId = "124";
                          
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].Id = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].TreatmentId = "124";

            HelperV4 helper = new();
            var result = helper.ReferenceIntegrityValidation(studyDto, out object _);
            Assert.IsTrue(result);

        }
        #endregion

        #endregion
    }
}
