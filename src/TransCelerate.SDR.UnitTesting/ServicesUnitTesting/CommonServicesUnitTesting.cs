using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;
using TransCelerate.SDR.Core.Entities.Common;
using System.Net.Http;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class CommonServicesUnitTesting
    {
        #region Variables
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<ICommonRepository> _mockCommonRepository = new Mock<ICommonRepository>(MockBehavior.Loose);        
        #endregion

        #region Setup
        public UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        [SetUp]
        public void SetUp()
        {
            Config.isGroupFilterEnabled = false;
        }
        #endregion

        #region Unit Testing
        [Test]
        public void GetRawJsonUnitTesting()
        {
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            CommonServices commonServices = new CommonServices(_mockCommonRepository.Object, _mockLogger);

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data =  JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));            

            var method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);


            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            var nullGroup = GetUserDataFromStaticJson().SDRGroups;
            nullGroup = null;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(nullGroup));

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.AreEqual(result, Constants.ErrorMessages.Forbidden);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.AreEqual(result, Constants.ErrorMessages.Forbidden);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.AreEqual(result, Constants.ErrorMessages.Forbidden);

            Config.isGroupFilterEnabled = false;


            data = null;            
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Throws(new Exception("Error"));
            method = commonServices.GetRawJson("a", 1, user);

            Assert.Throws<AggregateException>(method.Wait);
        }

        [Test]
        public void CheckAccessForAStudy_UnitTesting()
        {
            Config.isGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            CommonServices commonServices = new CommonServices(_mockCommonRepository.Object, _mockLogger);
            var method = commonServices.CheckAccessForAStudy("a", "INTERVENTIONAL", user);
            method.Wait();
            var result = method.Result;
            Assert.IsTrue(result);

            var allGroup = GetUserDataFromStaticJson().SDRGroups;
            allGroup[0].groupFilter[0].groupFilterValues[1].groupFilterValueId = "ALL";
            allGroup[0].groupFilter[0].groupFilterValues[1].title = "ALL";
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(allGroup));
            method = commonServices.CheckAccessForAStudy("a", "INTERVENTIONAL", user);
            method.Wait();
            result = method.Result;
            Assert.IsTrue(result);


            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            method = commonServices.CheckAccessForAStudy("studyId1", "INTERVENTIONAL", user);
            method.Wait();
            result = method.Result;
            Assert.IsTrue(result);

            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));
            method = commonServices.CheckAccessForAStudy("Study", "A", user);
            method.Wait();
            result = method.Result;
            Assert.False(result);

            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Throws(new Exception("Error"));
            method = commonServices.CheckAccessForAStudy("a", "INTERVENTIONAL", user);

            Assert.Throws<AggregateException>(method.Wait);
            Config.isGroupFilterEnabled = false;

            
        }
        #endregion
    }
}
