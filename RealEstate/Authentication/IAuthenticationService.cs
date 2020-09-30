using RealEstate.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseContainer> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
