using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Enums;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.UnitTesting.ServicesUnitTesting
{
    public class CommonServicesUnitTesting
    {
        #region Variables
        private readonly ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private readonly Mock<ICommonRepository> _mockCommonRepository = new(MockBehavior.Loose);
        private IMapper _mockMapper;
        #endregion

        #region Setup
        public static UserGroupMappingEntity GetUserDataFromStaticJson()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/UserGroupMappingData_ForEntity.json");
            var userGrouppMapping = JsonConvert.DeserializeObject<UserGroupMappingEntity>(jsonData);
            return userGrouppMapping;
        }
        public static List<SearchTitleResponseEntity> GetSearchResponse()
        {
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V1_9);
            return new()
            {
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = mvp.ClinicalStudy.StudyIdentifiers,
                    StudyId = mvp.ClinicalStudy.StudyId,
                    StudyTitle = mvp.ClinicalStudy.StudyTitle,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    SDRUploadVersion = mvp.AuditTrail.SDRUploadVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v1.ClinicalStudy.StudyIdentifiers,
                    StudyId = v1.ClinicalStudy.StudyId,
                    StudyTitle = v1.ClinicalStudy.StudyTitle,
                    StudyType = v1.ClinicalStudy.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v2.ClinicalStudy.StudyIdentifiers,
                    StudyId = v2.ClinicalStudy.StudyId,
                    StudyTitle = v2.ClinicalStudy.StudyTitle,
                    StudyType = v2.ClinicalStudy.StudyType,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                }
            };
        }
        public static CommonStudyEntity GetData(string usdmVersion)
        {
            if (usdmVersion == Constants.USDMVersions.MVP)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/PostStudyData.json");
                var mvpData = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return mvpData;
            }
            else if (usdmVersion == Constants.USDMVersions.V1)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
                var v1 = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return v1;
            }
            else
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
                var v2 = JsonConvert.DeserializeObject<CommonStudyEntity>(jsonData);
                return v2;
            }
        }
        readonly LoggedInUser user = new()
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
            _mockMapper = new Mapper(mockMapper);
            Config.IsGroupFilterEnabled = false;

            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        #endregion

        #region Unit Testing
        #region GET Raw JSON
        [Test]
        public void GetRawJsonUnitTesting()
        {
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            CommonServices commonServices = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
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


            Config.IsGroupFilterEnabled = true;
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

            Config.IsGroupFilterEnabled = false;


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
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(GetUserDataFromStaticJson().SDRGroups));

            CommonServices commonServices = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);
            var method = commonServices.CheckAccessForAStudy("a", "INTERVENTIONAL", user);
            method.Wait();
            var result = method.Result;
            Assert.IsTrue(result);

            var allGroup = GetUserDataFromStaticJson().SDRGroups;
            allGroup[0].GroupFilter[0].GroupFilterValues[1].GroupFilterValueId = "ALL";
            allGroup[0].GroupFilter[0].GroupFilterValues[1].Title = "ALL";
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
            Config.IsGroupFilterEnabled = false;


        }
        #endregion

        #region GET AuditTrail
        [Test]
        public void GetStudyAudit_UnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    SDRUploadVersion = 1,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    StudyType = v1.ClinicalStudy.StudyType,
                    SDRUploadVersion = 2,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    StudyType = v2.ClinicalStudy.StudyType,
                    SDRUploadVersion = 2,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion
                }
            };
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .Returns(Task.FromResult(auditTrailResponseEntities));
            var grps = GetUserDataFromStaticJson().SDRGroups;
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.studyType.ToString()).ToList().ForEach(y =>
                  {
                      y.GroupFilterValues.ForEach(z =>
                      {
                          z.GroupFilterValueId = "PATIENT_REGISTRY"; z.Title = "PATIENT_REGISTRY";
                      });
                  });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AuditTrailResponseEntity>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            //No Access Check
            user.UserRole = Constants.Roles.App_User;
            method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            //Exception
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Throws(new Exception("Error"));

            method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);


            Assert.Throws<AggregateException>(method.Wait);

            //Null Response
            List<AuditTrailResponseEntity> nullAuditTrailResponseEntities = new();
            nullAuditTrailResponseEntities = null;
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Returns(Task.FromResult(nullAuditTrailResponseEntities));

            method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNull(method.Result);

            //Access with ALL studyType
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.studyType.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "ALL"; z.Title = "ALL";
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Returns(Task.FromResult(auditTrailResponseEntities));
            method = CommonService.GetAuditTrail(mvp.ClinicalStudy.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            //Access with studyId
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = mvp.ClinicalStudy.StudyId; z.Title = mvp.ClinicalStudy.StudyTitle;
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            method = CommonService.GetAuditTrail(mvp.ClinicalStudy.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            //Groups Null
            grps = null;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            method = CommonService.GetAuditTrail(mvp.ClinicalStudy.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            //Error
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                    .Throws(new Exception("Error"));
            method = CommonService.GetAuditTrail(mvp.ClinicalStudy.StudyId, DateTime.MinValue, DateTime.MinValue, user);

            Assert.Throws<AggregateException>(method.Wait);

            user.UserRole = Constants.Roles.Org_Admin;
        }
        #endregion

        #region GET StudyHistory
        [Test]
        public void GetStudyHistory_UnitTesting()
        {
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<StudyHistoryResponseEntity> studyHistories = new()
            {
                new StudyHistoryResponseEntity
                {
                    StudyId = mvp.ClinicalStudy.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = mvp.ClinicalStudy.StudyIdentifiers,
                    StudyTitle = mvp.ClinicalStudy.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = mvp.ClinicalStudy.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    UsdmVersion = Constants.USDMVersions.MVP
                },
                new StudyHistoryResponseEntity
                {
                    StudyId = v1.ClinicalStudy.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v1.ClinicalStudy.StudyIdentifiers,
                    StudyTitle = v1.ClinicalStudy.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v1.ClinicalStudy.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v1.ClinicalStudy.StudyType,
                    UsdmVersion = Constants.USDMVersions.V1
                },
                new StudyHistoryResponseEntity
                {
                    StudyId = v2.ClinicalStudy.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v2.ClinicalStudy.StudyIdentifiers,
                    StudyTitle = v2.ClinicalStudy.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v2.ClinicalStudy.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v2.ClinicalStudy.StudyType,
                    UsdmVersion = Constants.USDMVersions.V1_9
                }
            };
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                   .Returns(Task.FromResult(studyHistories));

            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            //Group is Null
            user.UserRole = Constants.Roles.App_User;
            Config.IsGroupFilterEnabled = true;
            var grps = GetUserDataFromStaticJson().SDRGroups;
            grps = null;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();
            result = method.Result;
            Assert.IsNull(result);
            Config.IsGroupFilterEnabled = false;
            user.UserRole = Constants.Roles.Org_Admin;

            //no studies
            studyHistories = null;
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(studyHistories));
            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                  .Throws(new Exception("Error"));

            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region POST SearchStudyTitle
        [Test]
        public void SearchTitleUnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<SearchTitleResponseEntity> studyList = new()
            {
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = mvp.ClinicalStudy.StudyIdentifiers,
                    StudyId = mvp.ClinicalStudy.StudyId,
                    StudyTitle = mvp.ClinicalStudy.StudyTitle,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    SDRUploadVersion = mvp.AuditTrail.SDRUploadVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v1.ClinicalStudy.StudyIdentifiers,
                    StudyId = v1.ClinicalStudy.StudyId,
                    StudyTitle = v1.ClinicalStudy.StudyTitle,
                    StudyType = v1.ClinicalStudy.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v2.ClinicalStudy.StudyIdentifiers,
                    StudyId = v2.ClinicalStudy.StudyId,
                    StudyTitle = v2.ClinicalStudy.StudyTitle,
                    StudyType = v2.ClinicalStudy.StudyType,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                }
            };
            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>(), user))
                 .Returns(Task.FromResult(studyList));
            SearchTitleParametersDto searchParameters = new()
            {
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                SponsorId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
            };

            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.SearchTitle(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>(), user))
                .Throws(new Exception("Error"));

            method = CommonService.SearchTitle(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>(), user))
                .Returns(Task.FromResult(null as List<SearchTitleResponseEntity>));

            method = CommonService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsEmpty(result);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>(), user))
                 .Returns(Task.FromResult(studyList));
            searchParameters.GroupByStudyId = true;
            method = CommonService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsNotEmpty(result);

            string[] sortByFields = { "studytitle", "sponsorid", "lastmodifieddate", "version", "" };
            string[] sortOrder = { "asc", "desc" };
            sortOrder.ToList().ForEach(sortOrderItem =>
            {
                sortByFields.ToList().ForEach(sortByField =>
                {
                    searchParameters.SortOrder = sortOrderItem;
                    searchParameters.SortBy = sortByField;
                    var method_sort = CommonServices.SortStudyTitle(result, searchParameters);
                    Assert.IsNotEmpty(method_sort);
                });
            });

            user.UserRole = Constants.Roles.App_User;
            method = CommonService.SearchTitle(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsEmpty(result);
            user.UserRole = Constants.Roles.Org_Admin;
        }
        #endregion

        #region CheckAccess for List Of Studies
        [Test]
        public void CheckAccessForListOfStudiesUnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.App_User;
            user.UserName = "user1@SDR.com";
            CommonStudyEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<SearchTitleResponseEntity> searchTitleResponseEntity = GetSearchResponse();

            //No Groups Matching
            var grps = GetUserDataFromStaticJson().SDRGroups;
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.studyType.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "PATIENT_REGISTRY"; z.Title = "PATIENT_REGISTRY";
                    });
                });
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "sd1"; z.Title = "sd1";
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var searchTitleDTOList = _mockMapper.Map<List<SearchTitleResponseDto>>(searchTitleResponseEntity);
            searchTitleDTOList = CommonServices.AssignStudyIdentifiers(searchTitleDTOList, searchTitleResponseEntity);
            var method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);
            method.Wait();
            var result = method.Result;

            Assert.IsNull(result);

            //ALL filter
            grps = GetUserDataFromStaticJson().SDRGroups;
            searchTitleResponseEntity = GetSearchResponse();
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.studyType.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "ALL"; z.Title = "ALL";
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));

            method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);
            method.Wait();
            result = method.Result;

            Assert.IsNotNull(result);

            //Matching StudyType
            grps = GetUserDataFromStaticJson().SDRGroups;
            searchTitleResponseEntity = GetSearchResponse();
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.studyType.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "INTERVENTIONAL"; z.Title = "INTERVENTIONAL";
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));

            method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);
            method.Wait();
            result = method.Result;

            Assert.IsNotNull(result);

            //Matching Study Id
            grps = GetUserDataFromStaticJson().SDRGroups;
            searchTitleResponseEntity = GetSearchResponse();
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = "aaed3efe-7d70-4c9e-90e2-3446e936c291"; z.Title = "Umbrella Study of Cancer";
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));

            method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);
            method.Wait();
            result = method.Result;

            Assert.IsNotNull(result);

            //No Groups
            grps = null;
            searchTitleResponseEntity = GetSearchResponse();
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));

            method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);
            method.Wait();
            result = method.Result;

            Assert.IsNull(result);

            //Error
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                  .Throws(new Exception(""));

            method = CommonService.CheckAccessForListOfStudies(searchTitleResponseEntity, user);

            Assert.Throws<AggregateException>(method.Wait);
            user.UserRole = Constants.Roles.Org_Admin;
        }
        #endregion

        #region Search
        [Test]
        public void SearchUnitTesting()
        {
            Config.IsGroupFilterEnabled = true;
            user.UserRole = Constants.Roles.Org_Admin;
            user.UserName = "user1@SDR.com";
            var mvp = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.Study.StudyEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.MVP)));
            var v1 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV1.StudyEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V1)));
            var v2 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV2.StudyEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V1_9)));
            mvp.AuditTrail.UsdmVersion = Constants.USDMVersions.MVP;
            v1.AuditTrail.UsdmVersion = Constants.USDMVersions.V1;
            v2.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            List<SearchResponseEntity> studyList = new()
            {
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(mvp.ClinicalStudy.StudyIdentifiers, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        }
                    })),
                    StudyId = mvp.ClinicalStudy.StudyId,
                    StudyTitle = mvp.ClinicalStudy.StudyTitle,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    StudyPhase = mvp.ClinicalStudy.StudyPhase,
                    SDRUploadVersion = mvp.AuditTrail.StudyVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                    InterventionModelMVP = mvp.ClinicalStudy.CurrentSections.Where(x => x.StudyDesigns != null).Select(x => x.StudyDesigns).Select(x => x.Select(x => x.CurrentSections.Where(x => x.InvestigationalInterventions != null).Select(x => x.InvestigationalInterventions.Select(x => x.InterventionModel)))) ?? null,
                    StudyDesignIdsMVP = mvp.ClinicalStudy.CurrentSections.Select(x => x.StudyDesigns?.Select(x => x.StudyDesignId)) ?? null,
                    StudyIndicationsMVP = mvp.ClinicalStudy.CurrentSections.Where(x => x.StudyIndications != null).Select(x => x.StudyIndications?.Select(x => x.Description)) ?? null,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v1.ClinicalStudy.StudyIdentifiers)),
                    StudyId = v1.ClinicalStudy.Uuid,
                    StudyTitle = v1.ClinicalStudy.StudyTitle,
                    StudyType = v1.ClinicalStudy.StudyType,
                    StudyPhase = v1.ClinicalStudy.StudyPhase,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                    InterventionModel = v1.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v1.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDesc)) ?? null,
                    StudyDesignIds = v1.ClinicalStudy.StudyDesigns.Select(x => x.Uuid ?? x.Uuid) ?? null,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v2.ClinicalStudy.StudyIdentifiers)),
                    StudyId = v2.ClinicalStudy.StudyId,
                    StudyTitle = v2.ClinicalStudy.StudyTitle,
                    StudyType = v2.ClinicalStudy.StudyType,
                    StudyPhase = v2.ClinicalStudy.StudyPhase,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                    InterventionModel = v2.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v2.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription)) ?? null,
                    StudyDesignIds = v2.ClinicalStudy.StudyDesigns.Select(x => x.Id) ?? null,
                }
            };
            CommonCodeEntity commonCode = JsonConvert.DeserializeObject<CommonCodeEntity>(JsonConvert.SerializeObject(v2.ClinicalStudy.StudyType));
            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>(), user))
                 .Returns(Task.FromResult(studyList));

            SearchParametersDto searchParameters = new()
            {
                Indication = "Bile",
                InterventionModel = "CROSS_OVER",
                StudyTitle = "Umbrella",
                PageNumber = 1,
                PageSize = 25,
                Phase = "PHASE_1_TRAIL",
                SponsorId = "100",
                FromDate = DateTime.Now.AddDays(-5).ToString(),
                ToDate = DateTime.Now.ToString(),
                Asc = true,
                Header = "phase",
                ValidateUsdmVersion = false
            };

            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.SearchStudy(searchParameters, user);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchResponseDto>>(
                JsonConvert.SerializeObject(result));

            string[] sortByFields = { "studytitle", "studyphase", "lastmodifieddate", "sponsorid", "sdrversion", "interventionmodel", "indication", "test" };
            bool[] sortOrder = { true, false };
            sortOrder.ToList().ForEach(sortOrderItem =>
            {
                sortByFields.ToList().ForEach(sortByField =>
                {
                    searchParameters.Asc = sortOrderItem;
                    searchParameters.Header = sortByField;
                    var method_sort = CommonServices.SortSearchResults(actual_result, searchParameters);
                    Assert.IsNotEmpty(method_sort);
                });
                searchParameters.Asc = sortOrderItem;
                searchParameters.Header = null;
                var method_sort = CommonServices.SortSearchResults(actual_result, searchParameters);
                Assert.IsNotEmpty(method_sort);
            });
            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            user.UserRole = Constants.Roles.App_User;
            Config.IsGroupFilterEnabled = true;
            //_mockCommonRepository.Setup(x => x.GetGroupsOfUser(It.IsAny<LoggedInUser>()))
            //    .Returns(Task.FromResult(null as List<SDRGroupsEntity>));
            //method = CommonService.SearchStudy(searchParameters, user);
            //method.Wait();
            //result = method.Result;
            //Assert.IsNull(result);
            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>(), user))
                .Throws(new Exception("Error"));

            method = CommonService.SearchStudy(searchParameters, user);
            Assert.Throws<AggregateException>(method.Wait);

            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(null as List<SearchResponseEntity>));

            method = CommonService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;
            Assert.IsNull(result);

            _mockCommonRepository.Setup(x => x.SearchStudyMVP(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.Study.SearchResponse> { new Core.Entities.Study.SearchResponse
                {
                    StudyId = mvp.ClinicalStudy.StudyId,
                    StudyTitle = mvp.ClinicalStudy.StudyTitle,
                    StudyIdentifiers = mvp.ClinicalStudy.StudyIdentifiers,
                    StudyType = mvp.ClinicalStudy.StudyType,
                    StudyPhase = mvp.ClinicalStudy.StudyPhase,
                    StudyVersion = mvp.AuditTrail.StudyVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,                    
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,                                                            
                } }));

            _mockCommonRepository.Setup(x => x.SearchStudyV1(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV1.SearchResponseEntity> { new Core.Entities.StudyV1.SearchResponseEntity
                {
                    StudyId = v1.ClinicalStudy.Uuid,
                    StudyTitle = v1.ClinicalStudy.StudyTitle,
                    StudyIdentifiers = v1.ClinicalStudy.StudyIdentifiers,
                    StudyType = v1.ClinicalStudy.StudyType,
                    StudyPhase = v1.ClinicalStudy.StudyPhase,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,                    
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                    InterventionModel = v1.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v1.ClinicalStudy.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                    StudyDesignIds = v1.ClinicalStudy.StudyDesigns.Select(x => x.Uuid ?? x.Uuid) ?? null,
                } }));

            _mockCommonRepository.Setup(x => x.SearchStudyV2(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV2.SearchResponseEntity> { new Core.Entities.StudyV2.SearchResponseEntity
                {                    
                    StudyId = v2.ClinicalStudy.StudyId,
                    StudyTitle = v2.ClinicalStudy.StudyTitle,
                    StudyIdentifiers = v2.ClinicalStudy.StudyIdentifiers,
                    StudyType = v2.ClinicalStudy.StudyType,
                    StudyPhase = v2.ClinicalStudy.StudyPhase,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,                    
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                    InterventionModel = v2.ClinicalStudy.StudyDesigns.Select(y => y.InterventionModel) ?? null,                    
                    StudyDesignIds = v2.ClinicalStudy.StudyDesigns.Select(x => x.Id) ?? null,
                } }));

            searchParameters.ValidateUsdmVersion = true;
            searchParameters.UsdmVersion = Constants.USDMVersions.MVP;
            method = CommonService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;

            searchParameters.UsdmVersion = Constants.USDMVersions.V1;
            method = CommonService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;

            searchParameters.UsdmVersion = Constants.USDMVersions.V1_9;
            method = CommonService.SearchStudy(searchParameters, user);
            method.Wait();
            result = method.Result;


        }
        #endregion

        #region GetLinks
        [Test]
        public void GetLinksUnitTesting()
        {
            _mockCommonRepository.Setup(x => x.GetUsdmVersion(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(Constants.USDMVersions.V1_9));
            CommonServices commonServices = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = commonServices.GetLinks("a", 1, user);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            _mockCommonRepository.Setup(x => x.GetUsdmVersion(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(null as string));
            method = commonServices.GetLinks("a", 1, user);
            method.Wait();
            Assert.IsNull(method.Result);

            _mockCommonRepository.Setup(x => x.GetUsdmVersion(It.IsAny<string>(), It.IsAny<int>()))
                     .Throws(new Exception("Error"));
            method = commonServices.GetLinks("a", 1, user);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion
        #endregion
    }
}
