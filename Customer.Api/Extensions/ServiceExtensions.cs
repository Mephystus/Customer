// -------------------------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Extensions;

using Customer.Api.HealthChecks;
using Customer.Api.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds the API health checks into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApiHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<AnotherHealthCheck>("Another");
        services.AddHealthChecks().AddCheck<DbHealthCheck>("Db");

        return services;
    }

    /// <summary>
    /// Adds the models fluent validations into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddModelsFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<CustomerRequestValidator>();
            fv.RegisterValidatorsFromAssemblyContaining<EmailRequestValidator>();
        });

        return services;
    }  
}