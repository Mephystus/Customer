﻿// -------------------------------------------------------------------------------------
//  <copyright file="IExternalCustomerServiceFactory.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Factories.Interfaces;

using Customer.ExternalServices.Interfaces;

/// <summary>
/// Provides the contract for the external customer services factory.
/// </summary>
public interface IExternalCustomerServiceFactory
{
    #region Public Methods

    /// <summary>
    /// Gets an external customer service.
    /// </summary>
    /// <param name="identifier">The service identifier.</param>
    /// <returns>An instance of <see cref="IExternalCustomerService"/></returns>
    IExternalCustomerService GetExternalCustomerService(string identifier);

    #endregion Public Methods
}