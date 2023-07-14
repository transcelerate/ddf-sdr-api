using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class UserGroupMappingControllerUnitTesting
    {
        #region Variables        
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<IUserGroupMappingService> _mockUserGroupMappingService = new(MockBehavior.Loose);
        #endregion

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
        public static List<GroupDetailsDTO> GetGroupDetailsDTO()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupDetailsDTO>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
            return groupDetails;
        }

        public static List<GroupDetailsDTO> GetListGroups()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            var groupDetails = JsonConvert.DeserializeObject<List<GroupDetailsDTO>>(JsonConvert.SerializeObject(userGrouppMapping.SDRGroups));
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
        public static IEnumerable<PostUserToGroupsDTO> UserList()
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
            }; GroupsTaggedToUser groupsTaggedToUser3 = new()
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
            List<PostUserToGroupsDTO> postUserToGroups = new()
            {
                postUserToGroupsDTO
            };
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
                cfg.AddProfile(new AutoMapperProfilesV1());
                cfg.AddProfile(new AutoMapperProfilesV2());
                cfg.AddProfile(new AutoMapperProfilesV3());
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
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
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(GetGroupDetailsDTO()));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

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

            Assert.AreEqual(expected[0].GroupId, actual_result[0].GroupId);
            Assert.AreEqual(expected[0].GroupName, actual_result[0].GroupName);
            Assert.NotNull(actual_result[0].GroupFilter);
            Assert.NotNull(actual_result[0].Permission);
            Assert.AreEqual(expected[0].GroupId, actual_result[0].GroupId);
        }
        [Test]
        public void GetUserList_Exception()
        {

            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.GetUserList();
            method.Wait();

            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

        }

        [Test]
        public void GetUserGroups_UnitTest_FailureResponse()
        {

            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(GetGroupDetailsDTO()));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 10
            };
            var method = userGroupMappingController.GetUserGroups(userGroupsQueryParameters1);
            method.Wait();

            //Actual
            var result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            List<GroupDetailsDTO> groupDetailsDTOs = new();
            _mockUserGroupMappingService.Setup(x => x.GetUserGroups(userGroupsQueryParameters))
                .Returns(Task.FromResult(groupDetailsDTOs));
            UserGroupsController userGroupMappingController1 = new(_mockUserGroupMappingService.Object, _mockLogger);
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
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUsersList(userGroupsQueryParameters))
                .Returns(Task.FromResult(UserList() as object));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

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

            Assert.IsTrue(expected.SDRGroups.Select(x => x.Users).Any(x => x.Any(x => x.Email == actual_result[0].Email)));
            Assert.IsTrue(expected.SDRGroups.Any(x => x.GroupName == actual_result[0].Groups[0].GroupName));

        }

        [Test]
        public void GetUsersList_UnitTest_FailureResponse()
        {
            UserGroupsQueryParameters userGroupsQueryParameters = new()
            {
                SortBy = "email",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 20
            };
            _mockUserGroupMappingService.Setup(x => x.GetUsersList(userGroupsQueryParameters))
                .Returns(Task.FromResult(UserList() as object));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            UserGroupsQueryParameters userGroupsQueryParameters1 = new()
            {
                SortBy = "name",
                SortOrder = "desc",
                PageNumber = 1,
                PageSize = 10
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
                    .Returns(Task.FromResult(JsonConvert.DeserializeObject<List<GroupListDTO>>(JsonConvert.SerializeObject(GetListGroups()))));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

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

            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[0].GroupName));
            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[1].GroupName));
            Assert.IsTrue(expected.Any(x => x.GroupName == actual_result[2].GroupName));
        }

        [Test]
        public void ListGroups_UnitTest_FailureResponse()
        {
            _mockUserGroupMappingService.Setup(x => x.ListGroups())
                 .Throws(new Exception("Error"));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.GetGroupList();
            method.Wait();
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
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.CheckGroupName("A");
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new { groupName = GetDataFromStaticJson().SDRGroups[0].GroupName, isExists = false };

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
            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(postDataDto as object));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.PostGroup(postDataDto);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<SDRGroupsDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postDataDto.GroupId, actual_result.GroupId);

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);


            //Failures
            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(null as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                    .Throws(new Exception("Error"));
            method = userGroupMappingController.PostGroup(postDataDto);
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                     .Returns(Task.FromResult(Constants.ErrorMessages.GroupNameExists as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                     .Returns(Task.FromResult(Constants.ErrorMessages.GroupNameExists as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostGroup(postDataDto, It.IsAny<LoggedInUser>()))
                     .Returns(Task.FromResult(Constants.ErrorMessages.GroupIdError as object));
            method = userGroupMappingController.PostGroup(postDataDto);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            method = userGroupMappingController.PostGroup(null);
            error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);
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
            }; GroupsTaggedToUser groupsTaggedToUser3 = new()
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
            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO, It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(postUserToGroupsDTO as object));
            UserGroupsController userGroupMappingController = new(_mockUserGroupMappingService.Object, _mockLogger);

            var method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<PostUserToGroupsDTO>(
                JsonConvert.SerializeObject((result as OkObjectResult).Value));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.AreEqual(postUserToGroupsDTO.Groups[0].GroupId, actual_result.Groups[0].GroupId);

            Assert.IsNotNull((result as OkObjectResult).Value);
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            //Failures
            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO, It.IsAny<LoggedInUser>()))
                   .Returns(Task.FromResult(null as object));
            method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            method.Wait();
            result = method.Result;

            //Assert          
            Assert.IsNotNull((result as NotFoundObjectResult).Value);
            Assert.AreEqual(404, (result as NotFoundObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);

            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO, It.IsAny<LoggedInUser>()))
                    .Throws(new Exception("Error"));
            method = userGroupMappingController.PostUserToGroups(postUserToGroupsDTO);
            var error = method.Result;

            Assert.IsNotNull((error as BadRequestObjectResult).Value);
            Assert.AreEqual(400, (error as BadRequestObjectResult).StatusCode);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), error);

            _mockUserGroupMappingService.Setup(x => x.PostUserToGroups(postUserToGroupsDTO, It.IsAny<LoggedInUser>()))
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
