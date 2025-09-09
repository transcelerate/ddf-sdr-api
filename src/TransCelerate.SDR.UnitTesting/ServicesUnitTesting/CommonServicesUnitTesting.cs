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
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
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
        public static List<SearchTitleResponseEntity> GetSearchResponse()
        {
            CommonStudyDefinitionsEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyDefinitionsEntity v4 = GetData(Constants.USDMVersions.V3);
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
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
                    StudyIdentifiers = v4.Study.Versions.FirstOrDefault()?.StudyIdentifiers,
                    StudyId = v4.Study.StudyId,
                    //StudyTitle = JsonConvert.DeserializeObject<List<CommonStudyTitle>>(JsonConvert.SerializeObject(v4.Study.Versions.FirstOrDefault()?.Titles)).GetStudyTitle(Constants.StudyTitle.OfficialStudyTitle),
                    StudyTitle = v4.Study.Versions.FirstOrDefault()?.Titles,
                    StudyType = v4.Study.Versions.FirstOrDefault()?.StudyType,
                    SDRUploadVersion = v4.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v4.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v4.AuditTrail.UsdmVersion,
                },
                new SearchTitleResponseEntity
                {
                    StudyIdentifiers = v3.Study.StudyIdentifiers,
                    StudyId = v3.Study.StudyId,
                    StudyTitle = v3.Study.StudyTitle,
                    StudyType = v3.Study.StudyType,
                    SDRUploadVersion = v3.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v3.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v3.AuditTrail.UsdmVersion,
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
            else
            {
                string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");                
                var v4 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntity>(jsonData);
                return v4;
            }
        }

        public static CommonStudyDefinitionsEntityV5 GetDataV5()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            var v5 = JsonConvert.DeserializeObject<CommonStudyDefinitionsEntityV5>(jsonData);
            return v5;
        }

        [SetUp]
        public void SetUp()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SharedAutoMapperProfiles());
            });
            _mockMapper = new Mapper(mockMapper);

            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json"));
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
        }
        #endregion

        #region Unit Testing
        #region GET Raw JSON
        [Test]
        public void GetRawJsonUnitTesting()
        {
            CommonServices commonServices = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));

            var method = commonServices.GetRawJson("a", 1);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV4.json");            
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV5.json");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);

            jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV3.json");
            jsonData = jsonData.Replace($"{IdFieldPropertyName.ParentElement.ClinicalStudy.ChangeToCamelCase()}", $"{nameof(CommonStudyDefinitionsEntity.Study).ChangeToCamelCase()}");
            data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1);
            method.Wait();
            result = method.Result;
            Assert.IsNotNull(result);

            data = null;
            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.FromResult(data));

            method = commonServices.GetRawJson("a", 1);
            method.Wait();
            result = method.Result;
            Assert.Null(result);

            _mockCommonRepository.Setup(x => x.GetStudyItemsAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .Throws(new Exception("Error"));
            method = commonServices.GetRawJson("a", 1);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region GET AuditTrail
        [Test]
        public void GetStudyAudit_UnitTesting()
        {
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v4 = GetData(Constants.USDMVersions.V3);
            CommonStudyDefinitionsEntityV5 v5 = GetDataV5();
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
                    EntryDateTime = v4.AuditTrail.EntryDateTime,
                    StudyType = v4.Study.Versions.FirstOrDefault()?.StudyType,
                    SDRUploadVersion = 2,
                    HasAccess = true,
                    UsdmVersion = v4.AuditTrail.UsdmVersion
                },
                new AuditTrailResponseEntity
                {
                    EntryDateTime = v5.AuditTrail.EntryDateTime,
                    StudyType = v5.Study.StudyType,
                    SDRUploadVersion = 2,
                    HasAccess = true,
                    UsdmVersion = v5.AuditTrail.UsdmVersion
                }
            };
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                   .Returns(Task.FromResult(auditTrailResponseEntities));

            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<AuditTrailResponseEntity>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            //Exception
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Throws(new Exception("Error"));

            method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue);

            Assert.Throws<AggregateException>(method.Wait);

            //Null Response
            List<AuditTrailResponseEntity> nullAuditTrailResponseEntities = new();
            nullAuditTrailResponseEntities = null;
            _mockCommonRepository.Setup(x => x.GetAuditTrail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                  .Returns(Task.FromResult(nullAuditTrailResponseEntities));

            method = CommonService.GetAuditTrail("1", DateTime.MinValue, DateTime.MinValue);
            method.Wait();

            Assert.IsNull(method.Result);
        }
        #endregion

        #region GET StudyHistory
        [Test]
        public void GetStudyHistory_UnitTesting()
        {
            CommonStudyDefinitionsEntity v3 = GetData(Constants.USDMVersions.V2);
            CommonStudyDefinitionsEntity v4 = GetData(Constants.USDMVersions.V3);
            CommonStudyDefinitionsEntityV5 v5 = GetDataV5();
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
                    StudyId = v4.Study.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v4.Study.StudyIdentifiers,
                    StudyTitle = v4.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v4.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v4.Study.StudyType,
                    UsdmVersion = Constants.USDMVersions.V3
                },
                new StudyHistoryResponseEntity
                {
                    StudyId = v5.Study.StudyId,
                    ProtocolVersions = new List<string>() { "1", "2" },
                    StudyIdentifiers = v5.Study.StudyIdentifiers,
                    StudyTitle = v5.Study.StudyTitle,
                    EntryDateTime = DateTime.Now,
                    StudyVersion = v5.Study.StudyVersion,
                    SDRUploadVersion = 1,
                    StudyType = v5.Study.StudyType,
                    UsdmVersion = Constants.USDMVersions.V1_9
                }
            };
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                   .Returns(Task.FromResult(studyHistories));

            CommonServices CommonService = new(_mockCommonRepository.Object, _mockLogger, _mockMapper);

            var method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "");
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);

            //no studies
            studyHistories = null;
            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(studyHistories));
            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "");
            method.Wait();

            Assert.IsNull(method.Result);

            _mockCommonRepository.Setup(x => x.GetStudyHistory(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                  .Throws(new Exception("Error"));

            method = CommonService.GetStudyHistory(DateTime.Now, DateTime.MinValue, "");


            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion

        #region POST SearchStudyTitle
        [Test]
        public void SearchTitleUnitTesting()
        {
            CommonStudyDefinitionsEntity mvp = GetData(Constants.USDMVersions.MVP);
            CommonStudyDefinitionsEntity v1 = GetData(Constants.USDMVersions.V3);
            CommonStudyDefinitionsEntityV5 v2 = GetDataV5();
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
            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>()))
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

            var method = CommonService.SearchTitle(searchParameters);
            method.Wait();
            var result = method.Result;

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<List<SearchTitleResponseDto>>(
                JsonConvert.SerializeObject(result));

            //Assert          
            Assert.IsNotNull(actual_result);
            Assert.IsNotEmpty(actual_result);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>()))
                .Throws(new Exception("Error"));

            method = CommonService.SearchTitle(searchParameters);
            Assert.Throws<AggregateException>(method.Wait);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>()))
                .Returns(Task.FromResult(null as List<SearchTitleResponseEntity>));

            method = CommonService.SearchTitle(searchParameters);
            method.Wait();
            result = method.Result;
            Assert.IsEmpty(result);

            _mockCommonRepository.Setup(x => x.SearchTitle(It.IsAny<SearchTitleParametersEntity>()))
                 .Returns(Task.FromResult(studyList));
            searchParameters.GroupByStudyId = true;
            method = CommonService.SearchTitle(searchParameters);
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
        }
        #endregion

        #region Search
        [Test]
        public void SearchUnitTesting()
        {
            var regex = new Regex(Regex.Escape(nameof(CommonStudyDefinitionsEntity.Study)));                        
            var v3 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV3.StudyDefinitionsEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V2)));            
            var v4 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV4.StudyDefinitionsEntity>(JsonConvert.SerializeObject(GetData(Constants.USDMVersions.V3)));
            var v5 = JsonConvert.DeserializeObject<TransCelerate.SDR.Core.Entities.StudyV5.StudyDefinitionsEntity>(JsonConvert.SerializeObject(GetDataV5()));
            v3.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            v4.AuditTrail.UsdmVersion = Constants.USDMVersions.V3;
            v5.AuditTrail.UsdmVersion = Constants.USDMVersions.V4;
            List<SearchResponseEntity> studyList = new()
            {
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
                },
                new SearchResponseEntity
                {
                    StudyIdentifiersV4 = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v4.Study.Versions.FirstOrDefault()?.StudyIdentifiers)),
                    StudyIdV4 = v4.Study.Id,
                    StudyTitleV4 = v4.Study.Versions.FirstOrDefault()?.Titles,
                    StudyTypeV4 = v4.Study.Versions.FirstOrDefault()?.StudyType,
                    StudyPhaseV4 = v4.Study.Versions.FirstOrDefault()?.StudyPhase,
                    SDRUploadVersion = v4.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v4.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v4.AuditTrail.UsdmVersion,
                    InterventionModel = v4.Study.Versions.FirstOrDefault()?.StudyDesigns.Select(y => y.InterventionModel) ?? null,
                    StudyIndications = v4.Study.Versions.FirstOrDefault()?.StudyDesigns.Select(y => y.Indications.Select(z => z.Description)) ?? null,
                    StudyDesignIds = v4.Study.Versions.FirstOrDefault()?.StudyDesigns.Select(x => x.Id) ?? null,
                },
                new SearchResponseEntity
                {
                    StudyIdentifiers = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(v5.Study.Versions.FirstOrDefault()?.StudyIdentifiers)),
                    StudyId = v5.Study.Id,
                    StudyTitleV4 = v5.Study.Versions.FirstOrDefault()?.Titles,
                    StudyTypeV4 = v5.Study.Versions.FirstOrDefault()?.StudyDesigns.FirstOrDefault().StudyType,
                    StudyPhase = v5.Study.Versions.FirstOrDefault()?.StudyDesigns.FirstOrDefault().StudyPhase,
                    SDRUploadVersion = v5.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v5.AuditTrail.EntryDateTime,
                    HasAccess = true,
                    UsdmVersion = v5.AuditTrail.UsdmVersion,
                    InterventionModelV4 = v5.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.StudyInterventionIds)) ?? null,
                    StudyIndicationsV4 = v5.Study.Versions.Select(z=>z.StudyDesigns.Select(y => JsonConvert.DeserializeObject<List<Core.Entities.Common.CommonStudyIndication>>(JsonConvert.SerializeObject(y.Indications)) )) ?? null,
                    StudyDesignIdsV4 = v5.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.Id)) ?? null,
                },
            };
            CommonCodeEntity commonCode = JsonConvert.DeserializeObject<CommonCodeEntity>(JsonConvert.SerializeObject(v5.Study.Versions.FirstOrDefault()?.StudyDesigns.FirstOrDefault().StudyType));
            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>()))
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

            var method = CommonService.SearchStudy(searchParameters);
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

            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>()))
                .Throws(new Exception("Error"));

            method = CommonService.SearchStudy(searchParameters);
            Assert.Throws<AggregateException>(method.Wait);

            _mockCommonRepository.Setup(x => x.SearchStudy(It.IsAny<SearchParametersEntity>()))
                .Returns(Task.FromResult(null as List<SearchResponseEntity>));

            method = CommonService.SearchStudy(searchParameters);
            method.Wait();
            result = method.Result;
            Assert.IsNull(result);

            _mockCommonRepository.Setup(x => x.SearchStudyV3(It.IsAny<SearchParametersEntity>()))
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

            _mockCommonRepository.Setup(x => x.SearchStudyV4(It.IsAny<SearchParametersEntity>()))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV4.SearchResponseEntity> { new Core.Entities.StudyV4.SearchResponseEntity
                {
                    StudyId = v4.Study.Id,
                    StudyTitle = v4.Study.Versions.FirstOrDefault()?.Titles,
                    StudyIdentifiers = v4.Study.Versions.FirstOrDefault()?.StudyIdentifiers,
                    StudyType = v4.Study.Versions.FirstOrDefault()?.StudyType,
                    StudyPhase = v4.Study.Versions.FirstOrDefault()?.StudyPhase,
                    SDRUploadVersion = v4.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v4.AuditTrail.EntryDateTime,
                    UsdmVersion = v4.AuditTrail.UsdmVersion,
                    InterventionModel = v4.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.InterventionModel)) ?? null,
                    StudyIndications = v4.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.Indications)) ?? null,
                    StudyDesignIds = v4.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.Id)) ?? null,
                } }));

            _mockCommonRepository.Setup(x => x.SearchStudyV5(It.IsAny<SearchParametersEntity>()))
                .Returns(Task.FromResult(new List<Core.Entities.StudyV5.SearchResponseEntity> { new Core.Entities.StudyV5.SearchResponseEntity
                {                    
                    StudyId = v5.Study.Id,
                    StudyTitle = v5.Study.Versions.FirstOrDefault()?.Titles,
                    StudyIdentifiers = v5.Study.Versions.FirstOrDefault()?.StudyIdentifiers,
                    StudyType = v5.Study.Versions.FirstOrDefault()?.StudyDesigns.FirstOrDefault().StudyType,
                    StudyPhase = v5.Study.Versions.FirstOrDefault()?.StudyDesigns.FirstOrDefault().StudyPhase,
                    SDRUploadVersion = v5.AuditTrail.SDRUploadVersion,
                    EntryDateTime = v5.AuditTrail.EntryDateTime,                    
                    UsdmVersion = v5.AuditTrail.UsdmVersion,
                    InterventionModel = v5.Study.Versions.Select(z=>z.StudyInterventions[0].Codes),                   
                    StudyDesignIds = v5.Study.Versions.Select(z=>z.StudyDesigns.Select(y => y.Id)) ?? null,
                } }));
            

            searchParameters.ValidateUsdmVersion = true;
            searchParameters.UsdmVersion = Constants.USDMVersions.MVP;
            method = CommonService.SearchStudy(searchParameters);
            method.Wait();
            result = method.Result;

            searchParameters.UsdmVersion = Constants.USDMVersions.V3;
            method = CommonService.SearchStudy(searchParameters);
            method.Wait();
            result = method.Result;

            searchParameters.UsdmVersion = Constants.USDMVersions.V1_9;
            method = CommonService.SearchStudy(searchParameters);
            method.Wait();
            result = method.Result;

            searchParameters.UsdmVersion = Constants.USDMVersions.V2;
            method = CommonService.SearchStudy(searchParameters);
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

            var method = commonServices.GetLinks("a", 1);
            method.Wait();
            var result = method.Result;
            Assert.IsNotNull(result);

            _mockCommonRepository.Setup(x => x.GetUsdmVersion(It.IsAny<string>(), It.IsAny<int>()))
                   .Returns(Task.FromResult(null as string));
            method = commonServices.GetLinks("a", 1);
            method.Wait();
            Assert.IsNull(method.Result);

            _mockCommonRepository.Setup(x => x.GetUsdmVersion(It.IsAny<string>(), It.IsAny<int>()))
                     .Throws(new Exception("Error"));
            method = commonServices.GetLinks("a", 1);

            Assert.Throws<AggregateException>(method.Wait);
        }
        #endregion
        #endregion
    }
}
