// -------------------------------------------------------------------------------------
//  <copyright file="Customer.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access;

using System;
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

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                DateOfBirth = new DateTime(2000, 1, 1),
                FirstName = "Jane",
                MiddleName = "K.",
                LastName = "Silver"
            },
            new Customer
            {
                CustomerId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                DateOfBirth = new DateTime(1990, 5, 21),
                FirstName = "John",
                MiddleName = "W.",
                LastName = "Dalton"
            },
            new Customer
            {
                CustomerId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                DateOfBirth = new DateTime(2005, 11, 9),
                FirstName = "Flip",
                MiddleName = "G.",
                LastName = "Korg"
            });
    }
}