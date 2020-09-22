using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseCreateRealEstateAd : ComponentBase
    {
        public Propertys Property = new Propertys();

        [Inject]
        public IRealEstateService RealEstateServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public async Task Register()
        {
            ShowRegistrationErros = false;
            var result = await RealEstateServices.PostANewRealEstate(Property);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                //Redirect to real estate details page?
                NavigationManager.NavigateTo("/Login");
            }
        }

        public void InValidRegister()
        {

        }
    }
}
