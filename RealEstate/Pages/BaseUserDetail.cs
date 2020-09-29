using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseUserDetail : ComponentBase
    {
        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        public User User { get; set; } = new User();

        [Parameter]
        public string UserName { get; set; }

        protected async override Task OnInitializedAsync()
        {
            User = await RealEstateService.GetUser(UserName);
        }
    }
}
