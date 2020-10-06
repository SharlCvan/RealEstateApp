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
        /// <summary>
        /// Indicates if there are any errors from API when logging in.
        /// </summary>
        public bool ShowAuthError { get; set; }
        /// <summary>
        /// Holds information about what errors the API sends back.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            //If the user is already logged in and has a valid user session
            if (await RealEstateService.UserLoggedInAndValid())
            {
                NavigationManager.NavigateTo("/");
            }
        }

        /// <summary>
        /// Sends the login request to the repository and displays any login errors.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteLogin()
        {
            
            ShowAuthError = false;
            _userForAuthentication.GrantType = "password";
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.Value.IsAuthSuccessful)
            {
                //Handles the event of a non sucessfull post to the api
                foreach (var error in result.Errors)
                {
                    Errors.Add(error.Value[0]);
                }

                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
