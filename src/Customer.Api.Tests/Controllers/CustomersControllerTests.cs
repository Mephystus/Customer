// -------------------------------------------------------------------------------------
//  <copyright file="CustomersControllerTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Tests.Controllers;

using System;
using System.Threading.Tasks;
using Customer.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Enums;
using NSubstitute;
using Services.Interfaces;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="CustomersController"/> class.
/// </summary>
public class CustomersControllerTests
{
    #region Private Fields

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;
    
    /// <summary>
    /// the System Under Test (Customer Controller)
    /// </summary>
    private readonly CustomersController _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomersControllerTests"/> class.
    /// </summary>
    public CustomersControllerTests()
    {
        _customerService = Substitute.For<ICustomerService>();
        ILogger<CustomersController> logger = Substitute.For<ILogger<CustomersController>>();

        _sut = new CustomersController(logger, _customerService);
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the <i>CreateCustomerAsync</i> returning a <see cref="CreatedAtActionResult"/> (201).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_ReturnsCreatedAtActionResult()
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
        var response = await _sut.CreateCustomerAsync(customerRequest);

        ////Assert
        await _customerService.Received(1).CreateCustomerAsync(Arg.Any<CustomerRequest>());

        Assert.IsType<CreatedAtActionResult>(response);
    }

    /// <summary>
    /// Test the <i>DeleteCustomerAsync</i> returning a <see cref="OkResult"/> (200).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_ReturnsOkObjectResult()
    {
        //// Arrange
        var id = Guid.NewGuid();

        _customerService.DeleteCustomerAsync(id).Returns(Task.CompletedTask);

        //// Act
        var response = await _sut.DeleteCustomerAsync(id);

        ////Assert
        await _customerService.Received(1).DeleteCustomerAsync(id);

        Assert.IsType<OkResult>(response);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i> returning a <see cref="OkObjectResult"/> (200).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_ReturnsOkObjectResult()
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
        var response = await _sut.GetCustomerAsync(id);

        ////Assert
        await _customerService.Received(1).GetCustomerAsync(id);

        Assert.IsType<OkObjectResult>(response);

        var actualResponse = ((OkObjectResult)response).Value as CustomerResponse;

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>GetCustomerRiskAsync</i> returning a <see cref="OkObjectResult"/> (200).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerRiskAsync_ReturnsOkObjectResult()
    {
        //// Arrange
        var expectedResponse = new CustomerRiskResponse
        {
            CustomerId = Guid.NewGuid(),
            Comments = "Comments 1,2,3...",
            RiskIndicator = CustomerRiskIndicator.Low
        };

        var id = Guid.NewGuid();

        _customerService.GetCustomerRiskAsync(id).Returns(Task.FromResult(expectedResponse));

        //// Act
        var response = await _sut.GetCustomerRiskAsync(id);

        ////Assert
        await _customerService.Received(1).GetCustomerRiskAsync(id);

        Assert.IsType<OkObjectResult>(response);

        var actualResponse = ((OkObjectResult)response).Value as CustomerRiskResponse;

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>Ping</i> returning a <see cref="OkObjectResult"/> (200).
    /// </summary>
    [Fact]
    public void Ping_ReturnsOkObjectResult()
    {
        //// Arrange

        //// Act
        var response = _sut.Ping();

        ////Assert
        Assert.IsType<OkObjectResult>(response);

        var actualResponse = ((OkObjectResult)response).Value as PingResponse;

        Assert.NotNull(actualResponse);
    }

    /// <summary>
    /// Test the <i>UpdateCustomerAsync</i> returning a <see cref="NoContentResult"/> (201).
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateCustomerAsync_ReturnsCreatedAtActionResult()
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
        var response = await _sut.UpdateCustomerAsync(customerRequest);

        ////Assert
        await _customerService.Received(1).UpdateCustomerAsync(Arg.Any<CustomerRequest>());

        Assert.IsType<NoContentResult>(response);
    }

    #endregion Public Methods
}