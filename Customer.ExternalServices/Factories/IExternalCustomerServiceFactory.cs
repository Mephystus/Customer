// -------------------------------------------------------------------------------------
//  <copyright file="IExternalCustomerServiceFactory.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------


using Customer.ExternalServices.Interfaces;

namespace Customer.ExternalServices.Factories;

/// <summary>
/// Provides the contract for the external customer services factory.
/// </summary>
public interface IExternalCustomerServiceFactory
{
    /// <summary>
    /// Gets an external customer service.
    /// </summary>
    /// <param name="identifier">The service identifier.</param>
    /// <returns>An instance of <see cref="IExternalCustomerService"/></returns>
    IExternalCustomerService GetExternalCustomerService(string identifier);
}