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

        public List<Propertys> RealEstates { get; set; } = new List<Propertys>();
        public List<Propertys> SearchResults { get; set; } = new List<Propertys>();

        protected override async Task OnInitializedAsync()
        {
            RealEstates = (await RealEstateServices.GetRealEstates()).ToList();
            SearchResults = RealEstates;
        }

        public void SearchEstates(SearchValues searchValues)
        {
            if (!String.IsNullOrEmpty(searchValues.SearchTerm))
            {
                SearchResults = RealEstates.Where(x =>
                                                  x.Address.Contains(searchValues.SearchTerm))
                                                  .ToList();

                SearchBySellAndRent(searchValues.ShowSale, searchValues.ShowRent);
            }

            else
            {
                SearchBySellAndRent(searchValues.ShowSale, searchValues.ShowRent);
            }

            SearchResults = SearchResults.Where(x =>
                                                    x.RealEstateType == searchValues.ShowHouse ||
                                                    x.RealEstateType == searchValues.ShowApartment ||
                                                    x.RealEstateType == searchValues.ShowStorageUnit ||
                                                    x.RealEstateType == searchValues.ShowOffice)
                                                    .ToList();
        }

        private void SearchBySellAndRent(bool showSale, bool showRent)
        {
            if (showSale && showRent)
            {
                SearchResults = RealEstates.Where(x =>
                                                  x.CanBeSold == showSale ||
                                                  x.CanBeRented == showRent)
                                                  .ToList();
            }

            else
            {
                SearchResults = RealEstates.Where(x =>
                                                  x.CanBeSold == showSale &&
                                                  x.CanBeRented == showRent)
                                                  .ToList();
            }
        }
    }
}
