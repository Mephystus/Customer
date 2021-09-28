// -------------------------------------------------------------------------------------
//  <copyright file="RetriesBackoffSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Configuration;

/// <summary>
/// Enumeration for retries backoff.
/// </summary>
public enum RetriesBackoffSettings
{
    /// <summary>
    /// Indicates a constant backoff
    /// </summary>
    Constant,

    /// <summary>
    /// Indicates a linear backoff
    /// </summary>
    Linear,

    /// <summary>
    /// Indicates an exponential backoff
    /// </summary>
    Exponential
}
