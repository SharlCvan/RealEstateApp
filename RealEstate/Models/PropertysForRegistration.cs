using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PropertysForRegistration
    {
        [Required(ErrorMessage = "A contact reference must be supplied.")]
        public string Contact { get; set; }

        [Range(1600, Int32.MaxValue, ErrorMessage = "The property must be constructed after the year 1600")]
        public int ConstructionYear { get; set; }

        [Required(ErrorMessage = "An adress is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A real estate type must be given.")]
        public RealEstateTypes RealEstateType { get; set; }

        //Holds info of which RealEstateType the realestate is. Needs to be a regular int instead of enum to serialize correctly.
        public int Type { get; set; }

        [MaxLength(1000, ErrorMessage = "Description can not contain more than 1000 characters")]
        [MinLength(10, ErrorMessage = "Description must contain at least 10 characters.")]
        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; }

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Title can not contain more than 50 characters")]
        [MinLength(5, ErrorMessage = "Title must contain at least 3 characters.")]
        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "A value greater than o must be given.")]
        public int? RentSalePrice { get; set; }

        public bool CanBeSold { get; set; }

        public bool CanBeRented { get; set; }

        //Holds which url is the first promotional picture required to post a realestatead with us.
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [Required(ErrorMessage = "A URL to a promotional picture is required")]
        public string ListingURL { get; set; }

        [Range(5, 5000, ErrorMessage = "Enter a valid number between 5 and 5000.")]
        public int SquareMeters { get; set; }

        [Range(1, 50, ErrorMessage = "Enter a valid number between 1 and 50.")]
        public int Rooms { get; set; }

        [Required(ErrorMessage = "Enter a valid city name.")]
        public string City { get; set; }

        public int? SellingPrice { get; set; }

        public int? RentingPrice { get; set; }

        //Contains the urls for secondary pictures sent along with the realestatead
        public List<string> Urls { get; set; }

        
        public bool IsSuccessfulRegistration { get; set; }

        //Holds error messages if anything with the registration goes wrong
        public Dictionary<string,string[]> Errors { get; set; }

    }
}
