using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using RealEstate.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealEstate.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

            var req = new HttpRequestMessage(HttpMethod.Post, "/Token") { Content = new FormUrlEncodedContent(dictionary) };

            var authResult = await _client.SendAsync(req);

            var authContent = await authResult.Content.ReadAsStringAsync();

            var resultContainer = JsonSerializer.Deserialize<AuthResponseContainer>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = resultContainer.Values;

            if (!authResult.IsSuccessStatusCode)
            {
                result.IsAuthSuccessful = false;
                return result;
            }

            await _localStorage.SetItemAsync("authToken", result.AcessToken);
            await _localStorage.SetItemAsync("userName", result.UserName);
            await _localStorage.SetItemAsync("authorizationExpires", result.Expires);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.UserName);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AcessToken);

            result.IsAuthSuccessful = true;

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userName");
            await _localStorage.RemoveItemAsync("authorizationExpires");

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

            var req = new HttpRequestMessage(HttpMethod.Post, "/Api/Account/Register") { Content = new FormUrlEncodedContent(dictionary) };

            var registrationResult = await _client.SendAsync(req);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RegisrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
