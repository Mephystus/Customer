// -------------------------------------------------------------------------------------
//  <copyright file="Customer.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access;

using Microsoft.EntityFrameworkCore;
using Schema;

/// <summary>
/// Defines the DB context access layer.
/// </summary>
public class CustomerContext : DbContext
{
    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DB context.</param>
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets the customers.
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    #endregion Public Properties

    #region Protected Methods

    /// <summary>
    /// Use to configure the model in the DB context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(o => o.CustomerId);
    }

    #endregion Protected Methods
}