// -------------------------------------------------------------------------------------
//  <copyright file="PingResponse.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models;

using System;

/// <summary>
/// Defines the response class used for the customer ping.
/// </summary>
public class PingResponse
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the date/time.
    /// </summary>
    public DateTime DateTime { get; set; }

    #endregion Public Properties
}