// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRiskResponse.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Dto;

using System;

/// <summary>
/// Defines the customer risk response.
/// </summary>
public class CustomerRiskResponse
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customer risk indicator.
    /// </summary>
    public string RiskIndicator { get; set; } = default!;

    #endregion Public Properties
}