using Web;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.Configure(app.Environment);

app.Run();
