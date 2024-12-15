using Application.Contracts;
using Application.Services.Users;
using DataBase.Repositories.Contracts;
using DataBase.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Application.Services.Shipments;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // Logovi će se prikazivati u konzol
    .Enrich.FromLogContext() // Dodaje dodatne informacije u log (kao što je putanja zahteva)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        RoleClaimType = ClaimTypes.Role
    };
});
// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();

builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseCors(config =>
{
    config.AllowAnyHeader();
    config.AllowAnyMethod();
    config.AllowAnyOrigin();
});

app.UseSerilogRequestLogging();

app.Run();
