using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

string? storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

if (string.IsNullOrEmpty(storageConnectionString))
{
    throw new InvalidOperationException("AzureWebJobsStorage is not set. Check local.settings.json.");
}

builder.Services.AddSingleton(new BlobServiceClient(storageConnectionString));
builder.Services.AddSingleton(new QueueServiceClient(storageConnectionString));

builder.Build().Run();
