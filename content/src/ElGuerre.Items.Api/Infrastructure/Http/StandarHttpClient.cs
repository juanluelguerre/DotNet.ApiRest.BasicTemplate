using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Infrastructure.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<StandardHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StandardHttpClient(
            ILogger<StandardHttpClient> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetStringAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(
            HttpMethod method,
            string uri,
            T item,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(method, uri);

            SetAuthorizationHeader(requestMessage);
           
            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var requestAccepts = _client.DefaultRequestHeaders.Accept;
            requestAccepts.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(
            string uri,
            T item,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, authorizationToken, requestId, authorizationMethod);
        }

        public async Task<HttpResponseMessage> PostFileAsync(
            string uri,
            byte[] file,
            string fileName,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            HttpResponseMessage response = null;

            Stream stream = new MemoryStream(file);
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StreamContent(stream)
                {
                    Headers =
                        {
                            ContentLength = file.Length,
                            ContentType = new MediaTypeHeaderValue("multipart/form-data")
                        }
                }, "file", fileName);

                requestMessage.Content = form;
                // response = await _client.PostAsync(uri, form);
                response = await _client.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }
            }

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(
            string uri,
            T item,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, authorizationToken, requestId, authorizationMethod);
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string uri,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            return await _client.SendAsync(requestMessage);
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }

    }
}

