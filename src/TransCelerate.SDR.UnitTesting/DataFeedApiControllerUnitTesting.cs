using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TransCelerate.SDR.WebApi.Controllers;

namespace TransCelerate.SDR.UnitTesting
{
    public class DataFeedApiControllerUnitTest
    {
        [TestCase("Welcome To Transcelerate SDR")]        
        public void DataFeedApiController_GetSample_SuccessResponse(string testCase)
        {
            DataFeedApiController dataFeedApiController = new DataFeedApiController();
            var method = dataFeedApiController.GetSample();
            Assert.IsNotNull(method);
            Assert.IsInstanceOf(typeof(OkObjectResult), method);
            Assert.AreEqual(200, (method as OkObjectResult).StatusCode);
            Assert.AreEqual(testCase, (method as OkObjectResult).Value);
            Assert.AreNotEqual("Welcome Transcelerate SDR", (method as OkObjectResult).Value);
        }
    }
}
