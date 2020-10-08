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
        public int QuantityPerPage { get; set; } = 5;
        public List<Propertys> RealEstates { get; set; } = new List<Propertys>();

        protected override async Task OnInitializedAsync()
        {
            TotalPagesQuantity = (int)Math.Ceiling((decimal)await RealEstateServices.GetTotalPages() / QuantityPerPage);
            await LoadRealEstates();
        }

        private async Task LoadRealEstates(int page = 1)
        {
            RealEstates = (await RealEstateServices.GetRealEstates(page, QuantityPerPage)).ToList();
            foreach(var estate in RealEstates)
            {
                Console.WriteLine(estate.Description.Length);
            }
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await LoadRealEstates(page);
        }

        public void SearchEstates(string searchTerm)
        {
            
        }
    }
}
