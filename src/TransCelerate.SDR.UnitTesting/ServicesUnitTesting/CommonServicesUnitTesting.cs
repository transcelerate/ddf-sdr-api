using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            CommonStudyDefinitionsEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            return new()
            {
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = mvp.Study.StudyIdentifiers,
                    StudyId = mvp.Study.StudyId,
                    StudyTitle = mvp.Study.StudyTitle,
                    StudyType = mvp.Study.StudyType,
                    SDRUploadVersion = mvp.AuditTrail.SDRUploadVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyId = v1.Study.StudyId,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyType = v1.Study.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyType = v2.Study.StudyType,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                }
            };
        }
        public static CommonStudyDefinitionsEntity GetData(string usdmVersion)
        {
            if (usdmVersion == Constants.USDMVersions.V2)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");                
                var v3 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
                return v3;
            }
            else if (usdmVersion == Constants.USDMVersions.V1)
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
                jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
                var v1 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
                return v1;
            }
            else
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
                jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
                var v2 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
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
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));

            var method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
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
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.AreEqual(result, Constants.ErrorMessages.Forbidden);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV1.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1, user);
            method.Wait();
            result = method.Result;
            Assert.AreEqual(result, Constants.ErrorMessages.Forbidden);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
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
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<AuditTrailResponseEntity> auditTrailResponseEntities = new()
            {
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v3.AuditTrail.EntryDateTime,
                    SDRUploadVersion = 1,
                    StudyType = v3.Study.StudyType,
                    HasAccess = true,
                    UsdmVersion = v3.AuditTrail.UsdmVersion
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    StudyType = v1.Study.StudyType,
                    SDRUploadVersion = 2,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    StudyType = v2.Study.StudyType,
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
            method = CommonService.GetAuditTrail(v3.Study.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            //Access with studyId
            grps.ForEach(x =>
            {
                x.GroupFilter.Where(y => y.GroupFieldName == GroupFieldNames.study.ToString()).ToList().ForEach(y =>
                {
                    y.GroupFilterValues.ForEach(z =>
                    {
                        z.GroupFilterValueId = v3.Study.StudyId; z.Title = v3.Study.StudyTitle;
                    });
                });
            });
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            method = CommonService.GetAuditTrail(v3.Study.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.IsNotNull(method.Result);

            //Groups Null
            grps = null;
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                   .Returns(Task.FromResult(grps));
            method = CommonService.GetAuditTrail(v3.Study.StudyId, DateTime.MinValue, DateTime.MinValue, user);
            method.Wait();

            Assert.AreEqual(method.Result.ToString(), Constants.ErrorMessages.Forbidden);

            //Error
            _mockCommonRepository.Setup(x => x.GetGroupsOfUser(user))
                    .Throws(new Exception("Error"));
            method = CommonService.GetAuditTrail(v3.Study.StudyId, DateTime.MinValue, DateTime.MinValue, user);

            Assert.Throws<AggregateException>(method.Wait);

            user.UserRole = Constants.Roles.Org_Admin;
        }
        #endregion

        #region GET StudyHistory
        [Test]
        public void GetStudyHistory_UnitTesting()
        {
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<StudyHistoryResponseEntity> studyHistories = new()
            {
                new StudyHistoryResponseEntity
                {
                    StudyId = v3.Study.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v3.Study.StudyIdentifiers,
                    StudyTitle = v3.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v3.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v3.Study.StudyType,
                    UsdmVersion = Constants.USDMVersions.MVP
                },
                new StudyHistoryResponseEntity
                {
                    StudyId = v1.Study.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyTitle = v1.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v1.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v1.Study.StudyType,
                    UsdmVersion = Constants.USDMVersions.V1
                },
                new StudyHistoryResponseEntity
                {
                    StudyId = v2.Study.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyTitle = v2.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v2.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v2.Study.StudyType,
                    UsdmVersion = Constants.USDMVersions.V1_9
                }
            };
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
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

            //no studies
            studyHistories = null;
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
                    .Returns(Task.FromResult(studyHistories));
            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "", user);
            method.Wait();

            Assert.IsNull(method.Result);

            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<LoggedInUser>()))
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
            CommonStudyDefinitionsEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
            List<SearchTitleResponseEntity> studyList = new()
            {
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = mvp.Study.StudyIdentifiers,
                    StudyId = mvp.Study.StudyId,
                    StudyTitle = mvp.Study.StudyTitle,
                    StudyType = mvp.Study.StudyType,
                    SDRUploadVersion = mvp.AuditTrail.SDRUploadVersion,
                    EntryDateTime = mvp.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = mvp.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyId = v1.Study.StudyId,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyType = v1.Study.StudyType,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyType = v2.Study.StudyType,
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
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V1);
            CommonStudyDefinitionsEntity v2 = GetData(Constants.USDMVersions.V1_9);
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
            var regex = new Regex(Regex.Escape(nameof(CommonStudyDefinitionsEntity.Study)));
            var v1String = regex.Replace(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V1)), IdFieldPropertyName.ParentElement.ClinicalStudy, 1);            
            var v1_9String = regex.Replace(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V1_9)), IdFieldPropertyName.ParentElement.ClinicalStudy, 1);                        
            var v1 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV1.StudyDefinitionsEntity>(v1String);
            var v2 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV2.StudyDefinitionsEntity>(v1_9String);
            var v3 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V2)));            
            v1.AuditTrail.UsdmVersion = Constants.USDMVersions.V1;
            v2.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            v3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            List<SearchResponseEntity> studyList = new()
            {                
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v1.Study.StudyIdentifiers)),
                    StudyId = v1.Study.Uuid,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyType = v1.Study.StudyType,
                    StudyPhase = v1.Study.StudyPhase,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                    InterventionModel = v1.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v1.Study.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDesc)) ?? null,
                    StudyDesignIds = v1.Study.StudyDesigns.Select(x => x.Uuid ?? x.Uuid) ?? null,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v2.Study.StudyIdentifiers)),
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyType = v2.Study.StudyType,
                    StudyPhase = v2.Study.StudyPhase,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                    InterventionModel = v2.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v2.Study.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription)) ?? null,
                    StudyDesignIds = v2.Study.StudyDesigns.Select(x => x.Id) ?? null,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v3.Study.StudyIdentifiers)),
                    StudyId = v3.Study.StudyId,
                    StudyTitle = v3.Study.StudyTitle,
                    StudyType = v3.Study.StudyType,
                    StudyPhase = v3.Study.StudyPhase,
                    SDRUploadVersion = v3.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v3.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v3.AuditTrail.UsdmVersion,
                    InterventionModel = v3.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v3.Study.StudyDesigns.Select(y => y.StudyIndications.Select(z => z.IndicationDescription)) ?? null,
                    StudyDesignIds = v3.Study.StudyDesigns.Select(x => x.Id) ?? null,
                }
            };
            CommonCodeEntity commonCode = JsonConvert.DeserializeObject<CommonCodeEntity>(JsonConvert.SerializeObject(v2.Study.StudyType));
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


            _mockCommonRepository.Setup(x => x.SearchStudyV1(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV1.SearchResponseEntity> { new Core.Entities.StudyV1.SearchResponseEntity
                {
                    StudyId = v1.Study.Uuid,
                    StudyTitle = v1.Study.StudyTitle,
                    StudyIdentifiers = v1.Study.StudyIdentifiers,
                    StudyType = v1.Study.StudyType,
                    StudyPhase = v1.Study.StudyPhase,
                    SDRUploadVersion = v1.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v1.AuditTrail.EntryDateTime,                    
                    UsdmVersion = v1.AuditTrail.UsdmVersion,
                    InterventionModel = v1.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v1.Study.StudyDesigns.Select(y => y.StudyIndications) ?? null,
                    StudyDesignIds = v1.Study.StudyDesigns.Select(x => x.Uuid ?? x.Uuid) ?? null,
                } }));

            _mockCommonRepository.Setup(x => x.SearchStudyV2(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV2.SearchResponseEntity> { new Core.Entities.StudyV2.SearchResponseEntity
                {                    
                    StudyId = v2.Study.StudyId,
                    StudyTitle = v2.Study.StudyTitle,
                    StudyIdentifiers = v2.Study.StudyIdentifiers,
                    StudyType = v2.Study.StudyType,
                    StudyPhase = v2.Study.StudyPhase,
                    SDRUploadVersion = v2.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v2.AuditTrail.EntryDateTime,                    
                    UsdmVersion = v2.AuditTrail.UsdmVersion,
                    InterventionModel = v2.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,                    
                    StudyDesignIds = v2.Study.StudyDesigns.Select(x => x.Id) ?? null,
                } }));
            _mockCommonRepository.Setup(x => x.SearchStudyV3(It.IsAny<SearchParametersEntity>(), user))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV3.SearchResponseEntity> { new Core.Entities.StudyV3.SearchResponseEntity
                {
                    StudyId = v3.Study.StudyId,
                    StudyTitle = v3.Study.StudyTitle,
                    StudyIdentifiers = v3.Study.StudyIdentifiers,
                    StudyType = v3.Study.StudyType,
                    StudyPhase = v3.Study.StudyPhase,
                    SDRUploadVersion = v3.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v3.AuditTrail.EntryDateTime,
                    UsdmVersion = v3.AuditTrail.UsdmVersion,
                    InterventionModel = v3.Study.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyDesignIds = v3.Study.StudyDesigns.Select(x => x.Id) ?? null,
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

            searchParameters.UsdmVersion = Constants.USDMVersions.V2;
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
