using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [MaxLength(1000, ErrorMessage = "Description can not contain more than 1000 characters")]
        [MinLength(10, ErrorMessage = "Description must contain at least 10 characters.")]
        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; }

        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Title can not contain more than 50 characters")]
        [MinLength(5, ErrorMessage = "Title must contain at least 3 characters.")]
        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "A value must be given")]
        public int RentSalePrice { get; set; }

        public bool CanBeSold { get; set; }

        public bool CanBeRented { get; set; }


        //Behövs den verkligen???
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (CanBeRented == true && CanBeSold == true)
            {
                errors.Add(new ValidationResult(
                    "A property can not be for sale and for rent at the same time.", new[] { nameof(CanBeSold) }
                    ));
                errors.Add(new ValidationResult(
                    "A property can not be for sale and for rent at the same time.", new[] { nameof(CanBeRented) }
                    ));
            }
            else if (CanBeSold != true && CanBeRented != true)
            {

                errors.Add(new ValidationResult(
                    "A property must be either for sale or for rent. Please enter a amount to enable for rent.", new[] { nameof(CanBeSold) }
                    ));
                errors.Add(new ValidationResult(
                    "A property must be either for sale or for rent. Please enter a amount to enable for sale.", new[] { nameof(CanBeRented) }
                    ));
            }

            return errors;
        }
    }
}
