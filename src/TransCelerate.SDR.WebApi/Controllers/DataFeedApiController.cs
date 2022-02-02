using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransCelerate.SDR.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataFeedApiController : ControllerBase
    {

        //Get Static Response from DataFeed API
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]        
        public IActionResult GetSample()
        {
            return Ok("Welcome To Transcelerate SDR");
        }
    }
}
