using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    /// <summary>
    /// Handles a single URL Input on the CreateRealEstateAd
    /// </summary>
    public class URLInput
    {
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string Input { get; set; }
    }
}
