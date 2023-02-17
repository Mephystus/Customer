// -------------------------------------------------------------------------------------
//  <copyright file="PhoneBase.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Base;

using System;
using Enums;

/// <summary>
/// Defines the common properties for the phone base class.
/// </summary>
public abstract class PhoneBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the phone Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone type.
    /// </summary>
    public PhoneType Type { get; set; }

    #endregion Public Properties
}