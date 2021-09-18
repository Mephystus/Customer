// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequest.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using Base;

/// <summary>
/// Defines the request class used for creating/updating customers.
/// </summary>
public class CustomerRequest : CustomerBase
{
    /// <summary>
    /// Gets or sets the user that created/updated the customer.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;
}