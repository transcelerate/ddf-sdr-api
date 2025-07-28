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
    }
}