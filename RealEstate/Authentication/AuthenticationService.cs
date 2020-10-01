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
using System.Xml.XPath;

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

        /// <summary>
        /// Post a login request to the api. Stores the given user credentials in cookies and forwards any errors the api sends back.
        /// </summary>
        /// <param name="userForAuthentication">Holds info about which username and password a user tries to log in with.</param>
        /// <returns></returns>
        public async Task<AuthResponseContainer> Login(UserForAuthenticationDto userForAuthentication)
        {
            //Serializes the UserForAuthenticationDTO to a dictionary to easily be able to encode it to x-www-form-urlencoded httpmessage body
            var content = JsonSerializer.Serialize(userForAuthentication);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

            var req = new HttpRequestMessage(HttpMethod.Post, "/Token") { Content = new FormUrlEncodedContent(dictionary) };

            var authResult = await _client.SendAsync(req);

            var authContent = await authResult.Content.ReadAsStringAsync();

            var resultContainer = JsonSerializer.Deserialize<AuthResponseContainer>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!authResult.IsSuccessStatusCode)
            {
                //Adds a error message if there is some undefined error has happened
                if (resultContainer.Values == null)
                {
                    resultContainer.Values = new AuthResponseDto();
                    resultContainer.Errors = new List<string>();
                }
                resultContainer.Values.IsAuthSuccessful = false;
                resultContainer.Errors.Add("There has been some networking error, please check connection and try again.");
                
                return resultContainer;
            }

            //Sets information about the user and acesstoken to local storage
            await _localStorage.SetItemAsync("authToken", resultContainer.Values.AcessToken);
            await _localStorage.SetItemAsync("userName", resultContainer.Values.UserName);
            await _localStorage.SetItemAsync("authorizationExpires", resultContainer.Values.Expires);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.UserName);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resultContainer.Values.AcessToken);

            resultContainer.Values.IsAuthSuccessful = true;

            return resultContainer;
        }

        /// <summary>
        /// Removes all the login credentials stored in cookis and sets the default authorization token to be null in the http client.
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userName");
            await _localStorage.RemoveItemAsync("authorizationExpires");

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userForRegistration">Holds info about which username, password and email a user tries to create a new account with.</param>
        /// <returns></returns>
        public async Task<RegisrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            //Serializes the user to a json
            var content = JsonSerializer.Serialize(userForRegistration);

            //Desiralizes the user to a dictionary to fit the api needs of a request body in x-www-form-urlencoded format
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

            var req = new HttpRequestMessage(HttpMethod.Post, "/Api/Account/Register") { Content = new FormUrlEncodedContent(dictionary) };

            var registrationResult = await _client.SendAsync(req);

            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RegisrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if(!registrationResult.IsSuccessStatusCode)
            {
                //Adds a error message if there is some undefined error has happened
                if (result.Errors == null)
                {
                    result.Errors = new List<string>();
                    result.Errors.Add("There has been some networking error, please check connection and try again.");
                    result.succeeded = false;
                }
            }

            return result;
        }
    }
}
