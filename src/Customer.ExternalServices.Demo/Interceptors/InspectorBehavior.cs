// -------------------------------------------------------------------------------------
//  <copyright file="InspectorBehavior.cs" company="{Company Name}">
//    Copyright (c) {Company Name}. All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.ExternalServices.Demo.Interceptors;

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

/// <summary>
/// Direct implementation of <see cref="IEndpointBehavior"/> to extend run-time behavior for an endpoint
/// </summary>
public class InspectorBehavior : IEndpointBehavior
{
    #region Private Fields

    /// <summary>
    /// The client message inspector.
    /// </summary>
    private readonly IClientMessageInspector _clientMessageInspector;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initialises a new instance of the <see cref="IClientMessageInspector"/> class.
    /// </summary>
    /// <param name="clientMessageInspector">An instance of <see cref="IClientMessageInspector"/>.</param>
    public InspectorBehavior(IClientMessageInspector clientMessageInspector)
    {
        _clientMessageInspector = clientMessageInspector;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Add data at runtime to bindings to support custom behavior.
    /// </summary>
    /// <param name="endpoint">The service endpoint.</param>
    /// <param name="bindingParameters">The binding parameter collection</param>
    public void AddBindingParameters(
        ServiceEndpoint endpoint,
        BindingParameterCollection bindingParameters)
    {
        //// No implementation required.
    }

    /// <summary>
    /// Apply the behaviour for the client run-time execution.
    /// </summary>
    /// <param name="endpoint">The service endpoint.</param>
    /// <param name="clientRuntime">The client runtime.</param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
        clientRuntime.ClientMessageInspectors.Add(_clientMessageInspector);
    }

    /// <summary>
    /// Apply the behaviour for the dispatcher run-time execution.
    /// </summary>
    /// <param name="endpoint">The service endpoint.</param>
    /// <param name="endpointDispatcher">The endpoint dispatcher.</param>
    public void ApplyDispatchBehavior(
        ServiceEndpoint endpoint,
        EndpointDispatcher endpointDispatcher)
    {
        //// No implementation required.
    }

    /// <summary>
    /// Validation to confirm that the endpoint meets some intended criteria.
    /// </summary>
    /// <param name="endpoint">The service endpoint</param>
    public void Validate(ServiceEndpoint endpoint)
    {
        //// No implementation required.
    }

    #endregion Public Methods
}