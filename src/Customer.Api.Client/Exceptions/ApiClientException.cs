// -------------------------------------------------------------------------------------
//  <copyright file="ApiClientException.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Exceptions;

using System.Net;
using System.Text;

/// <summary>
/// Defines the exception thrown by the API clients. 
/// </summary>
public class ApiClientException : Exception
{
    /// <summary>
    /// The HTTP response message.
    /// </summary>
    private readonly HttpResponseMessage _httpResponseMessage;

    /// <summary>
    /// Initialises a new instance of the <see cref="ApiClientException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="httpResponseMessage">The HTTP response message.</param>
    public ApiClientException(
        string message, 
        HttpResponseMessage httpResponseMessage)
        : this(message, httpResponseMessage, null)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ApiClientException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="httpResponseMessage">The HTTP response message.</param>
    /// <param name="exception">The inner exception</param>
    public ApiClientException(
        string message, 
        HttpResponseMessage httpResponseMessage,
        Exception? exception)
        : base(message, exception) 
    {
        _httpResponseMessage = httpResponseMessage ?? throw new ArgumentException(nameof(httpResponseMessage));
    }

    /// <summary>
    /// Gets the request Uri.
    /// </summary>
    public Uri? RequestUri => _httpResponseMessage.RequestMessage?.RequestUri;

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public HttpStatusCode HttpStatusCode => _httpResponseMessage.StatusCode;

    /// <summary>
    /// Gets the HTTP response message as a string.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The HTTP response message</returns>
    public async Task<string> GetResponseContentAsync(CancellationToken cancellationToken = default)
    {
        return await _httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
    }

    /// <summary>
    /// Creates and returns a string representation of the current exception.
    /// </summary>
    /// <returns>A string representation of the current exception.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{nameof(RequestUri)}: {RequestUri}");
        sb.AppendLine($"{nameof(HttpStatusCode)}: {HttpStatusCode}");
        sb.AppendLine();
        sb.AppendLine(base.ToString());

        return sb.ToString();
    }
}