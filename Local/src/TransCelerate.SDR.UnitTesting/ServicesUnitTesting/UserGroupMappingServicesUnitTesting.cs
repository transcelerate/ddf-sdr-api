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
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class UserGroupMappingServicesUnitTesting
    {
        #region Variables
        private IMapper _mockMapper;
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IUserGroupMappingRepository> _mockUserGroupMappingRepository = new(MockBehavior.Loose);
        #endregion

        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public static UserGroupMappingEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public static List<GroupDetailsEntity> GetGroupDetails()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupDetailsEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }

        public static List<GroupListEntity> GetListGroups()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupListEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }
        public static SDRGroupsDTO PostAGroupDto()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<SDRGroupsDTO>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups[0]));
            return groupDetails;
        }
        public static SDRGroupsEntity PostAGroupEntity()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<SDRGroupsEntity>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups[0]));
            return groupDetails;
        }
        #region Setup        
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
            _mockMapper = new Mapper(mockMapper);
        }
        #endregion

        #region Test Cases

        #region GET Methods

        #region GET User Groups UnitTesting
        [Test]
        public void GetUserGroups_UnitTest_SuccessResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Returns(Task.FromResult(GetGroupDetails()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.GetUserGroups(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetGroupDetails();

            //Actual            
            var actual_result = result;

            //Assert          
            Assert.AreEqual(expected[0].GroupId, actual_result[0].GroupId);
            Assert.AreEqual(expected[0].GroupName, actual_result[0].GroupName);
            Assert.NotNull(actual_result[0].GroupFilter);
            Assert.NotNull(actual_result[0].Permission);
            Assert.AreEqual(expected[0].GroupId, actual_result[0].GroupId);
        }

        [Test]
        public void GetUserGroups_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Returns(Task.FromResult(GetGroupDetails()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 10
            };
            var method = userGroupMappingService.GetUserGroups(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);

            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Throws(new Exception("Error"));
            var error = userGroupMappingService.GetUserGroups(userGroupsQueryParameters);

            Assert.Throws<AggregateException>(error.Wait);


        }


        #endregion

        #region GetUsersList UnitTesting
        [Test]
        public void GetUsersList_UnitTest_SuccessResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 0,
                PageSize = 0
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.GetUsersList(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<IEnumerable<PostUserToGroupsDTO>>(JsonConvert.SerializeObject(result)).ToList();

            //Assert          
            Assert.IsTrue(expected.SDRGroups.Select(x => x.Users).Any(x => x.Any(x => x.Email == actual_result[0].Email)));
            Assert.IsTrue(expected.SDRGroups.Select(x => x.Users).Any(x => x.Any(x => x.Email == actual_result[1].Email)));
            Assert.IsTrue(expected.SDRGroups.Select(x => x.Users).Any(x => x.Any(x => x.Email == actual_result[2].Email)));
            Assert.IsTrue(expected.SDRGroups.Select(x => x.Users).Any(x => x.Any(x => x.Email == actual_result[3].Email)));
            Assert.IsTrue(expected.SDRGroups.Any(x => x.GroupName == actual_result[0].Groups[0].GroupName));

            var nullObject = GetDataFromStaticJson();
            nullObject = null;
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                   .Returns(Task.FromResult(nullObject));

            method = userGroupMappingService.GetUsersList(userGroupsQueryParameters);

            method.Wait();
            result = method.Result;



        }

        [Test]
        public void GetUsersList_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                   .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 10,
                PageSize = 10
            };
            var method = userGroupMappingService.GetUsersList(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);

            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Throws(new Exception("Error"));
            var error = userGroupMappingService.GetUsersList(userGroupsQueryParameters);


            Assert.Throws<AggregateException>(error.Wait);
        }
        #endregion

        #region ListGroups UnitTesting
        [Test]
        public void ListGroups_UnitTest_SuccessResponse()
        {
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                    .Returns(Task.FromResult(GetListGroups()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.ListGroups();
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListGroups();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GroupListEntity>>(JsonConvert.SerializeObject(result)).ToList();

            //Assert          
            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[0].GroupName));
            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[1].GroupName));
            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[2].GroupName));
        }

        [Test]
        public void ListGroups_UnitTest_FailureResponse()
        {
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                   .Returns(Task.FromResult(new List<GroupListEntity>()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.ListGroups();
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsEmpty(actual_result);

            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                    .Throws(new Exception("Error"));
            UserGroupMappingService userGroupMappingService1 = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
            var error = userGroupMappingService1.ListGroups();


            Assert.Throws<AggregateException>(error.Wait);
        }
        #endregion

        #region Check GroupName 
        [Test]
        public void CheckGroupName_UnitTest_FailureResponse()
        {
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupByName("A"))
                   .Returns(Task.FromResult(GetDataFromStaticJson().SDRGroups[0]));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.CheckGroupName("A");
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNotNull(actual_result);

            method = userGroupMappingService.CheckGroupName("B");
            method.Wait();
            Assert.IsNotNull(method.Result);


            _mockUserGroupMappingRepository.Setup(x => x.GetGroupByName("A"))
                    .Throws(new Exception("Error"));
            UserGroupMappingService userGroupMappingService1 = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
            var error = userGroupMappingService1.CheckGroupName("A");


            Assert.Throws<AggregateException>(error.Wait);
        }
        #endregion

        #endregion

        #region POST Methods
        [Test]
        public void PostAGroup_UnitTest_SuccessResponse()
        {
            var postDataDto = PostAGroupDto();
            var postDataEntity = PostAGroupEntity();
            _mockUserGroupMappingRepository.Setup(x => x.UpdateAGroup(It.IsAny<SDRGroupsEntity>()))
                    .Returns(Task.FromResult(postDataEntity));
            _mockUserGroupMappingRepository.Setup(x => x.GetAGroupById(postDataEntity.GroupId))
                    .Returns(Task.FromResult(postDataEntity));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.PostGroup(postDataDto, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postDataDto.GroupId, actual_result.GroupId);

            _mockUserGroupMappingRepository.Setup(x => x.AddAGroup(It.IsAny<SDRGroupsEntity>()))
                   .Returns(Task.FromResult(postDataEntity));
            UserGroupMappingService userGroupMappingService1 = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
            postDataDto.GroupId = null;
            var method1 = userGroupMappingService1.PostGroup(postDataDto, user);
            method.Wait();
            var result1 = method.Result;

            //Actual            
            var actual_result1 = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result1);
            Assert.IsNotNull(actual_result1.GroupId);

            //Exception
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                .Throws(new Exception());

            var error = userGroupMappingService1.PostGroup(postDataDto, user);
            Assert.Throws<AggregateException>(error.Wait);


            List<GroupListEntity> nullObject = null;
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                   .Returns(Task.FromResult(nullObject));
            _mockUserGroupMappingRepository.Setup(x => x.AddAGroup(It.IsAny<SDRGroupsEntity>()))
                   .Returns(Task.FromResult(postDataEntity));
            method = userGroupMappingService1.PostGroup(postDataDto, user);
            method.Wait();
            result = method.Result;
        }



        [Test]
        public void PostUsersToGroups_UnitTest_SuccessResponse()
        {
            List<GroupsTaggedToUser> groupList = new();
            GroupsTaggedToUser groupsTaggedToUser = new()
            {
                GroupId = "0193a357-8519-4488-90e4-522f701658b9",
                GroupName = "OncologyRead",
                IsActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new()
            {
                GroupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser3 = new()
            {
                GroupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                GroupName = "AmnesiaReadWrite",
                IsActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new()
            {
                Email = "user1@SDR.com",
                Oid = "aw2dq254wfdsf",
                Groups = groupList
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            _mockUserGroupMappingRepository.Setup(x => x.UpdateUsersToGroups(It.IsAny<UserGroupMappingEntity>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.PostUserToGroups(postUserToGroupsDTO, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<UserGroupMappingService>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            var dataFromDatabase = GetDataFromStaticJson();
            dataFromDatabase.SDRGroups.ForEach(x =>
            {
                if (x.GroupId == "b9f848b8-9af7-46c1-9a3c-2663f547cc7a")
                    x.Users = null;
            });
            GroupsTaggedToUser groupsTaggedToUser4 = new()
            {
                GroupId = "b9f848b8-9af7-46c1-9a3c-2663f547cc7a",
                GroupName = "OncologyRead",
                IsActive = true
            };
            groupList.Add(groupsTaggedToUser4);
            postUserToGroupsDTO.Groups = groupList;

            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Returns(Task.FromResult(dataFromDatabase));

            method = userGroupMappingService.PostUserToGroups(postUserToGroupsDTO, user);
            method.Wait();
            result = method.Result;

            //Exception
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                                    .Throws(new Exception());

            var error = userGroupMappingService.PostUserToGroups(postUserToGroupsDTO, user);
            Assert.Throws<AggregateException>(error.Wait);

        }


        #endregion

        #endregion
    }
}
