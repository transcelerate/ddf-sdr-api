﻿using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Core.Utilities.Common;
using AutoMapper;
using NUnit.Framework;
using System.Net;
using TransCelerate.SDR.WebApi.Controllers;
using TransCelerate.SDR.Services.Services;
using Microsoft.AspNetCore.Mvc;
using TransCelerate.SDR.Core.ErrorModels;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.DTO.Common;
using Newtonsoft.Json.Serialization;

namespace TransCelerate.SDR.UnitTesting.ControllerUnitTesting
{
    public class CommonControllerunitTesting
    {
        #region Variables
        private ILogHelper _mockLogger = Mock.Of<ILogHelper>();
        private Mock<ICommonService> _mockCommonService = new Mock<ICommonService>(MockBehavior.Loose);
        #endregion

        #region Setup

        LoggedInUser user = new LoggedInUser
        {
            UserName = "user1@SDR.com",
            UserRole = Constants.Roles.Org_Admin
        };
        #endregion

        #region Get RawJson
        [Test]
        public void GetRawJsonUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);
            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
                .Returns(Task.FromResult(data as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new GetRawJsonDto()
            {
                StudyDefinitions = JsonConvert.SerializeObject(data,new JsonSerializerSettings
                            {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                })
            };

            //Actual            
            var actual_result = JsonConvert.DeserializeObject<GetRawJsonDto>(
                 JsonConvert.SerializeObject((result as OkObjectResult).Value));

            Assert.AreEqual(expected.StudyDefinitions,actual_result.StudyDefinitions);
        }
        [Test]
        public void GetRawJsonFailureUnitTesting()
        {

            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/StudyDataV2.json");
            var data = JsonConvert.DeserializeObject<GetRawJsonEntity>(jsonData);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
             .Returns(Task.FromResult(Constants.ErrorMessages.Forbidden as object));
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetRawJson("sd", 1);
            method.Wait();
            var result = method.Result;

            //Expected
            var expected = new ErrorModel { message = Constants.ErrorMessages.Forbidden, statusCode = "403" };

            //Actual            
            var actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(403, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Throws(new Exception(""));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.GenericError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);

            _mockCommonService.Setup(x => x.GetRawJson(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LoggedInUser>()))
               .Returns(Task.FromResult(null as object));

            method = commonController.GetRawJson("sd", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyNotFound, statusCode = "404" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(404, (result as ObjectResult).StatusCode);

            method = commonController.GetRawJson("", 1);
            method.Wait();
            result = method.Result;

            //Expected
            expected = new ErrorModel { message = Constants.ErrorMessages.StudyInputError, statusCode = "400" };

            //Actual            
            actual_result = (result as ObjectResult).Value as ErrorModel;

            Assert.AreEqual(expected.message, actual_result.message);
            Assert.IsInstanceOf(typeof(ObjectResult), result);
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);           
        }
        #endregion

        #region Get API -> USDM Version Mapping
        [Test]
        public void GetApiUsdmVersionMappingUnitTesting()
        {
            string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + @"/Data/ApiUsdmVersionMapping.json");            
            ApiUsdmVersionMapping_NonStatic apiUsdmVersionMapping_NonStatic = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(jsonData);
            ApiUsdmVersionMapping.SDRVersions = apiUsdmVersionMapping_NonStatic.SDRVersions;
            CommonController commonController = new CommonController(_mockCommonService.Object, _mockLogger);

            var method = commonController.GetApiUsdmMapping();

            var result = (method as OkObjectResult).Value;

            //Expected
            var expected = apiUsdmVersionMapping_NonStatic;
            
            //Actual            
            var actual_result = JsonConvert.DeserializeObject<ApiUsdmVersionMapping_NonStatic>(JsonConvert.SerializeObject(result));

            Assert.AreEqual(expected.SDRVersions.Count, actual_result.SDRVersions.Count);

            Mock<ILogHelper> _mockLogg = new Mock<ILogHelper>(MockBehavior.Loose);
            _mockLogg.Setup(x => x.LogInformation(It.IsAny<string>())).Throws(new Exception("Error"));

            CommonController commonController1 = new CommonController(_mockCommonService.Object, _mockLogg.Object);
            Assert.Throws<System.Exception>(() => commonController1.GetApiUsdmMapping());
        }
        #endregion
    }
}
