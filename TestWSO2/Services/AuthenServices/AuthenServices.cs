using NewProject.Services.AuthenServices;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using NewProject.Models;
using System.Net.Http.Headers;
using System.Net.Http;
namespace NewProject
{
    public class AuthenServices:IAuthenServices
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private string token = "";
        private readonly HttpClient _httpClient;
        public AuthenServices(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentUserTokenAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            token = authState.User.Identity?.Name ?? "";
            return token;
        }
        public string GetCurrentToken()
        {
            var response = GetCurrentUserTokenAsync().GetAwaiter().GetResult();
            //task.Wait();
            return response;
        }
        public async Task<tbUser> GetMe()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            token = authState.User.Identity?.Name ?? "";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/auth/me");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ServiceResponse<tbUser>>();
                if (data == null || data.Data == null)
                {
                    throw new NullReferenceException("The API response or Data is null.");
                }
                return data.Data;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }
    }
}

