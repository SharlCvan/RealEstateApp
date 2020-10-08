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

        public string SearchTerm { get; set; }
        public int TotalPagesQuantity { get; set; }
        public int TotalPagesAllRealEstates { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int QuantityPerPage { get; set; } = 5;
        public bool Error { get; set; }
        public bool EstatesFound { get; set; } = true;
        public List<Propertys> RealEstates { get; set; } = new List<Propertys>();

        protected override async Task OnInitializedAsync()
        {
            TotalPagesAllRealEstates = (int)Math.Ceiling((decimal)await RealEstateServices.GetTotalPages() / QuantityPerPage);
            TotalPagesQuantity = TotalPagesAllRealEstates;
            await LoadRealEstates();
        }

        public async Task LoadRealEstates(int page = 1)
        {
            if (!String.IsNullOrEmpty(SearchTerm))
            {
                TotalPagesQuantity = (int)Math.Ceiling((decimal)await RealEstateServices.GetTotalPagesSearch(SearchTerm) / QuantityPerPage);
            }
            else
            {
                TotalPagesQuantity = TotalPagesAllRealEstates;
            }
            var result = (await RealEstateServices.GetRealEstates(page, QuantityPerPage, SearchTerm));

            if (result.error)
            {
                Error = result.error;
            }

            else
            {
                Error = result.error;
                RealEstates = result.realEstates.ToList();
                if (RealEstates.Count == 0)
                    EstatesFound = false;
                else
                    EstatesFound = true;
            }
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadRealEstates(page);
        }

        public async void SearchEstates(string searchTerm)
        {
            SearchTerm = searchTerm;
            CurrentPage = 1;
            await LoadRealEstates();
        }

        public async void TryAgain()
        {
            TotalPagesAllRealEstates = (int)Math.Ceiling((decimal)await RealEstateServices.GetTotalPages() / QuantityPerPage);
            await LoadRealEstates(CurrentPage);
        }
    }
}
