// -------------------------------------------------------------------------------------
//  <copyright file="AppSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Infrastructure.Settings;

using System.Collections.Generic;

/// <summary>
/// Defines the configuration/settings.
/// </summary>
public class AppSettings
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the settings for the 'Demo' customer xternal service;
    /// </summary>
    public DemoExternalCustomerService DemoExternalCustomerService { get; set; } = default!;

    /// <summary>
    /// Gets or sets the external customer services.
    /// </summary>
    public Dictionary<string, string> ExternalCustomerServices { get; set; } = new Dictionary<string, string>();

    #endregion Public Properties
}