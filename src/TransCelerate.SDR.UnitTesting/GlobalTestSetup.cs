using FluentValidation;
using NUnit.Framework;

namespace TransCelerate.SDR.UnitTesting
{
    [SetUpFixture]
    public class GlobalTestSetup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        }
    }
}