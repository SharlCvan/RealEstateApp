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

        public string BuyersOptions(Propertys estate)
        {
            foreach(var estates in realEstates)
            {

            }
            if (estate.CanBeSold == true && estate.CanBeRented == true)
            {
                return "for sale, for rent";
            }

            else if (estate.CanBeSold == true && estate.CanBeRented == false)
            {
                return "for sale";
            }

            else if (estate.CanBeSold == false && estate.CanBeRented == true)
            {
                return "for rent";
            }

            else
            {
                return "not for sale, not for rent";
            }
        }
    }
}
