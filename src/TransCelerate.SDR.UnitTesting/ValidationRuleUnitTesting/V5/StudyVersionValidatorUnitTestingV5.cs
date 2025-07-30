using System.Collections.Generic;
using NUnit.Framework;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.RuleEngineV5;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.UnitTesting.ValidationRuleUnitTesting
{
    [TestFixture]
    public class StudyVersionValidatorUnitTestingV5
    {
        private StudyVersionValidator _studyVersionValidator;

        private static StudyIdentifierDto CreateStudyIdentifier(string decode, int index = 1)
        {
            return new StudyIdentifierDto
            {
                Id = $"study-identifier-{index}",
                Scope = new OrganizationDto
                {
                    Id = $"organization-{index}",
                    Type = new CodeDto
                    {
                        Id = $"code-{index}",
                        Decode = decode
                    }
                }
            };
        }

        [SetUp]
        public void Setup()
        {
            _studyVersionValidator = new StudyVersionValidator(null);
        }

        [TestFixture]
        public class HaveOneClinicalStudySponsorOrganizationIdentifierTests : StudyVersionValidatorUnitTestingV5
        {
            [Test]
            public void NullStudyIdentifierList_ReturnsFalse()
            {
                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(null);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void EmptyStudyIdentifierList_ReturnsFalse()
            {
                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(new List<StudyIdentifierDto>());

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void ExactlyOneSponsor_ReturnsTrue()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    CreateStudyIdentifier(Constants.IdType.SPONSOR_ID_V1)
                };

                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void MultipleSponsors_ReturnsFalse()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    CreateStudyIdentifier(Constants.IdType.SPONSOR_ID_V1, 1),
                    CreateStudyIdentifier(Constants.IdType.SPONSOR_ID_V1, 2)
                };

                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(studyIdentifiers);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void NoSponsor_ReturnsFalse()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    CreateStudyIdentifier("OTHER_TYPE", 1),
                    CreateStudyIdentifier("ANOTHER_TYPE", 2)
                };

                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(studyIdentifiers);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void OneSponsorAmongMultiple_ReturnsTrue()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    CreateStudyIdentifier("OTHER_TYPE", 1),
                    CreateStudyIdentifier(Constants.IdType.SPONSOR_ID_V1, 2),
                    CreateStudyIdentifier("ANOTHER_TYPE", 3)
                };

                // Act
                var result = StudyVersionValidator.HasExactlyOneClinicalStudySponsorOrganizationIdentifier(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [TestFixture]
        public class EveryOrganizationIdentifierUniqueWithinStudyTests : StudyVersionValidatorUnitTestingV5
        {
            [Test]
            public void NullStudyIdentifierList_ReturnsTrue()
            {
                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(null);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void EmptyStudyIdentifierList_ReturnsTrue()
            {
                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(new List<StudyIdentifierDto>());

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void AllOrganizationIdentifiersUnique_ReturnsTrue()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-2",
                        Scope = new OrganizationDto { Identifier = "ORG-B" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-3",
                        Scope = new OrganizationDto { Identifier = "ORG-C" }
                    }
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void DuplicateOrganizationIdentifiers_ReturnsFalse()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-2",
                        Scope = new OrganizationDto { Identifier = "ORG-B" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-3",
                        Scope = new OrganizationDto { Identifier = "ORG-A" } // Duplicate ORG-A
                    }
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void NullScope_IgnoresNullScopes()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-2",
                        Scope = null // Null scope should be ignored
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-3",
                        Scope = new OrganizationDto { Identifier = "ORG-B" }
                    }
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void NullOrganizationIdentifier_IgnoresNullIdentifiers()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-2",
                        Scope = new OrganizationDto { Identifier = null } // Null identifier should be ignored
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-3",
                        Scope = new OrganizationDto { Identifier = "ORG-B" }
                    },
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-4",
                        Scope = new OrganizationDto { Identifier = null } // Null identifier should be ignored
                    },
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void NullStudyIdentifier_IgnoresNullStudyIdentifiers()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    },
                    null, // Null study identifier should be ignored
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-2",
                        Scope = new OrganizationDto { Identifier = "ORG-B" }
                    }
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void SingleStudyIdentifier_ReturnsTrue()
            {
                // Arrange
                var studyIdentifiers = new List<StudyIdentifierDto>
                {
                    new StudyIdentifierDto
                    {
                        Id = "study-identifier-1",
                        Scope = new OrganizationDto { Identifier = "ORG-A" }
                    }
                };

                // Act
                var result = StudyVersionValidator.EveryOrganizationIdentifierUniqueWithinStudy(studyIdentifiers);

                // Assert
                Assert.IsTrue(result);
            }
        }
    }
}