// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRepositoryTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.Tests.Repositories.Implementations;

using System;
using System.Linq;
using System.Threading.Tasks;
using Customer.Data.Access.Repositories.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="CustomerRepository"/> class.
/// </summary>
public class CustomerRepositoryTests
{
    #region Public Methods

    /// <summary>
    /// Test the <i>AddCustomer</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddCustomer_Successfully()
    {
        //// Arrange
        var options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new CustomerContext(options);

        context.Customers.Add(new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Kane",
            DateOfBirth = DateTime.Today.AddYears(-42),
            MiddleName = "J.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var sut = new CustomerRepository(context);

        var beforeCustomers = await context.Customers.ToListAsync();

        var customer = new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        //// Act
        sut.AddCustomer(customer);

        await sut.SaveChangesAsync();

        //// Assert
        var afterCustomers = await context.Customers.ToListAsync();

        Assert.True(beforeCustomers.Count == 1);
        Assert.DoesNotContain(beforeCustomers, x => x.CustomerId == customer.CustomerId);

        Assert.True(afterCustomers.Count == 2);
        Assert.Contains(afterCustomers, x => x.CustomerId == customer.CustomerId);

        var createdCustomer = afterCustomers.First(x => x.CustomerId == customer.CustomerId);

        createdCustomer.Should().BeEquivalentTo(customer);
    }

    /// <summary>
    /// Test the <i>DeleteCustomer</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomer_InvalidCustomer_ThrowsDbUpdateConcurrencyException()
    {
        //// Arrange
        var options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Guid customerId = Guid.NewGuid();

        await using var context = new CustomerContext(options);

        var customer = new Schema.Customer
        {
            CustomerId = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        context.Customers.Add(new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Kane",
            DateOfBirth = DateTime.Today.AddYears(-42),
            MiddleName = "J.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var sut = new CustomerRepository(context);

        //// Act
        sut.DeleteCustomer(customer);

        async Task Action() { await sut.SaveChangesAsync(); }

        //// Assert
        var exception = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(Action);

        Assert.Equal("Attempted to update or delete an entity that does not exist in the store.", exception.Message);
    }

    /// <summary>
    /// Test the <i>DeleteCustomer</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomer_Successfully()
    {
        //// Arrange
        var options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Guid customerId = Guid.NewGuid();

        await using var context = new CustomerContext(options);

        var customer = new Schema.Customer
        {
            CustomerId = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        context.Customers.Add(customer);

        context.Customers.Add(new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Kane",
            DateOfBirth = DateTime.Today.AddYears(-42),
            MiddleName = "J.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var beforeCustomers = await context.Customers.ToListAsync();

        var sut = new CustomerRepository(context);

        //// Act
        sut.DeleteCustomer(customer);

        await sut.SaveChangesAsync();

        //// Assert
        var afterCustomers = await context.Customers.ToListAsync();

        Assert.True(beforeCustomers.Count == 2);
        Assert.Contains(beforeCustomers, x => x.CustomerId == customerId);

        Assert.True(afterCustomers.Count == 1);
        Assert.DoesNotContain(afterCustomers, x => x.CustomerId == customerId);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_InvalidCustomer_ReturnsNull()
    {
        //// Arrange
        var options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Guid customerId = Guid.NewGuid();

        await using var context = new CustomerContext(options);

        context.Customers.Add(new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Kane",
            DateOfBirth = DateTime.Today.AddYears(-42),
            MiddleName = "J.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var sut = new CustomerRepository(context);

        //// Act
        var actualResponse = await sut.GetCustomerAsync(customerId);

        //// Assert
        Assert.Null(actualResponse);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_ReturnsCustomer()
    {
        //// Arrange
        var options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Guid customerId = Guid.NewGuid();

        await using var context = new CustomerContext(options);

        var expectedResponse = new Schema.Customer
        {
            CustomerId = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        context.Customers.Add(expectedResponse);

        context.Customers.Add(new Schema.Customer
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Kane",
            DateOfBirth = DateTime.Today.AddYears(-42),
            MiddleName = "J.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var sut = new CustomerRepository(context);

        //// Act
        var actualResponse = await sut.GetCustomerAsync(customerId);

        //// Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods
}