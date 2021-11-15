// -------------------------------------------------------------------------------------
//  <copyright file="ICustomerApiClient.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Interfaces;

using Models;
using SharedLibrary.Models.Ping;

/// <summary>
/// Provides the contracts for customer API.
/// </summary>
public interface ICustomerApiClient
{
    #region Public Methods

    /// <summary>
    /// Creates a customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateCustomerAsync(CustomerRequest customerRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<CustomerResponse> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pings the customer API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<PingResponse> PingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateCustomerAsync(CustomerRequest customerRequest, CancellationToken cancellationToken = default);

    #endregion Public Methods
}