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
        public int CurrentPage { get; set; } = 1;

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public int Radius { get; set; } = 3;

        [Parameter]
        public EventCallback<int> SelectedPade { get; set; }

        public List<LinkModel> Links;

        protected override void OnParametersSet()
        {
            LoadPages();
        }

        public async Task SelectedPageInternal(LinkModel link)
        {
            if(link.Page == CurrentPage)
            {
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

        //For realestateDetails page

        [Parameter]
        public string RealEstateId { get; set; }

        [Parameter]
        public bool EnablePostComment { get; set; }

        public Comment NewComment { get; set; } = new Comment();
        public PostedComment postedComment { get; set; } = new PostedComment();

        //_____________________________

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

        public class LinkModel
        {
            public LinkModel(int page):this(page, true) 
            {
                
            }

            public LinkModel(int page, bool enabled):this(page, enabled, page.ToString())
            {

            }

            public LinkModel(int page, bool enabled, string text)
            {
                Page = page;
                Enabled = enabled;
                Text = text;
            }

            public string Text { get; set; }
            public int Page { get; set; }
            public bool Enabled { get; set; } = true;
            public bool Active { get; set; } = false;
        }

    }

    
}
