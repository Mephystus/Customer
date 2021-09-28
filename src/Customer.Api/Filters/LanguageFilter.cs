// -------------------------------------------------------------------------------------
//  <copyright file="LanguageFilter.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Filters;

using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Customer.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Filter to intercept the language from the headers.
/// </summary>
public class LanguageFilter : IAsyncActionFilter
{
    /// <summary>
    /// Intercepts the execution pipeline to obtain the language.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <param name="next">The next action to be executed.</param>
    /// <returns>A <see cref="Task"/> that on completion indicates the filter has executed.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Pre
        CheckLanguage(context);

        await next();


        // Post
    }

    /// <summary>
    /// Checks the language code (via headers) and set it into the principal.
    /// </summary>
    /// <param name="context">The execution context.</param>
    private static void CheckLanguage(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers.ToList();

        var languageHeader = headers.FirstOrDefault(x => x.Key == "Accept-Language");

        if (languageHeader.Key == null)
        {
            return;
        }

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Locality, languageHeader.Value));

        context.HttpContext.User = new ApplicationPrincipal(identity);

        Thread.CurrentPrincipal = context.HttpContext.User;
    }
}