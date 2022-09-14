using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.DTO.UserGroups;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TransCelerate.SDR.WebApi.Mappers;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using System.Linq;
using TransCelerate.SDR.Core.DTO.Token;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public  class UserGroupMappingServicesUnitTesting
    {
        #region Variables
        private IMapper _mockMapper;
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IUserGroupMappingRepository> _mockUserGroupMappingRepository = new Mock<IUserGroupMappingRepository>(MockBehavior.Loose);
        #endregion

        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        public UserGroupMappingEntity GetDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public List<GroupDetailsEntity> GetGroupDetails()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupDetailsEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }

        public List<GroupListEntity> GetListGroups()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupListEntity>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }
        public SDRGroupsDTO PostAGroupDto()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<SDRGroupsDTO>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups[0]));
            return groupDetails;
        }
        public SDRGroupsEntity PostAGroupEntity()
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
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "email",sortOrder = "desc",pageNumber = 1,pageSize=20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Returns(Task.FromResult(GetGroupDetails()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.GetUserGroups(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetGroupDetails();

            //Actual            
            var actual_result = result;

            //Assert          
            Assert.AreEqual(expected[0].groupId, actual_result[0].groupId);
            Assert.AreEqual(expected[0].groupName, actual_result[0].groupName);
            Assert.NotNull(actual_result[0].groupFilter);
            Assert.NotNull(actual_result[0].permission);            
            Assert.AreEqual(expected[0].groupId, actual_result[0].groupId);
        }

        [Test]
        public void GetUserGroups_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Returns(Task.FromResult(GetGroupDetails()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 10
            };
            var method = userGroupMappingService.GetUserGroups(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsNull(actual_result);

            _mockUserGroupMappingRepository.Setup(x => x.GetGroups(userGroupsQueryParameters))
                    .Throws(new Exception("Error"));
            var error  = userGroupMappingService.GetUserGroups(userGroupsQueryParameters);

            
            Assert.Throws<AggregateException>(error.Wait);
        }
        #endregion

        #region GetUsersList UnitTesting
        [Test]
        public void GetUsersList_UnitTest_SuccessResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.GetUsersList(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<IEnumerable<PostUserToGroupsDTO>>(JsonConvert.SerializeObject(result)).ToList();

            //Assert          
            Assert.IsTrue(expected.SDRGroups.Select(x=>x.users).Any(x=>x.Any(x=>x.email== actual_result[0].email)));
            Assert.IsTrue(expected.SDRGroups.Select(x=>x.users).Any(x=>x.Any(x=>x.email== actual_result[1].email)));
            Assert.IsTrue(expected.SDRGroups.Select(x=>x.users).Any(x=>x.Any(x=>x.email== actual_result[2].email)));
            Assert.IsTrue(expected.SDRGroups.Select(x=>x.users).Any(x=>x.Any(x=>x.email== actual_result[3].email)));
            Assert.IsTrue(expected.SDRGroups.Any(x=>x.groupName==actual_result[0].groups[0].groupName));
           
        }

        [Test]
        public void GetUsersList_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                   .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 10,
                pageSize = 10
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
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.ListGroups();
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListGroups();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GroupListEntity>>(JsonConvert.SerializeObject(result)).ToList();

            //Assert          
            Assert.IsTrue(expected.Any(x=>x.groupName==actual_result[0].groupName));
            Assert.IsTrue(expected.Any(x=>x.groupName==actual_result[1].groupName));
            Assert.IsTrue(expected.Any(x=>x.groupName==actual_result[2].groupName));                     
        }

        [Test]
        public void ListGroups_UnitTest_FailureResponse()
        {          
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                   .Returns(Task.FromResult(new List<GroupListEntity>()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
          
            var method = userGroupMappingService.ListGroups();
            method.Wait();

            //Actual
            var actual_result = method.Result;

            //Assert          
            Assert.IsEmpty(actual_result);

            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                    .Throws(new Exception("Error"));
            UserGroupMappingService userGroupMappingService1 = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
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
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

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
            UserGroupMappingService userGroupMappingService1 = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
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
            _mockUserGroupMappingRepository.Setup(x => x.GetAGroupById(postDataEntity.groupId))
                    .Returns(Task.FromResult(postDataEntity));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
           
            var method = userGroupMappingService.PostGroup(postDataDto, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postDataDto.groupId,actual_result.groupId);

            _mockUserGroupMappingRepository.Setup(x => x.AddAGroup(It.IsAny<SDRGroupsEntity>()))
                   .Returns(Task.FromResult(postDataEntity));
            UserGroupMappingService userGroupMappingService1 = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);
            postDataDto.groupId = null;
            var method1 = userGroupMappingService1.PostGroup(postDataDto, user);
            method.Wait();
            var result1 = method.Result;

            //Actual            
            var actual_result1 = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result1);
            Assert.IsNotNull(actual_result1.groupId);

            //Exception
            _mockUserGroupMappingRepository.Setup(x => x.GetGroupList())
                .Throws(new Exception());

            var error = userGroupMappingService1.PostGroup(postDataDto, user);
            Assert.Throws<AggregateException>(error.Wait);
        }

        [Test]
        public void PostUsersToGroups_UnitTest_SuccessResponse()
        {
            List<GroupsTaggedToUser> groupList = new List<GroupsTaggedToUser>();
            GroupsTaggedToUser groupsTaggedToUser = new GroupsTaggedToUser
            {
                groupId = "0193a357-8519-4488-90e4-522f701658b9",
                groupName = "OncologyRead", isActive=true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new GroupsTaggedToUser
            {
                groupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                groupName = "AmnesiaReadWrite", isActive=true
            };
            GroupsTaggedToUser groupsTaggedToUser3 = new GroupsTaggedToUser
            {
                groupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                groupName = "AmnesiaReadWrite", isActive=true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new PostUserToGroupsDTO
            {
                email = "user1@SDR.com",oid = "aw2dq254wfdsf",groups = groupList
            };
            _mockUserGroupMappingRepository.Setup(x => x.GetAllUserGroups())
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            _mockUserGroupMappingRepository.Setup(x => x.UpdateUsersToGroups(It.IsAny<UserGroupMappingEntity>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson()));
            UserGroupMappingService userGroupMappingService = new UserGroupMappingService(_mockUserGroupMappingRepository.Object, _mockMapper, _mockLogger);

            var method = userGroupMappingService.PostUserToGroups(postUserToGroupsDTO, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<UserGroupMappingService>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);    
            
            var dataFromDatabase= GetDataFromStaticJson();
            dataFromDatabase.SDRGroups.ForEach(x =>
            {
                if (x.groupId == "b9f848b8-9af7-46c1-9a3c-2663f547cc7a")
                    x.users = null;
            });
            GroupsTaggedToUser groupsTaggedToUser4 = new GroupsTaggedToUser
            {
                groupId = "b9f848b8-9af7-46c1-9a3c-2663f547cc7a",
                groupName = "OncologyRead",
                isActive = true
            };
            groupList.Add(groupsTaggedToUser4);
            postUserToGroupsDTO.groups=groupList;

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
