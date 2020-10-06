using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Comment
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }

        //For posting
        public int RealEstateId { get; set; }

        //For invalid post request
        public bool IsSuccesfullCommentPost { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
