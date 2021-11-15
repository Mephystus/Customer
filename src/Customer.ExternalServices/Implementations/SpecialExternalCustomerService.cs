// -------------------------------------------------------------------------------------
//  <copyright file="SpecialExternalCustomerService.cs" company="The AA (Ireland)">
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
public class SpecialExternalCustomerService : ExternalCustomerServiceBase, IExternalCustomerService
{
    #region Public Methods

    /// <summary>
    /// Gets the customer risk .
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <returns>The <see cref="CustomerRiskResponse"/></returns>
    public async Task<CustomerRiskResponse> GetCustomerRiskAsync(Guid customerId)
    {
        await Task.Delay(10);

        var riskIndicator = GetRiskIndicator(customerId);

        return new CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = riskIndicator,
            Description = "Special description!"
        };
    }

    #endregion Public Methods
}