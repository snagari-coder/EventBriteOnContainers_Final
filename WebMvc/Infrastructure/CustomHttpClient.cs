using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMvc.Models;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace WebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {

        private readonly HttpClient _client;  //makes actual call get post

        public CustomHttpClient()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            return await _client.SendAsync(requestMessage);
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            var response = await _client.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();



        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method,
          string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
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

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, authorizationToken, authorizationMethod);
        }

        public async Task<string> PostEventAsync(string uri, EventItem item, IFormFile file, string authorizationToken = null, string authorizationMethod = "Bearer")
        {

            var httpContent = new MultipartFormDataContent();        
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
            data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);         

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "multipart/form-data");
            httpContent.Add(content);
            httpContent.Add(bytes, "file", file.FileName);
            requestMessage.Content=httpContent;
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);
           
            return await response.Content.ReadAsStringAsync();
           
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, authorizationToken, authorizationMethod);
        }
    }
    
}
