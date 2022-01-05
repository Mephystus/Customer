// -------------------------------------------------------------------------------------
//  <copyright file="ICustomerService.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

/// <summary>
/// Defines the contract for the customer services.
/// </summary>
public interface ICustomerService
{
    #region Public Methods

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateCustomerAsync(CustomerRequest customerRequest);

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteCustomerAsync(Guid customerId);

    /// <summary>
    /// Gets a customer.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerResponse"/></returns>
    Task<CustomerResponse> GetCustomerAsync(Guid customerId);

    /// <summary>
    /// Gets the client risk info.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerRiskResponse"/></returns>
    Task<CustomerRiskResponse> GetCustomerRiskAsync(Guid customerId);

    /// <summary>
    /// Searches the customers.
    /// </summary>
    /// <param name="request">The customer search criteria</param>
    /// <returns>A list of customers.</returns>
    Task<List<CustomerResponse>> SearchCustomersAsync(CustomerSearchRequest request);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateCustomerAsync(CustomerRequest customerRequest);

    #endregion Public Methods
}