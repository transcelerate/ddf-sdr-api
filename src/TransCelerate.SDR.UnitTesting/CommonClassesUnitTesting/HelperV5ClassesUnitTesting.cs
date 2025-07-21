
using NUnit.Framework;
using System.Collections.Generic;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV5ClassesUnitTesting
    {
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