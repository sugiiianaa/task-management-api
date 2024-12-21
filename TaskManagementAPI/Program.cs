using TaskManagementAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Service Configuration
builder.Services.ConfigureAppServices(builder.Configuration);

var app = builder.Build();

// Middleware Configuration
app.ConfigureAppMiddleware();

app.Run();