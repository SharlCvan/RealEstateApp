using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    /// <summary>
    /// Container used to hold information when a register request is sent to the api
    /// </summary>
    public class UserForRegistrationDto
    {
        [MaxLength(30, ErrorMessage = "Username should contain a maximun of 30 characters.")]
        [MinLength(3, ErrorMessage = "Username should contain at least 3 characters.")]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", ErrorMessage = "Password should contain at least one digit, one lowercase and a uppercase letter.")]
        [MinLength(6, ErrorMessage = "Password should contain at least 6 characters.")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
