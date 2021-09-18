// -------------------------------------------------------------------------------------
//  <copyright file="IExternalCustomerServiceFactory.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Factories.Interfaces;

using Customer.ExternalServices.Interfaces;

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