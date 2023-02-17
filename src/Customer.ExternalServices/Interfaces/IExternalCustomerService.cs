// -------------------------------------------------------------------------------------
//  <copyright file="IExternalCustomerService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Interfaces;

using System;
using System.Threading.Tasks;
using Dto;

/// <summary>
/// Defines the contract for the customer external services.
/// </summary>
public interface IExternalCustomerService
{
    #region Public Methods

    /// <summary>
    /// Gets the customer risk .
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerRiskResponse"/></returns>
    Task<CustomerRiskResponse> GetCustomerRiskAsync(Guid customerId);

    #endregion Public Methods
}