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
            HttpResponseMessage task = await http.GetAsync($"api/RealEstates/{id}");
            string jsonString = await task.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<Propertys>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Propertys>> GetRealEstates(int page, int quantityPerPage)
        {
            HttpResponseMessage task = await http.GetAsync($"api/RealEstates?skip={(page - 1) * quantityPerPage}&take={quantityPerPage}");
            string jsonString = await task.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Propertys>>(jsonString);

        }

        public async Task<int> GetTotalPages()
        {
            HttpResponseMessage task = await http.GetAsync("api/RealEstates/count");
            string jsonString = await task.Content.ReadAsStringAsync();

            int pageCount = int.Parse(jsonString);

            return pageCount;
        }

        public async Task<PropertysForRegistration> PostANewRealEstate(PropertysForRegistration newRealEstateToRegister)
        {

            var serializedRealEstate = JsonConvert.SerializeObject(newRealEstateToRegister);
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

            var postResult = await http.PostAsync("api/comments", bodyContent);

            var authContent = await postResult.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<Comment>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (postResult.IsSuccessStatusCode)
            {
                result.IsSuccesfullCommentPost = true;
            }

            return result;
        }

        public async Task<User> GetUser(string UserName)
        {
            return await http.GetFromJsonAsync<User>($"api/Users/{UserName}");
        }

        public async Task<PostedRating> RateUser(PostedRating postedRating)
        {
            var serializedComment = System.Text.Json.JsonSerializer.Serialize(postedRating);
            var bodyContent = new StringContent(serializedComment, Encoding.UTF8, "application/json");

            var putResult = await http.PutAsync("api/Users/rate", bodyContent);

            //var authContent = await putResult.Content.ReadAsStringAsync();
            //var result = System.Text.Json.JsonSerializer.Deserialize<PostedRating>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = new PostedRating();

            if (putResult.IsSuccessStatusCode)
            {
                result.PostRatingSuccess = true;
                result.Value = postedRating.Value;
            }
            else
            {
                result.Value = -1;
            }

            return result;

        }

        public async Task<CommentsPaging> GetUserComments(string UserName, int page, int quantityPerPage)
        {
            int skip = (page - 1) * quantityPerPage;
            var httpResponse = await http.GetAsync($"api/Comments/byuser/{UserName}?skip={skip}&take={quantityPerPage}");
            var commentsPaging = new CommentsPaging();

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                commentsPaging.Comments = JsonSerializer.Deserialize<List<Comment>>(responseString);
            }

            return commentsPaging;

        }

        public async Task<int> GetTotalUserComments(string userName)
        {
            HttpResponseMessage task = await http.GetAsync($"api/comments/byuser/{userName}/count");
            string jsonString = await task.Content.ReadAsStringAsync();

            int pageCount = int.Parse(jsonString);

            return pageCount;
        }

        public async Task<List<Comment>> GetRealEstateComments(string RealEstateId, int page, int quantityPerPage)
        {
            int skip = (page - 1) * quantityPerPage;
            var httpResponse = await http.GetAsync($"api/Comments/{RealEstateId}?skip={skip}&take={quantityPerPage}");
            var comments = new List<Comment>();

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                comments = JsonSerializer.Deserialize<List<Comment>>(responseString);
            }

            return comments;
        }

        public async Task<int> GetTotalRealEstateComments(int id)
        {
            HttpResponseMessage task = await http.GetAsync($"api/comments/{id}/count");
            string jsonString = await task.Content.ReadAsStringAsync();

            int commentCount = int.Parse(jsonString);

            return commentCount;
        }
    }
}


