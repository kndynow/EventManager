using System.Reflection;
using System.Text;
using EventManager.Api.Endpoints;
using EventManager.Api.Jwt;
using EventManager.Core;
using EventManager.Core.Validator;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static EventManager.Api.Jwt.TokenService;

var builder = WebApplication.CreateBuilder(args);

//Avoiding CORS-error
builder.Services.AddCors(options =>
{
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

builder.Services.AddAuthorization();
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

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
