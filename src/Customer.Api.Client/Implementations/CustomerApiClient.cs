// -------------------------------------------------------------------------------------
//  <copyright file="CustomerApiClient.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Implementations;

using System;
using System.Threading.Tasks;
using Interfaces;
using Models;
using SharedLibrary.Api.Client;
using SharedLibrary.Models.Ping;

/// <summary>
/// Direct implementation of the <see cref="ICustomerApiClient"/>.
/// </summary>
public class CustomerApiClient : ApiClientBase, ICustomerApiClient
{
    #region Private Fields

    /// <summary>
    /// The base endpoint for the customer API.
    /// </summary>
    private const string BaseEndpoint = "api/customers";

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    public CustomerApiClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Creates a customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreateCustomerAsync(CustomerRequest customerRequest, CancellationToken cancellationToken = default)
    {
        await PostAsync(BaseEndpoint, customerRequest, cancellationToken);
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }

    /// <summary>
    /// Gets a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<CustomerResponse> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<CustomerResponse>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    /// <summary>
    /// Pings the customer API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<PingResponse>($"{BaseEndpoint}/ping", cancellationToken);
    }

    /// <summary>
    /// Searches for customers.
    /// </summary>
    /// <param name="request">The search criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<List<CustomerResponse>> SearchCustomersAsync(CustomerSearchRequest request, CancellationToken cancellationToken = default)
    {
        return await GetAsync<List<CustomerResponse>>($"{BaseEndpoint}/?FistName={request.FistName}&LastName={request.LastName}&MyProperty={request.MyProperty}", cancellationToken);
    }

    /// <summary>
    /// Updates a customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpdateCustomerAsync(CustomerRequest customerRequest, CancellationToken cancellationToken = default)
    {
        await PutAsync(BaseEndpoint, customerRequest, cancellationToken);
    }

    #endregion Public Methods
}