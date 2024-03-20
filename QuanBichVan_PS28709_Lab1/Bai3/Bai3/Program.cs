var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome Fpoly to Asp.net Core");

app.Run();
