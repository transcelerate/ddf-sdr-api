using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.DataAccess.Filters;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Core.Utilities.Common;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TransCelerate.SDR.RuleEngineV1;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.WebApi.DependencyInjection;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperV1ClassesUnitTesting
    {
        #region Variables
        private IServiceCollection serviceDescriptors = Mock.Of<IServiceCollection>();
        #endregion
        #region Setup
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public StudyEntity GetEntityDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            return JsonConvert.DeserializeObject<StudyEntity>(jsonData);
        }
        #endregion

        #region Test Cases
        #region HelperV1 Unit Testing
        [Test]
        public void HelpersUnitTesting()
        {
            ApplicationDependencyInjector.AddApplicationDependencies(serviceDescriptors);
            Helper helper = new Helper();
            AuditTrailEntity auditTrailEntity = helper.GetAuditTrail(user.UserName);
            Assert.IsInstanceOf(typeof(DateTime), auditTrailEntity.EntryDateTime);

            StudyEntity studyEntity = GetEntityDataFromStaticJson();

            studyEntity = helper.GeneratedSectionId(studyEntity);
            Assert.IsNotNull(studyEntity);

            StudyEntity studyEntity1 = GetEntityDataFromStaticJson();
            StudyEntity studyEntity2 = GetEntityDataFromStaticJson();
            var isSameStudy = helper.IsSameStudy(studyEntity1, studyEntity2);
            Assert.IsTrue(isSameStudy);

            studyEntity = helper.CheckForSections(studyEntity1, studyEntity2);
        }
        #endregion

        #region DataFilters
        [Test]
        public void DataFiltersUnitTesting()
        {
            var filter = DataFilters.GetFiltersForGetStudy("1", 1);
            Assert.IsNotNull(filter);

            SearchParameters searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
                Asc = true,
                Header = "sdrversion"
            };
            filter = DataFilters.GetFiltersForSearchStudy(searchParameters);
            Assert.IsNotNull(filter);
        }
        #endregion
        #region Validation Classes
        [Test]
        public void ValidationDependenciesUnitTesting()
        {
            ValidationDependenciesV1.AddValidationDependenciesV1(serviceDescriptors);
            SearchParametersDto searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                StudyId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString()
            };
            SearchParametersValidator searchValidationRules = new SearchParametersValidator();
            Assert.IsTrue((searchValidationRules.Validate(searchParameters)).IsValid);
        }
        #endregion
        #endregion
    }
}
