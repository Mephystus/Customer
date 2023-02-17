// -------------------------------------------------------------------------------------
//  <copyright file="EmailRequestValidator.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Models.Validators;

using FluentValidation;
using Models;

/// <summary>
/// Performs the validation for the <see cref="EmailRequest"/>
/// </summary>
public class EmailRequestValidator : AbstractValidator<EmailRequest>
{
    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="EmailRequestValidator"/> class.
    /// </summary>
    public EmailRequestValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.EmailAddress).EmailAddress();
    }

    #endregion Public Constructors
}