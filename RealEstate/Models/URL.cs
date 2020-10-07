using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class URL
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        public URL(string url)
        {
            Url = url;
        }
    }
}
