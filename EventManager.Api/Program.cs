using EventManager.Api.Endpoints;
using EventManager.Core;
using EventManager.Core.Validator;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB connection string
builder
    .Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserValidator, UserValidator>();
builder.Services.AddTransient<IEventValidator, EventValidator>();

//Probably change to scoped if switching to JWT
builder.Services.AddSingleton<IUserService, UserService>();
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
