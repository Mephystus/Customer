// -------------------------------------------------------------------------------------
//  <copyright file="CustomerResponse.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System.Collections.Generic;
using Base;

/// <summary>
/// Defines the customer response.
/// </summary>
public class CustomerResponse : CustomerBase
{
    /// <summary>
    /// Gets or sets the addresses.
    /// </summary>
    public List<AddressResponse> Addresses { get; set; } = new List<AddressResponse>();

    /// <summary>
    /// Gets or sets the emails.
    /// </summary>
    public List<EmailResponse> Emails { get; set; } = new List<EmailResponse>();    

    /// <summary>
    /// Gets or sets the phones.
    /// </summary>
    public List<PhoneResponse> Phones { get; set; } = new List<PhoneResponse>();
}