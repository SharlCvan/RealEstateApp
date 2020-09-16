using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstate : ComponentBase
    {
        [Inject]
        public IRealEstateService RealEstateServices { get; set; }

        public List<Propertys> realEstates { get; set; } = new List<Propertys>();

        protected override async Task OnInitializedAsync()
        {
            realEstates = (await RealEstateServices.GetRealEstates()).ToList();
        }
    }
}
