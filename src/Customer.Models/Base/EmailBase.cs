// -------------------------------------------------------------------------------------
//  <copyright file="EmailBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Base;

using System;
using Enums;

/// <summary>
/// Defines the common properties for the email base class.
/// </summary>
public abstract class EmailBase
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the email type.
    /// </summary>
    public EmailType Type { get; set; }
}