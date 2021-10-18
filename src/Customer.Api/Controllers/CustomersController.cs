// -------------------------------------------------------------------------------------
//  <copyright file="CustomersController.cs" company="The AA (Ireland)">
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
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The customers controller.
/// </summary>
[Route("api/[controller]")]
public class CustomersController : ApiControllerBase
{
    /// <summary>
    /// The customer service.
    /// </summary>
    private readonly ICustomerService _customerService;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CustomersController> _logger;

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

        if (id == null)
        {
            return BadRequest();
        }

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
    /// Gets the risk for multiple customers.
    /// </summary>
    /// <param name="ids">The customers Id.</param>
    /// <returns>The customers risk.</returns>
    [HttpGet("risks")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer", typeof(List<CustomerRiskResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
    public async Task<IActionResult> GetCustomersRiskAsync([FromQuery]Guid[] ids)
    {
        _logger.LogInformation("Input: {ids}", ids);

        var response = new List<CustomerRiskResponse>();

        foreach (var id in ids)
        {
            var customerRisk = await _customerService.GetCustomerRiskAsync(id);
            response.Add(customerRisk);
        }
         
        _logger.LogInformation("Output: {@riskResponse}", response);

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

}

