// -------------------------------------------------------------------------------------
//  <copyright file="DemoExternalCustomerService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Demo;

using DemoServiceReference;
using Dto;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The direct implementation for the external services.
/// </summary>
public class DemoExternalCustomerService : IExternalCustomerService
{
    #region Private Fields

    /// <summary>
    /// The SOAP Demo instance.
    /// </summary>
    private readonly SOAPDemoSoap _soapDemoSoap;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="DemoExternalCustomerService"/> class.
    /// </summary>
    /// <param name="serviceProvider">An instance of <see cref="IServiceProvider"/></param>
    public DemoExternalCustomerService(IServiceProvider serviceProvider)
    {
        _soapDemoSoap = serviceProvider.GetService<SOAPDemoSoap>() ?? throw new ArgumentNullException(nameof(SOAPDemoSoap));
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
        var customerIdStr = customerId.ToString();
        var personId = customerIdStr.Substring(customerIdStr.Length - 1);

        var person = await _soapDemoSoap.FindPersonAsync(personId);

        var riskIndicator = GetRiskIndicator(person.Age);

        return new CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = riskIndicator,
            Description = "DEMO description!"
        };
    }

    #endregion Public Methods

    #region Protected Methods

    /// <summary>
    /// Dummy/Stub method to return the risk indicator based on the customer Id.
    /// </summary>
    /// <param name="customerAge"></param>
    /// <returns>The risk indicator.</returns>
    protected static string GetRiskIndicator(long customerAge)
    {
        var riskIndicator = "LOW";

        if (customerAge % 5 == 0)
        {
            riskIndicator = "MEDIUM";
        }
        else if (customerAge % 2 == 0)
        {
            riskIndicator = "HIGH";
        }

        return riskIndicator;
    }

    #endregion Protected Methods
}