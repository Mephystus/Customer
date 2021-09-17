// -------------------------------------------------------------------------------------
//  <copyright file="Customer.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access;

using Customer.Data.Schema;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Defines the DB context access layer.
/// </summary>
public class CustomerContext : DbContext
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DB context.</param>
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the customers.
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>
    /// Use to configure the model in the DB context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(o => o.CustomerId);
    }
}