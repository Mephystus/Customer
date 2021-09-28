// -------------------------------------------------------------------------------------
//  <copyright file="TimeoutSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Configuration;

using Polly.Timeout;

/// <summary>
/// Defines the timeout settings.
/// </summary>
public class TimeoutSettings
{
    /// <summary>
    /// Gets or sets a value indicatig whether this is enabled. 
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the timeout in seconds.
    /// </summary>
    public int TimeoutInSeconds { get; set; }

    /// <summary>
    /// Gets or sets the timeout strategy.
    /// </summary>
    public TimeoutStrategy TimeoutStrategy { get; set; }
}