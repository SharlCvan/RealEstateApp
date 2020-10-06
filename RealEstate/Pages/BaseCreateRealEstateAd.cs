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
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Handles the data 
        /// </summary>
        /// <returns></returns>
        public async Task Register()
        {
            Errors = new List<string>();
            ShowRegistrationErros = false;

            var PropertyForRegistration = ConvertToValidPropertysForRegistration(this.PropertyForRegistration);

            //Forwards the data to repository to post to the API
            var result = await RealEstateServices.PostANewRealEstate(PropertyForRegistration);

            if (!result.IsSuccessfulRegistration)
            {
                //Handles the event of a non sucessfull post to the api
                foreach (var error in result.Errors)
                {
                    Errors.Add(error.Value[0]);
                }

                ShowRegistrationErros = true;
            }
            else
            {
                //Redirects to the realEstateDetails page for that newly created RealEstate
                NavigationManager.NavigateTo($"/RealEstate/{result.Id}");
            }
        }

        /// <summary>
        /// Converts from the PropertysForRegistration to regular Propertys model. 
        /// </summary>
        /// <param name="propertyForRegistration"></param>
        /// <returns></returns>
        private PropertysForRegistration ConvertToValidPropertysForRegistration(PropertysForRegistration propertyForRegistration)
        {
            propertyForRegistration.Type = (int)propertyForRegistration.RealEstateType;
            propertyForRegistration.Urls = ImageURL;


            if (propertyForRegistration.CanBeSold)
            {
                propertyForRegistration.CanBeSold = true;
                propertyForRegistration.CanBeRented = false;
                propertyForRegistration.SellingPrice = propertyForRegistration.RentSalePrice;
                propertyForRegistration.RentingPrice = null;
            }
            else
            {
                propertyForRegistration.CanBeSold = false;
                propertyForRegistration.CanBeRented = true;
                propertyForRegistration.RentingPrice = propertyForRegistration.RentSalePrice;
                propertyForRegistration.SellingPrice = null;
            }

            return propertyForRegistration;
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
