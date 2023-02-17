// -------------------------------------------------------------------------------------
//  <copyright file="ExternalCustomerServiceFactory.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Factories.Implementations;

using System;
using Customer.ExternalServices.Interfaces;
using Infrastructure.Settings;
using Interfaces;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Provides a direct implementation for the external customer factory.
/// </summary>
public class ExternalCustomerServiceFactory : IExternalCustomerServiceFactory
{
    #region Private Fields

    /// <summary>
    /// The configuration.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="ExternalCustomerServiceFactory"/> class.
    /// </summary>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
    /// <param name="serviceProvider">An instance of <see cref="IServiceProvider"/>.</param>
    public ExternalCustomerServiceFactory(
        IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Gets an external customer service.
    /// </summary>
    /// <param name="identifier">The service identifier.</param>
    /// <returns>An instance of <see cref="IExternalCustomerService"/></returns>
    public IExternalCustomerService GetExternalCustomerService(string identifier)
    {
        var appSettings = _configuration.Get<AppSettings>();

        var assemblyName = appSettings.ExternalCustomerServices[identifier];

        var service = GetInstance<IExternalCustomerService>(assemblyName);

        return service;
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Gets an instance of an object by its assembly qualified name.
    /// </summary>
    /// <typeparam name="T">The instance type.</typeparam>
    /// <param name="assemblyName">
    /// The object full assembly qualified name:
    ///     <i>"{namespace}.{class name}, {assembly name}"</i>
    /// </param>
    /// <returns>An instance of <see cref="T"/></returns>
    private   T GetInstance<T>(string assemblyName) where T : class
    {
        if (string.IsNullOrWhiteSpace(assemblyName))
        {
            throw new ArgumentNullException(nameof(assemblyName));
        }

        var objectType = Type.GetType(assemblyName);

        if (objectType == null)
        {
            throw new ArgumentNullException(nameof(objectType));
        }

        var instance = Activator.CreateInstance(objectType, _serviceProvider) as T;

        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        return instance;
    }

    #endregion Private Methods
}