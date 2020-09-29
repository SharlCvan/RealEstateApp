using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Propertys 
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
        
        public string ListingURL { get; set; }
        public int SquareMeters { get; set; }
        public int Rooms { get; set; }
        public string City { get; set; }

        public List<string> Urls { get; set; }

        //Hantera felmeddelande som API:et skickar tillbaka vid POST request
        public bool IsSuccessfulRegistration { get; set; }
        

        public IEnumerable<string> Errors { get; set; }

    }
}
