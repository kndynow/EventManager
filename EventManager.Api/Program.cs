using EventManager.Api.Endpoints;
using EventManager.Core;
using EventManager.Core.Data;
using EventManager.Core.Services;
using EventManager.Core.Validator;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Access MongoDB connection string from environment variable
var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");

// Configure MongoDbSettings using the environment variable
builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoConnectionString;  // Get it from the environment variable
    options.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"];  // From appsettings
});

builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUserValidator, UserValidator>();
builder.Services.AddTransient<IEventValidator, EventValidator>();


//Probably change to scoped if switching to JWT
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IEventService, EventService>();

// Cookie authentication (might switch to JWT later)
builder
    .Services.AddAuthentication("Cookies")
    .AddCookie(
        "Cookies",
        options =>
        {
            options.Cookie.Name = "auth";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
        }
    );

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Todo: consider scalar? https://youtu.be/Tx49o-5tkis?feature=shared
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map all endpoints
app.MapEndpoints<Program>();

app.Run();
