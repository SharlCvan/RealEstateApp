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

        public string Contact { get; set; }
        public DateTime CreatedOn { get; set; }

        public int ConstructionYear { get; set; }

        public string Address  { get; set; }

        public int RealEstateType { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public int SellingPrice { get; set; }

        public int RentingPrice { get; set; }

        public bool CanBeSold { get; set; }

        public bool CanBeRented { get; set; }

        public List<string> ImageUrl { get; set; }

        //Hantera felmeddelande som API:et skickar tillbaka vid POST request
        public bool IsSuccessfulRegistration { get; set; }
        

        public IEnumerable<string> Errors { get; set; }

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
