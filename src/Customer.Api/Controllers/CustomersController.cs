// -------------------------------------------------------------------------------------
//  <copyright file="CustomerController.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Controllers;

using System;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// The customer controller.
/// </summary>
[Route("api/[controller]")]
public class CustomerController : ApiControllerBase
{
    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CustomerController> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerController" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{CustomerController}"/></param>
    /// <param name="customerService">An instance of <see cref="ICustomerService"/></param>
    public CustomerController(
        ILogger<CustomerController> logger,
        ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

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

        var id = await _customerService.CreateCustomerAsync(customerRequest);

        if(id == null)
        {
            return BadRequest();
        }

        var customer = await _customerService.GetCustomerAsync((Guid)id);

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
    [SwaggerResponse(StatusCodes.Status200OK, "The customer deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id)
    {
        _logger.LogInformation("Input: {id}", id);

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
        _logger.LogInformation("Input: {id}", id);

        var customer = await _customerService.GetCustomerAsync(id);

        _logger.LogInformation("Output: {@customer}", customer);

        return Ok(customer);
    }

    /// <summary>
    /// Gets the customer risk.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <returns>The customer risk.</returns>
    [HttpGet("{id:guid}/risk")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer", typeof(CustomerRiskResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> GetCustomerRiskAsync(Guid id)
    {
        _logger.LogInformation("Input: {id}", id);

        var riskResponse = await _customerService.GetCustomerRiskAsync(id);

        _logger.LogInformation("Output: {@riskResponse}", riskResponse);

        return Ok(riskResponse);
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

}

