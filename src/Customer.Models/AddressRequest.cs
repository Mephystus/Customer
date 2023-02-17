// -------------------------------------------------------------------------------------
//  <copyright file="AddressRequest.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System;
using Base;

/// <summary>
/// Defines the request class used for creating/updating addresses.
/// </summary>
public class AddressRequest : AddressBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the user that created/updated the address.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;

    #endregion Public Properties
}