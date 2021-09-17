// -------------------------------------------------------------------------------------
//  <copyright file="DbHealthCheck.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Defines the health check implementation for the dependency: 'DB'
/// </summary>
public class DbHealthCheck : IHealthCheck
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<DbHealthCheck> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="DbHealthCheck" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{DbHealthCheck}"/></param>
    public DbHealthCheck(ILogger<DbHealthCheck> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Runs the health check, returning the status of the component being checked.
    /// </summary>
    /// <param name="context">A context object associated with the current execution.</param>
    /// <param name="cancellationToken">The token that can be used to cancel the health check.</param>
    /// <returns>A <see cref="Task"/> that completes when the health check has finished,
    /// yielding the status of the component being checked.</returns>
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            //// TODO: implement health check against dependencies

            var rnd = new Random();
            var val = rnd.Next(0, 200);

            if (val % 2 == 0)
            {
                throw new Exception("Failed DB health check!");
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed DB health check!");

            return Task.FromResult(new HealthCheckResult(
                context.Registration.FailureStatus,
                description: "Failed health check!",
                exception: ex,
                data: null));
        }
    }
}