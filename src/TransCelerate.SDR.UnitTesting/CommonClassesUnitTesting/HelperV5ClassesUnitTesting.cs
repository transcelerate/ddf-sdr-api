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
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV5;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.RuleEngine.Common;
using TransCelerate.SDR.RuleEngine.StudyV5Rules;
using TransCelerate.SDR.RuleEngineV5;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV5ClassesUnitTesting
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

        public static StudyDefinitionsDto GetDtoDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
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
        #region HelperV5 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            HelperV5 helper = new();
            AuditTrailEntity auditTrailEntity = helper.GetAuditTrail(user.UserName, Constants.USDMVersions.V4);
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
            var usdmVersion = Constants.USDMVersions.V4;
            contextAccessor.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(contextAccessor);


            StudyDefinitionsValidator studyDefinitionsValidator = new(httpContextAccessor.Object);
            var errors = studyDefinitionsValidator.Validate(studyDto).Errors;
            context.ModelState.AddModelError("study", errors[0].ErrorMessage);
            var response = apiBehaviourOptionsHelper.ModelStateResponse(context);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), response);


            context.ModelState.Clear();
            studyDto = GetDtoDataFromStaticJson();
            studyDto.Study.Versions.FirstOrDefault().Titles = null;

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
            var filter = DataFiltersV5.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);

            Assert.IsNotNull(DataFiltersV5.GetProjectionForCheckAccessForAStudy());

            Assert.IsNotNull(DataFiltersV5.GetProjectionForPartialStudyElements(Constants.StudyElementsV5.Select(x => x.ToLower()).ToArray()));

            Assert.IsNotNull(DataFiltersV5.GetProjectionForPartialStudyDesignElementsFullStudy());
        }
        #endregion

        #region Partial Study Elements
        [Test]
        public void AreValidStudyElementsUnitTesting()
        {
            HelperV5 helper = new();
            var listofelements = string.Join(",", Constants.StudyElementsV5);
            Assert.IsTrue(helper.AreValidStudyElements(listofelements, out string[] _));
            Assert.IsFalse(helper.AreValidStudyElements("a,b", out _));
        }
        [Test]
        public void AreValidStudyDesignElementsUnitTesting()
        {
            HelperV5 helper = new();
            var listofelements = string.Join(",", Constants.StudyDesignElementsV5);
            Assert.IsTrue(helper.AreValidStudyDesignElements(listofelements, out string[] _));
            Assert.IsFalse(helper.AreValidStudyDesignElements("a,b", out _));
        }
        [Test]
        public void RemoveStudyElementsUnitTesting()
        {
            HelperV5 helper = new();
            var stringArray = Constants.StudyElementsV5.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
            stringArray = Constants.StudyElementsV5.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyElements(stringArray, GetDtoDataFromStaticJson()));
        }
        [Test]
        public void RemoveStudyDesignElementsUnitTesting()
        {
            HelperV5 helper = new();
            var stringArray = Constants.StudyElementsV5.Where(x => x.StartsWith("s")).ToArray();

            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().Study.Versions.FirstOrDefault().StudyDesigns, "a"));
            stringArray = Constants.StudyElementsV5.Where(x => !x.StartsWith("s")).ToArray();
            Assert.IsNotNull(helper.RemoveStudyDesignElements(stringArray, GetDtoDataFromStaticJson().Study.Versions.FirstOrDefault().StudyDesigns, "a"));
        }
        #endregion

        #region Conformance V4 UnitTesting
        [Test]
        public void ConformanceV5UnitTesting()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            var usdmVersion = Constants.USDMVersions.V4;
            context.Request.Headers["usdmVersion"] = usdmVersion;
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            ValidationDependenciesV5.AddValidationDependenciesV5(serviceDescriptors);
            var studyDto = GetDtoDataFromStaticJson();
        }

        #endregion

        #region UUID Conformance Helper
        [Test]
        public void UniquenessValidationHelper_UnitTesting()
        {
            var studyDto = GetDtoDataFromStaticJson();
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV5(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
            studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers.Add(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers[0]);
            Assert.IsFalse(UniquenessArrayValidator.ValidateArrayV5(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
            studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers = null;
            Assert.IsTrue(UniquenessArrayValidator.ValidateArrayV5(studyDto.Study.Versions.FirstOrDefault().StudyIdentifiers));
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

            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands.Add(
                JsonConvert.DeserializeObject<EstimandDto>(
                    JsonConvert.SerializeObject(
                        studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0]
                        )
                )
            );
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].Id = "123";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].InterventionIds[0] = "124";

            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].Id = "124";
            studyDto.Study.Versions.FirstOrDefault().StudyDesigns[0].Estimands[0].InterventionIds[0] = "124";

            HelperV5 helper = new();
            var result = helper.ReferenceIntegrityValidation(studyDto, out object _);
            Assert.IsTrue(result);

        }
        #endregion

        #endregion


        [TestFixture]
        public class StudyRoleReferenceIntegrityValidationTests
        {
            private StudyDefinitionsDto _validStudy;

            [SetUp]
            public void SetUp()
            {
                _validStudy = new StudyDefinitionsDto
                {
                    Study = new StudyDto
                    {
                        Versions = new List<StudyVersionDto>
                        {
                            new StudyVersionDto
                            {
                                Id = "version-1",
                                StudyDesigns = new List<StudyDesignDto>
                                {
                                    new StudyDesignDto { Id = "design-1" },
                                    new StudyDesignDto { Id = "design-2" }
                                }
                            },
                            new StudyVersionDto
                            {
                                Id = "version-2",
                                StudyDesigns = new List<StudyDesignDto>
                                {
                                    new StudyDesignDto { Id = "design-3" }
                                }
                            }
                        }
                    }
                };
            }

            [TestCase("version-1")]
            [TestCase("version-2")]
            [TestCase("design-1")]
            [TestCase("design-2")]
            [TestCase("design-3")]
            public void ReferenceIntegrityValidationForStudyRole_ValidIds_ReturnsNoErrors(string validId)
            {
                // Arrange
                var studyRole = new StudyRoleDto
                {
                    Id = "role-1",
                    AppliesToIds = new List<string> { validId }
                };

                // Act
                    var result = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRole, _validStudy, 0);

                // Assert
                Assert.That(result, Is.Empty);
            }

            [TestCase("")]
            [TestCase(null)]
            [TestCase("   ")]
            [TestCase("\t")]
            [TestCase("\n")]
            public void ReferenceIntegrityValidationForStudyRole_EmptyOrWhitespaceIds_ReturnsError(string invalidId)
            {
                // Arrange
                var studyRole = new StudyRoleDto
                {
                    Id = "role-1",
                    AppliesToIds = new List<string> { invalidId }
                };

                // Act
                var result = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRole, _validStudy, 0);

                // Assert
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.EqualTo("StudyRoleDto[0].AppliesToIds[0]"));
            }

            [TestCase("invalid-version")]
            [TestCase("invalid-design")]
            [TestCase("non-existent-id")]
            public void ReferenceIntegrityValidationForStudyRole_InvalidIds_ReturnsError(string invalidId)
            {
                // Arrange
                var studyRole = new StudyRoleDto
                {
                    Id = "role-1",
                    AppliesToIds = new List<string> { invalidId }
                };

                // Act
                var result = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRole, _validStudy, 0);

                // Assert
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.EqualTo("StudyRoleDto[0].AppliesToIds[0]"));
            }

            [Test]
            public void ReferenceIntegrityValidationForStudyRole_MixedValidAndInvalidIds_ReturnsErrorsForInvalidOnly()
            {
                // Arrange
                var studyRole = new StudyRoleDto
                {
                    Id = "role-1",
                    AppliesToIds = new List<string> { "version-1", "invalid-id", "", "design-1" }
                };

                // Act
                var result = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRole, _validStudy, 0);

                // Assert
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0], Is.EqualTo("StudyRoleDto[0].AppliesToIds[1]"));
                Assert.That(result[1], Is.EqualTo("StudyRoleDto[0].AppliesToIds[2]"));
            }

            [Test]
            public void ReferenceIntegrityValidationForStudyRole_NullOrEmptyAppliesToIds_ReturnsNoErrors()
            {
                // Arrange
                var studyRoleWithNull = new StudyRoleDto { Id = "role-1", AppliesToIds = null };
                var studyRoleWithEmpty = new StudyRoleDto { Id = "role-2", AppliesToIds = new List<string>() };

                // Act
                var resultNull = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRoleWithNull, _validStudy, 0);
                var resultEmpty = HelperV5.ReferenceIntegrityValidationForStudyRole(studyRoleWithEmpty, _validStudy, 0);

                // Assert
                Assert.That(resultNull, Is.Empty);
                Assert.That(resultEmpty, Is.Empty);
            }

            [Test]
            public void ReferenceIntegrityValidationForStudyRole_NullStudyRole_ReturnsNoErrors()
            {
                // Arrange
                StudyRoleDto nullStudyRole = null;

                // Act
                var result = HelperV5.ReferenceIntegrityValidationForStudyRole(nullStudyRole, _validStudy, 0);

                // Assert
                Assert.That(result, Is.Empty);
            }
        }
    }
}
