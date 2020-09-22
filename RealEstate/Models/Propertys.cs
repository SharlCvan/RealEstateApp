using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Propertys : IValidatableObject
    {
        public List<Comment> Comments { get; set; }

        [Required(ErrorMessage = "A contact reference must be supplied.")]
        public string Contact { get; set; }
        public DateTime CreatedOn { get; set; }

        [Range(1600, Int32.MaxValue, ErrorMessage = "The property must be constructed after the year 1600")]
        public int ConstructionYear { get; set; }

        [Required(ErrorMessage = "An adress is required.")]
        public string Address  { get; set; }

        [Required(ErrorMessage = "A real estate type must be given.")]
        public string RealEstateType { get; set; }

        [MaxLength(1000, ErrorMessage = "Description can not contain more than 1000 characters")]
        [MinLength(10, ErrorMessage = "Description must contain at least 10 characters.")]
        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; }

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Title can not contain more than 50 characters")]
        [MinLength(5, ErrorMessage = "Title must contain at least 3 characters.")]
        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        public int SellingPrice { get; set; }

        public int RentingPrice { get; set; }

        public bool CanBeSold { get; set; }

        public bool CanBeRented { get; set; }

        //Hantera felmeddelande som API:et skickar tillbaka vid POST request

        public bool IsSuccessfulRegistration { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (SellingPrice != 0 && RentingPrice != 0)
            {
                errors.Add(new ValidationResult(
                    "A property can not be for sale and for rent at the same time. Enter 0 or empty field.", new[] { nameof(SellingPrice) }
                    ));
                errors.Add(new ValidationResult(
                    "A property can not be for sale and for rent at the same time. Enter 0 or empty field.", new[] { nameof(RentingPrice) }
                    ));
            }
            else if (SellingPrice == 0 && RentingPrice == 0) 
            {

                errors.Add(new ValidationResult(
                    "A property must be either for sale or for rent. Please enter a amount to enable for rent.", new[] { nameof(RentingPrice) }
                    ));
                errors.Add(new ValidationResult(
                    "A property must be either for sale or for rent. Please enter a amount to enable for sale.", new[] { nameof(SellingPrice) }
                    ));
            }

            return errors;
        }
    }
}
