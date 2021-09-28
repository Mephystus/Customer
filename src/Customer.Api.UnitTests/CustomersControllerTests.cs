// -------------------------------------------------------------------------------------
//  <copyright file="CustomersControllerTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.UnitTests;

using System;
using System.Threading.Tasks;
using Customer.Api.Controllers;
using Customer.Models;
using Customer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

/// <summary>
/// Defines the unit tests associated with the Customer API.
/// </summary>
public class CustomersControllerTests
{

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;

    /// <summary>
    /// the System Under Test (Customer Controller)
    /// </summary>
    private readonly CustomersController _sut;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CustomersController> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomersControllerTests"/> class.
    /// </summary>
    public CustomersControllerTests()
    {
        _customerService = Substitute.For<ICustomerService>();
        _logger = Substitute.For<ILogger<CustomersController>>();

        _sut = new CustomersController(_logger, _customerService);
    }

    /// <summary>
    /// Test the create customer.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_ReturnsCreatedResult()
    {
        //// Arrange
        CustomerRequest customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin"
        };

        Guid? id = Guid.NewGuid();

        _customerService.CreateCustomerAsync(customerRequest).Returns(Task.FromResult(id));

        _customerService.GetCustomerAsync((Guid)id).Returns(Task.FromResult(new CustomerResponse { }));

        //// Act
        var response = await _sut.CreateCustomerAsync(customerRequest);

        ////Assert
        await _customerService.Received(1).CreateCustomerAsync(Arg.Any<CustomerRequest>());

        await _customerService.Received(1).GetCustomerAsync((Guid)id);

        Assert.IsType<CreatedAtActionResult>(response);
    }

    /// <summary>
    /// Test the create customer.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_ReturnsBadRequestResult()
    {
        //// Arrange
        CustomerRequest customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin"
        };        

        _customerService.CreateCustomerAsync(customerRequest).Returns(Task.FromResult((Guid?)null));

        //// Act
        var response = await _sut.CreateCustomerAsync(customerRequest);

        ////Assert
        await _customerService.Received(1).CreateCustomerAsync(Arg.Any<CustomerRequest>());

        await _customerService.Received(0).GetCustomerAsync(Arg.Any<Guid>());

        Assert.IsType<BadRequestResult>(response);
    }

    /// <summary>
    /// Test the get customer.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_ReturnsOkResult()
    {
        //// Arrange
        CustomerResponse expectedResponse = new CustomerResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10)
        };

        var id = Guid.NewGuid();

        _customerService.GetCustomerAsync(id).Returns(Task.FromResult(expectedResponse));

        //// Act
        var response = await _sut.GetCustomerAsync(id);

        ////Assert
        await _customerService.Received(1).GetCustomerAsync(id);

        Assert.IsType<OkObjectResult>(response);

        var actualResponse = ((OkObjectResult)response).Value as CustomerResponse;

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }
}