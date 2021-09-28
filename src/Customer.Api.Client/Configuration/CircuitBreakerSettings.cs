// -------------------------------------------------------------------------------------
//  <copyright file="CircuitBreakerSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Configuration;

/// <summary>
/// Defines the settings for the circuit breaker.
/// </summary>
public class CircuitBreakerSettings
{
    /// <summary>
    /// Gets or sets the break duration in seconds.
    /// </summary>
    public int BreakDurationInSeconds { get; set; }

    /// <summary>
    /// Gets or sets a value indicatig whether this is enabled. 
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the number of events before breaking the circuit.
    /// </summary>
    public int EventsBeforeBreak { get; set; }
}