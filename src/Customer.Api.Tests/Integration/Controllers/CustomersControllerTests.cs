// -------------------------------------------------------------------------------------
//  <copyright file="CustomersControllerTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Customer.Api.Controllers;
using Customer.Data.Access;
using Customer.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharedLibrary.Models.Models.Error;
using SharedLibrary.Models.Models.Validation;
using Xunit;

namespace Customer.Api.Tests.Integration.Controllers;

/// <summary>
/// Performs the integrations tests associated with the <see cref="CustomersController"/> class.
/// </summary>
public class CustomersControllerTests
{
    #region Private Fields

    /// <summary>
    /// The API base endpoint suffix.
    /// </summary>
    private const string BaseEndpointSuffix = "api/customers";

    /// <summary>
    /// The lock object for the customer context.
    /// </summary>
    private static readonly object CustomerContextLock = new();

    /// <summary>
    /// Indicates whether the DB context was initialised.
    /// </summary>
    private static bool _isDbContextInitialised;

    /// <summary>
    /// The HTTP client.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// The customer context.
    /// </summary>
    private ServiceProvider? _serviceProvider;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialise a new instance of the <see cref="CustomersControllerTests"/> class.
    /// </summary>
    public CustomersControllerTests()
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
                        options.UseInMemoryDatabase(databaseName: "DB Test");
                    }, ServiceLifetime.Singleton);

                    _serviceProvider = services.BuildServiceProvider();

                    using var scope = _serviceProvider.CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

                    context.Database.EnsureCreated();
                    InitializeDb(context);
                });
            });

        _httpClient = factory.CreateClient();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the <i>CreateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_InvalidCustomer_ReturnsBadRequest()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        var request = new CustomerRequest
        {
            Id = customerId,
            FirstName = String.Empty,
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(150),
            MiddleName = "L.",
            UpdatedBy = "admin",
        };

        //// Act
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpointSuffix}", request);

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<ValidationResponse>(responseText);

        var expectedResponse = new ValidationResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Details = new List<ValidationResponseDetail>
            {
              new ValidationResponseDetail
              {
                  FieldName = "FirstName",
                  Message = "'First Name' must not be empty."
              },
              new ValidationResponseDetail
              {
                  FieldName = "DateOfBirth",
                  Message = "Did you came from the future?"
              }
            }
        };

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>CreateCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCustomerAsync_ValidCustomer_ReturnsCreated()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        var request = new CustomerRequest
        {
            Id = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L.",
            UpdatedBy = "admin",
        };

        //// Act
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpointSuffix}", request);

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<CustomerResponse>(responseText);

        var expectedResponse = await _httpClient.GetFromJsonAsync<CustomerResponse>($"{BaseEndpointSuffix}/{customerId}");

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>DeleteCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_InvalidCustomerId_ReturnsNotFound()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        //// Act
        var response = await _httpClient.DeleteAsync($"{BaseEndpointSuffix}/{customerId}");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseText);

        var expectedResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status404NotFound,
            Details = new List<ErrorResponseDetail>
            {
              new ErrorResponseDetail
              {
                  Message = $"The customer ({customerId}) does not exist."
              }
            }
        };

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>DeleteCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteCustomerAsync_WithCustomerId_ReturnsOk()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        var customer = new Data.Schema.Customer
        {
            CustomerId = customerId,
            FirstName = "Jane",
            LastName = "Pearl",
            DateOfBirth = DateTime.Today.AddYears(-39),
            MiddleName = "K.",
            UpdatedBy = "admin",
            UpdatedDate = DateTime.Now
        };

        Assert.NotNull(_serviceProvider);

        await using var customerContext = _serviceProvider.GetRequiredService<CustomerContext>();

        customerContext.Customers.Add(customer);
        await customerContext.SaveChangesAsync();

        bool beforeAction = customerContext.Customers.Any(x => x.CustomerId == customerId);

        //// Act
        var response = await _httpClient.DeleteAsync($"{BaseEndpointSuffix}/{customerId}");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        bool afterAction = customerContext.Customers.Any(x => x.CustomerId == customerId);

        Assert.True(beforeAction);
        Assert.False(afterAction);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_InvalidCustomerId_ReturnsNotFound()
    {
        //// Arrange
        var customerId = Guid.NewGuid();

        //// Act
        var response = await _httpClient.GetAsync($"{BaseEndpointSuffix}/{customerId}");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseText);

        var expectedResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status404NotFound,
            Details = new List<ErrorResponseDetail>
            {
              new ErrorResponseDetail
              {
                  Message = $"The customer ({customerId}) does not exist."
              }
            }
        };

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    /// <summary>
    /// Test the <i>GetCustomerAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerAsync_WithCustomerId_ReturnsOk()
    {
        //// Arrange
        var customerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa1");

        //// Act
        var response = await _httpClient.GetAsync($"{BaseEndpointSuffix}/{customerId}");

        //// Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseText = await response.Content.ReadAsStringAsync();
        var actualResponse = JsonConvert.DeserializeObject<CustomerResponse>(responseText);

        var expectedResponse = new CustomerResponse
        {
            Id = customerId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Today.AddYears(-50),
            MiddleName = "L."
        };

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Initializes the database with test data.
    /// </summary>
    /// <param name="context">The DB context.</param>
    private static void InitializeDb(CustomerContext context)
    {
        lock (CustomerContextLock)
        {
            if (_isDbContextInitialised)
            {
                return;
            }

            context.Customers.Add(new Data.Schema.Customer
            {
                CustomerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa1"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today.AddYears(-50),
                MiddleName = "L.",
                UpdatedBy = "admin",
                UpdatedDate = DateTime.UtcNow
            });

            context.Customers.Add(new Data.Schema.Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Kane",
                DateOfBirth = DateTime.Today.AddYears(-42),
                MiddleName = "J.",
                UpdatedBy = "admin",
                UpdatedDate = DateTime.UtcNow
            });

            context.SaveChanges();

            _isDbContextInitialised = true;
        }
    }

    #endregion Private Methods
}