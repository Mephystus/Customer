// -------------------------------------------------------------------------------------
//  <copyright file="CustomerServiceTests.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Tests.Implementations;

using System;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Data.Access.Repositories.Interfaces;
using Customer.ExternalServices.Factories.Interfaces;
using Customer.ExternalServices.Interfaces;
using Customer.Services.Implementations;
using Data.Schema;
using FluentAssertions;
using Models;
using Models.Enums;
using NSubstitute;
using SharedLibrary.Exceptions;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="CustomerService"/> class.
/// </summary>
public class CustomerServiceTests
{
    #region Private Fields

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    private readonly IExternalCustomerService _externalCustomerService;

    /// <summary>
    /// The external customer service factory.
    /// </summary>
    private readonly IExternalCustomerServiceFactory _externalCustomerServiceFactory;

    /// <summary>
    /// The mapper instance.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The customer request validator (SUT).
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

        _externalCustomerService = Substitute.For<IExternalCustomerService>();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the <i>CreateCustomerAsync</i>.
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
    /// Test the <i>CreateCustomerAsync</i>.
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
    /// Test the <i>CreateCustomerAsync</i> successfully.
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
    /// Test the <i>DeleteCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_Successfully()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        var customer = new Customer
        {
            CustomerId = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        _customerRepository.GetCustomerAsync(customerId)!.Returns(Task.FromResult(customer));

        _customerRepository.SaveChangesAsync().Returns(Task.FromResult(1));

        //// Act
        await _sut.DeleteCustomerAsync(customerId);

        ////Assert
        await _customerRepository.Received(1).GetCustomerAsync(customerId);

        _customerRepository.Received(1).DeleteCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(1).SaveChangesAsync();
    }

    /// <summary>
    /// Test the <i>DeleteCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_ThrowsNotFoundException()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        _customerRepository.GetCustomerAsync(customerId)!.Returns(Task.FromResult((Customer)null!));

        _customerRepository.SaveChangesAsync().Returns(Task.FromResult(1));

        //// Act
        async Task Action(Guid id) { await _sut.DeleteCustomerAsync(id); }

        ////Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => Action(customerId));

        Assert.Equal($"The customer ({customerId}) does not exist.", exception.Message);

        await _customerRepository.Received(1).GetCustomerAsync(customerId);

        _customerRepository.Received(0).DeleteCustomer(Arg.Any<Customer>());

        await _customerRepository.Received(0).SaveChangesAsync();
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_Successfully()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        var customer = new Customer
        {
            CustomerId = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-10),
            MiddleName = "L.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.UtcNow
        };

        _customerRepository.GetCustomerAsync(customerId)!.Returns(Task.FromResult(customer));

        var expectedResponse = new CustomerResponse
        {
            Id = customerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.DateOfBirth,
            MiddleName = customer.MiddleName
        };

        _mapper.Map<CustomerResponse>(customer).Returns(expectedResponse);

        //// Act
        var actualResponse = await _sut.GetCustomerAsync(customerId);

        ////Assert
        await _customerRepository.Received(1).GetCustomerAsync(customerId);

        _mapper.Received(1).Map<CustomerResponse>(Arg.Any<Customer>());

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_ThrowsNotFoundException()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        _customerRepository.GetCustomerAsync(customerId)!.Returns(Task.FromResult((Customer)null!));

        //// Act
        async Task Action(Guid id) { await _sut.DeleteCustomerAsync(id); }

        ////Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => Action(customerId));

        Assert.Equal($"The customer ({customerId}) does not exist.", exception.Message);

        await _customerRepository.Received(1).GetCustomerAsync(customerId);
    }

    /// <summary>
    /// Test the <i>GetCustomerRiskAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerRiskAsync_Successfully()
    {
        //// Arrange
        var customerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa2");

        _externalCustomerServiceFactory.GetExternalCustomerService("2").Returns(_externalCustomerService);

        var externalRiskResponse = new ExternalServices.Dto.CustomerRiskResponse
        {
            CustomerId = customerId,
            Description = "Comments 123...",
            RiskIndicator = "Medium"
        };

        _externalCustomerService.GetCustomerRiskAsync(customerId).Returns(externalRiskResponse);

        var expectedResponse = new CustomerRiskResponse
        {
            CustomerId = customerId,
            Comments = externalRiskResponse.Description,
            RiskIndicator = CustomerRiskIndicator.Medium
        };

        _mapper.Map<CustomerRiskResponse>(externalRiskResponse).Returns(expectedResponse);

        //// Act
        var actualResponse = await _sut.GetCustomerRiskAsync(customerId);

        ////Assert
        _externalCustomerServiceFactory.Received(1).GetExternalCustomerService("2");

        await _externalCustomerService.Received(1).GetCustomerRiskAsync(customerId);

        _mapper.Received(1).Map<CustomerRiskResponse>(Arg.Any<ExternalServices.Dto.CustomerRiskResponse>());

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>UpdateCustomerAsync</i>.
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
    /// Test the <i>UpdateCustomerAsync</i> successfully.
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