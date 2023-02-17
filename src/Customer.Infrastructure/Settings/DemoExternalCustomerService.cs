// -------------------------------------------------------------------------------------
//  <copyright file="DemoExternalCustomerService.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Infrastructure.Settings;

/// <summary>
/// Defines the settings for the 'Demo' external customer service.
/// </summary>
public class DemoExternalCustomerService
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the endpoint.
    /// </summary>
    public string Endpoint { get; set; } = default!;

    #endregion Public Properties
}