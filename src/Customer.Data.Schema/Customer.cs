// -------------------------------------------------------------------------------------
//  <copyright file="Customer.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Schema;

using System;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Defines the customer entity model.
/// </summary>
public class Customer
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the customer Id.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the middle name.
    /// </summary>
    public string MiddleName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user that created/updated the customer.
    /// </summary>
    public string UpdatedBy { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date/time that the customer was created/updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    #endregion Public Properties
}