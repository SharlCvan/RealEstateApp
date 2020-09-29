using RealEstate.Models;
using RealEstate.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate
{
    public class SearchValues
    {
        public bool ShowSale { get; set; }
        public bool ShowRent { get; set; }
        public RealEstateTypes ShowHouse { get; set; }
        public RealEstateTypes ShowApartment { get; set; }
        public RealEstateTypes ShowStorageUnit { get; set; }
        public RealEstateTypes ShowOffice { get; set; }
        public string SearchTerm { get; set; }
    }
}
