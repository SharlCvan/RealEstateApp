using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstateCard : ComponentBase
    {
        [Parameter]
        public Propertys RealEstate { get; set; }

    }
}
