// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Demo.Extensions;

using DemoServiceReference;
using Infrastructure.Settings;
using Interceptors;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

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
    /// <param name="settings">The settings.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddExternalServicesDependencyInjection(
        this IServiceCollection services,
        AppSettings settings)
    {
        services.AddScoped<IClientMessageInspector, MessageInspector>();
        services.AddScoped<IEndpointBehavior, InspectorBehavior>();

        services.AddScoped<SOAPDemoSoap>(serviceProvider =>
        {
            var endPointBehavior = serviceProvider.GetRequiredService<IEndpointBehavior>();

            var soapClient = new SOAPDemoSoapClient(
                    SOAPDemoSoapClient.EndpointConfiguration.SOAPDemoSoap,
                    settings.DemoExternalCustomerService.Endpoint);

            soapClient.Endpoint.EndpointBehaviors.Add(endPointBehavior);

            return soapClient;
        });

        return services;
    }

    #endregion Public Methods
}