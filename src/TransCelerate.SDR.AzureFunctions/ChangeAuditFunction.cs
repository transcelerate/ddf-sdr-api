using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TransCelerate.SDR.AzureFunctions
{
    public class ChangeAuditFunction
    {
        [FunctionName("ChangeAuditFunction")]
        public void Run([ServiceBusTrigger("testqueue", Connection = "AzureServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message from deployed code: {myQueueItem}");
        }
    }
}
