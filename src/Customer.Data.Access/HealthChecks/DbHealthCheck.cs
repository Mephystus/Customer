// -------------------------------------------------------------------------------------
//  <copyright file="DbHealthCheck.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.HealthChecks;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

/// <summary>
/// Defines the health check implementation for the dependency: 'DB'
/// </summary>
public class DbHealthCheck : IHealthCheck
{
    #region Private Fields

    /// <summary>
    /// The customer repository.
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<DbHealthCheck> _logger;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="DbHealthCheck" /> class.
    /// </summary>
    /// <param name="customerRepository">An instance of <see cref="ICustomerRepository"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger{DbHealthCheck}"/></param>
    public DbHealthCheck(
        ICustomerRepository customerRepository,
        ILogger<DbHealthCheck> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Runs the health check, returning the status of the component being checked.
    /// </summary>
    /// <param name="context">A context object associated with the current execution.</param>
    /// <param name="cancellationToken">The token that can be used to cancel the health check.</param>
    /// <returns>A <see cref="Task"/> that completes when the health check has finished,
    /// yielding the status of the component being checked.</returns>
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (await _customerRepository.CheckDatabaseConnectionAsync(cancellationToken))
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed DB health check!");

            return new HealthCheckResult(
                context.Registration.FailureStatus,
                "Failed health check!",
                ex);
        }
    }

    #endregion Public Methods
}