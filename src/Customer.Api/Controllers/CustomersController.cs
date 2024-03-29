﻿// -------------------------------------------------------------------------------------
//  <copyright file="CustomersController.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Controllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interfaces;
using SharedLibrary.Models.Ping;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// The customers controller.
/// </summary>
[Route("api/[controller]")]
public class CustomersController : ApiControllerBase
{
    #region Private Fields

    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CustomersController> _logger;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomersController" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{CustomerController}"/></param>
    /// <param name="customerService">An instance of <see cref="ICustomerService"/></param>
    public CustomersController(
        ILogger<CustomersController> logger,
        ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>The customer.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "The customer was created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed validation")]
    public async Task<IActionResult> CreateCustomerAsync(CustomerRequest customerRequest)
    {
        _logger.LogInformation("Input: {@customerRequest}", customerRequest);

        await _customerService.CreateCustomerAsync(customerRequest);

        return CreatedAtAction(nameof(GetCustomerAsync),
                                new { id = customerRequest.Id },
                                customerRequest);
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <returns>The customer.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer was deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id)
    {
        _logger.LogInformation("Input: {@id}", id);

        await _customerService.DeleteCustomerAsync(id);

        return Ok();
    }

    /// <summary>
    /// Gets the customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <returns>The customer.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer", typeof(CustomerResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> GetCustomerAsync(Guid id)
    {
        _logger.LogInformation("Input: {@id}", id);

        var customer = await _customerService.GetCustomerAsync(id);

        if (customer.FirstName == "John")
        {
            var customer3 = await _customerService.GetCustomerAsync(Guid.NewGuid());
        }

        customer.FirstName = $"Mr. {customer.FirstName}";

        _logger.LogInformation("Output: {@customer}", customer);

        return Ok(customer);
    }

    /// <summary>
    /// Gets the customer risk.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <returns>The customer risk.</returns>
    [HttpGet("{id:guid}/risk")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer risk", typeof(CustomerRiskResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> GetCustomerRiskAsync(Guid id)
    {
        _logger.LogInformation("Input: {@id}", id);

        var riskResponse = await _customerService.GetCustomerRiskAsync(id);

        _logger.LogInformation("Output: {@riskResponse}", riskResponse);

        return Ok(riskResponse);
    }

    /// <summary>
    /// Ping the action to check/test the API.
    /// </summary>
    /// <returns>The current date.</returns>
    [HttpGet("ping")]
    [SwaggerResponse(StatusCodes.Status200OK, "The current date")]
    public IActionResult Ping()
    {
        _logger.LogInformation("Pinging... ");

        return Ok(new PingResponse
        {
            DateTime = DateTime.Now
        });
    }

    /// <summary>
    /// Searches the customers.
    /// </summary>
    /// <param name="request">The customer search criteria.</param>
    /// <returns>The customers.</returns>
    [HttpGet()]
    [SwaggerResponse(StatusCodes.Status200OK, "The customers", typeof(List<CustomerResponse>))]
    public async Task<IActionResult> SearchCustomersAsync([FromQuery] CustomerSearchRequest request)
    {
        _logger.LogInformation("Input: {@request}", request);

        var response = await _customerService.SearchCustomersAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customerRequest">The customer request.</param>
    /// <returns>The customer.</returns>
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The customer was updated successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed validation")]
    public async Task<IActionResult> UpdateCustomerAsync(CustomerRequest customerRequest)
    {
        _logger.LogInformation("Input: {@customerRequest}", customerRequest);

        await _customerService.UpdateCustomerAsync(customerRequest);

        return NoContent();
    }

    #endregion Public Methods
}