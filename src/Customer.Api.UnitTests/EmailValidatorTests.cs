// -------------------------------------------------------------------------------------
//  <copyright file="EmailValidatorTests.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.UnitTests;

using Customer.Api.Validators;
using Customer.Models;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

/// <summary>
/// Performs the unit tests associated with email validation.
/// </summary>
public class EmailValidatorTests
{
    /// <summary>
    /// The email request validator.
    /// </summary>
    private readonly EmailRequestValidator _sut;

    /// <summary>
    /// Initialises a new instance of the <see cref="EmailValidatorTests"/> class.
    /// </summary>
    public EmailValidatorTests() 
    {
        _sut = new EmailRequestValidator();
    }

    /// <summary>
    /// Test the email request validator for empty email address.
    /// </summary>
    [Fact]
    public void EmailRequestValidator_EmptyEmailAddress_RaisesValidationError()
    {
        //// Arrange
        var request = new EmailRequest();

        //// Act
        var result = _sut.TestValidate(request);

        //// Assert
        result.ShouldHaveValidationErrorFor(x => x.EmailAddress)
                .WithErrorMessage("'Email Address' must not be empty.")
                .WithSeverity(Severity.Error);
    }

    /// <summary>
    /// Test the email request validator for invalid email address.
    /// </summary>
    [Fact]
    public void EmailRequestValidator_InvalidEmailAddress_RaisesValidationError()
    {

        var request = new EmailRequest 
        {
            EmailAddress = "test.test"
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.EmailAddress)
                .WithErrorMessage("'Email Address' is not a valid email address.")
                .WithSeverity(Severity.Error);
    }
} 
