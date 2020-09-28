using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    /// <summary>
    /// Stores information about all image url's and the corresponding RealEstateID.
    /// </summary>
    public class RealEstateURLInputDTO
    {
        public int RealEstateId { get; set; }
        public List<string> Urls { get; set; }
    }
}
