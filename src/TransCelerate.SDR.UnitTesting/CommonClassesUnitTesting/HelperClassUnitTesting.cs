using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1;
using TransCelerate.SDR.Core.Utilities.Common;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace TransCelerate.SDR.UnitTesting
{
    public class HelperClassUnitTesting
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
            TransCelerate.SDR.WebApi.DependencyInjection.ApplicationDependencyInjector.AddApplicationDependencies(serviceDescriptors);
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
        #endregion
    }
}
