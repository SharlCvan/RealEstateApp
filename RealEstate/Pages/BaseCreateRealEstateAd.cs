using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseCreateRealEstateAd : ComponentBase
    {
        public PropertysForRegistration PropertyForRegistration = new PropertysForRegistration();

        [Inject]
        public IRealEstateService RealEstateServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string SaleOrRent { get; set; }

        public List<string> ImageURL { get; set; } = new List<string>();

        public URLInput URLInput = new URLInput();


        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        protected async override Task OnInitializedAsync()
        {
            
        }

        public async Task Register()
        {


            ShowRegistrationErros = false;


            //TO:DO konvertera till en propertys modell och skicka iväg. Ta bara emot felkoder.

            var Property = ConvertToProperty(PropertyForRegistration);

            var result = await RealEstateServices.PostANewRealEstate(Property);

            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                //DO:DO Post imagaes to API and link with newly created listing
                //TO:DO Redirect to real estate details page?
                NavigationManager.NavigateTo("/Login");
            }
        }

        private Propertys ConvertToProperty(PropertysForRegistration propertyForRegistration)
        {
            Propertys convertedProperty = new Propertys();

            convertedProperty.Title = propertyForRegistration.Title;
            convertedProperty.Description = propertyForRegistration.Description;
            convertedProperty.Contact = propertyForRegistration.Contact;
            convertedProperty.ConstructionYear = propertyForRegistration.ConstructionYear;
            convertedProperty.RealEstateType = (int)propertyForRegistration.RealEstateType;
            convertedProperty.Address = propertyForRegistration.Address;

            convertedProperty.ImageUrl = ImageURL;

            if (propertyForRegistration.CanBeSold)
            {
                convertedProperty.CanBeSold = true;
                convertedProperty.CanBeRented = false;
                convertedProperty.SellingPrice = propertyForRegistration.RentSalePrice;
                convertedProperty.RentingPrice = 0;
            }
            else
            {
                convertedProperty.CanBeSold = false;
                convertedProperty.CanBeRented = true;
                convertedProperty.RentingPrice = propertyForRegistration.RentSalePrice;
                convertedProperty.SellingPrice = 0;
            }

            return convertedProperty;
        }

        public void InValidRegister()
        {

        }

        public void ClearForm()
        {
            PropertyForRegistration = new PropertysForRegistration();
        }

        public void ForSaleOrRent(ChangeEventArgs args)
        {
            var result = args.Value.ToString();

            if (result == "Rent")
            {
                PropertyForRegistration.CanBeRented = true;
                PropertyForRegistration.CanBeSold = false;
                SaleOrRent = "Rent";
            }
            else if (result == "Sale")
            {
                PropertyForRegistration.CanBeRented = false;
                PropertyForRegistration.CanBeSold = true;
                SaleOrRent = "Sale";
            }

            StateHasChanged();
        }

        public void AddListItem()
        {
            if (URLInput.Input != "")
            {
                ImageURL.Add(URLInput.Input);
                URLInput.Input = "";
            }

            StateHasChanged();
        }

        public void RemoveListItem(string url)
        {
            ImageURL.Remove(url);
            StateHasChanged();
        }
    }
}
