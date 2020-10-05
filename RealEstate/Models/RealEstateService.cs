﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class RealEstateService : IRealEstateService
    {
        private readonly HttpClient http;
        private readonly ILocalStorageService localStorage;
        private readonly NavigationManager NavigationManager;

        public RealEstateService(HttpClient http, ILocalStorageService localStorageService, NavigationManager navigationMangager)
        {
            this.http = http;
            this.localStorage = localStorageService;
            this.NavigationManager = navigationMangager;
        }

        public async Task<bool> UserLoggedInAndValid()
        {
            DateTime timeOfAuthExpiration = Convert.ToDateTime(await localStorage.GetItemAsync<string>("authorizationExpires"));

            if (DateTime.Compare(DateTime.Now, timeOfAuthExpiration) > 0)
            {

                return false;
            }

            return true;
        }

        public async Task<Propertys> GetRealEstate(int id)
        {
            return await http.GetFromJsonAsync<Propertys>($"RealEstate/{id}");
        }

        public async Task<IEnumerable<Propertys>> GetRealEstates()
        {
            HttpResponseMessage task = await http.GetAsync("RealEstates");
            string jsonString = await task.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Propertys>>(jsonString);
        }

        public async Task<PropertysForRegistration> PostANewRealEstate(PropertysForRegistration newRealEstateToRegister)
        {

            var serializedRealEstate = JsonSerializer.Serialize(newRealEstateToRegister);
            var bodyContent = new StringContent(serializedRealEstate, Encoding.UTF8, "application/json");

            var postResult = await http.PostAsync("api/RealEstates", bodyContent);

            var authContent = await postResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PropertysForRegistration>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!postResult.IsSuccessStatusCode)
            {
                //Adds a error message if there is some undefined error that has happened
                if (result.Errors == null)
                {
                    result.Errors = new Dictionary<string, string[]>();

                    string[] errorArray = { "There has been some networking error, please check connection and try again." };

                    result.Errors.Add("Error", errorArray );
                }
                result.IsSuccessfulRegistration = false;
            }

            result.IsSuccessfulRegistration = true;
            return result;
        }


        public async Task<Comment> PostComment(PostedComment comment)
        {
            var serializedComment = JsonSerializer.Serialize(comment);
            var bodyContent = new StringContent(serializedComment, Encoding.UTF8, "application/json");

            var postResult = await http.PostAsync("comment", bodyContent);

            var authContent = await postResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Comment>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (postResult.IsSuccessStatusCode)
            {
                result.IsSuccesfullCommentPost = true;
            }

            return result;
        }

        public async Task<User> GetUser(string UserName)
        {
            return await http.GetFromJsonAsync<User>($"Users/{UserName}");
        }
    }
}
