// -------------------------------------------------------------------------------------
//  <copyright file="CustomerApiClientTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Tests.Implementations;

using System;
using System.Threading.Tasks;
using Controllers;
using Customer.Api.Client.Implementations;
using Customer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Models;
using NSubstitute;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="CustomerApiClient"/> class.
/// </summary>
public class CustomerApiClientTests
{
    #region Private Fields

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;

    /// <summary>
    /// The customer API client (SUT).
    /// </summary>
    private readonly CustomerApiClient _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialise a new instance of the <see cref="CustomerApiClientTests"/> class.
    /// </summary>
    public CustomerApiClientTests()
    {
        _customerService = Substitute.For<ICustomerService>();

        var factory = new WebApplicationFactory<CustomersController>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _customerService);
                });
            });

        var httpClient = factory.CreateClient();

        _sut = new CustomerApiClient(httpClient);
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the <i>CreateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_Successfully()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin"
        };

        _customerService.CreateCustomerAsync(customerRequest).Returns(Task.CompletedTask);

        //// Act
        await _sut.CreateCustomerAsync(customerRequest);

        //// Assert
        await _customerService.Received(1).CreateCustomerAsync(Arg.Any<CustomerRequest>());
    }

    /// <summary>
    /// Test the <i>DeleteCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_Successfully()
    {
        //// Arrange
        var id = Guid.NewGuid();

        _customerService.DeleteCustomerAsync(id).Returns(Task.CompletedTask);

        //// Act
        await _sut.DeleteCustomerAsync(id);

        //// Assert
        await _customerService.Received(1).DeleteCustomerAsync(id);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_ReturnsCustomer()
    {
        //// Arrange
        var id = Guid.NewGuid();

        var expectedResponse = new CustomerResponse
        {
            Id = id,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10)
        };

        _customerService.GetCustomerAsync(id).Returns(Task.FromResult(expectedResponse));

        //// Act
        var actualResponse = await _sut.GetCustomerAsync(id);

        //// Assert
        await _customerService.Received(1).GetCustomerAsync(id);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>PingAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task PingAsync_Successfully()
    {
        //// Arrange

        //// Act
        var response = await _sut.PingAsync();

        //// Assert
        Assert.NotNull(response);

        Assert.True(response.DateTime != DateTime.MinValue);
    }

    /// <summary>
    /// Test the <i>UpdateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateCustomerAsync_Successfully()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin"
        };

        _customerService.UpdateCustomerAsync(customerRequest).Returns(Task.CompletedTask);

        //// Act
        await _sut.UpdateCustomerAsync(customerRequest);

        //// Assert
        await _customerService.Received(1).UpdateCustomerAsync(Arg.Any<CustomerRequest>());
    }

    #endregion Public Methods
}