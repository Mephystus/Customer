// -------------------------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Extensions;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using SharedLibrary.Models.Models.HealthCheck;


/// <summary>
/// Provides extension methods for the <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ApplicationBuilderExtensions
{

    /// <summary>
    /// Adds a middleware that provides the health check status for the API.
    /// </summary>
    /// <param name="app">The applications builder.</param>
    /// <returns>An instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseApiHealthChecks(this IApplicationBuilder app) 
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = WriteHealthCheckResponseAsync
        });

        return app;
    }

    /// <summary>
    /// Writes the health check response into the HTTP context response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="report">The health report.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task WriteHealthCheckResponseAsync(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            Details = report.Entries.Select(x => new HealthCheckDetail
            {
                Component = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description ?? string.Empty
            }),
            Duration = report.TotalDuration
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
