// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Demo;

using Customer.Infrastructure.Settings;
using DemoServiceReference;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the services.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the services dependency injection into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddExternalServicesDependencyInjection(
        this IServiceCollection services,
        AppSettings settings)
    {
        services.AddScoped<SOAPDemoSoap>(
            x => new SOAPDemoSoapClient(
                SOAPDemoSoapClient.EndpointConfiguration.SOAPDemoSoap,
                settings.DemoExternalCustomerService.Endpoint));

        return services;
    }

    #endregion Public Methods
}