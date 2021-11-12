// -------------------------------------------------------------------------------------
//  <copyright file="CustomerBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Base;

using System;

/// <summary>
/// Defines the common properties for the customer base class.
/// </summary>
public abstract class CustomerBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the middle name.
    /// </summary>
    public string MiddleName { get; set; } = default!;

    #endregion Public Properties
}