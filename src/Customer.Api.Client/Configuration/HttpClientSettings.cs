// -------------------------------------------------------------------------------------
//  <copyright file="HttpClientSettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Configuration;

/// <summary>
/// Defines the HTTP client configuration/settings/
/// </summary>
public class HttpClientSettings
{
    /// <summary>
    /// Gets or sets the base URL.
    /// </summary>
    public string BaseUrl { get; set; } = default!;

    /// <summary>
    /// Gets or sets the circuit breaker settings.
    /// </summary>
    public CircuitBreakerSettings CircuitBreaker { get; set; } = default!;

    /// <summary>
    /// Gets or sets the request headers.
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets or sets the retry settings.
    /// </summary>
    public RetrySettings Retry { get; set; } = default!;

    /// <summary>
    /// Gets or sets the timeout settings.
    /// </summary>
    public TimeoutSettings Timeout { get; set; } = default!;
}