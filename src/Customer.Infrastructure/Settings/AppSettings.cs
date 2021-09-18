// -------------------------------------------------------------------------------------
//  <copyright file="AppSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Customer.Infrastructure.Settings;

/// <summary>
/// Defines the configuration/settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Gets or sets the external customer services.
    /// </summary>
    public Dictionary<string, string> ExternalCustomerServices { get; set; } = new Dictionary<string, string>();
}
