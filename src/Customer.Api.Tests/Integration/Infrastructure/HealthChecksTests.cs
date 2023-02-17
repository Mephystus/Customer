// -------------------------------------------------------------------------------------
//  <copyright file="HealthChecksTests.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Tests.Integration.Infrastructure;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Customer.Api.Controllers;
using Data.Access;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharedLibrary.Models.HealthCheck;
using Xunit;

/// <summary>
/// Performs the integrations tests associated with the health check endpoint.
/// </summary>
public class HealthChecksTests
{
    #region Private Fields

    /// <summary>
    /// The HTTP client.
    /// </summary>
    private readonly HttpClient _httpClient;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialise a new instance of the <see cref="HealthChecksTests"/> class.
    /// </summary>
    public HealthChecksTests()
    {
        var factory = new WebApplicationFactory<CustomersController>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<CustomerContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<CustomerContext>(options =>
                    {
                        options.UseInMemoryDatabase(databaseName: "DB Health Test");
                    }, ServiceLifetime.Singleton);

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

                    context.Database.EnsureCreated();
                });
            });

        _httpClient = factory.CreateClient();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the health check endpoint.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
        //// Arrange

        //// Act
        var response = await _httpClient.GetAsync("health");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<HealthCheckResponse>(responseText);

        Assert.NotNull(actualResponse);

        var expectedResponse = new HealthCheckResponse
        {
            Status = "Healthy",
            Details = new List<HealthCheckDetail>
             {
                 new()
                 {
                     Status = "Healthy",
                     Description = "",
                     Component = "Db"
                 }
             },
            Duration = actualResponse.Duration  //// Need to set this with the same value, because the duration is variable/uncertain.
        };

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods
}