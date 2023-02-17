// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequest.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using Base;

/// <summary>
/// Defines the request class used for creating/updating customers.
/// </summary>
public class CustomerRequest : CustomerBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the user that created/updated the customer.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;

    #endregion Public Properties
}