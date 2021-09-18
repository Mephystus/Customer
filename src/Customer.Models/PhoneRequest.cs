// -------------------------------------------------------------------------------------
//  <copyright file="PhoneRequest.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System;
using Base;

/// <summary>
/// Defines the request class used for creating/updating phones.
/// </summary>
public class PhoneRequest : PhoneBase
{
    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the user that created/updated the phones.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;
}