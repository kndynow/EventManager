using System.Text;
using EventManager.Api.Endpoints;
<<<<<<< HEAD
<<<<<<< HEAD
using EventManager.Api.Jwt;
using EventManager.Core;
using EventManager.Core.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static EventManager.Api.Jwt.TokenService;
=======
=======
using EventManager.Api.Jwt;
>>>>>>> 1aa3bc0 (Created TokenService which generates a JWT-token on succesful login in the Login-endpoint.)
using EventManager.Core.Services;
using EventManager.Core.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
<<<<<<< HEAD
>>>>>>> b7db955 (Setting up the Jwt Authentication with token validation parameters.)
=======
using static EventManager.Api.Jwt.TokenService;
>>>>>>> 1aa3bc0 (Created TokenService which generates a JWT-token on succesful login in the Login-endpoint.)

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//Avoiding CORS-error
builder.Services.AddCors(options =>
{
<<<<<<< HEAD
    options.AddPolicy(
        "AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7274").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// Configure MongoDB connection string
builder
    .Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IUserValidator, UserValidator>();
builder.Services.AddSingleton<IEventValidator, EventValidator>();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        //In production = true
        //options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
    });
=======
    options.ConnectionString = mongoConnectionString;
    options.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"];  // From appsettings
});

builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUserValidator, UserValidator>();
builder.Services.AddTransient<IEventValidator, EventValidator>();
builder.Services.AddSingleton<ITokenService, TokenService>();

//Probably change to scoped if switching to JWT
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IEventService, EventService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //In production = true
    //options.RequireHttpsMetadata = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

//// Cookie authentication (might switch to JWT later)
//builder
//    .Services.AddAuthentication("Cookies")
//    .AddCookie(
//        "Cookies",
//        options =>
//        {
//            options.Cookie.Name = "auth";
//            options.Cookie.HttpOnly = true;
//            options.Cookie.SameSite = SameSiteMode.Strict;
//        }
//    );
>>>>>>> b7db955 (Setting up the Jwt Authentication with token validation parameters.)

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

app.UseCors("AllowBlazorClient");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map all endpoints
app.MapEndpoints<Program>();

app.Run();
