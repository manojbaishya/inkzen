using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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

public class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigurationManager config = builder.Configuration;

        builder.Services.AddAuthentication()
            .AddCookie(options =>
                {
                    options.AccessDeniedPath = new("/manager/login/");
                    options.LoginPath = new("/manager/Login/");
                    options.SlidingExpiration = true;
                }
            )
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = ctx => Task.CompletedTask,
                        OnAuthenticationFailed = ctx =>
                        {
                            Console.WriteLine("JwtBearerEvents Exception: {0}", ctx.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                }
            );

        builder.AddPiranha(options =>
        {
            options.UseCms();
            options.UseManager();

            options.UseFileStorage(naming: FileStorageNaming.UniqueFolderNames);
            options.UseImageSharp();
            options.UseTinyMCE();
            options.UseMemoryCache();

            options.UseApi(api => api.AllowAnonymousAccess = true);

            string connectionString = config.GetConnectionString("piranha");
            options.UseEF<SQLiteDb>(db => db.UseSqlite(connectionString));
            options.UseIdentityWithSeed<IdentitySQLiteDb>(db => db.UseSqlite(connectionString));
        });


        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PiranhaCMS API", Version = "v1" });
            options.CustomSchemaIds(x => x.FullName);
        });


        WebApplication app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PiranhaCMS API V1"); });
        }


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

        app.Run();
    }
}