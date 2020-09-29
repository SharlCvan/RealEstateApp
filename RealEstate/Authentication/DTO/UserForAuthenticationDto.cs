﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    /// <summary>
    /// Container used to hold information when a login/authentication request is sent to the api
    /// </summary>
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string GrantType { get; set; }
    }
}
