// -------------------------------------------------------------------------------------
//  <copyright file="ExternalCustomerServiceTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Tests.Implementations;

using System;
using System.Threading.Tasks;
using Customer.ExternalServices.Implementations;
using FluentAssertions;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="ExternalCustomerService"/> class.
/// </summary>
public class ExternalCustomerServiceTests
{
    #region Private Fields

    /// <summary>
    /// The external customer service (SUT).
    /// </summary>
    private readonly ExternalCustomerService _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="ExternalCustomerServiceTests"/> class.
    /// </summary>
    public ExternalCustomerServiceTests()
    {
        _sut = new ExternalCustomerService();
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
        var customerId = new Guid("bb2b412a-7f9a-43ff-b592-7def8c572aa2");

        var expectedResponse = new Dto.CustomerRiskResponse
        {
            CustomerId = customerId,
            RiskIndicator = "MEDIUM",
            Description = "Some description!"
        };

        //// Act
        var actualResponse = await _sut.GetCustomerRiskAsync(customerId);

        //// Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion Public Methods
}