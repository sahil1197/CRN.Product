using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CRN.Product.Api.Extensions;
using CRN.Product.Application.Authentication;
using CRN.Product.Application.Interfaces;
using CRN.Product.Application.Mapping;
using CRN.Product.Application.Services;
using CRN.Product.Application.Validators;
using CRN.Product.Infrastructure.Data;
using CRN.Product.Infrastructure.Identity;
using CRN.Product.Infrastructure.Repository;
using CRN.Product.Infrastructure.Seed;
using CRN.Product.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

#endregion

#region Serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region Health Checks

builder.Services.AddHealthChecks();

#endregion

#region Controllers

builder.Services.AddControllers();

#endregion

#region Apiversioning
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);

        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ReportApiVersions = true;

        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";

        options.SubstituteApiVersionInUrl = true;
    });
#endregion

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRN Product REST API",
        Version = "v1",
        Description = "RESTful Product Management API built using ASP.NET Core 8, Entity Framework Core, SQL Server and JWT Authentication.",
        Contact = new OpenApiContact
        {
            Name = "Kishore Baradkar",
            Email = "kishorbaradkar007@gmail.com"
        }
    });

    // XML Documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    


    // JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization using the Bearer scheme.\n\nExample:\nBearer eyJhbGciOiJIUzI1NiIs...",

        Name = "Authorization",

        In = ParameterLocation.Header,

        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

#region Database

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region JWT Authentication

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

#endregion

#region Dependency Injection

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

#endregion

#region FluentValidation

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

#endregion

var app = builder.Build();

#region Database Seed

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await DbSeeder.SeedAsync(context);
}

#endregion

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "CRN Product API";

        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "CRN Product API v1");

        options.RoutePrefix = "swagger";

        options.DisplayRequestDuration();

        options.EnableTryItOutByDefault();

        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseGlobalExceptionHandling();

app.UseAuthentication();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

#endregion

app.Run();