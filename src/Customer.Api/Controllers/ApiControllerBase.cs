// -------------------------------------------------------------------------------------
//  <copyright file="ApiControllerBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Controllers;

using Customer.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Base API controller.
/// </summary>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Gets the user (Application Principal).
    /// </summary>
    public new ApplicationPrincipal User => (ApplicationPrincipal)HttpContext.User;
}


