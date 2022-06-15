using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.DTO.UserGroups;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TransCelerate.SDR.WebApi.Mappers;
using TransCelerate.SDR.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public  class UserGroupMappingControllerUnitTesting
    {
        #region Variables
        private IMapper _mockMapper;
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<IUserGroupMappingRepository> _mockUserGroupMappingRepository = new Mock<IUserGroupMappingRepository>(MockBehavior.Loose);
        private Mock<IUserGroupMappingService> _mockUserGroupMappingService = new Mock<IUserGroupMappingService>(MockBehavior.Loose);
        #endregion
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
        public List<GroupDetailsDTO> GetGroupDetailsDTO()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupDetailsDTO>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
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
        public IEnumerable<PostUserToGroupsDTO> UserList()
        {
            List<GroupsTaggedToUser> groupList = new List<GroupsTaggedToUser>();
            GroupsTaggedToUser groupsTaggedToUser = new GroupsTaggedToUser
            {
                groupId = "0193a357-8519-4488-90e4-522f701658b9",
                groupName = "OncologyRead",
                isActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new GroupsTaggedToUser
            {
                groupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                groupName = "AmnesiaReadWrite",
                isActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new GroupsTaggedToUser
            {
                groupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                groupName = "AmnesiaReadWrite",
                isActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new PostUserToGroupsDTO
            {
                email = "user1@SDR.com",
                oid = "aw2dq254wfdsf",
                groups = groupList
            };
            List<PostUserToGroupsDTO> postUserToGroups = new List<PostUserToGroupsDTO>();
            postUserToGroups.Add(postUserToGroupsDTO);
            IEnumerable<PostUserToGroupsDTO> postUserToGroupsIenum = JsonConvert.DeserializeObject<IEnumerable<PostUserToGroupsDTO>>(
                                                                    JsonConvert.SerializeObject(postUserToGroups));
            return postUserToGroupsIenum;
        }
        #region Setup        
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfies());
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
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(GetGroupDetailsDTO()));            
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.GetUserGroups(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetGroupDetails();

            //Actual            
            var actual_result = (result as OkObjectResult).Value as List<GroupDetailsDTO>;

            //Assert
            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

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
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(GetGroupDetailsDTO()));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 10
            };
            var method = userGroupMappingController.GetUserGroups(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            List<GroupDetailsDTO> groupDetailsDTOs = new   List<GroupDetailsDTO>();
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(groupDetailsDTOs));
            UserGroupsController userGroupMappingController1 = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);
            method = userGroupMappingController1.GetUserGroups(userGroupsQueryParameters);
            method.Wait();

            //Actual
            result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                    .Throws(new Exception("Error"));
            method = userGroupMappingController.GetUserGroups(userGroupsQueryParameters);
            method.Wait();
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
        }
        #endregion

        #region GetUsersList UnitTesting
        [Test]
        public void GetUsersList_UnitTest_SuccessResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUsersList(userGroupsQueryParameters))
                .Returns(Task.FromResult(UserList() as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.GetUsersList(userGroupsQueryParameters);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetDataFromStaticJson();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<IEnumerable<PostUserToGroupsDTO>>(JsonConvert.SerializeObject((result as OkObjectResult).Value)).ToList();

            //Assert

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.IsTrue(expected.SDRGroups.Select(x => x.users).Any(x => x.Any(x => x.email == actual_result[0].email)));           
            Assert.IsTrue(expected.SDRGroups.Any(x => x.groupName == actual_result[0].groups[0].groupName));

        }

        [Test]
        public void GetUsersList_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new UserGroupsQueryParameters
            {
                sortBy = "email",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 20
            };            
            _mockUserGroupMappingService.Setup(x => x.GetUsersList(userGroupsQueryParameters))
                .Returns(Task.FromResult(UserList() as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new UserGroupsQueryParameters
            {
                sortBy = "name",
                sortOrder = "desc",
                pageNumber = 1,
                pageSize = 10
            };
            var method = userGroupMappingController.GetUsersList(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);           


            _mockUserGroupMappingService.Setup(x => x.GetUsersList(userGroupsQueryParameters))
                   .Throws(new Exception("Error"));
            method = userGroupMappingController.GetUsersList(userGroupsQueryParameters);
            method.Wait();
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
        }
        #endregion

        #region ListGroups UnitTesting
        [Test]
        public void ListGroups_UnitTest_SuccessResponse()
        {
            _mockUserGroupMappingService.Setup(x => x.ListGroups())
                    .Returns(Task.FromResult(GetListGroups() as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object,_mockLogger);

            var method = userGroupMappingController.GetGroupList();
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = GetListGroups();

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<GroupListDTO>>(JsonConvert.SerializeObject((result as OkObjectResult).Value)).ToList();

            //Assert          

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            Assert.IsTrue(expected.Any(x => x.groupName == actual_result[0].groupName));
            Assert.IsTrue(expected.Any(x => x.groupName == actual_result[1].groupName));
            Assert.IsTrue(expected.Any(x => x.groupName == actual_result[2].groupName));           
        }

        [Test]
        public void ListGroups_UnitTest_FailureResponse()
        {
            _mockUserGroupMappingService.Setup(x => x.ListGroups())
                   .Returns(Task.FromResult(null as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.GetGroupList();
            method.Wait();
            var result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.ListGroups())
                    .Throws(new Exception("Error"));            
            method = userGroupMappingController.GetGroupList();
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
        }
        #endregion

        #region CheckGroupName
        [Test]
        public void CheckGroupName_UnitTesting()
        {
            _mockUserGroupMappingService.Setup(x => x.CheckGroupName(It.IsAny<string>()))
                    .Returns(Task.FromResult(GetDataFromStaticJson().SDRGroups[0] as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.CheckGroupName("A");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new {groupName = GetDataFromStaticJson().SDRGroups[0].groupName,isExists = false};

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            actual_result.Equals(expected);

            method = userGroupMappingController.CheckGroupName(null);
            method.Wait();
            result = method.Result;

            Assert.IsNotNull((result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.CheckGroupName(It.IsAny<string>()))
                    .Throws(new Exception("Error"));

            method = userGroupMappingController.CheckGroupName("A");
            method.Wait();
            result = method.Result;

            Assert.IsNotNull((result as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (result as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
        }
        #endregion

        #endregion

        #region POST Methods
        [Test]
        public void PostAGroup_UnitTest_SuccessResponse()
        {
            var postDataDto = PostAGroupDto();
            var postDataEntity = PostAGroupEntity();
            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto))
                    .Returns(Task.FromResult(postDataDto as object));
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.PostGroup(postDataDto);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postDataDto.groupId, actual_result.groupId);

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);


            //Failures
            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto))
                   .Returns(Task.FromResult(null as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto))
                    .Throws(new Exception("Error"));
            method = userGroupMappingController.PostGroup(postDataDto);
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
            
            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto))
                     .Returns(Task.FromResult(Constants.ErrorMessages.GroupNameExists as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto))
                     .Returns(Task.FromResult(Constants.ErrorMessages.GroupNameExists as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
        }

        [Test]
        public void PostUsersToGroups_UnitTest_SuccessResponse()
        {
            List<GroupsTaggedToUser> groupList = new List<GroupsTaggedToUser>();
            GroupsTaggedToUser groupsTaggedToUser = new GroupsTaggedToUser
            {
                groupId = "0193a357-8519-4488-90e4-522f701658b9",
                groupName = "OncologyRead",
                isActive = true
            };
            GroupsTaggedToUser groupsTaggedToUser2 = new GroupsTaggedToUser
            {
                groupId = "c50ccb41-db9b-4b97-b132-cbbfaa68af5a",
                groupName = "AmnesiaReadWrite",
                isActive = true
            }; GroupsTaggedToUser groupsTaggedToUser3 = new GroupsTaggedToUser
            {
                groupId = "83864612-ffbd-463f-90ce-3e8819c5d132",
                groupName = "AmnesiaReadWrite",
                isActive = true
            };
            groupList.Add(groupsTaggedToUser);
            groupList.Add(groupsTaggedToUser2);
            groupList.Add(groupsTaggedToUser3);
            PostUserToGroupsDTO postUserToGroupsDTO = new PostUserToGroupsDTO
            {
                email = "user1@SDR.com",
                oid = "aw2dq254wfdsf",
                groups = groupList
            };
            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO))
                    .Returns(Task.FromResult(postUserToGroupsDTO as object));  
            UserGroupsController userGroupMappingController = new UserGroupsController(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostUserToGroupsDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postUserToGroupsDTO.groups[0].groupId, actual_result.groups[0].groupId);

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            //Failures
            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO))
                   .Returns(Task.FromResult(null as object));
            method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO))
                    .Throws(new Exception("Error"));
            method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO))
                    .Returns(Task.FromResult(postUserToGroupsDTO as object));
            method = userGroupMappingController.PostUserToGroups(null);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

        }
        #endregion

        #endregion
    }
}
