using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLite;
using Piranha.Local;
using Piranha.Manager.Editor;

namespace Inkzen.Api;

public static class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigurationManager appsettings = builder.Configuration;
        ConfigureBuilder(builder, appsettings);

        WebApplication app = builder.Build();
        ConfigureApplication(app, appsettings);
        app.Run();
    }
    private static void ConfigureBuilder(WebApplicationBuilder builder, ConfigurationManager appsettings)
    {
        builder.Services
        .AddAuthentication()
        .AddCookie(options =>
        {
            options.AccessDeniedPath = new("/manager/login/");
            options.LoginPath = new("/manager/login/");
            options.SlidingExpiration = true;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidIssuer = appsettings["JwtSettings:Issuer"],
                ValidAudience = appsettings["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings["JwtSettings:Key"])),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = _ => Task.CompletedTask,
                OnAuthenticationFailed = ctx =>
                {
                    Console.WriteLine("JwtBearerEvents Exception: {0}", ctx.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });

        // builder.Services.AddHostFiltering(options => options.AllowedHosts = appsettings.GetSection("AllowedHosts").Get<string[]>());
        // foreach (Cors corsSetting in appsettings.GetSection("Cors").Get<Cors[]>())
        // {
        //     builder.Services.AddCors(options => options.AddPolicy(name: corsSetting.Origin, policy => policy.WithOrigins(corsSetting.Endpoints)));
        // }

        builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins",
                builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()));

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.Request;
            logging.RequestHeaders.Add("Origin");
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PiranhaCMS API", Version = "v1" });
            options.CustomSchemaIds(x => x.FullName);
        });

        builder.AddPiranha(options =>
        {
            options.UseCms();
            options.UseManager();
            options.UseFileStorage(naming: FileStorageNaming.UniqueFolderNames);
            options.UseImageSharp();
            options.UseTinyMCE();
            options.UseMemoryCache();
            options.UseApi(api => api.AllowAnonymousAccess = true);
            string databaseConnection = appsettings.GetConnectionString("piranha");
            options.UseEF<SQLiteDb>(db => db.UseSqlite(databaseConnection));
            options.UseIdentityWithSeed<IdentitySQLiteDb>(db => db.UseSqlite(databaseConnection));
        });

        builder.Services.AddPiranhaMemoryCache();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    }
    private static void ConfigureApplication(WebApplication app, ConfigurationManager appsettings)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PiranhaCMS API V1"));
        }

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        // app.UseHostFiltering();
        // foreach (Cors corsSetting in appsettings.GetSection("Cors").Get<Cors[]>())
        // {
        //     app.UseCors(corsSetting.Origin);
        // }

        app.UseCors("AllowAllOrigins");

        app.UseFileServer();
        app.UseHttpLogging();
        app.UseExceptionHandler((_ => { }));

        app.UsePiranha(options =>
        {
            // Initialize Piranha
            App.Init(options.Api);
            options.UseManager();
            // Build content types
            new ContentTypeBuilder(options.Api)
                .AddAssembly(typeof(Program).Assembly)
                .Build()
                .DeleteOrphans();
            // Configure Tiny MCE
            options.UseTinyMCE();
            EditorConfig.FromFile("editorconfig.json");
            options.UseIdentity();
        });
    }
}

public class Cors
{
    public string Origin { get; set; }
    public string[] Endpoints { get; set; }
}