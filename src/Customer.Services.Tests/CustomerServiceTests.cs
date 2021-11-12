// -------------------------------------------------------------------------------------
//  <copyright file="CustomerServiceTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Tests;

using System;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Data.Access.Repositories.Interfaces;
using Customer.ExternalServices.Factories.Interfaces;
using Data.Schema;
using Implementations;
using Models;
using NSubstitute;
using SharedLibrary.Exceptions;
using Xunit;

/// <summary>
/// Performs the unit tests associated with customer services.
/// </summary>
public class CustomerServiceTests
{
    #region Private Fields

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// The external customer service factory.
    /// </summary>
    private readonly IExternalCustomerServiceFactory _externalCustomerServiceFactory;

    /// <summary>
    /// The mapper instance.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The customer request validator.
    /// </summary>
    private readonly CustomerService _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerServiceTests"/> class.
    /// </summary>
    public CustomerServiceTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _externalCustomerServiceFactory = Substitute.For<IExternalCustomerServiceFactory>();
        _mapper = Substitute.For<IMapper>();
        _sut = new CustomerService(_customerRepository, _externalCustomerServiceFactory, _mapper);
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Tests the <i>CreateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_FailMiddleNameValidation_ThrowsException()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin",
            MiddleName = "e."
        };

        //// Act
        async Task Action(CustomerRequest c) { await _sut.CreateCustomerAsync(c); }

        ////Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => Action(customerRequest));

        Assert.Equal("Unhandled exception...", exception.Message);

        _mapper.Received(0).Map<Customer>(Arg.Any<CustomerRequest>());

        _customerRepository.Received(0).AddCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(0).SaveChangesAsync();
    }

    /// <summary>
    /// Tests the <i>CreateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_FailMiddleNameValidation_ThrowsValidationException()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin",
            MiddleName = "v."
        };

        //// Act
        async Task Action(CustomerRequest c) { await _sut.CreateCustomerAsync(c); }

        ////Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => Action(customerRequest));

        Assert.Equal("Failed to validate the 'Middle Name'!", exception.Message);

        _mapper.Received(0).Map<Customer>(Arg.Any<CustomerRequest>());

        _customerRepository.Received(0).AddCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(0).SaveChangesAsync();
    }

    /// <summary>
    /// Tests the <i>CreateCustomerAsync</i> successfully.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_Successful()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin",
            MiddleName = "L."
        };

        var customer = new Customer
        {
            CustomerId = customerRequest.Id,
            FirstName = customerRequest.FirstName,
            LastName = customerRequest.LastName,
            DateOfBirth = customerRequest.DateOfBirth,
            MiddleName = customerRequest.MiddleName,
            UpdatedBy = customerRequest.UpdatedBy,
            UpdatedDate = DateTime.UtcNow
        };

        _mapper.Map<Customer>(customerRequest).Returns(customer);

        _customerRepository.SaveChangesAsync().Returns(Task.FromResult(1));

        //// Act
        await _sut.CreateCustomerAsync(customerRequest);

        ////Assert
        _mapper.Received(1).Map<Customer>(Arg.Any<CustomerRequest>());

        _customerRepository.Received(1).AddCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(1).SaveChangesAsync();
    }

    /// <summary>
    /// Tests the <i>UpdateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateCustomerAsync_FailMiddleNameValidation_ThrowsNotFoundException()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin",
            MiddleName = "n."
        };

        //// Act
        async Task Action(CustomerRequest c) { await _sut.UpdateCustomerAsync(c); }

        ////Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => Action(customerRequest));

        Assert.Equal($"The customer ({customerRequest.Id}) does not exist.", exception.Message);

        _mapper.Received(0).Map<Customer>(Arg.Any<CustomerRequest>());

        _customerRepository.Received(0).UpdateCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(0).SaveChangesAsync();
    }

    /// <summary>
    /// Tests the <i>UpdateCustomerAsync</i> successfully.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateCustomerAsync_Successful()
    {
        //// Arrange
        var customerRequest = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            UpdatedBy = "Admin",
            MiddleName = "L."
        };

        var customer = new Customer
        {
            CustomerId = customerRequest.Id,
            FirstName = customerRequest.FirstName,
            LastName = customerRequest.LastName,
            DateOfBirth = customerRequest.DateOfBirth,
            MiddleName = customerRequest.MiddleName,
            UpdatedBy = customerRequest.UpdatedBy,
            UpdatedDate = DateTime.UtcNow
        };

        _mapper.Map<Customer>(customerRequest).Returns(customer);

        _customerRepository.SaveChangesAsync().Returns(Task.FromResult(1));

        //// Act
        await _sut.UpdateCustomerAsync(customerRequest);

        ////Assert
        _mapper.Received(1).Map<Customer>(Arg.Any<CustomerRequest>());

        _customerRepository.Received(1).UpdateCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(1).SaveChangesAsync();
    }

    #endregion Public Methods
}