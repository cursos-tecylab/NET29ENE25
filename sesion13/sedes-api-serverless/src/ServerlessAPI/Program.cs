using System.Text.Json;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using ServerlessAPI.Repositories;
using ServerlessAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Configuracion
builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

//Logger
builder.Logging
        .ClearProviders()
        .AddJsonConsole();
 
// Add services to the container.
builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

string region = Environment.GetEnvironmentVariable("AWS_REGION") ?? RegionEndpoint.USEast2.SystemName;

string? dynamoDbLocalUrl = builder.Configuration["AWS:DynamoDBLocalUrl"];
builder.Services.AddSingleton<IAmazonDynamoDB>(provider =>
{
    return !string.IsNullOrEmpty(dynamoDbLocalUrl)
        ? new AmazonDynamoDBClient(new AmazonDynamoDBConfig { ServiceURL = dynamoDbLocalUrl })
        : new AmazonDynamoDBClient(RegionEndpoint.GetBySystemName(region));
});
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

var s3LocalConfig = builder.Configuration.GetSection("AWS:S3Local");
if (!string.IsNullOrEmpty(s3LocalConfig["ServiceUrl"]))
{
    builder.Services.AddSingleton<IAmazonS3>(new AmazonS3Client(
        s3LocalConfig["AccessKey"],
        s3LocalConfig["SecretKey"],
        new AmazonS3Config
        {
            ServiceURL = s3LocalConfig["ServiceUrl"],
            ForcePathStyle = true
        }));
}
else
{
    builder.Services.AddSingleton<IAmazonS3>(new AmazonS3Client(RegionEndpoint.GetBySystemName(region)));
}


// Add AWS Lambda support. When running the application as an AWS Serverless application, Kestrel is replaced
// with a Lambda function contained in the Amazon.Lambda.AspNetCoreServer package, which marshals the request into the ASP.NET Core hosting framework.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddScoped<ISedeRepository, SedeRepository>();
builder.Services.AddScoped<IS3Service, S3Service>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
