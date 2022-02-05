using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Seraphim.Interactions;

public static class TestInteractionHandler
{
    [FunctionName("TestInteractionHandler")]
    public static void Run(
        [ServiceBusTrigger(
            topicName: "interactions",
            subscriptionName: "Test",
            Connection = "SERAPHIM_SERVICE_BUS_CONNECTION_STRING")]
        string myQueueItem, 
        ILogger log)
    {
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
    }
}
