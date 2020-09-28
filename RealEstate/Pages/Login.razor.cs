using Microsoft.AspNetCore.Components;
using RealEstate.Authentication.DTO;
using RealEstate.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace RealEstate.Pages
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IRealEstateService RealEstateService { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await RealEstateService.UserLoggedInAndValid())
            {
                NavigationManager.NavigateTo("/");
            }
        }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            _userForAuthentication.GrantType = "password";
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.Error;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
