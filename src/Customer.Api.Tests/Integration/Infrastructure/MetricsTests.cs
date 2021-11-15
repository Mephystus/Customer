// -------------------------------------------------------------------------------------
//  <copyright file="MetricsTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Tests.Integration.Infrastructure;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Customer.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

/// <summary>
/// Performs the integrations tests associated with the metrics endpoint.
/// </summary>
public class MetricsTests
{
    #region Private Fields

    /// <summary>
    /// The HTTP client.
    /// </summary>
    private readonly HttpClient _httpClient;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialise a new instance of the <see cref="MetricsTests"/> class.
    /// </summary>
    public MetricsTests()
    {
        var factory = new WebApplicationFactory<CustomersController>()
            .WithWebHostBuilder(_ => { });

        _httpClient = factory.CreateClient();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the metrics endpoint.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task Metrics_ReturnsOk()
    {
        //// Arrange

        //// Act
        var response = await _httpClient.GetAsync("metrics-text");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseText = await response.Content.ReadAsStringAsync();

        Assert.NotNull(responseText);

        Assert.StartsWith("# HELP application_httprequests_apdex", responseText);
    }

    #endregion Public Methods
}