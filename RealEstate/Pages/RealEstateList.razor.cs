using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public partial class RealEstateList
    {
        [Parameter]
        public List<Propertys> Estates { get; set; }

        public int Counter { get; set; }
        public int[] SquareMeters { get; set; } = new int[] { 60, 80, 75, 100, 48 };
        public int[] Rooms { get; set; } = new int[] { 3, 5, 3, 4, 1 };

        public string BuyersOptions(Propertys estate)
        {
            if (estate.CanBeSold)
            {
                return "for sale";
            }

            else
            {
                return "for rent";
            }
        }
    }
}
