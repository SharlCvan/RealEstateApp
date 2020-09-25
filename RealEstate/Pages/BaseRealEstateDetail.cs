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

        public string Message { get; set; }

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public IEnumerable<string> Errors { get; set; }

        protected async override Task OnInitializedAsync()
        {
            RealEstate.Comments = new List<Comment>();
            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));
        }

        protected async void ValidPostComment()
        {
            //Remove text from form after submit
            postedComment.RealEstateId = int.Parse(Id);
            var result = await RealEstateService.PostComment(postedComment);

            NewComment.Content = result.Content;
            NewComment.UserName = result.UserName;
            NewComment.CreatedOn = result.CreatedOn;

        }

    }
}
