using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class CommentsPaging
    {
        public List<Comment> Comments { get; set; }
        public int TotalPages { get; set; }
    }
}
