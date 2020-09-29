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
