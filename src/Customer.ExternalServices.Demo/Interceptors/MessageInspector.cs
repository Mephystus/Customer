// -------------------------------------------------------------------------------------
//  <copyright file="MessageInspector.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Demo.Interceptors;

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Extensions.Logging;

/// <summary>
/// Direct implementation of <see cref="IClientMessageInspector"/> to log the SOAP request/response.
/// </summary>
public class MessageInspector : IClientMessageInspector
{
    #region Private Fields

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MessageInspector> _logger;

    /// <summary>
    /// The unique identifier of the request/response.
    /// </summary>
    private readonly Guid _requestId;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="MessageInspector"/> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{MessageInspector}"></see>.</param>
    public MessageInspector(ILogger<MessageInspector> logger)
    {
        _logger = logger;
        _requestId = Guid.NewGuid();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Enables inspection or modification of a message after a reply message is received
    /// but prior to passing it back to the client application.
    /// </summary>
    /// <param name="reply">The reply message</param>
    /// <param name="correlationState">The correlation state.</param>
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
        var replyMessage = reply.ToString();

        var logMessage = $"Response ({_requestId}) {Environment.NewLine} {{@replyMessage}} {Environment.NewLine}";

        _logger.LogInformation(logMessage, replyMessage);
    }

    /// <summary>
    /// Enables inspection or modification of a message before a request message is sent to a service.
    /// </summary>
    /// <param name="request">The request message.</param>
    /// <param name="channel">The client channel.</param>
    /// <returns></returns>
    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
        var requestMessage = request.ToString();

        var logMessage = $"Request ({_requestId} | {channel.RemoteAddress.Uri.AbsoluteUri}) {Environment.NewLine} {{@requestMessage}} {Environment.NewLine}";

        _logger.LogInformation(logMessage, requestMessage);

        return request;
    }

    #endregion Public Methods
}