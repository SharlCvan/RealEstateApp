using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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

        public async Task<Propertys> PostANewRealEstate(Propertys newRealEstate)
        {

            var serializedRealEstate = JsonSerializer.Serialize(newRealEstate);
            var bodyContent = new StringContent(serializedRealEstate, Encoding.UTF8, "application/json");

            var postResult = await http.PostAsync("api/RealEstates", bodyContent);

            var authContent = await postResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Propertys>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!postResult.IsSuccessStatusCode)
            {
                //Adds a error message if there is some undefined error has happened
                result.IsSuccessfulRegistration = false;
                if (result.Errors.Count < 1)
                {
                    result.Errors.Add("There has been some networking error, please check connection and try again.");
                }
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

        public async Task<PostedRating> RateUser(PostedRating postedRating)
        {
            var serializedComment = JsonSerializer.Serialize(postedRating);
            var bodyContent = new StringContent(serializedComment, Encoding.UTF8, "application/json");

            var putResult = await http.PutAsync("Users/rate", bodyContent);

            var authContent = await putResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PostedRating>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (putResult.IsSuccessStatusCode)
            {
                result.PostRatingSuccess = true;
            }

            return result;
        }

        public async Task<CommentsPaging> GetUserComments(string UserName, int page, int quantityPerPage)
        {
             int skip = (page - 1) * quantityPerPage;
             var httpResponse = await http.GetAsync($"Comments/byuser/{UserName}?skip={skip}&take={quantityPerPage}");

            if(httpResponse.IsSuccessStatusCode)
            {
                var commentsPaging = new CommentsPaging();

                //commentsPaging.TotalPages = int.Parse(httpResponse.Headers.GetValues("pagesQuantity").FirstOrDefault());
                commentsPaging.TotalPages = 2;

                var responseString = await httpResponse.Content.ReadAsStringAsync();

                commentsPaging.Comments = JsonSerializer.Deserialize<List<Comment>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );

                return commentsPaging;
            }
            else
            {
                //Error
                return null;
            }
        }

        public async Task<CommentsPaging> GetRealEstateComments(string RealEstateId, int page, int quantityPerPage)
        {
            int skip = (page - 1) * quantityPerPage;
            var httpResponse = await http.GetAsync($"Comments/{RealEstateId}?skip={skip}&take={quantityPerPage}");

            if (httpResponse.IsSuccessStatusCode)
            {
                var commentsPaging = new CommentsPaging();

                //commentsPaging.TotalPages = int.Parse(httpResponse.Headers.GetValues("pagesQuantity").FirstOrDefault());
                commentsPaging.TotalPages = 2;

                var responseString = await httpResponse.Content.ReadAsStringAsync();

                commentsPaging.Comments = JsonSerializer.Deserialize<List<Comment>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return commentsPaging;
            }
            else
            {
                //Error
                return null;
            }
        }
    }
}
