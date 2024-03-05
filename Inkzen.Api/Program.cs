using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AttributeBuilder;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.Data.EF.SQLite;
using Piranha.Manager.Editor;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Inkzen.Api;

public class Program
{
    private static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.AddPiranha(options =>
        {

            options.UseCms();
            options.UseManager();

            options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
            options.UseImageSharp();
            options.UseTinyMCE();
            options.UseMemoryCache();

            options.UseApi(api => api.AllowAnonymousAccess = true);

            string connectionString = builder.Configuration.GetConnectionString("piranha");
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
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PiranhaCMS API V1");
            });
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