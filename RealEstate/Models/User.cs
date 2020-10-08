using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class User
    {
        public string UserName { get; set; }

        [JsonPropertyName("realEstateCount")]
        public int RealEstates { get; set; }

        [JsonPropertyName("commentCount")]
        public int Comments { get; set; }

        [JsonPropertyName("ratingAvrage")]
        public double Rating { get; set; }

        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
