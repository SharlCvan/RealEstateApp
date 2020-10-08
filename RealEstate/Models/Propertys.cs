using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Propertys 
    {
        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; }

        [JsonPropertyName("contact")]
        public string Contact { get; set; }

        [JsonPropertyName("dateOfAdvertCreation")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("constructionYear")]
        public int ConstructionYear { get; set; }

        [JsonPropertyName("address")]
        public string Address  { get; set; }

        [JsonPropertyName("realEstateType")]
        public int RealEstateType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("sellPrice")]
        public double? SellingPrice { get; set; }

        [JsonPropertyName("rent")]
        public double? RentingPrice { get; set; }

        [JsonPropertyName("isSellable")]
        public bool CanBeSold { get; set; }

        [JsonPropertyName("isRentable")]
        public bool CanBeRented { get; set; }

        [JsonPropertyName("listingUrl")]
        public string ListingURL { get; set; }

        [JsonPropertyName("squareMeters")]
        public int SquareMeters { get; set; }

        [JsonPropertyName("rooms")]
        public int Rooms { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("urls")]
        public List<URL> Urls { get; set; }

        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    }
}
