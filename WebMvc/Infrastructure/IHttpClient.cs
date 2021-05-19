using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationmethod = "Bearer");
        Task<string> PostEventAsync(string uri, EventItem item,IFormFile file,string authorizationToken = null, string authorizationmethod = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri,
           T item,
           string authorizationToken = null,
           string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri,
            T item, string authorizationToken = null,
            string authorizationMethod = "Bearer");
    }
}
