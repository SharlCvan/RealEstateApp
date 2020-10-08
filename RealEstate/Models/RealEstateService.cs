using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Converters;

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

            var property = new Propertys();

            try
            {
                string jsonString;

                var task = await http.GetAsync($"api/RealEstates/{id}");
                jsonString = await task.Content.ReadAsStringAsync();
                property = JsonSerializer.Deserialize<Propertys>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch(Exception)
            {
                property.Errors.Add("Offine", new string[1] { "Not conneted to network" });
            }

            return property;
        }

        public async Task<(IEnumerable<Propertys> realEstates, bool error)> GetRealEstates(int page, int quantityPerPage, string searchTerm)
        {
            HttpResponseMessage task;
            List<Propertys> realEstates = new List<Propertys>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    task = await http.GetAsync($"api/RealEstates?skip={(page - 1) * quantityPerPage}&take={quantityPerPage}");
                    string jsonString = await task.Content.ReadAsStringAsync();
                    realEstates = JsonSerializer.Deserialize<List<Propertys>>(jsonString);

                    return (realEstates, false);
                }
                catch
                {
                    return (realEstates, true);
                }
            }

            else
            {
                try
                {
                    task = await http.GetAsync($"api/RealEstates?skip={(page - 1) * quantityPerPage}&take={quantityPerPage}&city={searchTerm}");
                    string jsonString = await task.Content.ReadAsStringAsync();
                    realEstates = JsonSerializer.Deserialize<List<Propertys>>(jsonString);

                    return (realEstates, false);
                }
                catch
                {
                    return (realEstates, true);
                }
            }
        }

        public async Task<int> GetTotalPages()
        {
            int pageCount;

            try
            {
                HttpResponseMessage task = await http.GetAsync("api/RealEstates/count");
                string jsonString = await task.Content.ReadAsStringAsync();

                pageCount = int.Parse(jsonString);
            }
            catch (Exception)
            {
                pageCount = -1;
            }

            return pageCount;
        }

        public async Task<int> GetTotalPagesSearch(string searchTerm)
        {
            int pageCount;

            try
            {
                HttpResponseMessage task = await http.GetAsync($"api/RealEstates/count?city={searchTerm}");
                string jsonString = await task.Content.ReadAsStringAsync();

                pageCount = int.Parse(jsonString);
            }
            catch (Exception)
            {
                pageCount = -1;
            }

            return pageCount;
        }

        public async Task<PropertysForRegistration> PostANewRealEstate(PropertysForRegistration newRealEstateToRegister)
        {

            var serializedRealEstate = JsonSerializer.Serialize(newRealEstateToRegister);
            var bodyContent = new StringContent(serializedRealEstate, Encoding.UTF8, "application/json");

            var result = new PropertysForRegistration();

            try 
            {
                var postResult = await http.PostAsync("api/RealEstates", bodyContent);
                var authContent = await postResult.Content.ReadAsStringAsync();
                result = System.Text.Json.JsonSerializer.Deserialize<PropertysForRegistration>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!postResult.IsSuccessStatusCode)
                {
                    result.IsSuccessfulRegistration = false;
                }

                result.IsSuccessfulRegistration = true;
            }
            catch 
            {
                //Adds a error message if there is some undefined error that has happened
                result.Errors = new Dictionary<string, string[]>();

                string[] errorArray = { "There has been some networking error, please check connection and try again." };

                result.Errors.Add("Error", errorArray);

                result.IsSuccessfulRegistration = false;
            }
            
            
            return result;
        }


        public async Task<Comment> PostComment(PostedComment comment)
        {
            var serializedComment = System.Text.Json.JsonSerializer.Serialize(comment);
            var bodyContent = new StringContent(serializedComment, Encoding.UTF8, "application/json");
            var result = new Comment();

            try
            {
                var postResult = await http.PostAsync("api/comments", bodyContent);
                var authContent = await postResult.Content.ReadAsStringAsync();
                result = System.Text.Json.JsonSerializer.Deserialize<Comment>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (postResult.IsSuccessStatusCode)
                {
                    result.IsSuccesfullCommentPost = true;
                }
            }
            catch(Exception)
            {
                result.errors = new Dictionary<string, string[]>();
                result.errors.Add("Offline", new string[1] { "Your are offline"});
            }

            return result;
        }

        public async Task<User> GetUser(string UserName)
        {
            var user = new User();
            try
            {
                user = await http.GetFromJsonAsync<User>($"api/Users/{UserName}");
            }
            catch(Exception)
            {
                user.Errors.Add("Offline", new string[1] { "You are offline "});
            }

            return user;
        }

        public async Task<PostedRating> RateUser(PostedRating postedRating)
        {
            var serializedComment = System.Text.Json.JsonSerializer.Serialize(postedRating);
            var bodyContent = new StringContent(serializedComment, Encoding.UTF8, "application/json");
            var result = new PostedRating();

            try
            {
                var putResult = await http.PutAsync("api/Users/rate", bodyContent);
                result.PostRatingSuccess = true;
                result.Value = postedRating.Value;
            }
            catch
            {
                result.PostRatingSuccess = false;
                result.Value = -1;
            }

            return result;

        }

        public async Task<List<Comment>> GetUserComments(string UserName, int page, int quantityPerPage)
        {
            int skip = (page - 1) * quantityPerPage;
            var comments = new List<Comment>();

            try
            {
                var httpResponse = await http.GetAsync($"api/Comments/byuser/{UserName}?skip={skip}&take={quantityPerPage}");

                var responseString = await httpResponse.Content.ReadAsStringAsync();
                comments = JsonSerializer.Deserialize<List<Comment>>(responseString);
            }
            catch(Exception)
            {                
                
            }

            return comments;

        }

        public async Task<int> GetTotalUserComments(string userName)
        {
            int pageCount;
            try
            {
                HttpResponseMessage task = await http.GetAsync($"api/comments/byuser/{userName}/count");
                string jsonString = await task.Content.ReadAsStringAsync();
                pageCount = int.Parse(jsonString);
            }
            catch
            {
                pageCount = 0;
            }

            return pageCount;
        }

        public async Task<List<Comment>> GetRealEstateComments(string RealEstateId, int page, int quantityPerPage)
        {
            int skip = (page - 1) * quantityPerPage;
            var comments = new List<Comment>();

            try
            {
                var httpResponse = await http.GetAsync($"api/Comments/{RealEstateId}?skip={skip}&take={quantityPerPage}");
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                comments = JsonSerializer.Deserialize<List<Comment>>(responseString);
            }
            catch
            {

            }

            return comments;
        }

        public async Task<int> GetTotalRealEstateComments(int id)
        {
            int commentCount;

            try
            {
                HttpResponseMessage task = await http.GetAsync($"api/comments/{id}/count");
                string jsonString = await task.Content.ReadAsStringAsync();
                commentCount = int.Parse(jsonString);
            }
            catch
            {
                commentCount = 0;
            }

            return commentCount;
        }
    }
}


