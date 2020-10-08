using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public partial class Search
    {
        public string SearchTerm { get; set; }

        [Parameter]
        public EventCallback<string> OnSearch {get; set;}

        public async void HandleSearch()
        {
            await OnSearch.InvokeAsync(SearchTerm);
        }
    }
}
