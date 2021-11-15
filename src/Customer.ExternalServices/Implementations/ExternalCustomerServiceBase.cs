// -------------------------------------------------------------------------------------
//  <copyright file="ExternalCustomerServiceBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Implementations;

using System;

/// <summary>
/// Base class for the external customer services.
/// </summary>
public abstract class ExternalCustomerServiceBase
{
    #region Protected Methods

    /// <summary>
    /// Dummy/Stub method to return the risk indicator based on the customer Id.
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>The risk indicator.</returns>
    protected static string GetRiskIndicator(Guid customerId)
    {
        var riskIndicator = "LOW";

        if (customerId.ToString().EndsWith("2"))
        {
            riskIndicator = "MEDIUM";
        }
        else if (customerId.ToString().EndsWith("3"))
        {
            riskIndicator = "HIGH";
        }

        return riskIndicator;
    }

    #endregion Protected Methods
}