using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PostedRating
    {
        public string UserName { get; set; }
        public int Value { get; set; }

        public bool PostRatingSuccess { get; set; }
    }
}
