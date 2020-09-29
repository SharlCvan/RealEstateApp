using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public partial class Search
    {
        public SearchValues SearchValues { get; set; } = new SearchValues
        {
            ShowSale = true,
            ShowRent = true,
            ShowHouse = RealEstateTypes.House,
            ShowApartment = RealEstateTypes.Apartment,
            ShowStorageUnit = RealEstateTypes.Warehouse,
            ShowOffice = RealEstateTypes.Office
        };

        [Parameter]
        public EventCallback<SearchValues> OnSearch {get; set;}

        public async void HandleSearch()
        {
            await OnSearch.InvokeAsync(SearchValues);
        }

        public void House(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowHouse = RealEstateTypes.House;
            }

            else
            {
                SearchValues.ShowHouse = RealEstateTypes.Nothing;
            }
        }

        public void Apartment(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowApartment = RealEstateTypes.Apartment;
            }

            else
            {
                SearchValues.ShowApartment = RealEstateTypes.Nothing;
            }
        }

        public void WareHouse(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowStorageUnit = RealEstateTypes.Warehouse;
            }

            else
            {
                SearchValues.ShowStorageUnit = RealEstateTypes.Nothing;
            }
        }

        public void Office(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowOffice = RealEstateTypes.Office;
            }

            else
            {
                SearchValues.ShowOffice = RealEstateTypes.Nothing;
            }
        }
    }
}
