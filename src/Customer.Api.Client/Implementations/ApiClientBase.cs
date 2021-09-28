// -------------------------------------------------------------------------------------
//  <copyright file="ApiClientBase.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using System.Net.Http.Json;
using Customer.Api.Client.Exceptions;
using Newtonsoft.Json;

namespace Customer.Api.Client.Implementations
{
    /// <summary>
    /// Provides base implementation for HTTP requests on the API clients.
    /// </summary>
    public abstract class ApiClientBase
    {
        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiClientBase"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client</param>
        public ApiClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Sends a DELETE request to the particular URI.
        /// </summary>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The expected object of type <see cref="TResponse"/></returns>
        protected virtual async Task<TResponse> DeleteAsync<TResponse>(
            string requestUri,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.DeleteAsync(requestUri, cancellationToken);

            return await DeserializeAsync<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Sends a DELETE request to the particular URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected virtual async Task DeleteAsync (
            string requestUri,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.DeleteAsync(requestUri, cancellationToken);

            CheckHttpResponseMessage(httpResponse);
        }

        /// <summary>
        /// Sends a GET request to the particular URI.
        /// </summary>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The expected object of type <see cref="TResponse"/></returns>
        protected virtual async Task<TResponse> GetAsync<TResponse>(
            string requestUri, 
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.GetAsync(requestUri, cancellationToken);

            return await DeserializeAsync<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Sends a POST request to the particular URI.
        /// </summary>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <typeparam name="TRequest">The request type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="requestUri">The request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The expected object of type <see cref="TResponse"/></returns>
        protected virtual async Task<TResponse> PostAsync<TResponse, TRequest>(
            string requestUri,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);

            return await DeserializeAsync<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Sends a POST request to the particular URI.
        /// </summary>
        /// <typeparam name="TRequest">The request type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="requestUri">The request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected virtual async Task PostAsync<TRequest>(
            string requestUri,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);
            
            CheckHttpResponseMessage(httpResponse);
        }

        /// <summary>
        /// Sends a PUT request to the particular URI.
        /// </summary>
        /// <typeparam name="TResponse">The response type.</typeparam>
        /// <typeparam name="TRequest">The request type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="requestUri">The request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The expected object of type <see cref="TResponse"/></returns>
        protected virtual async Task<TResponse> PutAsync<TResponse, TRequest>(
            string requestUri,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, request, cancellationToken);

            return await DeserializeAsync<TResponse>(httpResponse, cancellationToken);
        }

        /// <summary>
        /// Sends a PUT request to the particular URI.
        /// </summary>
        /// <typeparam name="TRequest">The request type.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="requestUri">The request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected virtual async Task PutAsync<TRequest>(
            string requestUri,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, request, cancellationToken);

            CheckHttpResponseMessage(httpResponse);
        }

        /// <summary>
        /// Checks the HTTP response message.
        /// It will throw an exception if response was not successful.
        /// </summary>
        /// <param name="httpResponse">The HTTP response message.</param>
        private static void CheckHttpResponseMessage(HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.RequestMessage == null)
            {
                throw new ArgumentNullException(nameof(httpResponse.RequestMessage));
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                string message = $"The call to '{httpResponse.RequestMessage.RequestUri} failed with status code {(int)httpResponse.StatusCode} ({httpResponse.StatusCode}.)";
                throw new ApiClientException(message, httpResponse);
            }
        }

        /// <summary>
        /// Deserialises the HTTP response into to the object.
        /// </summary>
        /// <typeparam name="TResponse">The type to return.</typeparam>
        /// <param name="httpResponse">The HTTP response message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object of type <see cref="Tresponse"/></returns>
        private async Task<TResponse> DeserializeAsync<TResponse>(
            HttpResponseMessage httpResponse,
            CancellationToken cancellationToken = default)
        {
            CheckHttpResponseMessage(httpResponse);

            try
            {
                string responseString = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

                var response = JsonConvert.DeserializeObject<TResponse>(responseString);

                if (response == null)
                {
                    throw new ArgumentNullException(nameof(response));
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new ApiClientException($"Failed to deserialise the content as '{typeof(TResponse)}'!", httpResponse, ex);
            }
        }

    }
}
