using Products.Api;
using Products.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApi();
var app = builder.Build();
app.ConfigureApp();
app.Run();