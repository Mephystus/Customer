// -------------------------------------------------------------------------------------
//  <copyright file="ExternalCustomerServiceFactory.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Factories.Implementations;

using System;
using Customer.ExternalServices.Factories.Interfaces;
using Customer.ExternalServices.Interfaces;
using Customer.Infrastructure.Settings;
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

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="ExternalCustomerServiceFactory"/> class.
    /// </summary>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
    public ExternalCustomerServiceFactory(IConfiguration configuration)
    {
        _configuration = configuration;
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
        var appsettings = _configuration.Get<AppSettings>();

        var assemblyName = appsettings.ExternalCustomerServices[identifier];

        IExternalCustomerService service = GetInstance<IExternalCustomerService>(assemblyName);

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
    private static T GetInstance<T>(string assemblyName) where T : class
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

        var instance = Activator.CreateInstance(objectType) as T;

        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        return instance;
    }

    #endregion Private Methods
}