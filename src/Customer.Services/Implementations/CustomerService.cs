// -------------------------------------------------------------------------------------
//  <copyright file="CustomerService.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Implementations;

using System;
using System.Threading.Tasks;
using AutoMapper;
using Customer.Data.Access.Repositories.Interfaces;
using Customer.ExternalServices.Factories.Interfaces;
using Data.Schema;
using Interfaces;
using Models;
using Models.Base;
using SharedLibrary.Exceptions;

/// <summary>
/// Provides a direct implementation of the customer service.
/// </summary>
public class CustomerService : ICustomerService
{
    #region Private Fields

    /// <summary>
    /// The customer repository.
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

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerService"/> class.
    /// </summary>
    /// <param name="customerRepository">An instance of <see cref="ICustomerRepository"/>.</param>
    /// <param name="externalCustomerServiceFactory">An instance of <see cref="IExternalCustomerServiceFactory"/>.</param>
    /// <param name="mapper">An instance of <see cref="IMapper"/>.</param>
    public CustomerService(
                ICustomerRepository customerRepository,
                IExternalCustomerServiceFactory externalCustomerServiceFactory,
                IMapper mapper)
    {
        _customerRepository = customerRepository;
        _externalCustomerServiceFactory = externalCustomerServiceFactory;
        _mapper = mapper;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CreateCustomerAsync(CustomerRequest customerRequest)
    {
        CheckMiddleName(customerRequest);

        var customer = _mapper.Map<Customer>(customerRequest);

        _customerRepository.AddCustomer(customer);

        await _customerRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteCustomerAsync(Guid customerId)
    {
        var customer = await _customerRepository.GetCustomerAsync(customerId);

        if (customer == null)
        {
            throw new NotFoundException($"The customer ({customerId}) does not exist.");
        }

        _customerRepository.DeleteCustomer(customer);

        await _customerRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a customer.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerResponse"/></returns>
    public async Task<CustomerResponse> GetCustomerAsync(Guid customerId)
    {
        var customer = await _customerRepository.GetCustomerAsync(customerId);

        if (customer == null)
        {
            throw new NotFoundException($"The customer ({customerId}) does not exist.");
        }

        var customerResponse = _mapper.Map<CustomerResponse>(customer);

        return customerResponse;
    }

    /// <summary>
    /// Gets the client risk info.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerRiskResponse"/></returns>
    public async Task<CustomerRiskResponse> GetCustomerRiskAsync(Guid customerId)
    {
        var identifier = GetIdentifier(customerId);

        var externalCustomerService = _externalCustomerServiceFactory.GetExternalCustomerService(identifier);

        var externalRiskResponse = await externalCustomerService.GetCustomerRiskAsync(customerId);

        var riskResponse = _mapper.Map<CustomerRiskResponse>(externalRiskResponse);

        return riskResponse;
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpdateCustomerAsync(CustomerRequest customerRequest)
    {
        CheckMiddleName(customerRequest, false);

        var customer = _mapper.Map<Customer>(customerRequest);

        _customerRepository.UpdateCustomer(customer);

        await _customerRepository.SaveChangesAsync();
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Dummy/Stub method to test/check the middle name.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <param name="isNew">Indicates whether it's a new customer.</param>
    private static void CheckMiddleName(CustomerBase customerRequest, bool isNew = true)
    {
        if (customerRequest.MiddleName.StartsWith("e", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new Exception("Unhandled exception...");
        }

        if (customerRequest.MiddleName.StartsWith("v", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ValidationException("Failed to validate the 'Middle Name'!", nameof(customerRequest.MiddleName));
        }

        if (!isNew && customerRequest.MiddleName.StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
        {
            throw new NotFoundException($"The customer ({customerRequest.Id}) does not exist.");
        }
    }

    /// <summary>
    /// Dummy/Stub method to return the identifier based on the customer Id.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The identifier.</returns>
    private static string GetIdentifier(Guid customerId)
    {
        if (customerId.ToString().EndsWith("1"))
        {
            return "1";
        }

        if (customerId.ToString().EndsWith("2"))
        {
            return "2";
        }

        return "3";
    }

    #endregion Private Methods
}