using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
