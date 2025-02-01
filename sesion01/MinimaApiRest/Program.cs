var builder = WebApplication.CreateBuilder(args);

ThreadPool.SetMinThreads(1, 1);
ThreadPool.SetMaxThreads(1, 1);

var app = builder.Build();

app.MapPost("createUser", (HttpContext context) => // X usa verbo y retorna 200
{
    var user = new { Id = Guid.NewGuid() , Name = "Fernando" , Email = "fernando@correo.com"  };
    return Results.Ok(user);
});

app.MapPost("user", (HttpContext context) => // X usa verbo y retorna 200
{
    var user = new { Id = Guid.NewGuid() , Name = "Fernando" , Email = "fernando@correo.com"  };
    return Results.Ok(user);
});

app.MapPost("beers", (HttpContext context) => // retornar 400 y 201
{
    var requestBody = context.Request.ReadFromJsonAsync<Beer>().Result;
    if (requestBody is null || string.IsNullOrWhiteSpace(requestBody.Name))
    {
        return Results.BadRequest("Name is required");
    }

    Thread.Sleep(2000);

    var beer = new Beer(Guid.NewGuid(), requestBody.Name);
    return Results.Created($"beers/{beer.Id}", beer );

});

app.MapPost("wines", async (HttpContext context) => // retornar 400 y 201
{
    var requestBody = await context.Request.ReadFromJsonAsync<Wines>();
    if (requestBody is null || string.IsNullOrWhiteSpace(requestBody.Name))
    {
        return Results.BadRequest("Name is required");
    }

    await Task.Delay(2000);

    var wine = new Wines(Guid.NewGuid(), requestBody.Name);
    return Results.Created($"wines/{wine.Id}", wine );

});

app.Run();
record Beer(Guid Id, string Name);
record Wines(Guid Id, string Name);