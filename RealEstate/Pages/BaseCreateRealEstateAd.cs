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
        /// <summary>
        /// Contains the image URL's that a user wants to show together with the ad.
        /// </summary>
        public List<string> ImageURL { get; set; } = new List<string>();

        /// <summary>
        /// Holds info about a single URL input. Needed to ease the use of validation input.
        /// </summary>
        public URLInput URLInput = new URLInput();

        /// <summary>
        /// Indicates if the request to the API is sucessfull or not.
        /// </summary>
        public bool ShowRegistrationErros { get; set; }
        /// <summary>
        /// Contains the errors recíeved from the API.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Handles the data 
        /// </summary>
        /// <returns></returns>
        public async Task Register()
        {
            ShowRegistrationErros = false;

            var Property = ConvertToProperty(PropertyForRegistration);

            //Forwards the data to repository to post to the API
            var result = await RealEstateServices.PostANewRealEstate(Property);

            if (!result.IsSuccessfulRegistration)
            {
                //Handles the event of a non sucessfull post to the api
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                //TO:DO Redirect to real estate details page?
                NavigationManager.NavigateTo("/Login");
            }
        }

        /// <summary>
        /// Converts from the PropertysForRegistration to regular Propertys model. 
        /// </summary>
        /// <param name="propertyForRegistration"></param>
        /// <returns></returns>
        private Propertys ConvertToProperty(PropertysForRegistration propertyForRegistration)
        {
            Propertys convertedProperty = new Propertys();

            convertedProperty.Title = propertyForRegistration.Title;
            convertedProperty.Description = propertyForRegistration.Description;
            convertedProperty.Contact = propertyForRegistration.Contact;
            convertedProperty.ConstructionYear = propertyForRegistration.ConstructionYear;
            convertedProperty.RealEstateType = (int)propertyForRegistration.RealEstateType;
            convertedProperty.Address = propertyForRegistration.Address;

            convertedProperty.Rooms = propertyForRegistration.Rooms;
            convertedProperty.SquareMeters = propertyForRegistration.SquareMeters;
            convertedProperty.City = propertyForRegistration.City;
            convertedProperty.ListingURL = propertyForRegistration.ListingURL;
            convertedProperty.Urls = ImageURL;

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

        /// <summary>
        /// Clears the form if a user wants to start over
        /// </summary>
        public void ClearForm()
        {
            PropertyForRegistration = new PropertysForRegistration();
        }

        /// <summary>
        /// Handles the input for radio buttons and refereshes the UI
        /// </summary>
        /// <param name="args"></param>
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

        /// <summary>
        /// Adds the slected URL into the URL list and resets the textinput box.
        /// </summary>
        public void AddListItem()
        {
            if (URLInput.Input != "")
            {
                ImageURL.Add(URLInput.Input);
                URLInput.Input = "";
            }

            StateHasChanged();
        }

        /// <summary>
        /// Method connected to the listitems "onclick" event and removes the item.
        /// </summary>
        /// <param name="url"></param>
        public void RemoveListItem(string url)
        {
            ImageURL.Remove(url);
            StateHasChanged();
        }
    }
}
