using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{

    //[JsonConverter(typeof(CommentConverter))]
    public class Propertys
    {
        public DateTime CreatedOn { get; set; }
        public int ConstructionYear { get; set; }
        public string Address  { get; set; }
        public string RealEstateType { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int SellingPrice { get; set; }
        public int RentingPrice { get; set; }
        public bool CanBeSold { get; set; }
        public bool CanBeRented { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
