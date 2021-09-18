// -------------------------------------------------------------------------------------
//  <copyright file="AddressBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Base;

using System;
using Enums;

/// <summary>
/// Defines the common properties for the address base class.
/// </summary>
public abstract class AddressBase
{
    /// <summary>
    /// Gets or sets the address line 1.
    /// </summary>
    public string AddressLine1 { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address line 2.
    /// </summary>
    public string AddressLine2 { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address line 3.
    /// </summary>
    public string AddressLine3 { get; set; } = default!;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    public string Country { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the post code.
    /// </summary>
    public string PostCode { get; set; } = default!;

    /// <summary>
    /// Gets or sets the the state.
    /// </summary>
    public string State { get; set; } = default!;

    /// <summary>
    /// Gets or sets the address type.
    /// </summary>
    public AddressType Type { get; set; }
}