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
            ShowHouse = RealEstateType.House,
            ShowApartment = RealEstateType.Apartment,
            ShowStorageUnit = RealEstateType.Warehouse,
            ShowOffice = RealEstateType.Office
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
                SearchValues.ShowHouse = RealEstateType.House;
            }

            else
            {
                SearchValues.ShowHouse = RealEstateType.Nothing;
            }
        }

        public void Apartment(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowApartment = RealEstateType.Apartment;
            }

            else
            {
                SearchValues.ShowApartment = RealEstateType.Nothing;
            }
        }

        public void WareHouse(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowStorageUnit = RealEstateType.Warehouse;
            }

            else
            {
                SearchValues.ShowStorageUnit = RealEstateType.Nothing;
            }
        }

        public void Office(ChangeEventArgs e)
        {
            if ((bool)e.Value)
            {
                SearchValues.ShowOffice = RealEstateType.Office;
            }

            else
            {
                SearchValues.ShowOffice = RealEstateType.Nothing;
            }
        }
    }
}
