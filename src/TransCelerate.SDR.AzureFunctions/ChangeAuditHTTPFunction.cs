using System.IO;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.AzureFunctions
{
    public class ChangeAuditHttpFunction
    {
        private readonly IMessageProcessor _messageProcessor;
        private readonly ILogHelper _logger;

        public ChangeAuditHttpFunction(IMessageProcessor messageProcessor, ILogHelper logger)
        {
            _messageProcessor = messageProcessor;
            _logger = logger;
        }

        [Function("getChangeAudit")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "getChangeAudit", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = new StreamReader(req.Body).ReadToEnd();
            _messageProcessor.ProcessMessage(requestBody);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
