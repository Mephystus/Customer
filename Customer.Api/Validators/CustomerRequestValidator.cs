// -------------------------------------------------------------------------------------
//  <copyright file="CustomerRequestValidator.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Validators;

using Customer.Models;
using FluentValidation;

/// <summary>
/// Performs the validation for the <see cref="CustomerRequest"/>
/// </summary>
public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerRequestValidator"/> class.
    /// </summary>
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today);
        RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.Today.AddYears(-100));
    }
}