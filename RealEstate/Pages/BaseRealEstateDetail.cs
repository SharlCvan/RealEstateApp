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
        public Comment NewComment = new Comment();

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

        protected async Task OnPostComment()
        {
            //Remove text from form after submit
            var result = await RealEstateService.PostComment(int.Parse(Id), NewComment.Content);

            if (!result.IsSuccesfullCommentPost)
            {
                Errors = result.Errors;
            }
            else
            {
                NewComment.Content = result.Content;
                NewComment.CreatedOn = result.CreatedOn;
                NewComment.UserName = result.UserName;
                NewComment.IsSuccesfullCommentPost = result.IsSuccesfullCommentPost;
            }

        }

    }
}
