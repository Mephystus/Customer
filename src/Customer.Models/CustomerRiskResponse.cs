// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRiskResponse.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System;
using Enums;

/// <summary>
/// Defines the customer risk response.
/// </summary>
public class CustomerRiskResponse
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string Comments { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer risk indicator.
    /// </summary>
    public CustomerRiskIndicator RiskIndicator { get; set; }

    #endregion Public Properties
}