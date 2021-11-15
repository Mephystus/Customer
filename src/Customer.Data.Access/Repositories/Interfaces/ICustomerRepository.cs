// -------------------------------------------------------------------------------------
//  <copyright file="ICustomerRepository.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.Repositories.Interfaces;

using System;
using System.Threading;
using System.Threading.Tasks;
using Schema;

/// <summary>
/// Defines the customer repository contracts.
/// </summary>
public interface ICustomerRepository
{
    #region Public Methods

    /// <summary>
    /// Adds the customer into the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    void AddCustomer(Customer customer);

    /// <summary>
    /// Checks the database connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if can connect. False otherwise.</returns>
    Task<bool> CheckDatabaseConnectionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the customer from the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    void DeleteCustomer(Customer customer);

    /// <summary>
    /// Gets the customer by Id.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The customer.</returns>
    Task<Customer> GetCustomerAsync(Guid customerId);

    /// <summary>
    /// Saves all context changes made into the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the customer into the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    void UpdateCustomer(Customer customer);

    #endregion Public Methods
}