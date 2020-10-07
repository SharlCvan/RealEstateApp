using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public partial class BaseCommentSection : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public bool EnablePostComment { get; set; }

        public PostedComment postedComment { get; set; } = new PostedComment();

        [Parameter]
        public bool PostSuccess { get; set; }

        [Parameter]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Parameter]
        public int CurrentPage { get; set; } = 1;

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public int Radius { get; set; } = 3;

        [Parameter]
        public EventCallback<int> SelectedPade { get; set; }

        [Parameter]
        public EventCallback<PostedComment> SubmittedComment { get; set; }

        public List<LinkModel> Links;


        protected override void OnParametersSet()
        {
            LoadPages();
        }

        public async Task SelectedPageInternal(LinkModel link)
        {
            if(link.Page == CurrentPage)
            {
                StateHasChanged();
                return;
            }
            if(!link.Enabled)
            {
                return;
            }

            CurrentPage = link.Page;
            await SelectedPade.InvokeAsync(link.Page);
        }

        private void LoadPages()
        {
            Links = new List<LinkModel>();
            var isPreviousLinkEnabled = CurrentPage != 1;
            var previousPage = CurrentPage - 1;
            Links.Add(new LinkModel(previousPage, isPreviousLinkEnabled, "Previous"));

            for (int i = 1; i <= TotalPages; i++)
            {
                if(i >= CurrentPage - Radius && i <= CurrentPage + Radius)
                {
                    Links.Add(new LinkModel(i)
                    {
                        Active = CurrentPage == i
                    });
                }
            }

            var isNextPageEnabled = CurrentPage != TotalPages;
            var NextPage = CurrentPage + 1;
            Links.Add(new LinkModel(NextPage, isNextPageEnabled, "Next"));
        }


        protected async Task ValidPostComment()
        {
            await SubmittedComment.InvokeAsync(postedComment);

            if(PostSuccess)
            {
                postedComment = new PostedComment();
                await SelectedPageInternal(new LinkModel(1));
            }
        }

    }

    
}
