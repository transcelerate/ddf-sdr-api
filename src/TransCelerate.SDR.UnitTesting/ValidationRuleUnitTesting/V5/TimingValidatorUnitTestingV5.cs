using NUnit.Framework;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.RuleEngineV5;
using TransCelerate.SDR.Core.Utilities.Common;


namespace TransCelerate.SDR.UnitTesting.ValidationRuleUnitTesting
{
    [TestFixture]
    public class TimingValidatorUnitTestingV5
    {
        [TestFixture]
        public class HasFullyDefinedTimingWindowsTests : TimingValidatorUnitTestingV5
        {
            [Test]
            public void AllWindowPropertiesSpecified_ReturnsTrue()
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
            public void NoWindowPropertiesSpecified_ReturnsTrue()
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
            public bool IncompleteCombinations_ReturnsFalse(
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

        [TestFixture]
        public class NoWindowForAnchorTimingTests : TimingValidatorUnitTestingV5
        {
            [Test]
            public void TypeIsNotFixedReference_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "Relative" },
                    WindowLabel = "Label",
                    WindowLower = "Lower",
                    WindowUpper = "Upper"
                };

                var result = TimingValidator.NoWindowForAnchorTiming(timing);

                Assert.IsTrue(result);
            }

            [Test]
            public void TypeIsFixedReference_NoWindowFields_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "Fixed Reference" },
                    WindowLabel = null,
                    WindowLower = null,
                    WindowUpper = null
                };

                var result = TimingValidator.NoWindowForAnchorTiming(timing);

                Assert.IsTrue(result);
            }

            [TestCase("Label", null, null)]
            [TestCase(null, "Lower", null)]
            [TestCase(null, null, "Upper")]
            [TestCase("Label", "Lower", null)]
            [TestCase("Label", null, "Upper")]
            [TestCase(null, "Lower", "Upper")]
            [TestCase("Label", "Lower", "Upper")]
            public void TypeIsFixedReference_WithAnyWindowField_ReturnsFalse(
                string windowLabel, string windowLower, string windowUpper)
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "Fixed Reference" },
                    WindowLabel = windowLabel,
                    WindowLower = windowLower,
                    WindowUpper = windowUpper
                };

                var result = TimingValidator.NoWindowForAnchorTiming(timing);

                Assert.IsFalse(result);
            }

            [Test]
            public void NullTiming_ReturnsTrue()
            {
                var result = TimingValidator.NoWindowForAnchorTiming(null);

                Assert.IsTrue(result);
            }

            [Test]
            public void NullType_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = null,
                    WindowLabel = "Label"
                };

                var result = TimingValidator.NoWindowForAnchorTiming(timing);

                Assert.IsTrue(result);
            }

            [Test]
            public void EmptyDecode_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "" },
                    WindowLabel = "Label"
                };

                var result = TimingValidator.NoWindowForAnchorTiming(timing);

                Assert.IsTrue(result);
            }

        }

        [TestFixture]
        public class FixedReferenceTimingIsRelativeToFromStartToStartTests : TimingValidatorUnitTestingV5
        {
            [Test]
            public void TypeIsNotFixedReference_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "Relative" },
                    RelativeToFrom = new CodeDto { Decode = "End to Start" }
                };

                var result = TimingValidator.FixedReferenceTimingIsRelativeToFromStartToStart(timing);

                Assert.IsTrue(result);
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("End to Start")]
            [TestCase("Start to End")]
            public void TypeIsFixedReference_WithInvalidRelativeToFrom_ReturnsFalse(string relativeToFromValue)
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = Constants.TimingType.FIXED_REFERENCE },
                    RelativeToFrom = new CodeDto { Decode = relativeToFromValue }
                };

                var result = TimingValidator.FixedReferenceTimingIsRelativeToFromStartToStart(timing);

                Assert.IsFalse(result);
            }

            [Test]
            public void NullTiming_ReturnsTrue()
            {
                var result = TimingValidator.FixedReferenceTimingIsRelativeToFromStartToStart(null);

                Assert.IsTrue(result);
            }

            [Test]
            public void NullType_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = null,
                    RelativeToFrom = new CodeDto { Decode = "Start to Start" }
                };

                var result = TimingValidator.FixedReferenceTimingIsRelativeToFromStartToStart(timing);

                Assert.IsTrue(result);
            }

            [Test]
            public void EmptyTypeDecode_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "" },
                    RelativeToFrom = new CodeDto { Decode = "Start to Start" }
                };

                var result = TimingValidator.FixedReferenceTimingIsRelativeToFromStartToStart(timing);

                Assert.IsTrue(result);
            }
        }

        [TestFixture]
        public class FixedReferenceTimingPointsToOnlyOneScheduledInstanceTests : TimingValidatorUnitTestingV5
        {
            [Test]
            public void TypeIsNotFixedReference_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = "Relative" },
                    RelativeFromScheduledInstanceId = "A",
                    RelativeToScheduledInstanceId = "B"
                };

                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(timing);

                Assert.IsTrue(result);
            }

            [Test]
            public void TypeIsFixedReference_BothIdsEqual_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = Constants.TimingType.FIXED_REFERENCE },
                    RelativeFromScheduledInstanceId = "Instance1",
                    RelativeToScheduledInstanceId = "Instance1"
                };

                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(timing);

                Assert.IsTrue(result);
            }

            [TestCase(null, "Instance1")]
            [TestCase("Instance1", null)]
            [TestCase(null, null)]
            public void TypeIsFixedReference_OneOrBothIdsMissing_ReturnsTrue(string fromIdValue, string toIdValue)
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = Constants.TimingType.FIXED_REFERENCE },
                    RelativeFromScheduledInstanceId = fromIdValue,
                    RelativeToScheduledInstanceId = toIdValue
                };

                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(timing);

                Assert.IsTrue(result);
            }

            [Test]
            public void TypeIsFixedReference_IdsNotEqual_ReturnsFalse()
            {
                var timing = new TimingDto
                {
                    Type = new CodeDto { Decode = Constants.TimingType.FIXED_REFERENCE },
                    RelativeFromScheduledInstanceId = "Instance1",
                    RelativeToScheduledInstanceId = "Instance2"
                };

                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(timing);

                Assert.IsFalse(result);
            }

            [Test]
            public void NullTiming_ReturnsTrue()
            {
                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(null);

                Assert.IsTrue(result);
            }

            [Test]
            public void NullType_ReturnsTrue()
            {
                var timing = new TimingDto
                {
                    Type = null,
                    RelativeFromScheduledInstanceId = "A",
                    RelativeToScheduledInstanceId = "B"
                };

                var result = TimingValidator.FixedReferenceTimingPointsToOnlyOneScheduledInstance(timing);

                Assert.IsTrue(result);
            }
        }

    }
}