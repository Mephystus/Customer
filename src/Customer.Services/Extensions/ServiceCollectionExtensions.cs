// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Extensions;

using Customer.ExternalServices.Factories.Implementations;
using Customer.ExternalServices.Factories.Interfaces;
using Implementations;
using Interfaces;
using Mappers;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the services.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the services auto-mapper configuration into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddServicesAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(c => c.AddProfile<CustomerMappingProfile>());

        return services;
    }

    /// <summary>
    /// Adds the services dependency injection into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IExternalCustomerServiceFactory, ExternalCustomerServiceFactory>();

        return services;
    }

    #endregion Public Methods
}