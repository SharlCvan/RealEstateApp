using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
            return await http.GetJsonAsync<Propertys[]>("RealEstates");
        }

        public async Task<Propertys> PostANewRealEstate(Propertys newRealEstate)
        {
            var serializedRealEstate = JsonSerializer.Serialize(newRealEstate);
            var bodyContent = new StringContent(serializedRealEstate, Encoding.UTF8, "application/json");

            var postResult = await http.PostAsync("api/RealEstates", bodyContent);

            var authContent = await postResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Propertys>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!postResult.IsSuccessStatusCode)
            {
                return result;
            }

            return result;
        }
    }
}
