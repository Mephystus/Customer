// -------------------------------------------------------------------------------------
//  <copyright file="ApplicationPrincipal.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Infrastructure.Security;

using System.Security.Claims;

/// <summary>
/// The application principal
/// </summary>
public class ApplicationPrincipal : ClaimsPrincipal
{

    /// <summary>
    /// Initialises a new instance of the <see cref="ApplicationPrincipal"/> class.
    /// </summary>
    /// <param name="claimsIdentity">The claims-based identity</param>
    public ApplicationPrincipal(ClaimsIdentity claimsIdentity)
        : base(claimsIdentity)
    {

    }

    /// <summary>
    /// Gets the language
    /// </summary>
    public string Language
    {
        get
        {
            var claim = FindFirst(x => x.Type == ClaimTypes.Locality);
            return claim != null ? claim.Value : string.Empty;
        }
    }
}