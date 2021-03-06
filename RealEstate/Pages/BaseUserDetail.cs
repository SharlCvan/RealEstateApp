﻿using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public CommentsPaging commentsPaging { get; set; } = new CommentsPaging();

        public PostedRating postedRating { get; set; } = new PostedRating();

        [Parameter]
        public string UserName { get; set; }

        public int QuantityPerPage { get; set; } = 4;

        public int totalpages;

        public int currentPage = 1;

        protected async override Task OnInitializedAsync()
        {
            //User = await RealEstateService.GetUser(UserName);
            //totalpages = (int)Math.Ceiling((decimal)await RealEstateService.GetTotalUserComments(UserName) / QuantityPerPage);
            //await LoadComments();
            await LoadUser();
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadUser();
        }

        public async Task LoadUser()
        {
            User.Errors = new Dictionary<string, string[]>();
            User = await RealEstateService.GetUser(UserName);
            totalpages = (int)Math.Ceiling((decimal)await RealEstateService.GetTotalUserComments(UserName) / QuantityPerPage);
            await LoadComments();
        }

        public async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadComments(page);
        }

        public async Task LoadComments(int page = 1, int quantityPerPage = 4)
        {
            UserComments = await RealEstateService.GetUserComments(UserName, page, quantityPerPage); 
        }



        public async Task RateUser(int rating)
        {
            postedRating.UserName = UserName;
            postedRating.Value = rating;
            RatingResult = await RealEstateService.RateUser(postedRating);
        }
    }
}


//____Gjort___
//Visar ej comment section om den är tom
//Fyller error dictionary vid offline läget samt andra errors
//Visar upp error fel på userdetail och RealEstate detail och postComment 