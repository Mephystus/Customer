﻿// -------------------------------------------------------------------------------------
//  <copyright file="AnotherExternalCustomerService.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Implementations;

using System;
using System.Threading.Tasks;
using Dto;
using Interfaces;

/// <summary>
/// The direct implementation for the external services.
/// </summary>
public class AnotherExternalCustomerService : IExternalCustomerService
{
    /// <summary>
    /// Gets the customer risk .
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerRiskResponse"/></returns>
    public async Task<CustomerRiskResponse> GetCustomerRiskAsync(Guid customerId)
    {
        await Task.Delay(1500);

        var rnd = new Random();
        var val = rnd.Next(0, 200);

        string riskIndicator = "LOW";

        if (val % 2 == 0)
        {
            riskIndicator = "MEDIUM";
        }
        else if (val % 3 == 0)
        {
            riskIndicator = "HIGH";
        }

        return new CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = riskIndicator,
            Description = "Another description!"
        };
    }
}