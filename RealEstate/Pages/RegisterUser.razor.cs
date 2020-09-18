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
        public async Task Register()
        {
            ShowRegistrationErros = false;
            var result = await AuthenticationService.RegisterUser(_userForRegistrationDto);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                NavigationManager.NavigateTo("/Login");
            }
        }

        public void InValidRegister()
        {
            _userForRegistrationDto.ConfirmPassword = "";
            _userForRegistrationDto.Password = "";
        }

        public void ClearForm()
        {
            _userForRegistrationDto = new UserForRegistrationDto();
        }
    }
}
