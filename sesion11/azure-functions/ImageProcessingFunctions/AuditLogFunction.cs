using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ImageProcessingFunctions
{
    public class AuditLogFunction
    {
        private readonly ILogger<AuditLogFunction> _logger;

        public AuditLogFunction(ILogger<AuditLogFunction> logger)
        {
            _logger = logger;
        }

        [Function("AuditLogFunction")]
        public Task Run([QueueTrigger("audit-queue", Connection = "AzureWebJobsStorage")] string message, FunctionContext context)
        {
            var logger = context.GetLogger("AuditLogFunction");

            logger.LogInformation($"Audit log received: {message}");

            return Task.CompletedTask;
        }
    }
}
