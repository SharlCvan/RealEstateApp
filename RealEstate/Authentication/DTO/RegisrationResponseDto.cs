using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    /// <summary>
    /// Container used to hold information when a response is received from the api
    /// </summary>
    public class RegisrationResponseDto
    {
        public bool succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
