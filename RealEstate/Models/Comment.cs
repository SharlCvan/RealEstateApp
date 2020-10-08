using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Comment
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("timeOfCreation")]
        public DateTime CreatedOn { get; set; }

        //For posting
        public int RealEstateId { get; set; }

        //For invalid post request
        public bool IsSuccesfullCommentPost { get; set; }

        public Dictionary<string, string[]> errors { get; set; } = new Dictionary<string, string[]>();

    }
}
