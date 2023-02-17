// -------------------------------------------------------------------------------------
//  <copyright file="SpecialExternalCustomerService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Implementations;

using Dto;
using Interfaces;
using System;
using System.Threading.Tasks;

/// <summary>
/// The direct implementation for the external services.
/// </summary>
public class SpecialExternalCustomerService : ExternalCustomerServiceBase, IExternalCustomerService
{
    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="SpecialExternalCustomerService"/> class.
    /// </summary>
    /// <param name="serviceProvider">An instance of <see cref="IServiceProvider"/></param>
    public SpecialExternalCustomerService(IServiceProvider serviceProvider)
    {
    }

    #endregion Public Constructors

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