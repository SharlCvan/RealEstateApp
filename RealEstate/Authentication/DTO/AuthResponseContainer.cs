using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    /// <summary>
    /// Container used to hold information when a response is received from the api. The answer from api is given in a values array.
    /// </summary>
    public class AuthResponseContainer
    {
        public AuthResponseDto Value { get; set; }
        public bool Succeeded { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
