using Cursos.Api.Extensions;
using Cursos.Api.gRPC;
using Cursos.Application;
using Cursos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel((context,options)=>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.MapControllers();
app.MapGrpcService<CursosGrpcService>();

app.Run();
