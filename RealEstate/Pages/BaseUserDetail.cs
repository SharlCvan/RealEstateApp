using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseUserDetail : ComponentBase
    {
        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        public User User { get; set; } = new User();

        public PostedRating RatingResult { get; set; } = new PostedRating();

        public List<Comment> UserComments { get; set; } = new List<Comment>();

        public PostedRating postedRating { get; set; } = new PostedRating();

        [Parameter]
        public string UserName { get; set; }

        protected async override Task OnInitializedAsync()
        {
            User = await RealEstateService.GetUser(UserName);
            UserComments = await RealEstateService.GetUserComments(UserName);
        }

        public async Task RateUser(int rating)
        {
            postedRating.UserName = UserName;
            postedRating.Value = rating;
            RatingResult = await RealEstateService.RateUser(postedRating);
            //Todo: Add confirmation on rating succes or failure
        }
    }
}
