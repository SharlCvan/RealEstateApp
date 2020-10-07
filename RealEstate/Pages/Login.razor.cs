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

        [Parameter]
        public EventCallback<bool> OnLogin { get; set; }

        [Parameter]
        public string Success { get; set; }

        //Holds a registration message which is displayed to a user after sucessfull login. Ex. a logged in user registers another new user.
        public bool ShowRegistrationMessage { get; set; }
        public string registrationMessage { get; set; }

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
            ShowRegistrationMessage = false;
            bool userLoggedIn = await RealEstateService.UserLoggedInAndValid();

            if (userLoggedIn)
            {
                NavigationManager.NavigateTo("/");
            }

            if (!String.IsNullOrEmpty(Success))
            {
                registrationMessage = Success;
                ShowRegistrationMessage = true;
            }
        }

        /// <summary>
        /// Sends the login request to the repository and displays any login errors.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteLogin()
        {
            Errors = new List<string>();

            ShowAuthError = false;
            _userForAuthentication.GrantType = "password";
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.Succeeded)
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
                await OnLogin.InvokeAsync(true);
                NavigationManager.NavigateTo("");
            }
        }
    }
}
