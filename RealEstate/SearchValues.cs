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
        public RealEstateType ShowHouse { get; set; }
        public RealEstateType ShowApartment { get; set; }
        public RealEstateType ShowStorageUnit { get; set; }
        public RealEstateType ShowOffice { get; set; }
        public string SearchTerm { get; set; }
    }
}
