using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstateDetail : ComponentBase
    {

        public Propertys RealEstate { get; set; } = new Propertys();
        public Comment NewComment { get; set; } = new Comment();
        public PostedComment postedComment { get; set; } = new PostedComment();        

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            RealEstate.Comments = new List<Comment>();
            NewComment.Errors = new List<string>();
            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));
        }

        protected async Task ValidPostComment()
        {
            //Remove text from form after submit
            postedComment.RealEstateId = int.Parse(Id);
            NewComment = await RealEstateService.PostComment(postedComment);
        }

    }
}
