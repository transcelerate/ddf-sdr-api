using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.AzureFunctions
{

    public class ChangeAuditFunction
    {
        private readonly IMessageProcessor _messageProcessor;
        private readonly ILogHelper _logger;

        public ChangeAuditFunction(IMessageProcessor messageProcessor,ILogHelper logger)
        {
            _messageProcessor = messageProcessor;
            _logger = logger;
        }

        /// <summary>
        /// Azure Service Bus Trigger for Change Audit
        /// </summary>
        /// <param name="myQueueItem">Queue Message</param>
        
        [FunctionName(Constants.FunctionAppConstants.ChangeAuditFunction)]
        public void Run([ServiceBusTrigger(Constants.FunctionAppConstants.AzureServiceBusQueueName,
            Connection = Constants.FunctionAppConstants.AzureServiceBusConnectionString)]string myQueueItem)
        {
            try
            {
                _logger.LogInformation($"C# ServiceBus queue trigger function message : {myQueueItem}");
                _messageProcessor.ProcessMessage(myQueueItem);
                _logger.LogInformation($"C# ServiceBus queue trigger function has processed the message : {myQueueItem}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Exception Occured: {ex}");
                throw;
            }
        }
    }
}
