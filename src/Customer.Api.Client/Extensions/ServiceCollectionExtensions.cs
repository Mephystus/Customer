// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Extensions;

using Implementations;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Api.Client.Configuration;
using SharedLibrary.Api.Client.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the customer API client into the DI pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <param name="clientSettingsDictionary">The collection of HTTP client settings.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomerHttpClient(
        this IServiceCollection services,
        Dictionary<string, HttpClientSettings> clientSettingsDictionary)
    {
        const string key = nameof(ICustomerApiClient);

        if (!clientSettingsDictionary.ContainsKey(key))
        {
            throw new ArgumentException($"Could not find the key '{key}' in the dictionary '{nameof(clientSettingsDictionary)}'", key);
        }

        var clientSettings = clientSettingsDictionary[key];

        services.AddHttpClient<ICustomerApiClient, CustomerApiClient>(clientSettings);

        return services;
    }

    #endregion Public Methods
}