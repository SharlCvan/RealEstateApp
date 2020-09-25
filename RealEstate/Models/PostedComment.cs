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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "minimum 5 charracter and Max 50 charracters")]
        public string Content { get; set; }
    }
}
