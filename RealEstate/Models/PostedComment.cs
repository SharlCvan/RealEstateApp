using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PostedComment
    {
        public int? RealEstateId { get; set; }

        [Required(ErrorMessage = "Input text to comment")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "minimum 10 charracter and Max 500 charracters")]
        public string Content { get; set; }
    }
}
