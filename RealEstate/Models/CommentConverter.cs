using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class CommentConverter : JsonConverter
    {
        public CommentConverter()
        {

        }

        public override bool CanConvert(Type objectType) => objectType == typeof(List<Comment>);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var response = new List<Comment>();

            // Loading the JSON object
            JObject comments = JObject.Load(reader);

            // Looping through all the properties. C# treats it as key value pair
            foreach (var comment in comments)
            {
                // Finally I'm deserializing the value into an actual Player object
                var c = JsonConvert.DeserializeObject<Comment>(comment.Value.ToString());
                response.Add(c);
            }

            return response;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
