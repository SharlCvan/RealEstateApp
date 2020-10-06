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

        public int TotalPagesQuantity { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<Propertys> RealEstates { get; set; } = new List<Propertys>();
        public List<Propertys> SearchResults { get; set; } = new List<Propertys>();

        protected override async Task OnInitializedAsync()
        {
            //TotalPagesQuantity = await RealEstateServices.GetTotalPages();
            TotalPagesQuantity = 2;
            await LoadRealEstates();
        }

        private async Task LoadRealEstates(int page = 1, int quantityPerPage = 5)
        {
            RealEstates = (await RealEstateServices.GetRealEstates(page, quantityPerPage)).ToList();
        }

        public async Task SelectedPage(int page)
        {
            Console.WriteLine(page);
            CurrentPage = page;
            await LoadRealEstates(page);
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
                                                    x.RealEstateType == (int)searchValues.ShowHouse ||
                                                    x.RealEstateType == (int)searchValues.ShowApartment ||
                                                    x.RealEstateType == (int)searchValues.ShowStorageUnit ||
                                                    x.RealEstateType == (int)searchValues.ShowOffice)
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
