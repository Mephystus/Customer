﻿// -------------------------------------------------------------------------------------
//  <copyright file="SpecialExternalCustomerServiceTests.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Tests.Implementations;

using Customer.ExternalServices.Implementations;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="SpecialExternalCustomerService"/> class.
/// </summary>
public class SpecialExternalCustomerServiceTests
{
    #region Private Fields

    /// <summary>
    /// The "special" external customer service (SUT).
    /// </summary>
    private readonly SpecialExternalCustomerService _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="SpecialExternalCustomerServiceTests"/> class.
    /// </summary>
    public SpecialExternalCustomerServiceTests()
    {
        _sut = new SpecialExternalCustomerService(Substitute.For<IServiceProvider>());
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the <i>GetCustomerRiskAsync</i>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetCustomerRiskAsync_ReturnsRiskResponse()
    {
        //// Arrange
        var customerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa3");

        var expectedResponse = new Dto.CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = "HIGH",
            Description = "Special description!"
        };

        //// Act
        var actualResponse = await _sut.GetCustomerRiskAsync(customerId);

        //// Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods
}