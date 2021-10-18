// -------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Customer.Api.Extensions;
using Customer.Api.Filters;
using Customer.Data.Access.Extensions;
using Customer.Infrastructure.Settings;
using Customer.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SharedLibrary.Api.Extensions;
using SharedLibrary.Filters.Filters;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

Log.Information("Starting up");

builder.Host.UseSerilog();

builder.Host.UseMetricsWebTracking()
    .UseMetrics(options => 
    {
        options.EndpointOptions = endpointsOptions =>
        {
            endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointsOptions.EnvironmentInfoEndpointEnabled = false;
        };
    });

builder.WebHost.UseKestrel(options =>
{
    options.AddServerHeader = false;
});

// Add services to the container.
var services = builder.Services;

services.Configure<KestrelServerOptions>(options => 
{
    options.AllowSynchronousIO = true;
});

services.AddMetrics();

services.AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
        options.Filters.Add<UnhandledExceptionFilter>();
        options.Filters.Add<NotFoundExceptionFilter>();
        options.Filters.Add<ValidationExceptionFilter>();
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add<LanguageFilter>();
    })
    .ConfigureApiBehaviorOptions(options => 
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        var enumConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(enumConverter);
    });

services.AddRouting(options => options.LowercaseUrls = true);

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Customer.Api", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
});

var appSettings = builder.Configuration.Get<AppSettings>();

services.AddSingleton(appSettings);

services.AddDbContext(builder.Configuration);
services.AddRepositoriesDependencyInjection();
services.AddServicesAutoMapper();
services.AddServicesDependencyInjection();
services.AddModelsFluentValidation();
services.AddApiHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.Api v1");
    });
}

app.UseRouting();

app.UseApiHealthChecks();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
