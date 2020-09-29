using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RealEstate.Authentication;
using RealEstate.Authentication.DTO;
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

        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Send  a request to the repository to register a new user. And displays any errors the api sends back.
        /// </summary>
        /// <returns></returns>
        public async Task Register()
        {
            ShowRegistrationErros = false;
            var result = await AuthenticationService.RegisterUser(_userForRegistrationDto);
            if (!result.succeeded)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                //TO:DO Message about sucessful registration?
                NavigationManager.NavigateTo("/Login");
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
