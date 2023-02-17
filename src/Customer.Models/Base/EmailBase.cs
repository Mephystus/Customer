// -------------------------------------------------------------------------------------
//  <copyright file="EmailBase.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
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
    #region Public Properties

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

    #endregion Public Properties
}