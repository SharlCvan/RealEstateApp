﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Authentication.DTO
{
    public class RegisrationResponseDto
    {
        public bool succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
