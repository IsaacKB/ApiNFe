using ApiNFe.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.RegisterMiddlewares();

app.Run();
