// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequestValidatorTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Tests.Factories;

using System;
using System.Collections.Generic;
using Customer.ExternalServices.Factories.Implementations;
using Customer.ExternalServices.Implementations;
using Microsoft.Extensions.Configuration;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="ExternalCustomerServiceFactory"/> class.
/// </summary>
public class ExternalCustomerServiceFactoryTests
{
    #region Public Methods

    /// <summary>
    /// Test the <i>GetExternalCustomerService</i>.
    /// </summary>
    [Fact]
    public void GetExternalCustomerService_EmptyAssemblyName_ThrowsArgumentNullException()
    {
        //// Arrange
        var inMemorySettings = new Dictionary<string, string> {
            { "ExternalCustomerServices:1", "" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var sut = new ExternalCustomerServiceFactory(configuration);

        //// Act
        void Action(string identifier) { sut.GetExternalCustomerService(identifier); }

        //// Assert
        var exception = Assert.Throws<ArgumentNullException>(() => Action("1"));

        Assert.Equal("Value cannot be null. (Parameter 'assemblyName')", exception.Message);
    }

    /// <summary>
    /// Test the <i>GetExternalCustomerService</i>.
    /// </summary>
    [Fact]
    public void GetExternalCustomerService_InvalidAssemblyType_ThrowsArgumentNullException()
    {
        //// Arrange
        var inMemorySettings = new Dictionary<string, string> {
            { "ExternalCustomerServices:1", "InvalidAssemblyType" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var sut = new ExternalCustomerServiceFactory(configuration);

        //// Act
        void Action(string identifier) { sut.GetExternalCustomerService(identifier); }

        //// Assert
        var exception = Assert.Throws<ArgumentNullException>(() => Action("1"));

        Assert.Equal("Value cannot be null. (Parameter 'objectType')", exception.Message);
    }

    /// <summary>
    /// Test the <i>GetExternalCustomerService</i>.
    /// </summary>
    [Fact]
    public void GetExternalCustomerService_ReturnsExternalCustomerService()
    {
        //// Arrange
        var inMemorySettings = new Dictionary<string, string> {
            { "ExternalCustomerServices:1", "Customer.ExternalServices.Implementations.ExternalCustomerService, Customer.ExternalServices" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var sut = new ExternalCustomerServiceFactory(configuration);

        //// Act
        var externalCustomerService = sut.GetExternalCustomerService("1");

        //// Assert
        Assert.NotNull(externalCustomerService);

        Assert.IsType<ExternalCustomerService>(externalCustomerService);
    }

    #endregion Public Methods
}