// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequestValidator.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Validators;

using System;
using FluentValidation;
using Models;

/// <summary>
/// Performs the validation for the <see cref="CustomerRequest"/>
/// </summary>
public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerRequestValidator"/> class.
    /// </summary>
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithMessage("Did you came from the future?");

        RuleFor(x => x.DateOfBirth)
            .GreaterThan(DateTime.Today.AddYears(-100))
            .WithMessage("You are too old!");
    }

    #endregion Public Constructors
}