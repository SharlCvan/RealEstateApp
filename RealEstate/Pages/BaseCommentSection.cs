using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseCommentSection : ComponentBase
    {
        [Parameter]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Parameter]
        public string RealEstateId { get; set; }

        public Comment NewComment { get; set; } = new Comment();
        public PostedComment postedComment { get; set; } = new PostedComment();

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            NewComment.Errors = new List<string>();
        }


        protected async Task ValidPostComment()
        {
            //Remove text from form after submit
            postedComment.RealEstateId = int.Parse(RealEstateId);
            NewComment = await RealEstateService.PostComment(postedComment);
        }
    }


}
