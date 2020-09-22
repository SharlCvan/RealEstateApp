﻿using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstateDetail : ComponentBase
    {

        public Propertys RealEstate { get; set; } = new Propertys();

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));
        }

        //protected List<Comment> LoadComments()
        //{
        //    List<Comment> comments = await RealEstateService.GetComments(int.Parse(Id));
        //}


    }
}
