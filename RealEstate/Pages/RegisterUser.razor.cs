using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RealEstate.Authentication;
using RealEstate.Authentication.DTO;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public partial class RegisterUser
    {
        private UserForRegistrationDto _userForRegistrationDto = new UserForRegistrationDto();
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        //Holds any information about a request that has rendered an error
        public bool ShowRegistrationErros { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        //Holds a registration message which is displayed to a user after sucessfull login. Ex. a logged in user registers another new user.
        public bool ShowRegistrationMessage { get; set; }
        public string registrationMessage { get; set; }

        /// <summary>
        /// Send  a request to the repository to register a new user. And displays any errors the api sends back.
        /// </summary>
        /// <returns></returns>
        public async Task Register()
        {
            ShowRegistrationErros = false;
            ShowRegistrationMessage = false;


            var result = await AuthenticationService.RegisterUser(_userForRegistrationDto);
            if (!result.Succeeded)
            {
                //Handles the event of a non sucessfull post to the api
                foreach (var error in result.Errors)
                {
                    Errors.Add(error.Value[0]);
                }

                ShowRegistrationErros = true;
            }
            else
            {
                var userLoggedIn = await RealEstateService.UserLoggedInAndValid();
                
                //If the user is logged in a message is created that the new user has sucessfully been created and redirects to Registration page. If user wants to register another user.
                if(userLoggedIn)
                {
                    ShowRegistrationMessage = true;
                    registrationMessage = $"User \"{_userForRegistrationDto.UserName}\"has been sucessfully registered.";

                    ClearForm();
                }
                else 
                {
                    NavigationManager.NavigateTo("/Login");
                }

                
            }   
        }

        /// <summary>
        /// Removes the entered password if the user has entered a faulty one.
        /// </summary>
        public void InValidRegister()
        {
            _userForRegistrationDto.ConfirmPassword = "";
            _userForRegistrationDto.Password = "";
        }

        /// <summary>
        /// Clears the form if user whishes to start over.
        /// </summary>
        public void ClearForm()
        {
            _userForRegistrationDto = new UserForRegistrationDto();
        }
    }
}
