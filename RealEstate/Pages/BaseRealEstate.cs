using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstate : ComponentBase
    {
        [Inject]
        public IRealEstateService RealEstateServices { get; set; }

        public List<Propertys> realEstates { get; set; } = new List<Propertys>();
        public List<Propertys> SearchResults { get; set; } = new List<Propertys>();
        public bool ShowSale { get; set; }
        public bool ShowRent { get; set; }
        public bool ShowHouse { get; set; }
        public bool ShowApartment { get; set; }
        public bool ShowStorageUnit { get; set; }
        public bool ShowOffice { get; set; }
        public string SearchTerm { get; set; }

        protected override async Task OnInitializedAsync()
        {
            realEstates = (await RealEstateServices.GetRealEstates()).ToList();
            SearchResults = realEstates;
            ShowSale = true;
            ShowRent = true;
            ShowHouse = true;
            ShowApartment = true;
            ShowStorageUnit = true;
            ShowOffice = true;
        }

        public string BuyersOptions(Propertys estate)
        {
            if (estate.CanBeSold)
            {
                return "for sale";
            }

            else
            {
                return "for rent";
            }
        }

        public async Task SearchEstates()
        {
            if (!String.IsNullOrEmpty(SearchTerm))
            {
                SearchResults = realEstates.Where(x => x.Address.Contains(SearchTerm) && x.CanBeSold == ShowSale).ToList();
            }

            else
            {
                SearchResults = realEstates.Where(x => x.CanBeSold == ShowSale && x.CanBeRented == ShowRent).ToList();
            }
        }
    }
}
