using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    /// <summary>
    /// Container used to hold information when a response is received from the api
    /// </summary>
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string TokenType { get; set; }
        public string AcessToken { get; set; }
        public string UserName { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
        public string Error { get; set; }
    }
}
