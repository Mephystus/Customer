// -------------------------------------------------------------------------------------
//  <copyright file="EmailRequestValidator.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Validators;

using Customer.Models;
using FluentValidation;

/// <summary>
/// Performs the validation for the <see cref="EmailRequest"/>
/// </summary>
public class EmailRequestValidator : AbstractValidator<EmailRequest>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="EmailRequestValidator"/> class.
    /// </summary>
    public EmailRequestValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.EmailAddress).EmailAddress();
    }
}