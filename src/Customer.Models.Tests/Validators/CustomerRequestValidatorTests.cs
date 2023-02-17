// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequestValidatorTests.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Tests.Validators;

using System;
using Customer.Models.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using Models;
using Xunit;

/// <summary>
/// Performs the unit tests associated with the <see cref="CustomerRequestValidator"/> class.
/// </summary>
public class CustomerRequestValidatorTests
{
    #region Private Fields

    /// <summary>
    /// The customer request validator (SUT).
    /// </summary>
    private readonly CustomerRequestValidator _sut;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerRequestValidatorTests"/> class.
    /// </summary>
    public CustomerRequestValidatorTests()
    {
        _sut = new CustomerRequestValidator();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Test the customer request validator for a customer with date in the future.
    /// </summary>
    [Fact]
    public void CustomerRequestValidator_DateOfBirthInFuture_RaisesValidationError()
    {
        //// Arrange
        var request = new CustomerRequest
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Now.AddYears(1)
        };

        //// Act
        var result = _sut.TestValidate(request);

        //// Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 1);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
               .WithErrorMessage("Did you came from the future?")
               .WithSeverity(Severity.Error);
    }

    /// <summary>
    /// Test the customer request validator for an 'empty' customer.
    /// </summary>
    [Fact]
    public void CustomerRequestValidator_EmptyCustomer_RaisesValidationError()
    {
        //// Arrange
        var request = new CustomerRequest();

        //// Act
        var result = _sut.TestValidate(request);

        //// Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count == 3);

        result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("'First Name' must not be empty.")
                .WithSeverity(Severity.Error);

        result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("'Last Name' must not be empty.")
                .WithSeverity(Severity.Error);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage("You are too old!")
                .WithSeverity(Severity.Error);
    }

    /// <summary>
    /// Test the customer request validator for a valid customer.
    /// </summary>
    [Fact]
    public void CustomerRequestValidator_ValidCustomer()
    {
        //// Arrange
        var request = new CustomerRequest
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Now.AddYears(-10)
        };

        //// Act
        var result = _sut.TestValidate(request);

        //// Assert
        Assert.True(result.IsValid);
        Assert.True(result.Errors.Count == 0);
    }

    #endregion Public Methods
}