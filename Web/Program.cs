using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

Startup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseAuthorization();

Startup.Configure(app, app.Environment);

app.Run();
