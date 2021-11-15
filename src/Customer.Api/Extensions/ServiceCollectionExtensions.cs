// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Extensions;

using Data.Access.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the API health checks into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<DbHealthCheck>("Db");

        return services;
    }

    #endregion Public Methods
}