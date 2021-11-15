// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRepository.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.Repositories.Implementations;

using System;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Schema;

/// <summary>
/// Direct implementation of the <see cref="ICustomerRepository"/>.
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    #region Private Fields

    /// <summary>
    /// The customer DB context.
    /// </summary>
    private readonly CustomerContext _context;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerRepository"/> class.
    /// </summary>
    /// <param name="context">The customer DB context.</param>
    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Adds the customer into the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    /// <summary>
    /// Checks the database connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if can connect. False otherwise.</returns>
    public async Task<bool> CheckDatabaseConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Database.CanConnectAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes the customer from the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    public void DeleteCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
    }

    /// <summary>
    /// Gets the customer by Id.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The customer.</returns>
    public async Task<Customer> GetCustomerAsync(Guid customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
    }

    /// <summary>
    /// Saves all context changes made into the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the customer into the DB context.
    /// </summary>
    /// <param name="customer">The customer</param>
    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    #endregion Public Methods
}