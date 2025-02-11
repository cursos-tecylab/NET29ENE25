using Usuario.Api.Extensions;
using Usuario.Application;
using Usuario.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.SeedData();
app.ApplyMigrations();

app.UseCustomExceptionHandler();

app.MapControllers();
app.Run();