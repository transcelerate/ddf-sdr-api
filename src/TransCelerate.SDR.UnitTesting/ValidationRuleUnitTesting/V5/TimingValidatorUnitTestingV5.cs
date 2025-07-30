using NUnit.Framework;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.RuleEngineV5;

namespace TransCelerate.SDR.UnitTesting.ValidationRuleUnitTesting
{
    [TestFixture]
    public class TimingValidatorUnitTestingV5
    {
        [TestFixture]
        public class HasFullyDefinedTimingWindowsTests : TimingValidatorUnitTestingV5
        {
            [Test]
            public void HasFullyDefinedTimingWindows_AllWindowPropertiesSpecified_ReturnsTrue()
            {
                // Arrange
                var timing = new TimingDto
                {
                    WindowLabel = "Test Label",
                    WindowLower = "Lower",
                    WindowUpper = "Upper"
                };

                // Act
                var result = TimingValidator.HasFullyDefinedTimingWindows(timing);

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void HasFullyDefinedTimingWindows_NoWindowPropertiesSpecified_ReturnsTrue()
            {
                // Arrange
                var timing = new TimingDto
                {
                    WindowLabel = null,
                    WindowLower = "",
                    WindowUpper = "      "
                };

                // Act
                var result = TimingValidator.HasFullyDefinedTimingWindows(timing);

                // Assert
                Assert.IsTrue(result);
            }

            [TestCase("Test Label", null, null, ExpectedResult = false)]
            [TestCase(null, "Lower", null, ExpectedResult = false)]
            [TestCase(null, null, "Upper", ExpectedResult = false)]
            [TestCase("Test Label", "Lower", null, ExpectedResult = false)]
            [TestCase("Test Label", null, "Upper", ExpectedResult = false)]
            [TestCase(null, "Lower", "Upper", ExpectedResult = false)]
            public bool HasFullyDefinedTimingWindows_IncompleteCombinations_ReturnsFalse(
                string windowLabel, string windowLower, string windowUpper)
            {
                // Arrange
                var timing = new TimingDto
                {
                    WindowLabel = windowLabel,
                    WindowLower = windowLower,
                    WindowUpper = windowUpper
                };

                // Act
                return TimingValidator.HasFullyDefinedTimingWindows(timing);
            }
        }
    }
}