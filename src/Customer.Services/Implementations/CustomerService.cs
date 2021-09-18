// -------------------------------------------------------------------------------------
//  <copyright file="CustomerService.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Services.Implementations;

using System;
using System.Threading.Tasks;
using AutoMapper;
using Models;
using Interfaces;
using SharedLibrary.Exceptions;
using Customer.Data.Access.Repositories.Interfaces;
using Customer.Data.Schema;
using Customer.ExternalServices.Factories.Interfaces;

/// <summary>
/// Provides a direct implementation of the customer service.
/// </summary>
public class CustomerService : ICustomerService
{
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

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<Guid?> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        // TODO: implementation
        var rnd = new Random();
        var val = rnd.Next(0, 200);

        if (val % 2 == 0)
        {
            throw new Exception("Failure creating the customer!");
        }

        var customer = _mapper.Map<Customer>(customerRequest);

        _customerRepository.AddCustomer(customer);

        await _customerRepository.SaveChangesAsync();

        return customer.CustomerId;
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
            return;
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
        var rnd = new Random();
        var val = rnd.Next(0, 200);

        int identifier = val % 2 == 0 ? 1 : (val % 3 == 0 ? 2 : 3);

        var externalCustomerService = _externalCustomerServiceFactory.GetExternalCustomerService(identifier.ToString());

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
        var customer = _mapper.Map<Customer>(customerRequest);

        _customerRepository.UpdateCustomer(customer);

        await _customerRepository.SaveChangesAsync();
    }
}
