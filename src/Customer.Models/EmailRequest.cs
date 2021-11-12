// -------------------------------------------------------------------------------------
//  <copyright file="EmailRequest.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System;
using Base;

/// <summary>
/// Defines the request class used for creating/updating emails.
/// </summary>
public class EmailRequest : EmailBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the user that created/updated the email.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;

    #endregion Public Properties
}