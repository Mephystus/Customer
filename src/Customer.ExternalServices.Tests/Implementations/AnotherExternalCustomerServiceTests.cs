// -------------------------------------------------------------------------------------
//  <copyright file="AnotherAnotherExternalCustomerServiceTests.cs" company="{Company Name}">
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
/// Performs the unit tests associated with the <see cref="AnotherExternalCustomerService"/> class.
/// </summary>
public class AnotherExternalCustomerServiceTests
{
    #region Private Fields

    /// <summary>
    /// The "another" external customer service (SUT).
    /// </summary>
    private readonly AnotherExternalCustomerService _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="AnotherExternalCustomerServiceTests"/> class.
    /// </summary>
    public AnotherExternalCustomerServiceTests()
    {
        _sut = new AnotherExternalCustomerService(Substitute.For<IServiceProvider>());
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
        var customerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa1");

        var expectedResponse = new Dto.CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = "LOW",
            Description = "Another description!"
        };

        //// Act
        var actualResponse = await _sut.GetCustomerRiskAsync(customerId);

        //// Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods
}